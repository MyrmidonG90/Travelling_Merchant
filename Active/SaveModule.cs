using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Active
{
    static class SaveModule
    {
        //==================================================================================
        //OM DU ÄNDRAR HUR SPELET SPARAR SÅ ***MÅSTE*** DU ÄNDRA VÄRDET I ver
        //==================================================================================
        //OM DU ÄNDRAR HUR SPELET SPARAR SÅ ***MÅSTE*** DU ÄNDRA VÄRDET I ver
        //==================================================================================
        //OM DU ÄNDRAR HUR SPELET SPARAR SÅ ***MÅSTE*** DU ÄNDRA VÄRDET I ver
        //==================================================================================
        static string ver = "1.5.1";
        static public bool GenerateSave(Inventory inventory, string location, string gameState)
        {
            string path = Path.Combine("./Saves/", "Save-" + DateTime.Now.ToString() + ".ptmsave");
            StreamWriter streamWriter = new StreamWriter(ToSafeFileName(path), false);

            streamWriter.WriteLine(ver);
            SaveAchievements(streamWriter);          
            streamWriter.WriteLine(gameState);
            streamWriter.WriteLine(Calendar.TotalDays);
            streamWriter.WriteLine(location);

            foreach (bool tempVisit in Player.VisitedCities)
            {
                if (tempVisit)
                {
                    streamWriter.WriteLine("true");
                }
                else
                {
                    streamWriter.WriteLine("false");
                }
            }


            int[] temp = Player.SkillLevels;
            for (int i = 0; i < 3; i++)
            {
                streamWriter.WriteLine(temp[i].ToString());
            }
            streamWriter.WriteLine(CharCreationMenu.ConfirmedAvatar);

            SaveInventory(streamWriter, inventory);

            SaveIfTravelling(streamWriter);
            
            streamWriter.Close();
            GlossaryManager.SaveGlossaries();
            return true;
        }

        static void SaveIfTravelling(StreamWriter streamWriter)
        {
            if (Player.Location != TravelMenu.Destination)
            {
                streamWriter.WriteLine('1');
                streamWriter.WriteLine(TravelMenu.TurnsLeft);
                streamWriter.WriteLine(TravelMenu.Destination);
            }
            else
            {
                streamWriter.WriteLine('0');
            }
        }

        static void SaveInventory(StreamWriter streamWriter, Inventory inventory)
        {
            streamWriter.WriteLine(inventory.Money);
            streamWriter.WriteLine(inventory.ItemList.Count);

            foreach (Item tempItem in inventory.ItemList)
            {
                streamWriter.WriteLine(tempItem.ToString());
            }
        }

        static void SaveAchievements(StreamWriter streamWriter)
        {
            foreach (Achievement achievement in AchievementManager.achievements)
            {
                if (achievement.complete)
                {
                    streamWriter.WriteLine('1');
                }
                else
                {
                    streamWriter.WriteLine('0');
                }
                streamWriter.WriteLine(achievement.currentAmount.ToString());
            }
        }

        //==================================================================================
        //OM DU ÄNDRAR HUR SPELET SPARAR SÅ ***MÅSTE*** DU ÄNDRA VÄRDET I ver
        //==================================================================================
        //OM DU ÄNDRAR HUR SPELET SPARAR SÅ ***MÅSTE*** DU ÄNDRA VÄRDET I ver
        //==================================================================================
        //OM DU ÄNDRAR HUR SPELET SPARAR SÅ ***MÅSTE*** DU ÄNDRA VÄRDET I ver
        //==================================================================================
        static public string[] LoadSave()
        {
            Inventory tempInv = new Inventory(0);
            StreamReader streamReader = InitLoadSave();

            if (streamReader == null)
            {
                return null;
            }

            if (streamReader.ReadLine() == ver)
            {
                LoadAchievements(streamReader);
                string[] data = new string[3];
                data[0] = streamReader.ReadLine();
                Calendar.TotalDays = int.Parse(streamReader.ReadLine());

                ReadPlayerData(streamReader, tempInv);
                TravelMenu.Destination = Player.Location;

                int check = int.Parse(streamReader.ReadLine());
                if (check == 1)
                {

                    data[1] = streamReader.ReadLine();
                    data[2] = streamReader.ReadLine();
                    streamReader.Close();
                    return data;
                }

                streamReader.Close();
                AchievementManager.LoadAchievements();
                return data;             
            }
            return null;
        }
        //==================================================================================
        //OM DU ÄNDRAR HUR SPELET SPARAR SÅ ***MÅSTE*** DU ÄNDRA VÄRDET I ver
        //==================================================================================
        //OM DU ÄNDRAR HUR SPELET SPARAR SÅ ***MÅSTE*** DU ÄNDRA VÄRDET I ver
        //==================================================================================
        //OM DU ÄNDRAR HUR SPELET SPARAR SÅ ***MÅSTE*** DU ÄNDRA VÄRDET I ver
        //==================================================================================

        static void LoadAchievements(StreamReader streamReader)
        {
            foreach (Achievement achievement in AchievementManager.achievements)
            {
                string temp = streamReader.ReadLine();
                if (temp == "1")
                {
                    achievement.complete = true;
                }
                else if (temp == "0")
                {
                    achievement.complete = false;
                }
                achievement.currentAmount = int.Parse(streamReader.ReadLine());
            }
        }

        //skamlöst stulen från the interwebs 
        //https://stackoverflow.com/questions/7348768/the-given-paths-format-is-not-supported
        static private string ToSafeFileName(this string s)
        {
            return s
                .Replace("\\", "")
                //.Replace("/", "") // ta bort på egen risk
                .Replace("\"", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
        }

        static private StreamReader InitLoadSave()
        {
            //hot steaming mess av stack overflow
            string path = "Saves\\";
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), path);
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Project Travelling Merchant savefiles (*.ptmsave)|*.ptmsave|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (Directory.Exists(path))
            {
                openFileDialog.InitialDirectory = path;
            }
            else
            {
                openFileDialog.InitialDirectory = @"C:\";
            }
            //hot steaming mess slutar här

            openFileDialog.ShowDialog();
            StreamReader streamReader;

            try
            {
                streamReader = new StreamReader(openFileDialog.FileName);
            }
            catch
            {
                return null;
            }
            return streamReader;
        }

        static private void ReadPlayerData(StreamReader streamReader, Inventory tempInv)
        {
            Player.Location = streamReader.ReadLine();

            //ÄNDRA SIFFRAN I FOR LOOPEN HÄR OCKSÅ OM DU HAR PILLAT MED ANTAL STÄDER
            for (int i = 0; i < 10; i++)
            {
                string temp = streamReader.ReadLine();
                if (temp == "true")
                {
                    Player.VisitedCities[i] = true;
                }
                else if (temp == "false")
                {
                    Player.VisitedCities[i] = false;
                }
            }

            int[] tempLevels = new int[3];
            for (int i = 0; i < 3; i++)
            {
                tempLevels[i] = int.Parse(streamReader.ReadLine());
            }
            Player.SkillLevels = tempLevels;

            CharCreationMenu.ConfirmedAvatar = int.Parse(streamReader.ReadLine());
            tempInv.Money = int.Parse(streamReader.ReadLine());
            int counter = int.Parse(streamReader.ReadLine());

            for (int i = 0; i < counter; i++)
            {
                string tempData = streamReader.ReadLine();
                string[] data2 = tempData.Split(';');
                Item newItem = ItemCreator.CreateItem(int.Parse(data2[0]), int.Parse(data2[1]));
                tempInv.AddItem(newItem);
            }
            Player.Inventory = tempInv;
        }
    }
}
