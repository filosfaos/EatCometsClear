using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace EatCometsClear
{
    class Ball
    {
        public Vector2f position;
        public CircleShape kolo;

        public Ball(int x, int y)
        {
            Random rnd = new Random();
            x = rnd.Next(10, x - 10);
            y = rnd.Next(10, y - 10);

            position = new Vector2f(x, y);
            //Console.WriteLine("x = " + x + "  y = " + y );
            kolo = new CircleShape();
            kolo.FillColor = new Color(100, 100, 100);
            kolo.Position = position;
            kolo.Radius = 6;

        }
    }
}
