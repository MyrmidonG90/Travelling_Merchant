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
        public int maxAmount, currentAmount;

        public Achievement(string name, string description, string progress, bool complete, int currentAmount, int maxAmount)
        {
            this.name = name;
            this.description = description;
            this.progress = progress;
            this.complete = complete;
            this.currentAmount = currentAmount;
            this.maxAmount = maxAmount;
            
        }
    }
}
