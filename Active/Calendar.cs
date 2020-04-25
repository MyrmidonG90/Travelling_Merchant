using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class Calendar
    {
        List<string> months = new List<string>();
        List<string> days = new List<string>();

        string currentDay;
        string currentMonth;
        string dayFollowUp;

        int totalDays = 0;
        int timer = 0;

        int day;
        int week;
        int month;
        int dayOfMonth;

        Vector2 pos = new Vector2(1600, 20);



        public Calendar(int day, int week, int month)
        {
            this.day = day;
            this.week = week;
            this.month = month;
        }



        public void PrepareCalendar()
        {
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



        public void AddDays(int addedDays)
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

        public void Update()
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



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.fontInventory, currentDay, pos, Color.Black);
            spriteBatch.DrawString(TextureManager.fontInventory, dayOfMonth.ToString() + dayFollowUp + " " + currentMonth, new Vector2(pos.X, pos.Y + 40), Color.Black);
            spriteBatch.DrawString(TextureManager.fontInventory, "Total days: " + totalDays.ToString(), new Vector2(pos.X, pos.Y + 80), Color.Black);
        }




    }
}


