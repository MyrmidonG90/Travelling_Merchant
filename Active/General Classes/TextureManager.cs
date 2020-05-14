﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Active
{
    static class TextureManager
    {
        //Font
        static public SpriteFont font;
        static public SpriteFont fontInventory;
        static public SpriteFont fontHeader;
        static public SpriteFont fontButton;

        //Backgrounds
        static public Texture2D texBGTravelScreen;
        static public Texture2D texCarrotTown;
        static public Texture2D texTowerTown;
        static public Texture2D texDefaultTown;
        static public Texture2D texMap;


        // Category
        static public Texture2D texCatFood;
        static public Texture2D texCatGear;
        static public Texture2D texCatMagic;
        static public Texture2D texCatManufactured;
        static public Texture2D texCatMetals;
        static public Texture2D texCatRawMaterials;
        static public Texture2D texCatTextiles;        
        static public Texture2D texCatValuables;

        //Icons
        static public Texture2D texIconTrashCan;
        static public Texture2D texIconCommon;
        static public Texture2D texIconUncommon;
        static public Texture2D texIconRare;
        static public Texture2D texIconIntimidation;
        static public Texture2D texIconPersuasion;
        static public Texture2D texIconWisdom;
        static public Texture2D texIconMerchantCity;

        // GUI
        static public Texture2D texButtonGo;
        static public Texture2D texTab;
        static public Texture2D texBackArrow;
        static public Texture2D texOptions;
        static public Texture2D texButton;

        // HUD
        static public Texture2D texRankUpIntimidation;
        static public Texture2D texRankUpWisdom;
        static public Texture2D texRankUpPersuasion;
        static public Texture2D texArrowDown;
        static public Texture2D texWhite;
        static public Texture2D texBox;
        static public Texture2D texSelect;
        static public Texture2D texInvMenu;
        static public Texture2D texSkillMenu;
        static public Texture2D texInvTab;
        static public Texture2D texSkillTab;
        static public Texture2D texTradeMenu;
        static public Texture2D texMoneyArrowLeft;
        static public Texture2D texMoneyArrowRight;

        //Items
        static public Texture2D texArmourFine, texArmourMagic, texArmourNormal, texCarrot, texCotton, texDragonScale, texFur, texGoldIngot, texGoldOre, texGryphonMeat, texIronIngot, texIronOre, texDiamond, texEmerald, texRuby, texLumber, texPotato, texPotion, texSilk, texSpices, texStone, texWeaponFine, texWeaponMagic, texWeaponNormal, texWhaleMeat;

        static public List<Texture2D> texItems;
        static public List<Texture2D> texCities;

        static public void LoadContent(ContentManager content)
        {
            LoadFonts(content);
            LoadCategories(content);
            LoadGUI(content);
            LoadHUD(content);
            LoadIcons(content);
            LoadItems(content);
            LoadBackGrounds(content);

            LoadLists();
        }

        static void LoadHUD(ContentManager content)
        {
            texRankUpIntimidation = content.Load<Texture2D>("rank_up_intimidation");
            texRankUpPersuasion = content.Load<Texture2D>("rank_up_persuasion");
            texRankUpWisdom = content.Load<Texture2D>("rank_up_wisdom");
            texArrowDown = content.Load<Texture2D>("arrow_down");
            texMoneyArrowLeft = content.Load<Texture2D>("arrow_money_left");
            texMoneyArrowRight = content.Load<Texture2D>("arrow_money_right");
            texBox = content.Load<Texture2D>("box");
            texWhite = content.Load<Texture2D>("White");
            texSelect = content.Load<Texture2D>("select");
            texInvMenu = content.Load<Texture2D>("inventory_menu");
            texSkillMenu = content.Load<Texture2D>("skill_menu");
            texTradeMenu = content.Load<Texture2D>("trading_menu");
        }
        static void LoadGUI(ContentManager content)
        {
            texButtonGo = content.Load<Texture2D>("go_button");
            texTab = content.Load<Texture2D>("Tab");
            texBackArrow = content.Load<Texture2D>("backarrow");
            texButton = content.Load<Texture2D>("btn");
            texInvTab = content.Load<Texture2D>("inventory_tab");
            texSkillTab = content.Load<Texture2D>("skill_tab");
            texOptions = content.Load<Texture2D>("options");
        }
        static void LoadItems(ContentManager content)
        {
            //RESOURCE SPRITES

            texArmourFine = content.Load<Texture2D>("armour_fine");
            texArmourMagic = content.Load<Texture2D>("armour_magic");
            texArmourNormal = content.Load<Texture2D>("armour_normal");
            texCarrot = content.Load<Texture2D>("carrot");
            texCotton = content.Load<Texture2D>("cotton");
            texDragonScale = content.Load<Texture2D>("dragon_scale");
            texFur = content.Load<Texture2D>("fur");
            texGoldIngot = content.Load<Texture2D>("gold_ingot");
            texGoldOre = content.Load<Texture2D>("gold_ore");
            texGryphonMeat = content.Load<Texture2D>("gryphon_meat");
            texIronIngot = content.Load<Texture2D>("iron_ingot");
            texIronOre = content.Load<Texture2D>("iron_ore");
            texDiamond = content.Load<Texture2D>("jewel_diamond");
            texEmerald = content.Load<Texture2D>("jewel_emerald");
            texRuby = content.Load<Texture2D>("jewel_ruby");
            texLumber = content.Load<Texture2D>("lumber");
            texPotato = content.Load<Texture2D>("potato");
            texPotion = content.Load<Texture2D>("potion");
            texSilk = content.Load<Texture2D>("silk");
            texSpices = content.Load<Texture2D>("spices");
            texStone = content.Load<Texture2D>("stone");
            texWeaponFine = content.Load<Texture2D>("weapon_fine");
            texWeaponMagic = content.Load<Texture2D>("weapon_magic");
            texWeaponNormal = content.Load<Texture2D>("weapon_normal");
            texWhaleMeat = content.Load<Texture2D>("whale_meat");
        }


        static void LoadBackGrounds(ContentManager content)
        {
            texBGTravelScreen = content.Load<Texture2D>("travel_screen");
            texCarrotTown = content.Load<Texture2D>("Carrot Town");
            texTowerTown = content.Load<Texture2D>("Tower Town");
            texDefaultTown = content.Load<Texture2D>("Default");
            texMap = content.Load<Texture2D>("merchant_map");
        }
        static void LoadFonts(ContentManager content)
        {
            font = content.Load<SpriteFont>("File");
            fontButton = content.Load<SpriteFont>("fontButton");
            fontInventory = content.Load<SpriteFont>("fontInventory");
            fontHeader = content.Load<SpriteFont>("fontHeader");
        }
        static void LoadIcons(ContentManager content)
        {
            texIconCommon = content.Load<Texture2D>("common_icon");
            texIconIntimidation = content.Load<Texture2D>("skill_icon_intimidation");
            texIconPersuasion = content.Load<Texture2D>("skill_icon_persuasion");
            texIconRare = content.Load<Texture2D>("rare_icon");
            texIconTrashCan = content.Load<Texture2D>("trash_can");
            texIconUncommon = content.Load<Texture2D>("uncommon_icon");
            texIconWisdom = content.Load<Texture2D>("skill_icon_wisdom");
            texIconMerchantCity = content.Load<Texture2D>("merchant_city_icon");
        }
        static void LoadCategories(ContentManager content)
        {
            texCatFood = content.Load<Texture2D>("category_food");
            texCatGear = content.Load<Texture2D>("category_gear");
            texCatMagic = content.Load<Texture2D>("category_magic");
            texCatManufactured = content.Load<Texture2D>("category_manufactured");
            texCatMetals = content.Load<Texture2D>("category_metals");
            texCatRawMaterials = content.Load<Texture2D>("category_raw_materials");
            texCatTextiles = content.Load<Texture2D>("category_textiles");
            texCatValuables = content.Load<Texture2D>("category_valuables");
        }

        static private void LoadLists()
        {
            texItems = new List<Texture2D>();
            texItems.Add(texCarrot);
            texItems.Add(texPotato);
            texItems.Add(texIronIngot);
            texItems.Add(texIronOre);
            texItems.Add(texGoldIngot);
            texItems.Add(texGoldOre);
            texItems.Add(texRuby);
            texItems.Add(texEmerald);
            texItems.Add(texDiamond);
            texItems.Add(texFur);
            texItems.Add(texSilk);
            texItems.Add(texCotton);
            texItems.Add(texLumber);
            texItems.Add(texStone);
            texItems.Add(texWeaponNormal);
            texItems.Add(texArmourNormal);
            texItems.Add(texWeaponFine);
            texItems.Add(texArmourFine);
            texItems.Add(texWeaponMagic);
            texItems.Add(texArmourMagic);
            texItems.Add(texWhaleMeat);
            texItems.Add(texGryphonMeat);
            texItems.Add(texDragonScale);
            texItems.Add(texSpices);
            texItems.Add(texPotion);

            texCities = new List<Texture2D>();
            texCities.Add(texCarrotTown);
            texCities.Add(texTowerTown);
        }
    }
}
