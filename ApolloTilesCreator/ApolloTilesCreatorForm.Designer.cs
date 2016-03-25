namespace ApolloTilesCreator
{
    partial class ApolloTilesCreatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openTextBox1 = new System.Windows.Forms.TextBox();
            this.openButton1 = new System.Windows.Forms.Button();
            this.saveTextBox1 = new System.Windows.Forms.TextBox();
            this.saveButton1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // openTextBox1
            // 
            this.openTextBox1.Location = new System.Drawing.Point(12, 25);
            this.openTextBox1.Name = "openTextBox1";
            this.openTextBox1.Size = new System.Drawing.Size(349, 20);
            this.openTextBox1.TabIndex = 0;
            // 
            // openButton1
            // 
            this.openButton1.Location = new System.Drawing.Point(12, 51);
            this.openButton1.Name = "openButton1";
            this.openButton1.Size = new System.Drawing.Size(95, 23);
            this.openButton1.TabIndex = 1;
            this.openButton1.Text = "Input image file";
            this.openButton1.UseVisualStyleBackColor = true;
            this.openButton1.Click += new System.EventHandler(this.openButton1_Click);
            // 
            // saveTextBox1
            // 
            this.saveTextBox1.Location = new System.Drawing.Point(12, 105);
            this.saveTextBox1.Name = "saveTextBox1";
            this.saveTextBox1.Size = new System.Drawing.Size(349, 20);
            this.saveTextBox1.TabIndex = 2;
            // 
            // saveButton1
            // 
            this.saveButton1.Location = new System.Drawing.Point(12, 131);
            this.saveButton1.Name = "saveButton1";
            this.saveButton1.Size = new System.Drawing.Size(95, 23);
            this.saveButton1.TabIndex = 3;
            this.saveButton1.Text = "Output image file";
            this.saveButton1.UseVisualStyleBackColor = true;
            this.saveButton1.Click += new System.EventHandler(this.saveButton1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Create tile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Image source (url or local file path)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Image output";
            // 
            // tileTypeComboBox
            // 
            this.tileTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tileTypeComboBox.FormattingEnabled = true;
            this.tileTypeComboBox.Location = new System.Drawing.Point(113, 131);
            this.tileTypeComboBox.Name = "tileTypeComboBox";
            this.tileTypeComboBox.Size = new System.Drawing.Size(167, 21);
            this.tileTypeComboBox.TabIndex = 8;
            // 
            // ApolloTilesCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 181);
            this.Controls.Add(this.tileTypeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.saveButton1);
            this.Controls.Add(this.saveTextBox1);
            this.Controls.Add(this.openButton1);
            this.Controls.Add(this.openTextBox1);
            this.Name = "ApolloTilesCreatorForm";
            this.Text = "Apollo Tiles Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox openTextBox1;
        private System.Windows.Forms.Button openButton1;
        private System.Windows.Forms.TextBox saveTextBox1;
        private System.Windows.Forms.Button saveButton1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tileTypeComboBox;
    }
}

