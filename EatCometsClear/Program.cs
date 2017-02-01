using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EatCometsClear
{
    class Program
    {
        static void Main(string[] args)
        {
            string x = System.Configuration.ConfigurationManager.AppSettings["screenX"];
            string y = System.Configuration.ConfigurationManager.AppSettings["screenY"];


            Console.Title = "Konsola żarcia komet";


            RPG myGame;
            myGame = new RPG(0,0);
            myGame.startNewGame = true;


            bool iterate = true;
            while (iterate)
            {
                bool kupa = myGame.startNewGame;
                if (kupa == true)
                {
                    myGame.CloseWindow();
                    myGame = null;
                    x = System.Configuration.ConfigurationManager.AppSettings["screenX"];
                    y = System.Configuration.ConfigurationManager.AppSettings["screenY"];
                    myGame = new RPG(Convert.ToUInt32(x), Convert.ToUInt32(y));
                    myGame.Run();
                }
                else
                {
                    iterate = false;
                }
            }
        }
    }
}
