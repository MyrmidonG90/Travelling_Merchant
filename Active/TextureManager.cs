using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Active
{
    static class TextureManager
    {

        static public SpriteFont font;
        static public SpriteFont fontInventory;
        static public SpriteFont fontHeader;
        static public SpriteFont fontButton;

        static public Texture2D texMap;
        static public Texture2D texWhite;
        static public Texture2D texBox;
        static public Texture2D texSelect;
        static public Texture2D texTab;
        static public Texture2D texBackArrow;
        static public Texture2D texOptions;
        static public Texture2D texButton;
        static public Texture2D texCarrotTown;
        static public Texture2D texTowerTown;
        static public Texture2D texDefaultTown;
        static public Texture2D texInvMenu;
        static public Texture2D texSkillMenu;
        static public Texture2D texInvTab;
        static public Texture2D texSkillTab;

        static public Texture2D texArmourFine, texArmourMagic, texArmourNormal, texCarrot, texCotton, texDragonScale, texFur, texGoldIngot, texGoldOre, texGryphonMeat, texIronIngot, texIronOre, texDiamond, texEmerald, texRuby, texLumber, texPotato, texPotion, texSilk, texSpices, texStone, texWeaponFine, texWeaponMagic, texWeaponNormal, texWhaleMeat;
        static public List<Texture2D> texItems;
        static public List<Texture2D> texCities;

        static public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("File");
            fontButton = content.Load<SpriteFont>("fontButton");
            fontInventory = content.Load<SpriteFont>("fontInventory");
            fontHeader = content.Load<SpriteFont>("fontHeader");

            texBox = content.Load<Texture2D>("box");
            texMap = content.Load<Texture2D>("merchant_map_with_nothing");
            texWhite = content.Load<Texture2D>("White");
            texSelect = content.Load<Texture2D>("select");
            texTab = content.Load<Texture2D>("Tab");
            texBackArrow = content.Load<Texture2D>("backarrow");
            texOptions = content.Load<Texture2D>("options");
            texButton = content.Load<Texture2D>("btn");
            texInvMenu = content.Load<Texture2D>("inventory_menu");
            texSkillMenu = content.Load<Texture2D>("skill_menu");
            texInvTab = content.Load<Texture2D>("inventory_tab");
            texSkillTab = content.Load<Texture2D>("skill_tab");

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

            //city backgrounds
            texCarrotTown = content.Load<Texture2D>("Carrot Town");
            texTowerTown = content.Load<Texture2D>("Tower Town");
            texDefaultTown = content.Load<Texture2D>("Default");

            LoadLists();
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
