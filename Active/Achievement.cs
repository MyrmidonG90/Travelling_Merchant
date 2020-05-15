using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class Achievement
    {

        
        string name, description;
        public bool compleate;


        public Achievement(string name, string description, bool compleate)
        {
            this.name = name;
            this.description = description;
            this.compleate = compleate;
        }



    }
}
