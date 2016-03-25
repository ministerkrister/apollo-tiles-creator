using ApolloTilesCreator.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ApolloTilesCreator
{
 
    internal class TileCreator
    {
        internal const string normalTile = "Normal tile";
        internal const string bigTile = "Big tile";
        internal const string darknessTile = "Darkness theme, Normal tile";
        internal const string bigdarknessTile = "Darkness theme, Big tile";


        private Bitmap FixedSize(Bitmap image, int Width, int Height, bool needToFill)
        {
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;

            if (sourceWidth < Width || sourceHeight < Height /*0*/)
            {
                int width = Math.Max(sourceWidth, Width);
                int height = Math.Max(sourceHeight, Height);
                using (Bitmap b = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                {
                    using (Graphics graphics = Graphics.FromImage(b))
                    {
                        graphics.CompositingMode = CompositingMode.SourceOver;
                        int x = (width / 2) - (sourceWidth / 2);//Math.Floor(((width * 1.0) - (sourceWidth * 1.0)) / 2.0);
                        int y = (height / 2) - (sourceHeight / 2);
                        y = y > 0 ? y / 3 : y;
                        graphics.DrawImage(image, x, y);
                        image = new Bitmap(b);
                    }
                }
            }
            sourceWidth = image.Width;
            sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)Width / (double)sourceWidth);
            nScaleH = ((double)Height / (double)sourceHeight);
            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (Height - sourceHeight * nScale) / 2;
                destX = (Width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);

            System.Drawing.Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, Width, Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = InterpolationMode.Default;
                grPhoto.CompositingQuality = CompositingQuality.Default;
                grPhoto.SmoothingMode = SmoothingMode.Default;

                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);

                return bmPhoto;
            }
        }

        private Bitmap DoApplyMask(Bitmap input, Bitmap mask)
        {
            Bitmap output = new Bitmap(input.Width, input.Height, PixelFormat.Format32bppArgb);
            output.MakeTransparent();
            var rect = new Rectangle(0, 0, input.Width, input.Height);

            var bitsMask = mask.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bitsInput = input.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bitsOutput = output.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                for (int y = 0; y < input.Height; y++)
                {
                    byte* ptrMask = (byte*)bitsMask.Scan0 + y * bitsMask.Stride;
                    byte* ptrInput = (byte*)bitsInput.Scan0 + y * bitsInput.Stride;
                    byte* ptrOutput = (byte*)bitsOutput.Scan0 + y * bitsOutput.Stride;
                    for (int x = 0; x < input.Width; x++)
                    {
                        //I think this is right - if the blue channel is 0 than all of them are (monochrome mask) which makes the mask black
                        if (ptrMask[4 * x] == 0)
                        {
                            ptrOutput[4 * x] = ptrInput[4 * x]; // blue
                            ptrOutput[4 * x + 1] = ptrInput[4 * x + 1]; // green
                            ptrOutput[4 * x + 2] = ptrInput[4 * x + 2]; // red

                            //Ensure opaque
                            //ptrOutput[4 * x + 3] = 255;
                            //Set alpha from input
                            ptrOutput[4 * x + 3] = ptrInput[4 * x + 3];
                        }
                        else
                        {
                            ptrOutput[4 * x] = 0; // blue
                            ptrOutput[4 * x + 1] = 0; // green
                            ptrOutput[4 * x + 2] = 0; // red

                            //Ensure Transparent
                            ptrOutput[4 * x + 3] = 0; // alpha
                        }
                    }
                }

            }
            mask.UnlockBits(bitsMask);
            input.UnlockBits(bitsInput);
            output.UnlockBits(bitsOutput);

            return output;
        }

        private Bitmap GetPicFromInternetz(string url)
        {
            using (MyWebClient wc = new MyWebClient())
            {
                wc.HeadOnly = true;
                byte[] body = wc.DownloadData(url);
                string type = wc.ResponseHeaders["content-type"];
                wc.HeadOnly = false;
                if (type.StartsWith(@"image/"))
                {
                    using (Stream s = wc.OpenRead(url))
                    {
                        return new Bitmap(s);
                    }
                }
                else
                {
                    throw new ApplicationException(string.Format("Url is not an image resource: {0}", url));
                }

            }
        }

        public void GenerateTile(string inputFile, string outputFile, string type)
        {
            string outExt = Path.GetExtension(outputFile);
            string inExt = Path.GetExtension(inputFile);
            string[] ext = { ".png", ".bmp", ".jpg"};
            if (outExt.ToLowerInvariant() != ".png")
            {
                throw new ApplicationException("Outputfile needs to be a png image");
            }
            if (!ext.Contains(inExt.ToLowerInvariant()))
            {
                throw new ApplicationException("Input needs to be a png/jpg/bmp image file");
            }
            Bitmap resFg;
            Bitmap resMask;
            switch (type)
            {
                case normalTile:
                    resFg = Resources.tile_fg;
                    resMask = Resources.mask;
                    break;
                case bigTile:
                    resFg = Resources.big_tile_fg;
                    resMask = Resources.big_mask;
                    break;
                case darknessTile:
                    resFg = Resources.dark_tile_fg;
                    resMask = Resources.dark_mask;
                    break;
                case bigdarknessTile:
                    resFg = Resources.dark_big_tile_fg;
                    resMask = Resources.dark_big_mask;
                    break;
                default:
                    throw new ApplicationException(type + " - not supported");
            }
            using (Bitmap fg = new Bitmap(resFg))
            {
                using (Bitmap mask = new Bitmap(resMask))
                {
                    Bitmap input;
                    if (Utils.IsValidURI(inputFile))
                        input = GetPicFromInternetz(inputFile);
                    else
                        input = new Bitmap(inputFile);
                    int w = mask.Width;
                    int h = mask.Height;
                    input = FixedSize(input, w, h, true);
                    input = DoApplyMask(input, mask);
                    using (Bitmap output = new Bitmap(w, h, PixelFormat.Format32bppArgb))
                    {
                        using (Graphics graphics = Graphics.FromImage(output))
                        {
                            graphics.CompositingMode = CompositingMode.SourceOver; // this is the default, but just to be clear
                            graphics.DrawImage(input, 0, 0);
                            graphics.DrawImage(fg, 0, 0);
                        }
                        output.Save(outputFile);
                    }
                    input.Dispose();
                }
            }
        }

    }
}
