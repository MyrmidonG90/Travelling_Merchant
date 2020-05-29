using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class AchievementManager
    {

        public static Achievement[] achievements = new Achievement[12];

        public static int boughtCarrots = 0;

        public static int totalCoinsEarned = 0;

        public static int travelCounter = 0;

        public static int boatTravelCounter = 0;

        public static int spentMoney = 0;

        public static bool hasDragonscale, hasGryphonMeat, hasDiamond, hasRuby, hasEmerald, hasWhaleMeat;

        public static int rareItem = 0;

        public static int jewels = 0;

        public static int rareItems = 0;

        public static int citiesVisited = 0;

        public static bool inCT, inSV, inPMAD, inTomb, inGrove, inMTG;

        public static int inventorySpaces = 0;

        public static int currentCoins;





        public static void CreateAchievements()
        {
            achievements[0] = new Achievement("Carrot Hunter", "Own 100 carrots", "0/100", false,0, 100);
            achievements[1] = new Achievement("Insane Wealth", "Have 100,000 coins in your inventory", "0/100000", false,0, 100000);
            achievements[2] = new Achievement("On the road again", "Travel 50 times", "0/50", false, 0, 50);
            achievements[3] = new Achievement("Investor", "Spend a total of 1,000,000 coins in trades", "0/1000000", false, 0, 1000000);
            achievements[4] = new Achievement("Treasure Finder", "Obtain a rare item", "0/1", false, 0, 1);
            achievements[5] = new Achievement("Treasure Hunter", "Obtain every rare item", "0/4", false, 0, 4);
            achievements[6] = new Achievement("Fat stash", "Have a full inventory", "0/25", false, 0, 25);
            achievements[7] = new Achievement("Jewel heist", "Obtain a ruby, an emerald and a diamond", "0/3", false, 0, 3);
            achievements[8] = new Achievement("Millionaire", "Have 1,000,000 coins in your inventory", "0/1000000", false, 0, 1000000);
            achievements[9] = new Achievement("Capitalist", "Earn a total of 10,000,000 coins", "0/10000000", false, 0, 10000000);
            achievements[10] = new Achievement("Mr. Worldwide", "Visit every city", "0/10", false, 0, 10);
            achievements[11] = new Achievement("On the boat again", "Travel over the sea 10 times", "0/10", false, 0, 10);
        }

        public static void LoadAchievements()
        {
            boughtCarrots = achievements[0].currentAmount;
            currentCoins = achievements[1].currentAmount;
            travelCounter = achievements[2].currentAmount;
            spentMoney = achievements[3].currentAmount;
            rareItems = achievements[4].currentAmount;
            rareItems = achievements[5].currentAmount;
            inventorySpaces = achievements[6].currentAmount;
            jewels = achievements[7].currentAmount;
            currentCoins = achievements[8].currentAmount;
            totalCoinsEarned = achievements[9].currentAmount;
            citiesVisited = achievements[10].currentAmount;
            boatTravelCounter = achievements[11].currentAmount;
        }


            public static void ChangeAchievements()
        {
            foreach (Achievement achievement in achievements)
            {
                if(achievement.currentAmount >= achievement.maxAmount)
                {
                    achievement.complete = true;
                }
            }
        }


        public static void Update() // Needs to be shorter
        {
            
            ChangeAchievements();
            UpdateAchievements();
            inventorySpaces = Player.Inventory.ItemList.Count;
            foreach (Item item in Player.Inventory.ItemList)
            {
                if (item.Name == "Diamond")
                {
                    hasDiamond = true;
                }
                else if (item.Name == "Emerald")
                {
                    hasEmerald = true;
                }
                else if (item.Name == "Ruby")
                {
                    hasRuby = true;
                }
                else if (item.Name == "Dragon Scale")
                {
                    hasDragonscale = true;
                }
                else if (item.Name == "Gryphon Meat")
                {
                    hasGryphonMeat = true;
                }
                else if (item.Name == "Whale Meat")
                {
                    hasWhaleMeat = true;
                }
            }

            if(hasDiamond || hasDragonscale ||  hasGryphonMeat || hasWhaleMeat)
            {
                rareItem = 1;
            }

            if (hasDiamond && hasRuby && hasEmerald)
            {
                jewels = 3;
            }
            else if((hasDiamond && hasEmerald) || (hasDiamond && hasRuby) || (hasEmerald && hasRuby))
            {
                jewels = 2;
            }
            else if(hasDiamond || hasRuby || hasDiamond)
            {
                jewels = 1;
            }

            currentCoins = Player.Inventory.Money;

        }

        public static void UpdateAchievements()
        {
            achievements[0].currentAmount = boughtCarrots;
            achievements[1].currentAmount = currentCoins;
            achievements[2].currentAmount = travelCounter;
            achievements[3].currentAmount = spentMoney;
            achievements[4].currentAmount = rareItem;
            achievements[5].currentAmount = rareItems;
            achievements[6].currentAmount = inventorySpaces;
            achievements[7].currentAmount = jewels;
            achievements[8].currentAmount = currentCoins;
            achievements[9].currentAmount = totalCoinsEarned;
            achievements[10].currentAmount = citiesVisited;
            achievements[11].currentAmount = boatTravelCounter;

            foreach (Achievement achievement in achievements)
            {
                if (achievement.complete)
                {
                    achievement.progress = achievement.maxAmount.ToString() + "/" + achievement.maxAmount.ToString();
                }
                else if(!achievement.complete)
                {
                    achievement.progress = achievement.currentAmount + "/" + achievement.maxAmount;
                }
            }
        }
       


    }
}
