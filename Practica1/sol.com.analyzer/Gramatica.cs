using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace Practica1.sol.com.analizador
{
    class Gramatica : Grammar
    {
        public Gramatica(): base(caseSensitive:true) {

            #region ER
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            //RegexBasedTerminal numeroDecimal = new RegexBasedTerminal("numeroDecimal", "[0-9]+[.][0-9]+");
            IdentifierTerminal identificador = new IdentifierTerminal("identificador");
            //StringLiteral cadena = new StringLiteral("cadena", "\"", StringOptions.IsTemplate);
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

            #region PalabrasReservadas
            KeyTerm _conteiner = ToTerm("Conteiner");
            KeyTerm _public = ToTerm("Pub");
            KeyTerm _private = ToTerm("Pri");
            KeyTerm _protected = ToTerm("Pro");
            KeyTerm _bool = ToTerm("Bool");
            KeyTerm _decimal = ToTerm("Dec");
            KeyTerm _str = ToTerm("Str");
            KeyTerm _num = ToTerm("Num");
            KeyTerm _prop = ToTerm("prop");
            KeyTerm _return = ToTerm("ret");
            KeyTerm _sif = ToTerm("sif");
            KeyTerm _sifnot = ToTerm("sifnot");
            KeyTerm _trus = ToTerm("sifnot");
            KeyTerm _fals = ToTerm("sifnot");
            KeyTerm _or = ToTerm("or");
            KeyTerm _and = ToTerm("and");
            KeyTerm _not = ToTerm("not");
            KeyTerm _whs = ToTerm("whs");
            KeyTerm _hc = ToTerm("hc");
            KeyTerm _mayor = ToTerm("Gt");
            KeyTerm _mayorQ = ToTerm("Gte");
            KeyTerm _equivalente = ToTerm("Eq");
            KeyTerm _distinto = ToTerm("Eqs");
            KeyTerm _menor = ToTerm("Lt");
            KeyTerm _menorQ = ToTerm("Lte");
            KeyTerm _break = ToTerm("brk");
            KeyTerm _print = ToTerm("print");
            KeyTerm _case = ToTerm("cas");
            KeyTerm _default = ToTerm("def");
            KeyTerm _select = ToTerm("select");
            KeyTerm _new = ToTerm("inst");

            #endregion

            #region No Terminales
            NonTerminal INICIO = new NonTerminal("INICIO");
            NonTerminal E = new NonTerminal("E");
            NonTerminal CLASE = new NonTerminal("CLASE");
            NonTerminal LISTA_CLASES = new NonTerminal("LISTA_CLASES");
            NonTerminal LISTA_SENTENCIAS = new NonTerminal("LISTA_SENTENCIAS");
            NonTerminal VISIBILIDAD = new NonTerminal("VISIBILIDAD");
            NonTerminal VARIABLES_GLOBALES = new NonTerminal("VARIABLES_GLOBALES");
            NonTerminal TIPO = new NonTerminal("TIPO");
            NonTerminal ASIGNACION = new NonTerminal("ASIGNACION");


            #endregion

            #region Gramatica
            INICIO.Rule = LISTA_CLASES;

            LISTA_CLASES.Rule = LISTA_CLASES + CLASE
                               |LISTA_CLASES + VARIABLES_GLOBALES
                               |CLASE
                               |VARIABLES_GLOBALES;

            CLASE.Rule = _conteiner + identificador + corcheteAb + corcheteCerr;

            LISTA_SENTENCIAS.Rule = LISTA_SENTENCIAS + VARIABLES_GLOBALES
                                   |VARIABLES_GLOBALES
                                   |Empty;

            VARIABLES_GLOBALES.Rule = VISIBILIDAD + TIPO + identificador + ptoYComa;

            VISIBILIDAD.Rule = _public
                              | _private
                              | _protected
                              |Empty;

            TIPO.Rule = _str
                       | _num
                       | _bool
                       | _decimal
                       | identificador;


            E.Rule = E + mas + E
                   | E + menos + E
                   | E + por + E
                   | E + div + E
                   | numero;
            #endregion

            #region Preferencias
            this.Root = INICIO;
            #endregion

        }
    }
}
