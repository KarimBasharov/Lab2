using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        bool drawing;
        int historyCounter;
        GraphicsPath currentPath;
        Point oldLocation;
        Pen currentPen;
        Color historyColor;
        List<Image> History;

        public Form1()
        {
            InitializeComponent();
            drawing = false;
            currentPen = new Pen(Color.Black);
            currentPen.Width = trackBar1.Value;
            History = new List<Image>();

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
                Console.WriteLine(historyColor);
                currentPen.Color = System.Drawing.Color.White;
            }
            if(pictureBox1.Image == null)
            {
                MessageBox.Show("Сначала создайте новый файл!");
                return;
            }
            if(e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
                historyColor = currentPen.Color;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового рисунка?", "Предупреждение", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: saveToolStripMenuItem_Click(sender, e); break;
                    case DialogResult.Cancel: return;
                }
            }

            Bitmap pic = new Bitmap(709, 377);
            pictureBox1.Image = pic;
            pictureBox1.BackColor = Color.White;

            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            g.Dispose();
            pictureBox1.Invalidate();

            History.Clear();
            historyCounter = 0;
            History.Add(new Bitmap(pictureBox1.Image));
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            SaveDlg.Title = "Save an Image File";
            SaveDlg.FilterIndex = 4;
            SaveDlg.ShowDialog();

            if(SaveDlg.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)SaveDlg.OpenFile();
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Gif);
                        break;

                    case 4:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            OP.Title = "Open an Image File";
            OP.FilterIndex = 1;

            if (OP.ShowDialog() != DialogResult.Cancel)
            
                pictureBox1.Load(OP.FileName);
                pictureBox1.AutoSize = true;
            
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового рисунка?", "Предупреждение", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: saveToolStripMenuItem_Click(sender, e); break;
                    case DialogResult.Cancel: return;
                }
            }

            Bitmap pic = new Bitmap(709, 377);
            pictureBox1.Image = pic;
            pictureBox1.BackColor = Color.White;

            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            g.Dispose();
            pictureBox1.Invalidate();

            History.Clear();
            historyCounter = 0;
            History.Add(new Bitmap(pictureBox1.Image));
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            OP.Title = "Open an Image File";
            OP.FilterIndex = 1;

            if (OP.ShowDialog() != DialogResult.Cancel)

                pictureBox1.Load(OP.FileName);
            pictureBox1.AutoSize = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            SaveDlg.Title = "Save an Image File";
            SaveDlg.FilterIndex = 4;
            SaveDlg.ShowDialog();

            if (SaveDlg.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)SaveDlg.OpenFile();
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Gif);
                        break;

                    case 4:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Png);
                        break;
                }

                fs.Close();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //History.RemoveRange(historyCounter + 1, History.Count - historyCounter - 1);
            History.Add(new Bitmap(pictureBox1.Image));
            historyCounter++;
            if (historyCounter > 10)
            {
                History.RemoveAt(0);
                historyCounter--;
            }
            //if (History.Count - 1 == 10) History.RemoveAt(0);



            currentPen.Color = historyColor;
            drawing = false;
            try
            {
                currentPath.Dispose();
            }
            catch { };
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X.ToString() + ", " + e.Y.ToString();

            if(drawing)
            {
                Graphics g = Graphics.FromImage(pictureBox1.Image);
                currentPath.AddLine(oldLocation, e.Location);
                g.DrawPath(currentPen, currentPath);
                oldLocation = e.Location;
                g.Dispose();
                pictureBox1.Invalidate();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            currentPen.Width = trackBar1.Value;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (History.Count != 0 && historyCounter != 0)
            {
                pictureBox1.Image = new Bitmap(History[--historyCounter]);
            }
            else MessageBox.Show("История пуста");
        }

        private void renToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (++historyCounter < History.Count)
            {
                historyCounter--;
                pictureBox1.Image = new Bitmap(History[++historyCounter]);
                //pictureBox1.Image = new Bitmap(History[++historyCounter]);
            }
            else 
            { 
                MessageBox.Show("История пуста"); 
            }
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Solid;

            solidToolStripMenuItem.Checked = true;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = false;
        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Dot;

            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = true;
            dashDotDotToolStripMenuItem.Checked = false;
        }

        private void dashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.DashDotDot;

            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
