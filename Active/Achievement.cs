using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class Achievement
    {

        
        public string name, description, progress;
        public bool complete;


        public Achievement(string name, string description, string progress, bool complete)
        {
            this.name = name;
            this.description = description;
            this.progress = progress;
            this.complete = complete;
        }



    }
}
