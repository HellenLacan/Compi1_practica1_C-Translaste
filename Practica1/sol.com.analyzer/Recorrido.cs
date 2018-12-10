using System;
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
                                    return recorrerAST(root.ChildNodes.ElementAt(0),lenguajeCS);

                                case "METODOS":
                                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                                case "FUNCIONES":
                                    return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                            }

                            break;
                        case 2:
                            String listaVars =  recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
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

                            return  "\n\t" + visibilidad + " " + tipo + " " + idVar[0] + expresion + ";";
                           

                        case 3:
                          
                            //Reconoce tipo
                            tipo = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String[] idVarSt = (root.ChildNodes.ElementAt(1).ToString()).Split(' ');

                            //Reconoce Expresion
                            expresion = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);

                            return  "\n\t " + tipo + " " + idVarSt[0] + expresion + ";";
                    }

                    break;
                    
                case "VISIBILIDAD":

                    switch (root.ChildNodes.ElementAt(0).Term.Name) {

                        case "Pub":
                            return "public";

                        case "Pri":
                            return "private" ;

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

                        case "CONDICION_COMPARACION":
                            return " = " + recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                        default:
                            String[] id_tipoAsingVar = (root.ChildNodes.ElementAt(1).ToString()).Split();
                            String parametros = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);
                            return " = new " + id_tipoAsingVar[0] +"("+ parametros +")";
                    }

                case "EXPR":
                    String hoja = "";
                    switch (root.ChildNodes.Count) {
                        case 1:
                            hoja = root.ChildNodes.ElementAt(0).ToString();
                            string[] separatingChars = { "("};
                            string[] cadena = hoja.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                            String[] numero = hoja.Split(' ');

                            if (hoja == "EXPR")
                            {
                                String[] hojaE = (recorrerAST(root.ChildNodes.ElementAt(0),lenguajeCS)).ToString().Split(' ');
                                return "(" + hojaE[0] + ")"; ;
                            }

                            if (root.ChildNodes.ElementAt(0).Term.Name == "cadena") {
                                hoja = "\"" + cadena[0] + "\"";
                            } else if (root.ChildNodes.ElementAt(0).Term.Name == "numero" | root.ChildNodes.ElementAt(0).Term.Name == "numeroDecimal" |
                                       root.ChildNodes.ElementAt(0).Term.Name == "identificador") {
                                hoja = numero[0];
                            }
                            return (hoja);
                            
                        case 2:
                            String[] signoN = (root.ChildNodes.ElementAt(0)).ToString().Split(' ');
                            hoja = signoN[0] + recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                            return hoja;

                        case 3:

                            String[] n1 = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS).Split(' ');
                            String[] n2 = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS).Split(' ');
                            String[] signo = (root.ChildNodes.ElementAt(1).ToString()).Split(' ') ;


                                switch (signo[0])
                                {

                                    case "+":
                                        return (n1[0] + "+" + n2[0]);

                                    case "-":
                                        return (n1[0] + "-" + n2[0]);


                                    case "*":
                                        return (n1[0] + "*" + n2[0]);


                                    case "/":
                                        return (n1[0] + "/" + n2[0]);

                                default:
                                        return (n1[0] + recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS) + n2[0]);

                            }

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
                                    return listaVars + "," +vars;
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
                    String constructor="";
                    String[] nombreConstructor = (root.ChildNodes.ElementAt(0)).ToString().Split(' ');
                    String paramConstructor = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);

                    constructor = "\n\n\tpublic " + nombreConstructor[0] + "(" + paramConstructor +"){" + "" + "\n\t}";

                    return constructor;

                case "LISTA_PARAMETROS":
                    String tipoVarPar = "";
                    String listaParams="";
                    switch (root.ChildNodes.Count) {

                        case 0:
                            return "";

                        case 2:
                            tipoVarPar = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String[] nombreVar = root.ChildNodes.ElementAt(1).ToString().Split(' ');
                            return tipoVarPar + " " + nombreVar[0];
                        case 3:
                            listaParams =  recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS) + ", ";
                            tipoVarPar = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                            String[] nombreVarLista = root.ChildNodes.ElementAt(2).ToString().Split(' ');
                            return listaParams + tipoVarPar + " "+nombreVarLista[0];
                    }
                    break;

                case "METODOS":
                    String visibilidadMetodo = recorrerAST(root.ChildNodes.ElementAt(0),lenguajeCS);
                    String[] nombreMetodo = root.ChildNodes.ElementAt(1).ToString().Split(' ');
                    String listaParametros = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);
                    String sentenciaMetodos = recorrerAST(root.ChildNodes.ElementAt(3), lenguajeCS);

                    return "\n\n\t" + visibilidadMetodo + " void " + nombreMetodo[0]+ "(" + listaParametros + "){\n\n\t" + sentenciaMetodos+ "\n\n\t}";

                case "FUNCIONES":

                    String visibilidadFuncion = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                    String[] nombreFuncion = root.ChildNodes.ElementAt(1).ToString().Split(' ');
                    String listaParametrosFuncion = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS);
                    String tipoFuncion = recorrerAST(root.ChildNodes.ElementAt(3), lenguajeCS);
                    String sentenciaFunciones = recorrerAST(root.ChildNodes.ElementAt(4), lenguajeCS);

                    return "\n\n\t" + visibilidadFuncion +" "+ tipoFuncion + " " +nombreFuncion[0] + "(" + listaParametrosFuncion + "){\n\n\t" + sentenciaFunciones+ "\n\n\t}";

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
                            return  L;

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
                                    String cond1 = "\n\n\t\tcase "+recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS) + ":";
                                    String cond2 = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                                    return cond1 + cond2;

                                default :
                                    if (root.ChildNodes.ElementAt(0).ToString() == "cas")
                                    {
                                        keywordCase = "\n\n\t\tcase " + ":" + recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                                    }
                                    else
                                    {
                                        keywordCase = "\n\n\t\tdefault: " + recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);

                                    }
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
                    String []idFor = root.ChildNodes.ElementAt(1).ToString().Split(' ');
                    String a = root.ChildNodes.ElementAt(4).ChildNodes.ElementAt(0).ToString();
                    String[] incr = (root.ChildNodes.ElementAt(4).ChildNodes.ElementAt(0).ToString().Split(' '));
                    String incremento = (incr[0] == "inc") ? "++" : "--";
                    String sentenciasFor = recorrerAST(root.ChildNodes.ElementAt(5), lenguajeCS);

                    String sentencia = "\n\n\t\tfor("+ tipoVarFor + " " + idFor[0] +"=" + "CONDICION"+ ";"+ "CONDICION"+ ";" +idFor[0]+ incremento +")"+"{\n\n" + sentenciasFor+"\n\t\t}";
                    return sentencia;
            }

            return lenguajeCS;
        }
    }
}
