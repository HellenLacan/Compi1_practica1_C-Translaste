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
                                       clase
                                     /   |   \
                             conteiner  id   lista_sentencias
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

                    String visibilidad = "";
                    String tipo = "";
                    
                    if (root.ChildNodes.ElementAt(0).ChildNodes.Count == 1){
                        visibilidad = recorrerAST(root.ChildNodes.ElementAt(0), lenguajeCS); ;
                    }

                    tipo = recorrerAST(root.ChildNodes.ElementAt(1), lenguajeCS);
                    String[] idVar = (root.ChildNodes.ElementAt(2).ToString()).Split(' ');

                    String var = "\n\t" + visibilidad + " " + tipo + " " + idVar[0] + ";";
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
            }

            return lenguajeCS;
        }
    }
}
