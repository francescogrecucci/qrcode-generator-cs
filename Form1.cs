using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace qrcode_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void QRCode()
        {
            string qrText = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(qrText))
            {
                using (var qrGenerator = new QRCodeGenerator())
                using (var qrData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q))
                using (var qrCode = new QRCode(qrData))
                {
                    pictureBox1.Image = qrCode.GetGraphic(20);
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QRCode();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
            saveFileDialog.Filter = "JPEG Image|*.jpg";
            saveFileDialog.Title = "Save an Image File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            }             
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            pictureBox1.Image = null;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                QRCode();
            }
        }
    }
}
