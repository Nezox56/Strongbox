using Strongbox.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Strongbox
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }
        
        int n;

        private PictureBox[,] pb = null;

        private void Start_Click(object sender, EventArgs e)
        {
            if (pb != null)
            {
                foreach (var pictureBox in pb)
                {
                    Controls.Remove(pictureBox);
                }
            }

            n = (int)numericUpDown1.Value;

            pb = new PictureBox[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    pb[i, j] = new PictureBox
                    {
                        Location = new Point(10 + i * 100, 20 + j * 100),
                        Size = new System.Drawing.Size(80, 80),
                        TabIndex = i,
                        BackColor = Color.Black,
                        Tag = 0,
                        Image = Resources.valve
                    };
                    Controls.Add(pb[i, j]);
                    int row = i;
                    int column = j;
                    pb[i, j].Click += (x, y) => { PbClick(row, column); };
                }
            }

            var rnd = new Random();
            for (int i = 0; i < 40; i++)
            {
                PbClick(rnd.Next(n), rnd.Next(n), true);
            }
        }

        void PbClick(int row, int column, bool init = false)
        {
            bool win = !init;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == row || j == column)
                    {
                        Image image = pb[i, j].Image;
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        pb[i, j].Image = image;

                        pb[i, j].Tag = ((int)pb[i, j].Tag + 1) % 2;
                    }

                    win &= (int)pb[i, j].Tag == (int)pb[0, 0].Tag;
                }

            }

            if (win)
            {
                
                MessageBox.Show("Сейф открыт!", "Победа!");
            }
        }
    }
}
