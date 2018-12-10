using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

/*
 Listas sin simbolo de separacion
              palabras.Rule = MakePlusRule(palabras, palabra);             
              Reconoce: palabra palabra1 palabra2  palabra3

Listas con simbolo de separacion

            l_ids.Rule = this.MakeListRule(l_ids, ToTerm(","), ID);
            Reconoce: id, id, id , id
*/

namespace Practica1.sol.com.analizador
{
    class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: true) {

            #region ER
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            RegexBasedTerminal numeroDecimal = new RegexBasedTerminal("numeroDecimal", "[0-9]+[.][0-9]+");
            IdentifierTerminal identificador = new IdentifierTerminal("identificador");
            StringLiteral cadena = new StringLiteral("cadena", "\"", StringOptions.IsTemplate);
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
            KeyTerm _vac = ToTerm("vac");
            KeyTerm _sfr = ToTerm("sfr");
            KeyTerm _inc = ToTerm("inc");
            KeyTerm _dec = ToTerm("dec");

            #endregion

            #region No Terminales
            NonTerminal INICIO = new NonTerminal("INICIO");
            NonTerminal EXPR = new NonTerminal("EXPR");
            NonTerminal CONDICION_COMPARACION = new NonTerminal("CONDICION_COMPARACION");
            NonTerminal CONDICION_LOGICA = new NonTerminal("CONDICION_LOGICA");
            NonTerminal ASIGNACION_VAR = new NonTerminal("ASIGNACION_VAR");
            NonTerminal TIPO_ASIGN_VAR = new NonTerminal("TIPO_ASIGN_VAR");
            NonTerminal CLASE = new NonTerminal("CLASE");
            NonTerminal LISTA_CLASES = new NonTerminal("LISTA_CLASES");
            NonTerminal SENTENCIAS_DE_CLASE = new NonTerminal("SENTENCIAS_DE_CLASE");
            NonTerminal VISIBILIDAD = new NonTerminal("VISIBILIDAD");
            NonTerminal VISIBILIDAD_OPC = new NonTerminal("VISIBILIDAD_OPC");
            NonTerminal VARIABLES_GLOBALES = new NonTerminal("VARIABLES_GLOBALES");
            NonTerminal CONSTRUCTOR = new NonTerminal("CONSTRUCTOR");
            NonTerminal TIPO = new NonTerminal("TIPO");
            NonTerminal LISTA_PARAMETROS = new NonTerminal("LISTA_PARAMETROS");
            NonTerminal LISTA_VARS = new NonTerminal("LISTA_VARS");
            NonTerminal VARS = new NonTerminal("VARS");
            NonTerminal PARAMETROS = new NonTerminal("PARAMETROS");
            NonTerminal METODOS = new NonTerminal("METODOS");
            NonTerminal FUNCIONES = new NonTerminal("FUNCIONES");
            NonTerminal VARIABLES_LOCALES = new NonTerminal("VARIABLES_LOCALES");
            NonTerminal SENTENCIAS_DE_BUCLE = new NonTerminal("SENTENCIAS_DE_BUCLE");
            NonTerminal LISTA_SENTENCIAS = new NonTerminal("LISTA_SENTENCIAS");
            NonTerminal LISTA_CASE_SWITCH = new NonTerminal("LISTA_CASE_SWITCH");
            NonTerminal CASE = new NonTerminal("CASE");
            NonTerminal LISTA_CASE = new NonTerminal("LISTA_CASE");
            NonTerminal CONDICION_CASE = new NonTerminal("CONDICION_CASE");
            NonTerminal PRINT = new NonTerminal("PRINT");
            NonTerminal SENT_SWITCH = new NonTerminal("SENT_SWITCH");
            NonTerminal BREAK = new NonTerminal("BREAK");
            NonTerminal FOR = new NonTerminal("FOR");
            NonTerminal VAR_FOR = new NonTerminal("VAR_FOR");
            NonTerminal TIPO_VAR_FOR = new NonTerminal("TIPO_VAR_FOR");
            NonTerminal INCREMENTO = new NonTerminal("INCREMENTO");

            #endregion

            #region Gramatica
            INICIO.Rule = LISTA_CLASES;

            LISTA_CLASES.Rule = LISTA_CLASES + CLASE
                               | CLASE;

            CLASE.Rule = _conteiner + identificador + corcheteAb + SENTENCIAS_DE_CLASE + corcheteCerr;

            SENTENCIAS_DE_CLASE.Rule = SENTENCIAS_DE_CLASE + VARIABLES_GLOBALES
                                   | SENTENCIAS_DE_CLASE + CONSTRUCTOR
                                   | SENTENCIAS_DE_CLASE + METODOS
                                   | SENTENCIAS_DE_CLASE + FUNCIONES
                                   | CONSTRUCTOR
                                   | VARIABLES_GLOBALES
                                   | METODOS
                                   | FUNCIONES
                                   | Empty;

            LISTA_SENTENCIAS.Rule = LISTA_SENTENCIAS + SENT_SWITCH
                                   | LISTA_SENTENCIAS + PRINT
                                   | LISTA_SENTENCIAS + BREAK
                                   | LISTA_SENTENCIAS + FOR
                                   | FOR
                                   | SENT_SWITCH
                                   | PRINT
                                   | BREAK
                                   | Empty;

            FOR.Rule = _sfr + parentesisAb + VAR_FOR+ identificador + asignacion + EXPR + ptoYComa + EXPR + ptoYComa + INCREMENTO+ parentesisCerr + corcheteAb + LISTA_SENTENCIAS+corcheteCerr;

            INCREMENTO.Rule = _inc
                             | _dec;

            VAR_FOR.Rule = _num
                          |_decimal;

            TIPO_VAR_FOR.Rule = numero
                               |numeroDecimal;

            BREAK.Rule = _break + ptoYComa;

            PRINT.Rule = _print + parentesisAb + EXPR + parentesisCerr + ptoYComa;

            VARIABLES_GLOBALES.Rule = VISIBILIDAD + TIPO + identificador + ASIGNACION_VAR + ptoYComa
                                     | TIPO + identificador + ASIGNACION_VAR + ptoYComa;

            //            VARIABLES_LOCALES.Rule = TIPO + identificador + ASIGNACION_VAR + ptoYComa;

            ASIGNACION_VAR.Rule = asignacion + TIPO_ASIGN_VAR
                                 | Empty;

            TIPO_ASIGN_VAR.Rule = EXPR
                                 | CONDICION_COMPARACION
                                 | _new + identificador + parentesisAb + LISTA_VARS + parentesisCerr;

            LISTA_VARS.Rule = LISTA_VARS + coma + EXPR
                             | VARS;

            VARS.Rule = EXPR
                        | Empty;

            CONSTRUCTOR.Rule = identificador + parentesisAb + LISTA_PARAMETROS + parentesisCerr + dosPtos + _public + corcheteAb + corcheteCerr;

            LISTA_PARAMETROS.Rule = LISTA_PARAMETROS + coma + TIPO + identificador
                                   | TIPO + identificador
                                   | Empty;

            METODOS.Rule = VISIBILIDAD + identificador + parentesisAb + LISTA_PARAMETROS + parentesisCerr + dosPtos + _vac + corcheteAb + LISTA_SENTENCIAS+ corcheteCerr;

            FUNCIONES.Rule = VISIBILIDAD + identificador + parentesisAb + LISTA_PARAMETROS + parentesisCerr + dosPtos + TIPO + corcheteAb + LISTA_SENTENCIAS+ corcheteCerr;

            SENT_SWITCH.Rule = _select + parentesisAb + EXPR +parentesisCerr + corcheteAb + CASE + corcheteCerr;

            LISTA_CASE_SWITCH.Rule = LISTA_CASE_SWITCH + LISTA_CASE
                                    | LISTA_CASE
                                    | Empty;

            LISTA_CASE.Rule = _case + CONDICION_CASE + dosPtos + LISTA_SENTENCIAS
                             | _default + dosPtos + LISTA_SENTENCIAS
                             | LISTA_SENTENCIAS;

            CASE.Rule = _case + CONDICION_CASE + dosPtos + LISTA_CASE_SWITCH;

            CONDICION_CASE.Rule = identificador
                                  | cadena
                                  | numero
                                  | numeroDecimal;

            VISIBILIDAD.Rule = _public
                              | _private
                              | _protected  ;

            TIPO.Rule = _str
                       | _num
                       | _bool
                       | _decimal
                       | identificador;

            EXPR.Rule = EXPR + por + EXPR
                   | EXPR + div + EXPR
                   | EXPR + mas + EXPR
                   | EXPR + menos + EXPR
                   | parentesisAb + EXPR + parentesisCerr
                   | numero
                   | numeroDecimal
                   | identificador
                   | cadena
                   | _trus
                   | _fals
                   | menos + EXPR
                   | mas + EXPR;

            CONDICION_COMPARACION.Rule =  EXPR + _mayor + EXPR
                            | EXPR + _mayorQ + EXPR
                            | EXPR + _menor + EXPR
                            | EXPR + _menorQ + EXPR
                            | EXPR + _equivalente + EXPR
                            | EXPR + _distinto + EXPR
                            //| _not + CONDICION_COMPARACION
                            //| EXPR + _and + EXPR
                            | parentesisAb + CONDICION_COMPARACION + parentesisCerr;
            #endregion

            #region Preferencias
            this.Root = INICIO;
            #endregion

            MarkPunctuation(corcheteAb, corcheteCerr, parentesisAb,parentesisCerr, asignacion ,ptoYComa, coma, dosPtos,
                            _vac, _select, _conteiner, _print, _case, _sfr);

            MarkTransient(VAR_FOR);
            
        }
    }
}
