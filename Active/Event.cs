using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class Event
    {
        int id;
        int eID;
        List<string> text;
        int percentage;
        public Event(int id, int eID, List<string> text, int percentage)
        {
            this.id = id;
            this.eID = eID;
            this.text = new List<string>(text);
            this.percentage = percentage;
        }

        public int Percentage { get => percentage;}
        public int EID { get => eID;}
        public int Id { get => id;}
        public List<string> Text { get => text;}
    }
}
