using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class GlossaryTab : TabClass
    {
        static Rectangle selectedSlot;
        static Item selectedItem;
        static List<Vector2> textInfo;
        static Rectangle[] categoryRects;
        static int indexOfSelectedSlot;
        static Button btnPrev;
        static Button btnNext;
        enum Glossary
        {
            Items,
            TravelEncounters,
            WorldEvents
        }
        static Glossary currentGlossary;

        public GlossaryTab(string tabName, string glossary)
        {
            name = tabName;
            mainBox = new Rectangle(260, 150, 1400, 880);
            GlossaryManager.Initialize(glossary);
            textInfo = new List<Vector2>();
            categoryRects = new Rectangle[3];
            categoryRects[0] = new Rectangle(1130, 760, 120, 120);
            categoryRects[1] = new Rectangle(1310, 760, 120, 120);
            categoryRects[2] = new Rectangle(1490, 760, 120, 120);
            btnNext = new Button(800,950,TextureManager.texButtonNext.Width, TextureManager.texButtonNext.Height, TextureManager.texButtonNext);
            btnPrev = new Button(300,950, TextureManager.texButtonPrev.Width, TextureManager.texButtonPrev.Height,TextureManager.texButtonPrev);
            
            indexOfSelectedSlot = -1;
            currentGlossary = (Glossary)GlossaryManager.GetGlossaryIndex(glossary);
        }

        static void Start(string glossary)
        {
            currentGlossary = (Glossary)GlossaryManager.GetGlossaryIndex(glossary);
            textInfo = new List<Vector2>();
            indexOfSelectedSlot = -1;
            selectedItem = null;
            textInfo.Add(new Vector2(1220, 200));
            textInfo.Add(new Vector2(1100, 310));
            textInfo.Add(new Vector2(1100, 660));
            GlossaryManager.InitateGlossary(glossary);
        }

        override public void Update()
        {
            if (KMReader.LeftMouseClick())
            {
                if (CheckSlotClick() != -1)
                {
                    int indexOfClicked = CheckSlotClick();
                    if (indexOfClicked != -1) // Om en slot blir klickad
                    {
                        if (indexOfClicked != indexOfSelectedSlot) // Om en slot som inte redan är iklickad blir klickad
                        {
                            indexOfSelectedSlot = indexOfClicked;
                            selectedItem = ItemCreator.CreateItem(indexOfSelectedSlot,0);
                            selectedSlot = new Rectangle(320 + 150 * (indexOfSelectedSlot % 5), 210 + 150 * (indexOfSelectedSlot / 5), 120, 120);
                        }
                    }
                }
                else
                {
                    CheckButtonClick();
                }
            }
        }

        void CheckButtonClick()
        {
            int tmp = (int)currentGlossary;
            if (btnNext.LeftClick())
            {
                ++tmp;
                Start(((Glossary)(tmp % GlossaryManager.AmountOfGlossaries)).ToString());
            }
            else if (btnPrev.LeftClick())
            {
                --tmp;
                if (tmp == -1)
                {
                    tmp = GlossaryManager.AmountOfGlossaries - 1;
                }
                Start(((Glossary)tmp).ToString());
            }
        }

        override public void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.texMenuGlossary, mainBox, Color.White);
            GlossaryManager.Draw(sb);
            if (indexOfSelectedSlot != -1)
            {
                if (currentGlossary == 0)
                {
                    sb.Draw(TextureManager.texSelect, selectedSlot, Color.White);
                    sb.DrawString(TextureManager.font48, selectedItem.Name, textInfo[0], Color.White);
                    sb.DrawString(TextureManager.font32, "Info: \n" + selectedItem.Description, textInfo[1], Color.White);
                    sb.DrawString(TextureManager.font32, "Standard Price: " + selectedItem.BasePrice.ToString() + "c", textInfo[2], Color.White);
                }
               
            }
            btnPrev.Draw(sb);
            btnNext.Draw(sb);
        }

        int CheckSlotClick()
        {
            int counter = 0;
            bool found = false;
            while (found != true && counter < GlossaryManager.Slots.Count)
            {
                if (GlossaryManager.Slots[counter].LeftClicked())
                {
                    found = true;
                }
                else
                {
                    ++counter;
                }
            }
            if (found== false)
            {
                counter = -1;
            }
            return counter;
        }
        
    }
}
