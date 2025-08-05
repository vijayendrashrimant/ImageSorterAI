using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace ImageSorterAI
{
    public partial class MainForm : Form
    {
        private InferenceSession session;
        private string inputName;

        public MainForm()
        {
            InitializeComponent();
            session = new InferenceSession("resnet50.onnx");
            inputName = session.InputMetadata.Keys.First();
        }

        private void btnLoadImages_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                listBoxImages.Items.Clear();
                var imageFiles = Directory.GetFiles(fbd.SelectedPath, "*.*")
                                          .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                                      f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                                      f.EndsWith(".png", StringComparison.OrdinalIgnoreCase));

                foreach (string path in imageFiles)
                {
                    listBoxImages.Items.Add(path);
                }
            }
        }

        private void btnSortImages_Click(object sender, EventArgs e)
        {
            var hashSet = new HashSet<string>();
            string baseDir = "sortedImages";
            string duplicateDir = Path.Combine(baseDir, "duplicates");

            Directory.CreateDirectory(baseDir);
            Directory.CreateDirectory(duplicateDir);

            foreach (string imagePath in listBoxImages.Items)
            {
                using var bitmap = new Bitmap(imagePath);
                string hash = ComputeImageHash(bitmap);
                string fileName = Path.GetFileName(imagePath);

                if (hashSet.Contains(hash))
                {
                    // Duplicate image
                    string destPath = Path.Combine(duplicateDir, fileName);
                    File.Copy(imagePath, destPath, true);
                }
                else
                {
                    // Unique image
                    hashSet.Add(hash);
                    string destPath = Path.Combine(baseDir, fileName);
                    File.Copy(imagePath, destPath, true);
                }
            }

            MessageBox.Show("Images sorted into 'sortedImages' and 'sortedImages\\duplicates' folders.");
        }

        private string ComputeImageHash(Bitmap bitmap)
        {
            var resized = new Bitmap(bitmap, new Size(8, 8));
            var grayValues = new List<byte>();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var pixel = resized.GetPixel(x, y);
                    byte gray = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    grayValues.Add(gray);
                }
            }

            byte avg = (byte)grayValues.Average(b => b);
            return string.Concat(grayValues.Select(b => b < avg ? "0" : "1"));
        }

        private string PredictImageLabel(string imagePath)
        {
            using var bitmap = new Bitmap(imagePath);
            var resized = new Bitmap(bitmap, new Size(224, 224));
            var input = new DenseTensor<float>(new[] { 1, 3, 224, 224 });

            for (int y = 0; y < 224; y++)
            {
                for (int x = 0; x < 224; x++)
                {
                    var color = resized.GetPixel(x, y);
                    input[0, 0, y, x] = color.R / 255f;
                    input[0, 1, y, x] = color.G / 255f;
                    input[0, 2, y, x] = color.B / 255f;
                }
            }

            var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor(inputName, input) };
            using var results = session.Run(inputs);
            var scores = results.First().AsEnumerable<float>().ToArray();
            int classIndex = Array.IndexOf(scores, scores.Max());
            return $"Class_{classIndex}";
        }

        private void listBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImgPreview.Image = Image.FromFile(listBoxImages.SelectedItem.ToString());
        }
    }
}
