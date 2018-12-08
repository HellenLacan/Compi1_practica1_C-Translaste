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
        public ParseTreeNode analyze(String text) {

            Gramatica myGrammar = new Gramatica();
            LanguageData lenguaje = new LanguageData(myGrammar);
            Parser p = new Parser(lenguaje);
            ParseTree tree = p.Parse(text);
            ParseTreeNode root = tree.Root;
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
