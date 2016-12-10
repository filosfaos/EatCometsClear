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
            RPG myGame;
            myGame = new RPG();
            myGame.startNewGame = true;


            bool iterate = true;
            while (iterate)
            {
                bool kupa = myGame.startNewGame;
                if (kupa == true)
                {
                    myGame.CloseWindow();
                    myGame = null;
                    myGame = new RPG();
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
