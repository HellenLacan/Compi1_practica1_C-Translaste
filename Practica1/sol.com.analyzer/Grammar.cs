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

            #region ER
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            #endregion

            #region Terminales
            var corcheteAb = ToTerm("[");
            var corcheteCerr = ToTerm("]");
            var llaveAb = ToTerm("{");
            var llaveCerr = ToTerm("{");
            var parentesisAb = ToTerm("(");
            var parentesisCerr = ToTerm(")");
            var asignacion = ToTerm("=");
            var ptoYComa = ToTerm(";");
            var coma = ToTerm(",");
            var punto = ToTerm(".");
            var mas = ToTerm("+");
            var menos = ToTerm("-");
            var por = ToTerm("*");
            var div = ToTerm("/");
            var menor = ToTerm("<");
            var mayor = ToTerm(">");
            var menorQ = ToTerm("<=");
            var mayorQ = ToTerm(">=");
            var igual = ToTerm("==");
            var distinto = ToTerm("!=");
            var and = ToTerm("&&");
            var or = ToTerm("||");
            var negacion = ToTerm("!");
            var dosPtos = ToTerm(":");
            #endregion

        }
    }
}
