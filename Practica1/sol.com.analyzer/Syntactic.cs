using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using Practica1.sol.com.analizador;

namespace Practica1.sol.com.analyzer
{
    class Syntactic : Grammar
    {
        public bool analyze(String text) {

            Gramatica myGrammar = new Gramatica();
            LanguageData lenguaje = new LanguageData(myGrammar);
            Parser p = new Parser(lenguaje);
            ParseTree tree = p.Parse(text);
            return tree.Root != null;
        }
    }
}
