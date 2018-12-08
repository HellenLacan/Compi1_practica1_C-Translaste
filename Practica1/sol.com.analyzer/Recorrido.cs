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
                    lenguajeCS += "\nclass " + idClass[0] + "{" + "\n}";
                    String hijo3_C = recorrerAST(root.ChildNodes.ElementAt(2), lenguajeCS); //HIJO 3 DE CLASE (LISTA_SENTENCIAS)
                    break;

                case "LISTA_SENTENCIAS":

                    break;
            }

            return lenguajeCS;
        }
    }
}
