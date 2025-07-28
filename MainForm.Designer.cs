namespace ImageSorterAI
{
    partial class MainForm
    {

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 

      
        private System.Windows.Forms.Button btnLoadImages;
        private System.Windows.Forms.Button btnSortImages;
        private System.Windows.Forms.ListBox listBoxImages;
        private void InitializeComponent()
        {
            btnLoadImages = new Button();
            btnSortImages = new Button();
            listBoxImages = new ListBox();
            ImgPreview = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)ImgPreview).BeginInit();
            SuspendLayout();
            // 
            // btnLoadImages
            // 
            btnLoadImages.Location = new Point(14, 16);
            btnLoadImages.Margin = new Padding(3, 4, 3, 4);
            btnLoadImages.Name = "btnLoadImages";
            btnLoadImages.Size = new Size(137, 40);
            btnLoadImages.TabIndex = 0;
            btnLoadImages.Text = "Load Images";
            btnLoadImages.UseVisualStyleBackColor = true;
            btnLoadImages.Click += btnLoadImages_Click;
            // 
            // btnSortImages
            // 
            btnSortImages.Location = new Point(157, 16);
            btnSortImages.Margin = new Padding(3, 4, 3, 4);
            btnSortImages.Name = "btnSortImages";
            btnSortImages.Size = new Size(137, 40);
            btnSortImages.TabIndex = 1;
            btnSortImages.Text = "Sort Images";
            btnSortImages.UseVisualStyleBackColor = true;
            btnSortImages.Click += btnSortImages_Click;
            // 
            // listBoxImages
            // 
            listBoxImages.FormattingEnabled = true;
            listBoxImages.Location = new Point(14, 80);
            listBoxImages.Margin = new Padding(3, 4, 3, 4);
            listBoxImages.Name = "listBoxImages";
            listBoxImages.Size = new Size(411, 304);
            listBoxImages.TabIndex = 2;
            listBoxImages.SelectedIndexChanged += listBoxImages_SelectedIndexChanged;
            // 
            // ImgPreview
            // 
            ImgPreview.BackgroundImageLayout = ImageLayout.Stretch;
            ImgPreview.Location = new Point(431, 80);
            ImgPreview.Name = "ImgPreview";
            ImgPreview.Size = new Size(457, 304);
            ImgPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            ImgPreview.TabIndex = 3;
            ImgPreview.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(898, 393);
            Controls.Add(ImgPreview);
            Controls.Add(listBoxImages);
            Controls.Add(btnSortImages);
            Controls.Add(btnLoadImages);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "AI Image Sorter";
            ((System.ComponentModel.ISupportInitialize)ImgPreview).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox ImgPreview;
    }
}
