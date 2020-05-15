using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class AchievementManager
    {

        static Achievement[] achievements = new Achievement[10];



        public static int boughtCarrots;

        public static int totalCoins;

        public static int travelCounter;

        public static int spentMoney;

        public static bool hasDragonscale, hasGryphonMeat, hasDiamond, hasRuby, hasEmerald;

        public static bool fullInventory;

        public static int currentCoins;





        public static void LoadAchievements()
        {
            achievements[0] = new Achievement("Carrot Hunter", "Own 100 carrots", false);
            achievements[1] = new Achievement("Insane Wealth", "Have 100,000 coins in your inventory", false);
            achievements[2] = new Achievement("On the road again", "Travel 10 times", false);
            achievements[3] = new Achievement("Gotta spend money to earn money", "Spend 1,000,000 coins in total", false);
            achievements[4] = new Achievement("Treasure Finder", "Obtain a rare item", false);
            achievements[5] = new Achievement("Treasure Hunter", "Obtain every rare item", false);
            achievements[6] = new Achievement("Fat stash", "Have a full inventory", false);
            achievements[7] = new Achievement("Jewel heist", "Obtain a ruby, an emerald and a diamond", false);
            achievements[8] = new Achievement("Millionaire", "Have 1,000,000 coins in your inventory", false);
            achievements[9] = new Achievement("Investor", "Earn a total of 10,000,000 coins", false);
        }


        public static void Update()
        {
            if (boughtCarrots >= 100)
            {
                achievements[0].compleate = true;
            }

            if (currentCoins >= 100000)
            {
                achievements[1].compleate = true;
            }

            if (travelCounter >= 10)
            {
                achievements[2].compleate = true;
            }

            if (spentMoney >= 1000000)
            {
                achievements[3].compleate = true;
            }

            if (hasDragonscale || hasGryphonMeat || hasDiamond)
            {
                achievements[4].compleate = true;
            }

            if (hasDragonscale && hasGryphonMeat && hasDiamond)
            {
                achievements[5].compleate = true;
            }

            if (fullInventory)
            {
                achievements[6].compleate = true;
            }

            if (hasRuby && hasEmerald && hasDiamond)
            {
                achievements[7].compleate = true;
            }

            if (currentCoins >= 1000000)
            {
                achievements[8].compleate = true;
            }

            if (totalCoins >= 10000000)
            {
                achievements[9].compleate = true;
            }

        }



    }
}
