using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Active
{
    static class EndCredits
    {
        /*
         How to add more titles and texts.
         1. Add Title to enum Stage
         2. Create Initializing method with "Ini" + title
         3. Add it appropiately in Initialize() at the correct index
             
             */
        static List<RollText> texts;
        static bool onGoing;
        enum Stage
        {
            Start,
            Developers,
            Testers,
            Music,
            SpecialThanks,
            End
        }
        static Stage currentStage;

        static public void Initialize()
        {
            onGoing = false;
            currentStage = Stage.Start;
            texts = new List<RollText>();
            for (int i = 0; i < (int)Stage.End+1; i++)
            {
                texts.Add(new RollText(3));
            }
            IniStart();
            IniDevelopers();
            IniTesters();
            IniMusic();
            IniSpecialThanks();
            IniEnd();
        }

        static public void Start()
        {
            onGoing = true;
        }

        static void Reset()
        {
            onGoing = false;
            currentStage = Stage.Start;
            foreach (var item in texts)
            {
                item.Reset();
            }
        }

        static public void Update(double timePassed)
        {
            if (onGoing)
            {
                if (texts[(int)currentStage].MoveTextVertical(timePassed) == true)
                {
                    if (currentStage != Stage.End)
                    {
                        ++currentStage;
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }

        static public void Draw(SpriteBatch sb)
        {
            if (onGoing)
            {
                texts[(int)currentStage].Draw(sb);
            }
        }

        static void IniStart()
        {
            // Start Stage
            texts[(int)Stage.Start].AddText("End Credits");
            texts[(int)Stage.Start].AddText("A big applause for");
        }

        static void IniDevelopers()
        {
            // Developers Stage
            texts[(int)Stage.Developers].AddText("Developers");
            texts[(int)Stage.Developers].AddText("Artist and Project Leader Frida Stenfeldt");
            texts[(int)Stage.Developers].AddText("My Cronqvist");
            texts[(int)Stage.Developers].AddText("Simon Wennolf");
            texts[(int)Stage.Developers].AddText("Jacob Larsson");
            texts[(int)Stage.Developers].AddText("Kevin Tang");
        }
        static void IniMusic()
        {
            texts[(int)Stage.Music].AddText("Music");
            texts[(int)Stage.Music].AddText("Keman Nguyen");
        }

        static void IniTesters()
        {
            texts[(int)Stage.Testers].AddText("Testers");
            texts[(int)Stage.Testers].AddText("Grupp 21");
            texts[(int)Stage.Testers].AddText("Kevin Tangs kusin");
        }

        static void IniSpecialThanks()
        {
            texts[(int)Stage.SpecialThanks].AddText("Special Thanks");
        }

        static void IniEnd()
        {
            texts[(int)Stage.End].AddText("The End");
        }

       


    }
}
