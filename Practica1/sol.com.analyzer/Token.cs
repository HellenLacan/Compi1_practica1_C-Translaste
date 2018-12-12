using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.sol.com.analyzer
{
    class Token
    {
        String _tipo;
        String _lexema;
        int _fila;
        int _columna;
        String _descripcion;

        public Token(String tipo, String lexema, int fila, int columna, String descripcion) {
            this._tipo = tipo;
            this._lexema = lexema;
            this._fila = fila;
            this._columna = columna;
            this._descripcion = descripcion;
        }

        public Token()
        {
           
        }

        public string tipo
        {
            get { return this._tipo; }
            private set { this._tipo = value; }
        }

        public string lexema
        {
            get { return this._lexema; }
            private set { this._lexema = value; }
        }

        public int fila
        {
            get { return this._fila; }
            private set { this._fila = value; }
        }

        public int columna
        {
            get { return this._columna; }
            private set { this._columna = value; }
        }


        public string descripcion
        {
            get { return this._descripcion; }
            private set { this._descripcion = value; }
        }
    }
}
