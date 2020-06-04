using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class Calendar
    {
        static List<string> months = new List<string>();
        static List<string> days = new List<string>();

        static string currentDay;
        static string currentMonth;
        static string dayFollowUp;

        static int totalDays = 0;
        static int timer = 0;

        static int day;
        static int week;
        static int month;
        static int dayOfMonth;

        static Vector2 pos = new Vector2(800, 24);
        static Button eventLog = new Button(1280, 10, 195, 90, "event", "Open log", TextureManager.texButton);
        static Rectangle box = new Rectangle(730, 0, 460, 200);

        static public void PrepareCalendar()
        {
            day = 0;
            week = 0;
            month = 0;

            months.Add("Jan");
            months.Add("Feb");
            months.Add("Mar");
            months.Add("Apr");
            months.Add("May");
            months.Add("Jun");
            months.Add("Jul");
            months.Add("Aug");
            months.Add("Sep");
            months.Add("Oct");
            months.Add("Nov");
            months.Add("Dec");

            days.Add("Monday");
            days.Add("Tuesday");
            days.Add("Wednesday");
            days.Add("Thursday");
            days.Add("Friday");
            days.Add("Saturday");
            days.Add("Sunday");
        }

        static public void AddDays(int addedDays)
        { 
            for (int i = 0; i < addedDays; i++)
            {
                totalDays++;
                day++;

                if (day >= 7)
                {
                    day = 0;
                    week++;
                }

                if (week >= 4)
                {
                    week = 0;
                    month++;
                }

                if (month >= 12)
                {
                    month = 0;
                }
            }
        }

        static public void Update()
        {
            currentDay = days[day];
            currentMonth = months[month];
            dayOfMonth = 7 * week + day + 1;

            if (dayOfMonth == 1)
            {
                dayFollowUp = "st";
            }
            else if (dayOfMonth == 2)
            {
                dayFollowUp = "nd";
            }
            else if (dayOfMonth == 3)
            {
                dayFollowUp = "rd";
            }
            else
            {
                dayFollowUp = "th";
            }

            
        }

        static public bool CheckEventClick()
        {
            if (eventLog.LeftClick())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.texCalendar, box, Color.White);
            spriteBatch.DrawString(TextureManager.font18, currentDay, pos, Color.Black);
            spriteBatch.DrawString(TextureManager.font18, currentMonth + " " + dayOfMonth.ToString() + dayFollowUp, new Vector2(pos.X, pos.Y + 30), Color.Black);
            spriteBatch.DrawString(TextureManager.font18, "Total days:\n       " + totalDays.ToString(), new Vector2(pos.X + 200, pos.Y), Color.Black);
            eventLog.Draw(spriteBatch);
        }

        public static int TotalDays
        {
            get
            {
                return totalDays;
            }
            set
            {
                totalDays = value;
            }
        }
    }
}