﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace EatCometsClear
{
    class Ball :Physical_object
    {
        public CircleShape kolo;
        private int x, y;
        Random rnd;

        public Ball(int x1, int y1, int seeder)
        {
            int Seed = (int)DateTime.Now.Ticks;
            rnd = new Random(Seed+seeder);
            this.gravityStrength = 1000;
            this.enableGravity = true;

            this.mass = 1;
            this.x = x1;
            this.y = y1;

            kolo = new CircleShape();
            kolo.FillColor = new Color(100, 100, 100);
            kolo.Radius = 6;

            this.Remake();

            //Console.WriteLine("x = " + x + "  y = " + y );


        }

        public void Remake()
        {
            int x1, y1;
            x1 = rnd.Next( (int)(x*0.01), (int)(x*0.99) );
            y1 = rnd.Next( (int)(y*0.01), (int)(y*0.99) );

            this.position = new Vector2f(x1, y1);
            this.ReDraw();
        }

        public void ReDraw()
        {
            this.kolo.Position = new Vector2f( this.position.X - 6, this.position.Y - 6);
        }
    }
}
