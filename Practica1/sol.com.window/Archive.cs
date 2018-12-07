using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1.sol.com.window{

    class Archive{

        public String _content;
        public String _path;
        public String _name;

        public Archive()
        {
        }

        public Archive(String content, String path) {
            this._content = content;
            this._path = path;
        }

        public string content {
            get { return this._content; }
            private set { this._content = value; }
        }

        public String name {
            get { return this._name; }
            private set { this._name = value; }
        }

        public string path { 
            get { return this._path; }
            private set { this._path = value; }
        }

        public void openFile(String a="s") {

            
        }
    }
}
