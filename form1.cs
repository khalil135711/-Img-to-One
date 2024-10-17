namespace test_Image_Combin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private List<Image> images = new List<Image>();
        private PictureBox pictureBox;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Image Combiner";
            this.Size = new Size(600, 400);

            Button loadImagesButton = new Button();
            loadImagesButton.Text = "Load Images";
            loadImagesButton.Location = new Point(20, 20);
            loadImagesButton.Click += new EventHandler(LoadImages);

            Button combineImagesButton = new Button();
            combineImagesButton.Text = "Combine Images";
            combineImagesButton.Location = new Point(120, 20);
            combineImagesButton.Click += new EventHandler(CombineImages);


            pictureBox = new PictureBox();
            pictureBox.Location = new Point(20, 60);
            pictureBox.Size = new Size(550, 300);
            pictureBox.BorderStyle = BorderStyle.Fixed3D;

            this.Controls.Add(loadImagesButton);
            this.Controls.Add(combineImagesButton);
            this.Controls.Add(pictureBox);
        }

        private void LoadImages(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load selected images 
                images.Clear();
                foreach (string filename in openFileDialog.FileNames)
                {
                    images.Add(Image.FromFile(filename));
                }
                MessageBox.Show($"{images.Count} images loaded.");
            }
        }
        private void CombineImages(object sender, EventArgs e)
        {
            if (images.Count == 0) { MessageBox.Show("No images loaded."); return; }
            int totalWidth = 0; int maxHeight = 0;
            foreach (var image in images)
            {
                totalWidth += image.Width;
                if (image.Height > maxHeight)
                { maxHeight = image.Height; }
            }
            Bitmap combinedImage = new Bitmap(totalWidth, maxHeight);
            using (Graphics g = Graphics.FromImage(combinedImage))
            {
                int currentX = 0;
                foreach (var image in images)
                {
                    g.DrawImage(image, new Point(currentX, 0));
                    currentX += image.Width;
                }
            }
            pictureBox.Image = combinedImage;
            combinedImage.Save("combined_image.jpg");

        }

        public void save_Image(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = System.IO.Path.GetExtension(saveFileDialog.FileName);
                    switch (filename)
                    {
                        case ".jpg":
                            pictureBox.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case ".jpeg":
                            pictureBox.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case ".png":
                            pictureBox.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            break;

                        default:
                            MessageBox.Show("Invalid file type.");
                            break;
                    }
                    pictureBox.Image.Save(saveFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("No image to save.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save_Image(sender, e);
        }
    }
}
