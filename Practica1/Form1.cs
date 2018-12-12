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
            selectionStart = 0;
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

            //TAB 2

            TabPage newTabPage2 = new TabPage("New Document");
            newTabPage2.Font = new Font("Verdana", 18);

            RichTextBox newTextBox2 = new RichTextBox();
            newTextBox2.Dock = DockStyle.Fill;
            newTextBox2.Font = new Font("Verdana", 10);
            newTextBox2.BackColor = Color.White;
            newTextBox2.BorderStyle = BorderStyle.None;

            newTabPage2.Controls.Add(newTextBox2);
            tabControl2.TabPages.Add(newTabPage2);

            getRichTextBox().Select();

            AddLineNumbers();
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = getRichTextBox().Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)getRichTextBox().Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)getRichTextBox().Font.Size;
            }
            else
            {
                w = 50 + (int)getRichTextBox().Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = getRichTextBox().GetCharIndexFromPosition(pt);
            int First_Line = getRichTextBox().GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = getRichTextBox().GetCharIndexFromPosition(pt);
            int Last_Line = getRichTextBox().GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
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
                openFileDialog.Filter = "psc files (*.psc)|*.psc";
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

        //Teclas Control de linea y columna en un richtexBox
        private void newTextBox_SelectionChanged(object sender, EventArgs e)
        {
            int index = getRichTextBox().SelectionStart;
            int line = getRichTextBox().GetLineFromCharIndex(index);

            // Get the column.
            int firstChar = getRichTextBox().GetFirstCharIndexFromLine(line);
            int column = index - firstChar;

            lblLinea.Text = "Line: " + (line +1) ;
            lblColumna.Text = "Col: " + (column + 1);

            Point pt = getRichTextBox().GetPositionFromCharIndex(getRichTextBox().SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }

        }

        //Control de linea y columna en un richtexBox
        private void newTextBox_MouseDown(object sender, EventArgs e)
        {
            int index = getRichTextBox().SelectionStart;
            int line = getRichTextBox().GetLineFromCharIndex(index);

            // Get the column.
            int firstChar = getRichTextBox().GetFirstCharIndexFromLine(line);
            int column = index - firstChar;

            lblLinea.Text = "Line: " + (line + 1);
            lblColumna.Text = "Col: " + (column + 1);

            Point pt = getRichTextBox().GetPositionFromCharIndex(getRichTextBox().SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }

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
            newTextBox.FontChanged += new System.EventHandler(this.newTextBox_FontChanged);
            newTextBox.TextChanged += new System.EventHandler(this.newTextBox_TextChanged);
            newTextBox.VScroll += new System.EventHandler(this.newTextBox_VScroll);


            selectionStart = newTextBox.SelectionStart;
            LineNumberTextBox.Text = "1\n2";
            LineNumberTextBox.Text = "1\n2";

            /*TAB CONTROL NO. 2*/

            TabPage newTabPage2 = new TabPage("New Document.cs");
            newTabPage2.Font = new Font("Verdana", 18);

            RichTextBox newTextBox2 = new RichTextBox();
            newTextBox2.Dock = DockStyle.Fill;
            newTextBox2.Font = new Font("Verdana", 10);
            newTextBox2.BackColor = Color.White;
            newTextBox2.BorderStyle = BorderStyle.None;

            newTabPage2.Controls.Add(newTextBox2);
            tabControl2.TabPages.Add(newTabPage2);


        }

        private void newTextBox_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }

        private void newTextBox_TextChanged(object sender, EventArgs e)
        {
            if (getRichTextBox().Text == "")
            {
                AddLineNumbers();
            }
        }

        private void newTextBox_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = richTextBox1.Font;
            getRichTextBox().Select();
            AddLineNumbers();
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            getRichTextBox().Select();
            LineNumberTextBox.DeselectAll();
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

        private RichTextBox getRichTextBox2()
        {
            RichTextBox richTextBox2 = null;
            TabPage tp = tabControl2.SelectedTab;
            var tab = tabControl2.SelectedTab;

            if (tp != null)
            {
                richTextBox2 = tp.Controls[0] as RichTextBox;
            }

            return richTextBox2;
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
                MessageBox.Show("Analisis Correcto");
                richTextBox1.Text = "";
                String text = "";
                String lenguaje = (Recorrido.recorrerAST(resultado.ChildNodes.ElementAt(0), text));
                getRichTextBox2().Text = lenguaje;
                Recorrido.traducir(resultado);
                Syntactic.generarImagen(resultado);
            }
            else {
                MessageBox.Show("Analisis con errores");
                richTextBox1.Text = "";

                foreach (sol.com.analyzer.Token item in Syntactic.lista)
                {
                    richTextBox1.Text+= "\nError " + item.tipo +": Lexema: \"" + item.lexema + "\"" + ", Linea: " + item.fila + ", Columna: " + item.columna + ", Descripcion: " + item.descripcion;
                }

            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage currentTab = tabControl1.SelectedTab;
                tabControl1.TabPages.Remove(currentTab);

                TabPage currentTab2 = tabControl2.SelectedTab;
                tabControl2.TabPages.Remove(currentTab2);
            }
            catch (Exception)
            {
                MessageBox.Show("No hay pestañas");
            }

        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Cs File | *.cs";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    myStream.Close();
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                        sw.WriteLine(getRichTextBox2().Text);

                }
            }

            String texto = getRichTextBox2().Text;
        }
    }
}
