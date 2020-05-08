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

        public WorldEvent(string n, string d, int id, string[] target, bool[] ei, int[] ev, int days)
        {
            eventName = n;
            eventDes = d;
            eventID = id;
            this.target = target;
            effectID = ei;
            effectVal = ev;
            daysLeft = days;
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

        public string EventName
        {
            get => eventName;
        }

        public string EventDes
        {
            get => eventDes;
        }

        public int EventID
        {
            get => eventID;
        }

        public string[] Target
        {
            get => target;
        }

        public bool[] EffectID
        {
            get => effectID;
        }

        public int[] EffectVal
        {
            get => effectVal;
        }

        public int DaysLeft
        {
            get => daysLeft;
            set => daysLeft = value;
        }

        public Inventory OldTemplateInv
        {
            get => oldTemplateInv;
            set => oldTemplateInv = value;
        }
    }
}
