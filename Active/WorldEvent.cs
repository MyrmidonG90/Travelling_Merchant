using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class WorldEvent
    {
        string eventName;
        string eventDes;
        int eventID;
        string[] target;
        bool[] effectID;
        int[] effectVal;
        int daysLeft;
        Inventory oldTemplateInv;

        public WorldEvent(int id, string[] target)
        {

        }

        public bool Countdown(int days)
        {
            daysLeft -= days;
            if (daysLeft <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
