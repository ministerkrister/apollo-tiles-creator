using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApolloTilesCreator
{
    public partial class ApolloTilesCreatorForm : Form
    {
        public ApolloTilesCreatorForm()
        {
            InitializeComponent();
            tileTypeComboBox.Items.Add(TileCreator.normalTile);
            tileTypeComboBox.Items.Add(TileCreator.bigTile);
            tileTypeComboBox.Items.Add(TileCreator.darknessTile);
            tileTypeComboBox.Items.Add(TileCreator.bigdarknessTile);
            tileTypeComboBox.SelectedIndex = 0;
        }

        private void openButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyPictures);
            openFileDialog1.Title = "Browse Image Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter =
            "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openTextBox1.Text = openFileDialog1.FileName;
            }
        }

        private void saveButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyPictures);
            saveFileDialog1.Filter = "PNG image (*.PNG)|*.png";
            saveFileDialog1.FilterIndex = 1;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveTextBox1.Text = saveFileDialog1.FileName;
            }
        }

        private TileForm imageForm = null;
        private Image image = null;

        private void button1_Click(object sender, EventArgs e)
        {
            if (imageForm != null)
            {
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                imageForm.Dispose();
                imageForm = null;
            }
            TileCreator imageUtil = new TileCreator();
            try
            {
                imageUtil.GenerateTile(openTextBox1.Text, saveTextBox1.Text, tileTypeComboBox.SelectedItem.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
            image = Image.FromFile(saveTextBox1.Text);
            imageForm = new TileForm();
            imageForm.FormClosed += (s, ev) =>
            {
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                imageForm.Dispose();
                imageForm = null;
            };
            imageForm.Size = new Size(image.Width + 20, image.Height + 40);
            PictureBox pb = imageForm.Controls.Find("tileBox", true).FirstOrDefault() as PictureBox;
            pb.Size = new Size(image.Width, image.Height);
            pb.Image = image;
            imageForm.Show();
        }
    }
}
