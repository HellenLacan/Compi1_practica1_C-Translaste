using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace Practica1.sol.com.analizador
{
    class Grammar: Irony.Parsing.Grammar
    {
        public Grammar(): base(caseSensitive:true) {

        }
    }
}
