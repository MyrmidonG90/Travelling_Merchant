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
        int duration;

        public WorldEvent(int id, string[] target)
        {

        }
    }
}
