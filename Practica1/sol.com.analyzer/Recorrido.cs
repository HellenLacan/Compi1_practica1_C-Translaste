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
            MessageBox.Show(tot);
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
                             conteiner  id   LISTA_SENTENCIAS
                    */
                    String[] idClass = (root.ChildNodes.ElementAt(1).ToString()).Split(' ');
                    lenguajeCS += "\nclass " + idClass[0] + "{";
                    String hijo3_C = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS); //HIJO 3 DE CLASE (LISTA_SENTENCIAS)
                    lenguajeCS += hijo3_C + "\n}";
                    break;

                case "LISTA_SENTENCIAS":
                    switch (root.ChildNodes.Count) {
                        case 1:

                            switch (root.ChildNodes.ElementAt(0).Term.Name) {

                                case "CONSTRUCTOR":
                                    break;

                                case "VARIABLES_GLOBALES":
                                    return recorrerAST(root.ChildNodes.ElementAt(0),lenguajeCS);

                                case "METODOS":
                                    break;

                            }

                            break;
                        case 2:
                            String listaVars =  recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                            String vars_lv = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);

                            return listaVars + vars_lv;

                    }
                    break;

                case "VARIABLES_GLOBALES":
                    /*
                                           VARIABLES_GLOBALES
                                     /       |         |             \
                             VISIBILIDAD + TIPO + identificador + ASIGNACION_VAR;
                    */
                    String visibilidad = "";
                    String tipo = "";
                    String expresion = "";

                    //Reconoce visibilidad
                    if (root.ChildNodes.ElementAt(0).ChildNodes.Count == 1){
                        visibilidad = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);
                    }

                    //Reconoce tipo
                    tipo = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                    String[] idVar = (root.ChildNodes.ElementAt(2).ToString()).Split(' ');

                    //Reconoce Expresion
                    expresion = recorrerAST(root.ChildNodes.ElementAt(3), lenguajeCS);

                    String var = "";
                    if (visibilidad != "")
                    {
                        var = "\n\t" + visibilidad + " " + tipo + " " + idVar[0] + expresion +";";
                    }
                    else {
                        var = "\n\t " + tipo + " " + idVar[0] + expresion + ";";
                    }

                    return var;

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
                            return root.ChildNodes.ElementAt(0).Term.Name;

                    }

                    break;

                case "ASIGNACION_VAR":

                    switch (root.ChildNodes.Count) {
                        case 1:
                            return recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS); ;

                    }

                    break;
                case "TIPO_ASIGN_VAR":

                    switch (root.ChildNodes.ElementAt(0).Term.Name) {

                        case "EXPR":
                            return " = " + recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS);

                        case "CONDICION":
                            break;

                        default:
                            String[] id_tipoAsingVar = (root.ChildNodes.ElementAt(1).ToString()).Split();
                            return " = new " + id_tipoAsingVar[0] +"()";
                    }

                    break;

                case "EXPR":
                    String hoja = "";
                    switch (root.ChildNodes.Count) {
                        case 1:
                            hoja = root.ChildNodes.ElementAt(0).ToString();

                            if (hoja == "EXPR")
                            {
                                String[] hojaE = (recorrerAST(root.ChildNodes.ElementAt(0),lenguajeCS)).ToString().Split(' ');
                                return "(" + hojaE[0] + ")"; ;
                            }

                            return (hoja);
                            
                        case 2:
                            break;

                        case 3:

                            String[] n1 = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS).Split(' ');
                            String[] n2 = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS).Split(' ');
                            String[] signo = (root.ChildNodes.ElementAt(1).ToString()).Split(' ') ;

                            if (n1[0] == "EXPRESION")
                            {
                                String[] a1 = (root.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString()).Split(' ') ;
                                Console.WriteLine(a1[0]);

                            }
                            else {
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
                    }

                    break;


            }

            return lenguajeCS;
        }
    }
}
