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
            Console.Title = "Konsola żarcia komet";

            Console.SetWindowSize(46, 46);
            Console.SetWindowPosition(0, 0);


            RPG myGame;
            myGame = new RPG();
            myGame.startNewGame = true;


            bool iterate = true;
            while (iterate)
            {
                bool kupa = myGame.startNewGame;
                if (kupa == true)
                {
                    myGame = null;
                    myGame = new RPG();
                    myGame.Run();
                }
                else
                {
                    iterate = false;
                    //myGame.OnClose();
                    //myGame = null;
                }
            }
        }
    }
}
