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
        public static List<Token> lista = new List<Token>();

        public ParseTreeNode analyze(String cadena) {
            lista = new List<Token>();
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            Token tk;
            if (arbol.Root == null)
            {
                for (int i = 0; i < arbol.ParserMessages.Count(); i++) {
                    String error = arbol.ParserMessages.ElementAt(i).Level.ToString();
                    int fila = arbol.ParserMessages.ElementAt(i).Location.Line;
                    int columna = arbol.ParserMessages.ElementAt(i).Location.Column;
                    String nombre = arbol.ParserMessages.ElementAt(i).Message.ToString();

                    string tipoError = nombre.Substring(0, 6);
                    Console.WriteLine("Substring: {0}", tipoError);

                    if (tipoError != "Syntax")
                    {
                        String caracter = nombre.Split('\'', '\'')[1];
                        tk = new Token("Lexico", caracter, fila + 1, columna + 1, "El caracter no pertenece al lenguaje");
                        lista.Add(tk);
                    }
                    else
                    {
                        tk = new Token("Sintactico", "", fila + 1, columna + 1, nombre);
                        lista.Add(tk);
                    }
                }
            }
            else {

            }

            return arbol.Root;
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
