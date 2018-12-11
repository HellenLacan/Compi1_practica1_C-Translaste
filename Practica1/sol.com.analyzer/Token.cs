using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.sol.com.analyzer
{
    class Token
    {
        String _token;
        String _id;
        String _fila;
        String _columna;

        public Token(String token, String id, String fila, String columna) {
            this._token = token;
            this._id = id;
            this._fila = fila;
            this._columna = columna;
        }

        public Token()
        {
           
        }

        public string token
        {
            get { return this._token; }
            private set { this._token = value; }
        }

        public string id
        {
            get { return this._id; }
            private set { this._id = value; }
        }

        public string fila
        {
            get { return this._fila; }
            private set { this._fila = value; }
        }

        public string columna
        {
            get { return this._columna; }
            private set { this._columna = value; }
        }
    }
}
