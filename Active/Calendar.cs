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

        static Vector2 pos = new Vector2(820, 18);

        static public void PrepareCalendar()
        {
            day = 0;
            week = 0;
            month = 0;

            months.Add("January");
            months.Add("February");
            months.Add("March");
            months.Add("April");
            months.Add("May");
            months.Add("June");
            months.Add("July");
            months.Add("August");
            months.Add("September");
            months.Add("October");
            months.Add("November");
            months.Add("December");

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
            timer++; //can be removed later

            if (timer == 60)
            {
                AddDays(1);
                timer = 0;
            }      //Can be removed later

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

        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.fontInventory, currentDay, pos, Color.Black);
            spriteBatch.DrawString(TextureManager.fontInventory, dayOfMonth.ToString() + dayFollowUp + " " + currentMonth, new Vector2(pos.X, pos.Y + 40), Color.Black);
            spriteBatch.DrawString(TextureManager.fontInventory, "Total days: " + totalDays.ToString(), new Vector2(pos.X, pos.Y + 80), Color.Black);
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