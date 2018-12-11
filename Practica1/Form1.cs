using Irony.Parsing;
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

        //Control de linea y columna en un richtexBox
        private void newTextBox_SelectionChanged(object sender, EventArgs e)
        {
            int index = getRichTextBox().SelectionStart;
            int line = getRichTextBox().GetLineFromCharIndex(index);

            // Get the column.
            int firstChar = getRichTextBox().GetFirstCharIndexFromLine(line);
            int column = index - firstChar;

            lblLinea.Text = "Line: " + line;
            lblColumna.Text = "Col: " + column;

        }

        int selectionStart = 1;

        //CREAR PESTAÑA NUEVA
        private void newTab_Click(object sender, EventArgs e)
        {
            //TABPANEL1
            TabPage newTabPage = new TabPage("New Document");
            newTabPage.Font = new Font("Verdana", 18);

            RichTextBox newTextBox = new RichTextBox();
            newTextBox.Dock = DockStyle.Fill;
            newTextBox.Font = new Font("Verdana", 10);
            newTextBox.BackColor = Color.White;
            newTextBox.BorderStyle = BorderStyle.None;

            newTabPage.Controls.Add(newTextBox);
            tabControl1.TabPages.Add(newTabPage);

            newTextBox.SelectionChanged += new System.EventHandler(this.newTextBox_SelectionChanged);
            selectionStart = newTextBox.SelectionStart;

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
            var tab = tabControl1.SelectedTab;

            if (tp != null) {
                richTextBox = tp.Controls[0] as RichTextBox;
            }

            return richTextBox;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void traducirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine(!(true || false));
            Syntactic mySyntactic = new Syntactic();
            //bool resultado = mySyntactic.analyze(getRichTextBox().Text);
            ParseTreeNode resultado = mySyntactic.analyze(getRichTextBox().Text);

            if (resultado != null)
            {
                Console.WriteLine("Analisis Correcto");
                String text = "";
                String lenguaje = (Recorrido.recorrerAST(resultado.ChildNodes.ElementAt(0), text));
                richTextBoxTraducido.Text = lenguaje;
                Recorrido.traducir(resultado);
                Syntactic.generarImagen(resultado);
            }
            else {
                Console.WriteLine("Analisis incorrecto");

            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
