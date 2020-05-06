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
        static string ver = "1.2.1";
        static public bool GenerateSave(Inventory inventory, string location, TravelMenu travelMenu, string gameState)
        {
            string path = Path.Combine("./Saves/", "Save-" + DateTime.Now.ToString() + ".ptmsave");
            StreamWriter streamWriter = new StreamWriter(ToSafeFileName(path), false);

            streamWriter.WriteLine(ver);
            streamWriter.WriteLine(gameState);
            streamWriter.WriteLine(Calendar.TotalDays);
            streamWriter.WriteLine(location);

            int[] temp = Player.SkillLevels;
            for (int i = 0; i < 3; i++)
            {
                streamWriter.WriteLine(temp[i].ToString());
            }

            streamWriter.WriteLine(inventory.Money);
            streamWriter.WriteLine(inventory.ItemList.Count);

            foreach (Item tempItem in inventory.ItemList)
            {
                streamWriter.WriteLine(tempItem.ToString());
            }
            if (Player.Location != travelMenu.Destination)
            {
                streamWriter.WriteLine('1');
                streamWriter.WriteLine(travelMenu.TurnsLeft);
                streamWriter.WriteLine(travelMenu.Destination);
            }
            else
            {
                streamWriter.WriteLine('0');
            }
            streamWriter.Close();

            return true;
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
            Inventory tempInv = new Inventory(0);

            StreamReader streamReader;
            try
            {
                streamReader = new StreamReader(openFileDialog.FileName);
            }
            catch
            {
                return null;
            }
            

            if (streamReader.ReadLine() == ver)
            {
                string[] data = new string[3];
                data[0] = streamReader.ReadLine();
                Calendar.TotalDays = int.Parse(streamReader.ReadLine());
                Player.Location = streamReader.ReadLine();

                int[] tempLevels = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    tempLevels[i] = int.Parse(streamReader.ReadLine());
                }
                Player.SkillLevels = tempLevels;

                tempInv.Money = int.Parse(streamReader.ReadLine());
                int counter = int.Parse(streamReader.ReadLine());

                for (int i = 0; i < counter; i++)
                {
                    string tempData = streamReader.ReadLine();
                    string[] data2 = tempData.Split(';');
                    Item newItem = ItemCreator.CreateItem(int.Parse(data2[0]), int.Parse(data2[1]));
                    tempInv.AddItem(newItem);
                }

                int check = int.Parse(streamReader.ReadLine());
                Player.Inventory = tempInv;

                if (check == 1)
                {
                    
                    data[1] = streamReader.ReadLine();
                    data[2] = streamReader.ReadLine();
                    streamReader.Close();
                    return data;
                }

                streamReader.Close();
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

        //skamlöst stulen från the interwebs 
        //https://stackoverflow.com/questions/7348768/the-given-paths-format-is-not-supported
        public static string ToSafeFileName(this string s)
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
    }
}
