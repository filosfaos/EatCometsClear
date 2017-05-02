using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;


namespace EatCometsClear
{
    class Satelite
    {
        public CircleShape kolo;
        public CircleShape obwodka;
        public float speed;
        private float kat;
        private float startPosition, maxPosition;
        private int sinuser, cosinuser;

        public Satelite(int i, float x, float y, int newspeed, bool showDescription)
        {

            this.kolo = new CircleShape(); // nowy obiekt: ksztalk kola w pamieci
            Random rnd;
            rnd = new Random();
            int r = (byte)rnd.Next(0, 255);
            int g = (byte)rnd.Next(0, 255);
            int b = (byte)rnd.Next(0, 255);

            if(showDescription)
                Console.WriteLine("    RGB " + r + " " + b + " " + g);

            this.kolo.FillColor = new Color((byte)r, (byte)g, (byte)b); // parametryzujemy go. New obiekt Color nie zostnaie w pamieci - brak referencji sprawi, ze zostanie usuniety

            if (i == 0)
            {
                this.kolo.FillColor = new Color(255, 195, 77);
            }
            this.kolo.Position = new Vector2f(x + 10, y + 10); // pozycja przyjmuje jako wartosc obiekt Vector2f, czyli zlozony z dwoch floatow (single)
            this.kolo.Radius = 1;

            if (i > 5)
                this.kolo.Radius = rnd.Next(1, i / 3);
            else if (i > 10)
                this.kolo.Radius = rnd.Next(1, i / 10);
            else if (i > 20)
                this.kolo.Radius = rnd.Next(1, 20);

            this.obwodka = new CircleShape(this.kolo.Radius + 1);

            int a = 50;
            int seederrandomer = rnd.Next(0, 100);
            if (seederrandomer % 2 == 0)
                a = -a;
            r += a;
            if (r >= 255)
                r = 255;
            if (r < 0)
                r = 0;

            g += a;
            if (g >= 255)
                g = 255;
            if (g < 0)
                g = 0;

            b += a;
            if (b >= 255)
                b = 255;
            if (b < 0)
                b = 0;

            this.obwodka.FillColor = new Color((byte)r, (byte)g, (byte)b);
            if(showDescription)
                Console.WriteLine("    RGB " + r + " " + b + " " + g);
            this.obwodka.Position = new Vector2f(this.kolo.Position.X - 1, this.kolo.Position.Y - 1);


            this.speed = (float)(rnd.Next(1, newspeed + 1));
            if (this.speed > 15)
                this.speed /= 20;
            else if (this.speed > 10)
                this.speed /= 10;
            else
                this.speed /= 2;

            this.BallLocation(i, x, y, i, 0f);

            int kierunek = rnd.Next(0, 100);
            if (kierunek % 2 == 0)
                this.speed = -this.speed;

            this.speed *= 4;
            if(showDescription)
                Console.WriteLine("    prędkość " + this.speed + "'");

            this.speed /= 60;

            startPosition = rnd.Next(0, 360);
            maxPosition = startPosition + 1080;
            this.kat = startPosition;

            this.sinuser = rnd.Next(6, 10);
            this.cosinuser = rnd.Next(4, 7);

            /*
             *water effects xD
            this.kolo.FillColor = new Color(0,0, 255);
            this.obwodka.FillColor = new Color(30, 30, 255);
            */
        }


        public int BallLocation(int number, float x, float y, int distance, float objectR)
        {

            Random rnd;
            rnd = new Random();

            this.kat += this.speed;

            if (this.kat > this.maxPosition)
                this.kat = startPosition;

            distance += (int)(this.obwodka.Radius*2.5);

            //te gówniaki *whichball zmieniają czy poruszaja sie po okręgu czy elipsie
            float cos = Convert.ToSingle((cosinuser + distance) * Math.Cos((this.kat * Math.PI) / 180));
            float sin = Convert.ToSingle((sinuser + distance) * Math.Sin((this.kat * Math.PI) / 180));

            this.kolo.Position = new Vector2f(x + sin - this.kolo.Radius, y + cos - this.kolo.Radius);
            this.obwodka.Position = new Vector2f(this.kolo.Position.X - 1, this.kolo.Position.Y - 1);
            //this.satelite[i].Position = this.position;

            return distance;
        }

        internal void Add(Satelite satelite)
        {
            throw new NotImplementedException();
        }
    }
}
