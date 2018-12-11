using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using Practica1.sol.com.analizador;
using Practica1.sol.com.controlDot;

namespace Practica1.sol.com.analyzer
{
    class Syntactic : Grammar
    {
        public static List<Token[]> lista = new List<Token[]>();

        public ParseTreeNode analyze(String text) {

            Gramatica myGrammar = new Gramatica();
            LanguageData lenguaje = new LanguageData(myGrammar);
            Parser p = new Parser(lenguaje);
            ParseTree tree = p.Parse(text);
            ParseTreeNode root = tree.Root;

            if (root == null)
            {
                for (int i = 0; i < tree.ParserMessages.Count(); i++) {
                    String error = tree.ParserMessages.ElementAt(i).Level.ToString();
                    String fila = tree.ParserMessages.ElementAt(i).Location.Line.ToString();
                    String columna = tree.ParserMessages.ElementAt(i).Location.Column.ToString();
                    String nombre = tree.ParserMessages.ElementAt(i).Message.ToString();

                    string[] separatingChars = { "'" };
                    string[] cadena = nombre.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

                    string[] separatingChars2 = { ":" };
                    string[] espectativa = nombre.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                    
                    Token token = new Token();
                }
            }
            else {

            }

            return root;
        }

        public static void generarImagen(ParseTreeNode raiz) {
            String grafoDot = ControlDot.getDot(raiz);
            Console.WriteLine(grafoDot);
            //WINGRAPHVIZLib.DOT dot = new WINGRAPHVIZLib.DOT();
           // WINGRAPHVIZLib.BinaryImage img = dot.ToPNG(grafoDot);
            //img.Save("AST.png");
        }

        
    }
}
