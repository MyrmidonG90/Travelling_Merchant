﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class AchievementManager
    {

        public static Achievement[] achievements = new Achievement[11];

        public static int boughtCarrots = 0;

        public static int totalCoins = 0;

        public static int travelCounter = 0;

        public static int spentMoney = 0;

        public static bool hasDragonscale, hasGryphonMeat, hasDiamond, hasRuby, hasEmerald;

        public static int rareItem = 0;

        public static int rareItems = 0;

        public static bool inCT, inSV, inPMAD, inTomb, inGrove, inMTG;

        public static bool fullInventory;

        public static int inventorySpaces = 0;

        public static int currentCoins;





        public static void LoadAchievements()
        {
            achievements[0] = new Achievement("Carrot Hunter", "Own 100 carrots", boughtCarrots + "/100", false);
            achievements[1] = new Achievement("Insane Wealth", "Have 100,000 coins in your inventory", currentCoins + "/100000", false);
            achievements[2] = new Achievement("On the road again", "Travel 10 times", travelCounter + "/10", false);
            achievements[3] = new Achievement("Gotta spend to earn", "Spend 1,000,000 coins in total", spentMoney + "/1000000", false);
            achievements[4] = new Achievement("Treasure Finder", "Obtain a rare item", rareItem + "/1", false);
            achievements[5] = new Achievement("Treasure Hunter", "Obtain every rare item", "/5", false);
            achievements[6] = new Achievement("Fat stash", "Have a full inventory", inventorySpaces + "/25", false);
            achievements[7] = new Achievement("Jewel heist", "Obtain a ruby, an emerald and a diamond", "/3", false);
            achievements[8] = new Achievement("Millionaire", "Have 1,000,000 coins in your inventory", currentCoins + "/1000000", false);
            achievements[9] = new Achievement("Investor", "Earn a total of 10,000,000 coins", totalCoins + "/10000000", false);
            achievements[10] = new Achievement("Mr. Worldwide", "Visit every city", "/10", false);
        }


        public static void Update()
        {
            CheckAchievements();
            LoadAchievements();




        }


        public static void CheckAchievements()
        {
            if (boughtCarrots >= 100)
            {
                achievements[0].complete = true;
            }

            if (currentCoins >= 100000)
            {
                achievements[1].complete = true;
            }

            if (travelCounter >= 10)
            {
                achievements[2].complete = true;
            }

            if (spentMoney >= 1000000)
            {
                achievements[3].complete = true;
            }

            if (hasDragonscale || hasGryphonMeat || hasDiamond)
            {
                achievements[4].complete = true;
                rareItem = 1;
            }

            if (hasDragonscale && hasGryphonMeat && hasDiamond)
            {
                achievements[5].complete = true;
            }

            if (fullInventory)
            {
                achievements[6].complete = true;
            }

            if (hasRuby && hasEmerald && hasDiamond)
            {
                achievements[7].complete = true;
            }

            if (currentCoins >= 1000000)
            {
                achievements[8].complete = true;
            }

            if (totalCoins >= 10000000)
            {
                achievements[9].complete = true;
            }
        }


    }
}