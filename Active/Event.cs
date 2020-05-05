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

        public int Percentage { get => percentage; set => percentage = value; }
        public int EID { get => eID; set => eID = value; }
        public int Id { get => id; set => id = value; }
    }
}
