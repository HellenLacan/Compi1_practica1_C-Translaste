﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Irony.Parsing;

namespace Practica1.sol.com.analyzer
{
    class Recorrido
    {
        public static void traducir(ParseTreeNode root) {
            String lenguajeCS="";
            String tot = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
            //MessageBox.Show(tot);
        }

        public static String recorrerAST(ParseTreeNode root, String lenguajeCS) {
            switch (root.Term.Name) {
                case "LISTA_CLASES":

                    switch (root.ChildNodes.Count) {
                        case 1:
                            return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                        case 2:
                            String hijo1_LC = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS); //HIJO 1 DE LISTA_CLASES(LISTA_CLASES)
                            String hijo2_LC = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS); //HIJO 2 DE LISTA_CLASES(CLASE)
                            return (hijo1_LC + hijo2_LC);
                    }

                    break;

                case "CLASE":
                    /*
                                       CLASE
                                     /   |   \
                             conteiner  id   SENTENCIAS_DE_CLASE
                    */
                    String[] idClass = (root.ChildNodes.ElementAt(0).ToString()).Split(' ');
                    lenguajeCS += "\nclass " + idClass[0] + "{";
                    String hijo3_C = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS); //HIJO 3 DE CLASE (SENTENCIAS_DE_CLASE)
                    lenguajeCS += hijo3_C + "\n\n}";
                    break;

                case "SENTENCIAS_DE_CLASE":
                    switch (root.ChildNodes.Count) {

                        case 0:
                            return "";
                        case 1:

                            switch (root.ChildNodes.ElementAt(0).Term.Name) {

                                case "CONSTRUCTOR":
                                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                                case "VARIABLES_GLOBALES":
                                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                                case "VARIABLES_LOCALES":
                                    String sentencias = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                                    return sentencias;

                                case "METODOS":
                                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                                case "FUNCIONES":
                                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                                case "LLAMADAS_MET_FUNC":
                                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                            }

                            break;
                        case 2:
                            String listaVars = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String vars_lv = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);

                            return listaVars + vars_lv;

                    }
                    break;

                case "VARIABLES_GLOBALES":
                    String visibilidad = "";
                    String tipo = "";
                    String expresion = "";

                    switch (root.ChildNodes.Count) {

                        case 4:
                            /*
                                           VARIABLES_GLOBALES
                                     /       |         |             \
                             VISIBILIDAD + TIPO + identificador + ASIGNACION_VAR;
                            */

                            //Reconoce visibilidad
                            if (root.ChildNodes.ElementAt(0).ChildNodes.Count == 1)
                            {
                                visibilidad = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            }

                            //Reconoce tipo
                            tipo = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                            String[] idVar = (root.ChildNodes.ElementAt(2).ToString()).Split(' ');

                            //Reconoce Expresion
                            expresion = recorrerAST(root.ChildNodes.ElementAt(3), lenguajeCS);

                            return "\n\t" + visibilidad + " " + tipo + " " + idVar[0] +expresion + ";";


                        case 3:

                            //Reconoce tipo
                            tipo = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String[] idVarSt = (root.ChildNodes.ElementAt(1).ToString()).Split(' ');

                            //Reconoce Expresion
                            expresion = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);

                            return "\n\t " + tipo + " " + idVarSt[0] + expresion + ";";
                    }

                    break;

                case "VISIBILIDAD":

                    switch (root.ChildNodes.ElementAt(0).Term.Name) {

                        case "Pub":
                            return "public";

                        case "Pri":
                            return "private";

                        case "Pro":
                            return "protected";

                    }

                    break;

                case "VISIBILIDAD_OPC":
                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                case "TIPO":

                    switch (root.ChildNodes.ElementAt(0).Term.Name) {
                        case "Str":
                            return "String";

                        case "Num":
                            return "int";

                        case "Dec":
                            return "Double";

                        case "Bool":
                            return "Boolean";

                        case "identificador":
                            String[] idExp = root.ChildNodes.ElementAt(0).ToString().Split(' ');
                            return idExp[0];

                    }

                    break;

                case "ASIGNACION_VAR":

                    switch (root.ChildNodes.Count) {
                        case 0:
                            return "";
                        case 1:
                            return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS); ;

                    }

                    break;
                case "TIPO_ASIGN_VAR":

                    switch (root.ChildNodes.ElementAt(0).Term.Name) {

                        case "EXPR":
                            return " = " + recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                        case "LLAMADAS_MET_FUNC":
                            String llamadas_metodos = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            return llamadas_metodos;

                        default:
                            String[] id_tipoAsingVar = (root.ChildNodes.ElementAt(1).ToString()).Split();
                            String parametros = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);
                            return " = new " + id_tipoAsingVar[0] + "(" + parametros + ")";
                    }

                case "EXPR":
                    String hoja = "";
                    switch (root.ChildNodes.Count) {
                        case 1:
                            hoja = root.ChildNodes.ElementAt(0).ToString();
                            string[] separatingChars = { "(" };
                            string[] cadena = hoja.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                            String[] numero = hoja.Split(' ');

                            if (hoja == "EXPR")
                            {
                                String hojaE = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                                return "(" + hojaE + ")"; ;
                            } else if (hoja == "trus (identificador)") {
                                return "true";
                            } else if (hoja == "fals (identificador)") {
                                return "false";
                            } else if (hoja == "LLAMADAS_MET_FUNC") {
                                return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            }

                            if (root.ChildNodes.ElementAt(0).Term.Name == "cadena") {
                                return hoja = "\"" + cadena[0] + "\"";
                            } else if (root.ChildNodes.ElementAt(0).Term.Name == "numero" || root.ChildNodes.ElementAt(0).Term.Name == "numeroDecimal" ||
                                       root.ChildNodes.ElementAt(0).Term.Name == "identificador" || root.ChildNodes.ElementAt(0).Term.Name == "_trus"
                                       ) {
                                return hoja = numero[0];
                            } else if (root.ChildNodes.ElementAt(0).Term.Name == "_fals") {
                                return hoja = "true";
                            }
                            break;
                        case 2:
                            String[] signoN = (root.ChildNodes.ElementAt(0)).ToString().Split(' ');

                            if (signoN[0] == "not" ) {
                                return "!";
                            }
                            hoja = signoN[0] + " " + recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                            return hoja;

                        case 3:

                            String caseOrDefault = (root.ChildNodes.ElementAt(0).Term.Name == "EXPR") ? "\n\n\t\tcase " + ":" : "\n\n\t\tdefault: ";
                            if (root.ChildNodes.ElementAt(0).Term.Name == "EXPR")
                            {
                                String n1 = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                                String n2 = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);
                                String[] signo = (root.ChildNodes.ElementAt(1).ToString()).Split(' ');
                                //return n1 + " " + signo[0] + " " + n2;

                                switch (signo[0])
                                {

                                    case "+":
                                        return (n1 + "+" + n2);

                                    case "-":
                                        return (n1 + "-" + n2);


                                    case "*":
                                        return (n1 + "*" + n2);


                                    case "/":
                                        return (n1 + "/" + n2);


                                    case "Lt":
                                        return (n1 + "<" + n2);

                                    case "Lte":
                                        return (n1 + " <= " + n2);

                                    case "Gt":
                                        return (n1 + " > " + n2);

                                    case "Gte":
                                        return (n1 + " >= " + n2);

                                    case "Eq":
                                        return (n1 + " == " + n2);

                                    case "Eqs":
                                        return (n1 + " != " + n2);

                                    case "and":
                                        return (n1 + " && " + n2);

                                    case "or":
                                        return (n1 + " || " + n2);

                                }
                            }
                            else {
                                //INSTANCIAS
                                String[] id_new = root.ChildNodes.ElementAt(1).ToString().Split();
                                String parametros = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);

                                return " new " + id_new[0] + "(" + parametros + ")";
                            }
                            break;

                    }

                    break;

                case "CONDICION_COMPARACION":
                    String hojaC = "";
                    switch (root.ChildNodes.Count)
                    {
                        case 1:
                            hojaC = root.ChildNodes.ElementAt(0).ToString();

                            if (hojaC == "CONDICION_COMPARACION")
                            {
                                String hojaE = (recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS));
                                return "(" + hojaE + ")"; ;
                            }

                            return (hojaC);

                        case 2:
                            break;
                        case 3:

                            String[] n1 = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS).Split(' ');
                            String[] n2 = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS).Split(' ');
                            String[] signo = (root.ChildNodes.ElementAt(1).ToString()).Split(' ');


                            switch (signo[0])
                            {

                                case "Lt":
                                    return (n1[0] + "<" + n2[0]);

                                case "Lte":
                                    return (n1[0] + " <= " + n2[0]);

                                case "Gt":
                                    return (n1[0] + " > " + n2[0]);

                                case "Gte":
                                    return (n1[0] + " >= " + n2[0]);

                                case "Eq":
                                    return (n1[0] + " == " + n2[0]);

                                case "Eqs":
                                    return (n1[0] + " != " + n2[0]);

                                default:
                                    return (n1[0] + recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS) + n2[0]);

                            }

                    }

                    break;

                case "LISTA_VARS":
                    switch (root.Term.Name) {
                        case "LISTA_VARS":
                            switch (root.ChildNodes.Count) {
                                case 1:
                                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                                case 2:
                                    String listaVars = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                                    String vars = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                                    return listaVars + "," + vars;
                            }
                            break;

                        case "VARS":

                            String varsListaVars = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            return varsListaVars;

                    }
                    break;
                case "VARS":

                    String varsE;
                    switch (root.ChildNodes.Count)
                    {
                        case 0:
                            return "";
                        case 1:

                            switch (root.ChildNodes.ElementAt(0).Term.Name)
                            {

                                case "EXPR":
                                    varsE = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                                    return varsE;

                                default:
                                    varsE = root.ChildNodes.ElementAt(0).ToString();
                                    return varsE;

                            }

                        default:
                            varsE = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            return varsE;
                    }

                case "CONSTRUCTOR":
                    String constructor = "";
                    String[] nombreConstructor = (root.ChildNodes.ElementAt(0)).ToString().Split(' ');
                    String paramConstructor = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                    String cuerpoConstructor = recorrerAST(root.ChildNodes.ElementAt(3), lenguajeCS);

                    constructor = "\n\n\tpublic " + nombreConstructor[0] + "(" + paramConstructor + "){\n\t\t" + cuerpoConstructor + "\n\t}";

                    return constructor;

                case "LISTA_PARAMETROS":
                    String tipoVarPar = "";
                    String listaParams = "";
                    switch (root.ChildNodes.Count) {

                        case 0:
                            return "";

                        case 2:
                            tipoVarPar = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String[] nombreVar = root.ChildNodes.ElementAt(1).ToString().Split(' ');
                            return tipoVarPar + " " + nombreVar[0];
                        case 3:
                            listaParams = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS) + ", ";
                            tipoVarPar = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                            String[] nombreVarLista = root.ChildNodes.ElementAt(2).ToString().Split(' ');
                            return listaParams + tipoVarPar + " " + nombreVarLista[0];
                    }
                    break;

                case "METODOS":
                    String visibilidadMetodo = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                    String[] nombreMetodo = root.ChildNodes.ElementAt(1).ToString().Split(' ');
                    String listaParametros = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);
                    String sentenciaMetodos = recorrerAST(root.ChildNodes.ElementAt(3), lenguajeCS);

                    return "\n\n\t" + visibilidadMetodo + " void " + nombreMetodo[0] + "(" + listaParametros + "){\n\n\t" + sentenciaMetodos + "\n\n\t}";

                case "FUNCIONES":

                    String visibilidadFuncion = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                    String[] nombreFuncion = root.ChildNodes.ElementAt(1).ToString().Split(' ');
                    String listaParametrosFuncion = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);
                    String tipoFuncion = recorrerAST(root.ChildNodes.ElementAt(3), lenguajeCS);
                    String sentenciaFunciones = recorrerAST(root.ChildNodes.ElementAt(4), lenguajeCS);

                    return "\n\n\t" + visibilidadFuncion + " " + tipoFuncion + " " + nombreFuncion[0] + "(" + listaParametrosFuncion + "){\n\n\t" + sentenciaFunciones + "\n\n\t}";

                case "LISTA_SENTENCIAS":
                    switch (root.ChildNodes.Count) {

                        case 0:
                            return "";

                        case 1:

                            return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                        case 2:
                            String listaSent = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String sentencias = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                            return listaSent + sentencias;
                    }

                    break;

                case "PRINT":
                    return "\n\t\tConsole.WriteLine(" + recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS) + ");";

                case "BREAK":
                    return "\n\t\tBreak;";


                case "SENT_SWITCH":
                    String exprSentSwitch = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                    String sent_switch = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                    String sentSentSwitch = "\n\n\t\tswitch(" + exprSentSwitch + "){\n" + sent_switch + "\n\n\t\t}";
                    return sentSentSwitch;

                case "CASE":
                    String condicionCase = "\n\t\tcase " + recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS) + ":";
                    String listaCase = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                    return condicionCase + listaCase;

                case "LISTA_CASE_SWITCH":
                    switch (root.ChildNodes.Count) {
                        case 0:
                            return "";
                        case 1:
                            String L = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            return L;

                        case 2:
                            String listaSwithCase = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String listCase = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                            return listaSwithCase + listCase;
                    }
                    break;

                case "LISTA_CASE":

                    String condicion;

                    switch (root.ChildNodes.Count) {

                        case 1:
                            condicion = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            return condicion;

                        case 2:
                            String keywordCase;

                            switch (root.ChildNodes.ElementAt(0).Term.Name) {

                                case "CONDICION_CASE":
                                    String cond1 = "\n\n\t\tcase " + recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS) + ":";
                                    String cond2 = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                                    return cond1 + cond2;

                                default:

                                    String caseOrDefault = (root.ChildNodes.ElementAt(0).ToString() == "cas") ? "\n\n\t\tcase " + ":" : "\n\n\t\tdefault: ";
                                    keywordCase = caseOrDefault + recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);

                                    return keywordCase;

                            }
                    }
                    break;

                case "CONDICION_CASE":
                    switch (root.ChildNodes.ElementAt(0).Term.Name) {
                        case "cadena":
                            string[] separatingChars = { "(" };
                            string[] cadena = root.ChildNodes.ElementAt(0).ToString().Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                            return "\"" + cadena[0] + "\"";
                        default:
                            String[] term = root.ChildNodes.ElementAt(0).ToString().Split(' ');
                            return term[0];
                    }

                case "FOR":
                    String tipoVarFor = (root.ChildNodes.ElementAt(0).ToString() == "Num") ? "int" : "Double";
                    String[] idFor = root.ChildNodes.ElementAt(1).ToString().Split(' ');
                    String a = root.ChildNodes.ElementAt(4).ChildNodes.ElementAt(0).ToString();
                    String[] incr = (root.ChildNodes.ElementAt(4).ChildNodes.ElementAt(0).ToString().Split(' '));
                    String incremento = (incr[0] == "inc") ? "++" : "--";
                    String sentenciasFor = recorrerAST(root.ChildNodes.ElementAt(5), lenguajeCS);
                    String expr1 = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);
                    String expr2 = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);

                    String sentencia = "\n\n\t\tfor(" + tipoVarFor + " " + idFor[0] + "=" + expr1 + "; " + expr2 + "; " + idFor[0] + incremento + ")" + "{\n\n" + sentenciasFor + "\n\t\t}";
                    return sentencia;

                case "WHILE":
                    String expre1 = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                    String listaSentencias = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                    String _while = "\n\n\t\twhile(" + expre1 + "){\n" + listaSentencias + "\n\n\t\t}";
                    return _while;

                case "DOWHILE":
                    listaSentencias = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                    expre1 = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                    return "\n\n\t\tdo{\n\n" + listaSentencias + "\n\n\t\t}while(" + expre1 + ");";

                case "IF":
                    expre1 = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                    listaSentencias = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                    String opcional = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS); ;
                    return "\n\n\t\tif(" + expre1 + "){\n\t\t" + listaSentencias + "\n\n\t\t}" + opcional;

                case "ELSE_OPCIONAL":
                    switch (root.ChildNodes.Count) {
                        case 0:
                            return "";
                        case 1:
                            listaSentencias = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            return "else{\n\t\t" + listaSentencias + "\n\t\t}";

                    }
                    break;

                case "ELSE":
                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                /*TIPO + identificador + ASIGNACION_VAR + ptoYComa
                                 |identificador + ASIGNACION_VAR + ptoYComa ;*/
                case "VARIABLES_LOCALES":
                    switch (root.ChildNodes.Count) {

                        case 1:
                            String varLocal = recorrerAST(root.ChildNodes.ElementAt(0),lenguajeCS);
                            return "\n\t\tthis" +  varLocal + ";";
                            
                        case 2:
                            String asign;
                            if (root.ChildNodes.ElementAt(0).Term.Name == "LLAMADAS_MET_FUNC")
                            {
                                String idVar = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                                asign = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                                return "\n\t\t" + idVar + " " + asign + ";";
                            }
                            else {
                                String[] id1 = root.ChildNodes.ElementAt(0).ToString().Split(' '); ;
                                asign = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                                return "\n\t\t" + id1[0] + " " + asign + ";";
                            }
                        case 3:
                            
                            tipo = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String []id2 = (root.ChildNodes.ElementAt(1)).ToString().Split(' ');
                            asign = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);

                            return "\n\t\t" + tipo + " " +id2[0] + asign + ";";
                    }
                    break;

                case "THIS":
                    String lista = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                    String E = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                    return lista + E;

                case "LISTA_THIS":
                    switch (root.ChildNodes.Count) {
                        case 1:
                            String[] _this = (root.ChildNodes.ElementAt(0)).ToString().Split(' ');
                            return "." + _this[0];
                        case 2:
                            String a1 = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String b1 = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            return a1 + b1;

                    }
                    break;
                case "LLAMADAS_MET_FUNC":
                   
                        String[] _name = (root.ChildNodes.ElementAt(0)).ToString().Split(' ');
                        lista = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);

                        return _name[0] + lista;

                case "RETURN":

                    switch (root.ChildNodes.Count) {
                        case 1:
                            return "\n\t\treturn;";

                        case 2:
                            lista = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                            return "\n\t\treturn " + lista + ";";
                    }

                    break;

                case "LISTA_LLAMADA_FUN":
                    String[] _name2 = (root.ChildNodes.ElementAt(0)).ToString().Split(' ');
                    lista = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);

                    return "."+_name2[0] + lista;


            }

            return lenguajeCS;
        }
    }
}
