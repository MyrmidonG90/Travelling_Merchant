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

        //jag ber om ursäkt över hur detta är upplagt men jag var trött och ville bara få det gjort så hoppas du okända person kan förlåta mig

        static int[] races;

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
            races = new int[10];
            races[0] = 2;
            races[1] = 1;
            races[2] = 2;
            races[3] = 0;
            races[4] = 2;
            races[5] = 0;
            races[6] = 1;
            races[7] = 2;
            races[8] = 0;
            races[9] = 1;
        }

        static public float ReturnSkillModifier(string location)
        {
            int temp = 0;

            for (int i = 0; i < WorldMapMenu.Cities.Length; i++)
            {
                if (Player.Location == WorldMapMenu.Cities[i].Name)
                {
                    temp = races[i];
                }
            }

            if (temp == 0)
            {
                return 0.05f * Player.ReturnSkillLevel("Wisdom");
            }
            if (temp == 1)
            {
                return 0.05f * Player.ReturnSkillLevel("Intimidation");
            }
            if (temp == 2)
            {
                return 0.05f * Player.ReturnSkillLevel("Persuasion");
            }
            return 0;
        }
    }
}
