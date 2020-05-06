using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class WorldEventManager
    {
        static List<string> eventNameList;
        static List<string> eventDesList;
        static List<bool[]> effectIDList;
        static List<int[]> effectValList;
        static List<int> durationList;

        static public void Init()
        {
            eventNameList = new List<string>();
            eventDesList = new List<string>();
            effectIDList = new List<bool[]>();
            effectValList = new List<int[]>();
            durationList = new List<int>();

            StreamReader sr = new StreamReader("./Data/WorldEvents.txt");

            while (!sr.EndOfStream)
            {
                eventNameList.Add(sr.ReadLine());
                eventDesList.Add(sr.ReadLine());

                string temp = sr.ReadLine();
                string[] temp2 = temp.Split(';');
                bool[] data = new bool[4];

                for (int i = 0; i < temp2.Length; i++)
                {
                    if (temp2[i] == "true")
                    {
                        data[i] = true;
                    }
                    else
                    {
                        data[i] = false;
                    }
                }
                effectIDList.Add(data);

                temp = sr.ReadLine();
                temp2 = temp.Split(';');
                int[] data2 = new int[4];

                for (int i = 0; i < temp2.Length; i++)
                {
                    data2[i] = int.Parse(temp2[i]);
                }
                effectValList.Add(data2);
                durationList.Add(int.Parse(sr.ReadLine()));
            }
        }

        static public void Update()
        {

        }
    }
}
