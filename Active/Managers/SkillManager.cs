using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class SkillManager
    {
        //Carrot Town - Halflings
        //Emberfell - Dwaves
        //Cloudspire - Humans
        //Hymn Harbor - Elves
        //Winghelm - Humans
        //Pyramaad - Lizardfolk
        //Mount Goblin - Orcs
        //Sanctuary - Halflings
        //Dryad's Grove - Elves
        //The Tomb - Dwarves

        //Persuasion - Human/Halfling
        //Wisdom - Elf/Lizardfolk
        //Intimidation - Dwarf/Orc

        //0. Human      (P)
        //1. Halfling   (P)
        //2. Elf        (W)
        //3. Lizardboi  (W)
        //4. Dwarf      (I)
        //5. Orc        (I)

        //My är analfabet och har säkert stavat någon av dessa fel
        //0. Wisdom
        //1. Intimidation
        //2. Persuation
        //My är analfabet och har säkert stavat någon av dessa fel

        static public void Init()
        {            
           
        }

        static public float ReturnSkillModifier(string location, int catMod)
        {
            int temp = 0;

            for (int i = 0; i < CityManager.Cities.Count; i++)
            {
                if (Player.Location == CityManager.Cities[i].Name)
                {
                    temp = CityManager.Cities[i].Race;
                }
            }
            if (catMod == 0)
            {
                if (temp == 2 || temp == 3)
                {
                    return 0.05f * Player.ReturnSkillLevel("Wisdom");
                }
                if (temp == 4 || temp == 5)
                {
                    return 0.05f * Player.ReturnSkillLevel("Intimidation");
                }
                if (temp == 0 || temp == 1)
                {
                    return 0.05f * Player.ReturnSkillLevel("Persuasion");
                }
                return 0;
            }
            else
            {
                if (temp == 2 || temp == 3)
                {
                    return 0.05f * Player.ReturnSkillLevel("Wisdom") + 1;
                }
                if (temp == 4 || temp == 5)
                {
                    return 0.05f * Player.ReturnSkillLevel("Intimidation") + 1;
                }
                if (temp == 0 || temp == 1)
                {
                    return 0.05f * Player.ReturnSkillLevel("Persuasion") + 1;
                }
                return 0;
            }
        }
    }
}
