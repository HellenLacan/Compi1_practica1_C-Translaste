using Practica1.sol.com.analyzer;
using Practica1.sol.com.window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Practica1
{
    public partial class Form1 : Form
    {
        static List<Archive> fileList = new List<Archive>();

        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e){
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void abrirToolStripMenuItem1_Click(object sender, EventArgs e){

            String content = "";
            String path = "";
            String name = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = (System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    path = openFileDialog.FileName;
                    name = openFileDialog.SafeFileName;
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        content = reader.ReadToEnd();
                    }
                }

                getRichTextBox().Text = content;
            }
        }

   

   
        private void newTab_Click(object sender, EventArgs e)
        {
            TabPage newTabPage = new TabPage("New Document");
            newTabPage.Font = new Font("Verdana", 18);

            RichTextBox newTextBox = new RichTextBox();
            newTextBox.Dock = DockStyle.Fill;
            newTextBox.Font = new Font("Verdana", 10);
            newTextBox.BackColor = Color.White;
            newTextBox.BorderStyle = BorderStyle.None;
            

            newTabPage.Controls.Add(newTextBox);
            tabControl1.TabPages.Add(newTabPage);
        }

        private void saveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(getRichTextBox().Text);
                }
            }
        }

        private RichTextBox getRichTextBox() {
            RichTextBox richTextBox = null;
            TabPage tp = tabControl1.SelectedTab;

            if (tp != null) {
                richTextBox = tp.Controls[0] as RichTextBox;
            }

            return richTextBox;
        }

        private void updateNumberLabel()
        {
            //we get index of first visible char and 
            //number of first visible line
            Point pos = new Point(0, 0);
            int firstIndex = getRichTextBox().GetCharIndexFromPosition(pos);
            int firstLine = getRichTextBox().GetLineFromCharIndex(firstIndex);

            //now we get index of last visible char 
            //and number of last visible line
            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = getRichTextBox().GetCharIndexFromPosition(pos);
            int lastLine = getRichTextBox().GetLineFromCharIndex(lastIndex);

            //this is point position of last visible char, we'll 
            //use its Y value for calculating numberLabel size
            pos = getRichTextBox().GetPositionFromCharIndex(lastIndex);

            //finally, renumber label
            //numberLabel.Text = "";
            for (int i = firstLine; i <= lastLine + 1; i++)
            {
              //  numberLabel.Text += i + 1 + "\n";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void traducirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Syntactic mySyntactic = new Syntactic();
            bool resultado = mySyntactic.analyze(getRichTextBox().Text);

            if (resultado == true)
            {
                Console.WriteLine("Analisis Correcto");
            }
            else {
                Console.WriteLine("Analisis incorrecto");

            }
        }
    }
}
