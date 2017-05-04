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
    class Ball : Physical_object
    {
        public CircleShape kolo;
        private int x, y;
        Random rnd;
        public int cycle;
        bool direction;
        public int speeder;

        public Ball(int x1, int y1, int seeder)
        {
            int Seed = (int)DateTime.Now.Ticks;
            rnd = new Random(Seed + seeder);

            cycle = 0;
            this.gravityStrength = 1000;
            this.enableGravity = true;

            this.mass = 2;
            this.minimalMass = 2;
            this.x = x1;
            this.y = y1;

            kolo = new CircleShape();
            kolo.FillColor = new Color(100, 100, 100);
            kolo.Radius = 6;

            this.Remake();

            //Console.WriteLine("x = " + x + "  y = " + y );


        }

        public void Tick()
        {
            float step = 0.5f;
            if (direction)
            {
                if (cycle >= 0 && cycle < 10)
                {
                    cycle++;
                    this.position.X += step;
                    this.position.Y += step;
                }
                else if (cycle >= 10 && cycle < 20)
                {
                    cycle++;
                    this.position.Y -= step;
                }
                else if (cycle >= 20 && cycle < 30)
                {
                    cycle++;
                    this.position.X -= step;
                    this.position.Y -= step;
                }
                else if (cycle >= 30 && cycle < 40)
                {
                    cycle++;
                    this.position.Y += step;
                }
                else
                    cycle = 0;
            }
            else
            {
                if (cycle >= 0 && cycle < 10)
                {
                    cycle--;
                    this.position.X += step;
                    this.position.Y += step;
                }
                else if (cycle >= 10 && cycle < 20)
                {
                    cycle--;
                    this.position.Y -= step;
                }
                else if (cycle >= 20 && cycle < 30)
                {
                    cycle--;
                    this.position.X -= step;
                    this.position.Y -= step;
                }
                else if (cycle >= 30 && cycle < 40)
                {
                    cycle--;
                    this.position.Y += step;
                }
                else
                    cycle = 39;
            }

            this.ReDraw();
        }

        public void Remake()
        {
            speeder = rnd.Next(1, 8);
            int pomes = rnd.Next(0, 10);
            if (pomes <= 5)
                direction = false;
            else
                direction = true;

            int x1, y1;
            x1 = rnd.Next((int)(x * 0.01), (int)(x * 0.99));
            y1 = rnd.Next((int)(y * 0.01), (int)(y * 0.99));

            this.position = new Vector2f(x1, y1);
            this.ReDraw();
        }

        public void ReDraw()
        {
            this.kolo.Position = new Vector2f(this.position.X - 6, this.position.Y - 6);
        }

        public void SetXY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}