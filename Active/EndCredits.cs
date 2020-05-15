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
        static List<string> developers;
        static List<string> specialThanks;
        static List<string> testers;
        static List<RollText> texts;
        static double timer;
        static bool onGoing;
        enum Stage
        {
            Start,
            Developers,
            Testers,
            SpecialThanks,
            End
        }
        static Stage currentStage;

        static public void Initialize()
        {
            onGoing = false;
            currentStage = Stage.Start;
            texts = new List<RollText>();
            for (int i = 0; i < (int)Stage.End; i++)
            {
                texts.Add(new RollText(5));
            }
            IniStart();
            IniDevelopers();
            IniTesters();
            IniSpecialThanks();
            IniEnd();
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

        static void IniTesters()
        {
            texts[(int)Stage.Testers].AddText("Testers");
        }

        static void IniSpecialThanks()
        {
            texts[(int)Stage.SpecialThanks].AddText("Special Thanks");
        }

        static void IniEnd()
        {
            texts[(int)Stage.End].AddText("The End");
        }

        static public void Start()
        {
            onGoing = true;
        }

        static void Reset()
        {
            onGoing = false;
            currentStage = Stage.Start;
        }

        static public void Update(double timePassed)
        {            
            if (onGoing)
            {
                timer -= timePassed;
                if (timer < 0)
                {
                    if (texts[(int)currentStage].MoveTextVertical() == true)
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
                    timer = 100;
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
    }
}
