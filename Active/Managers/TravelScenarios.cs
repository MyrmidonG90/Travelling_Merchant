using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;

namespace Active
{
    static class TravelScenarios
    {        
        static public void ChangeMoney(int change)
        {
            Player.Inventory.Money += change;
        }

        static public void ChangeMoneyByModifier(float modifier)
        {
            Player.Inventory.Money = (int)((float)(modifier*(Player.Inventory.Money)));
        }

        static public void ChangeTravelTime(int change)
        {
            TravelMenu.TurnsLeft += change;
        }

        static public bool SkillCheck(int difficulty, string skillName)
        {

            Random rand = new Random();
            bool answer = false;
            int modifiers = 95 - difficulty * 20 + Player.ReturnSkillLevel(skillName) * 5 + 10;
            if (modifiers > 95)
            {
                modifiers = 95;
            }
            if (rand.Next(0, 100) < modifiers)
            {
                answer = true;
                Player.AddXP(skillName,25*difficulty);
            }
            return answer;
        }
    }
}
