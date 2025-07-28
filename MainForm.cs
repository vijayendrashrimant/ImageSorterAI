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
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Image Files|*.jpg;*.jpeg;*.png"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                listBoxImages.Items.Clear();
                foreach (string path in ofd.FileNames)
                {
                    listBoxImages.Items.Add(path);
                }
            }
        }

        private void btnSortImages_Click(object sender, EventArgs e)
        {
            foreach (string imagePath in listBoxImages.Items)
            {
                string label = PredictImageLabel(imagePath);
                string destDir = Path.Combine("sorted", label);
                Directory.CreateDirectory(destDir);
                File.Copy(imagePath, Path.Combine(destDir, Path.GetFileName(imagePath)), true);
            }

            MessageBox.Show("Images sorted into 'sorted' folder.");
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
