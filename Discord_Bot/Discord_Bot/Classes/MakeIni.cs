using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscuro
{
   public static class MakeIni
    {
        public static void Create()
        {
            if (!File.Exists("Bot_Settings.ini"))
            {
                IniFile MyIni = new IniFile("Bot_Settings.ini");

                MyIni.Write("Bot Token", "MzY5MTM1NjgyMTE3MDQyMTg3.DMUIIA.t9PsQOCRdsaZOuWVlAdTGP7G7WQ");
                MyIni.Write("Bot Name", "Oscuro");
                MyIni.Write("Game its playing", ":3");
                MyIni.Write("Prefix", "##");

                Console.WriteLine("You did not have a ini file set up. \nA defualt one has been made with the name \"Bot_Settings.ini\" \nPlease go ahead and configure it and restart the bot.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
