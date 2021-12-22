using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPR_LR6
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string path = "C:\\Users\\Spidrre\\Desktop\\универ\\Четвертый курс\\ТПР\\Теория.rtf";

            richTextBox1.LoadFile(path);
            richTextBox1.Find("Text", RichTextBoxFinds.MatchCase);

            //richTextBox1.LoadFile(path, RichTextBoxStreamType.RichText);
        }
    }
}
