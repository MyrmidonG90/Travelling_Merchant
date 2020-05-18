using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class Achievement
    {

        
        public string name, description;
        public bool complete;


        public Achievement(string name, string description, bool complete)
        {
            this.name = name;
            this.description = description;
            this.complete = complete;
        }



    }
}
