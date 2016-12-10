using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;


namespace EatCometsClear
{
    class Hero : ICloneable
    {
        public Vector2f position;
        public CircleShape kolo;
        public CircleShape obwodka;
        public satelite[] satelite;
        private RenderWindow okienko;
        public bool enablemovement;
        private int numberofballs;
        private int numberofsatelites;
        private int status;

        int sterowanie;
        const int maxballs = 20000;

        public Hero(RenderWindow okienko, float x, float y, Color color, int screenX, int screenY, bool eneblemovementt, int sterowanie)
        {
            this.sterowanie = sterowanie;
            this.status = 10;
            numberofballs = 0;
            numberofsatelites = 0;

            this.enablemovement = eneblemovementt;
            this.okienko = okienko;
            position = new Vector2f(x, y);
            obwodka = new CircleShape();
            obwodka.FillColor = new Color(229, 83, 0);
            obwodka.Position = position;
            obwodka.Radius = 6;

            kolo = new CircleShape();
            kolo.FillColor = color;
            kolo.Position = position;
            kolo.Radius = 4;


            this.satelite = new satelite[1000];
            this.NewBall(0, 1);


            this.Go('x', 0, screenX, screenY);
        }

        public void Draw()
        {

            okienko.Draw(this.obwodka);
            okienko.Draw(this.kolo);

            for (int i = 0; i <= this.numberofsatelites; i++)
            {
                if (i != 0)
                {
                    okienko.Draw(this.satelite[i].obwodka);
                    okienko.Draw(this.satelite[i].kolo);

                }
            }
        }

        public bool Near(Vector2f position, float poprawka, uint howmanyplanets, int bonusRange)
        {
            if (howmanyplanets == 0)
                howmanyplanets = 1;

            howmanyplanets *= (uint)(bonusRange / 10) + 800;


            uint distance = (uint)Math.Pow(howmanyplanets + bonusRange, 2);
            //this is squared value ( potęgowana wartość )


            uint x = (uint)Math.Pow(Math.Abs(this.position.X - position.X - poprawka), 2);
            uint y = (uint)Math.Pow(Math.Abs(this.position.Y - position.Y - poprawka), 2);

            if ((x + y) <= distance)
                return true;

            return false;
        }

        public void Go(Char dimension, int step, int screenX, int screenY)
        {
            if (enablemovement == true)
            {
                if ((dimension == 'x') || (dimension == 'X'))
                {
                    if (step > 0)
                    {

                        if (((this.position.X + step) <= (screenX - this.obwodka.Radius)))
                            this.position.X += step;

                    }
                    else
                    {
                        if ((this.position.X + step) >= this.obwodka.Radius)
                            this.position.X += step;
                    }
                }

                if ((dimension == 'y') || (dimension == 'Y'))
                {
                    if (step > 0)
                    {

                        if (((this.position.Y + step) <= (screenY - this.obwodka.Radius)))
                            this.position.Y += step;

                    }
                    else
                    {
                        if ((this.position.Y + step) >= this.obwodka.Radius)
                            this.position.Y += step;
                    }
                }

                Vector2f newposition = new Vector2f(this.position.X - this.obwodka.Radius, this.position.Y - this.obwodka.Radius);
                this.obwodka.Position = newposition;
                this.kolo.Position = new Vector2f(this.position.X - this.kolo.Radius, this.position.Y - this.kolo.Radius);
            }
        }

        public void NewBall(int i, int newspeed)
        {
            satelite[i] = new satelite(i, this.position.X, this.position.Y, newspeed);

        }

        public void ChangeStatus(int howmuch)
        {
            if (howmuch == 10)
            {
                this.kolo.Radius += 5;
                this.obwodka.Radius += 5;
                Console.WriteLine("Super słońce !");
            }
            else if (howmuch == 25)
            {
                this.kolo.Radius += 5;
                this.obwodka.Radius += 5;
                Console.WriteLine("Super słońce !");

            }
            else if (howmuch == 50)
            {
                this.kolo.Radius += 10;
                this.obwodka.Radius += 10;

                this.kolo.FillColor = new Color(128, 0, 128);
                this.obwodka.FillColor = new Color(255, 0, 255);

                Console.WriteLine("Neutronowy olbrzym !");

            }
            else if (howmuch == 100)
            {
                this.kolo.Radius = 3;
                this.obwodka.Radius = 4;
                this.kolo.FillColor = new Color(128, 127, 127);
                this.obwodka.FillColor = new Color(Color.White);
                Console.WriteLine("Biały niewypał !");
            }
            else if (howmuch == 150)
            {

                this.kolo.Radius = 150;
                this.obwodka.Radius = 151;
                this.kolo.FillColor = new Color(200, 200, 255);
                this.obwodka.FillColor = new Color(Color.White);
                Console.WriteLine("Supernova !!!!");
            }
            else if (howmuch == 300)
            {
                this.obwodka.Radius = 20;
                this.kolo.Radius = 19;


                this.kolo.FillColor = new Color(Color.Black);
                this.obwodka.FillColor = new Color(50, 50, 50);

                Console.WriteLine("Czarna dziura !");
            }


            this.Go('x', 0, 800, 600);
        }

        public int Tick(bool movement, int numberofframe, Ball ball, int difficulty)
        {


            for (int i = 0; i <= this.numberofsatelites; i++)
            {
                this.satelite[i].BallLocation(this.numberofsatelites, this.position.X, this.position.Y, i, this.kolo.Radius);
            }


            if (status == 200)
            {
                this.kolo.Radius = new System.Random().Next(20, 60);
                this.obwodka.Radius = this.kolo.Radius + 2;
                this.Go('x', 0, 800, 600);
            }

            if (status == 300)
            {
                int magic = numberofballs * 8;
                magic /= 100;
                this.kolo.Radius = new System.Random().Next(20, magic);
                this.obwodka.Radius = this.kolo.Radius + 2;
                this.Go('x', 0, (int)okienko.Size.X, (int)okienko.Size.Y);

            }





            if (movement)
            {
                int step = (int)okienko.Size.X / 200;
                if (sterowanie == 3)
                {

                    this.Go('x', Mouse.GetPosition(okienko).X - (int)this.position.X, (int)okienko.Size.X, (int)okienko.Size.Y);
                    this.Go('y', Mouse.GetPosition(okienko).Y - (int)this.position.Y, (int)okienko.Size.X, (int)okienko.Size.Y);
                }
                if (sterowanie == 2)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
                        this.Go('x', step, (int)okienko.Size.X, (int)okienko.Size.Y);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
                        this.Go('x', -step, (int)okienko.Size.X, (int)okienko.Size.Y);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
                        this.Go('y', -step, (int)okienko.Size.X, (int)okienko.Size.Y);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
                        this.Go('y', step, (int)okienko.Size.X, (int)okienko.Size.Y);
                }
                else if (sterowanie == 1)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                        this.Go('x', step, (int)okienko.Size.X, (int)okienko.Size.Y);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                        this.Go('x', -step, (int)okienko.Size.X, (int)okienko.Size.Y);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                        this.Go('y', -step, (int)okienko.Size.X, (int)okienko.Size.Y);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                        this.Go('y', step, (int)okienko.Size.X, (int)okienko.Size.Y);
                }
                else if (sterowanie == 0)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                        this.Go('x', step, (int)okienko.Size.X, (int)okienko.Size.Y);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                        this.Go('x', -step, (int)okienko.Size.X, (int)okienko.Size.Y);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                        this.Go('y', -step, (int)okienko.Size.X, (int)okienko.Size.Y);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                        this.Go('y', step, (int)okienko.Size.X, (int)okienko.Size.Y);
                }



                if (status < 300)
                {
                    if (this.Near(ball.position, ball.kolo.Radius, (uint)(numberofsatelites), difficulty))
                    {

                        numberofballs++;


                        if (0 == (numberofballs % 10) && (numberofballs > 0))
                        {
                            if (numberofballs < (maxballs / 10))
                            {
                                numberofsatelites++;
                                Console.WriteLine("planeta numer " + numberofsatelites);
                                this.NewBall(numberofsatelites, numberofsatelites);


                            }
                            if ((numberofballs / 10) == status)
                            {
                                this.ChangeStatus(status);
                                if (status == 10)
                                    status = 25;
                                else if (status == 25)
                                    status = 50;
                                else if (status == 50)
                                    status = 100;
                                else if (status == 100)
                                {
                                    status = 150;
                                }
                                else if (status == 150)
                                {
                                    status = 200;
                                }
                                else if (status == 200)
                                {
                                    status = 300;
                                    ball = null;
                                    numberofsatelites++;
                                    this.NewBall(numberofsatelites, numberofsatelites);
                                    Console.WriteLine("planeta numer " + numberofsatelites);
                                    //numberofsatelites--;
                                    return 3;
                                }
                            }
                        }

                        return 1;
                    }
                }
                else if (status == 300)
                {
                    numberofballs++;
                    if ((numberofballs / 10) == 300)
                    {
                        this.ChangeStatus(status);
                        status = 400;
                    }
                }
                else if (status == 400)
                {
                    if (numberofsatelites > 0)
                    {
                        if (numberofframe % 3 == 0)
                        {
                            for (int i = 0; i < numberofsatelites; i++)
                            {
                                int j = i;
                                j++;
                                this.satelite[i] = null;
                                this.satelite[i] = this.satelite[j];
                            }
                            this.satelite[numberofsatelites] = null;
                            numberofsatelites--;
                        }
                    }
                    else if (numberofsatelites == 0)
                    {
                        this.satelite[0] = null;
                        numberofsatelites--;
                        Console.WriteLine("Wsiorbałeś cały kosmos xD");
                        Console.WriteLine("Pozdro i papatki");

                        return 2;
                    }
                }
            }


            return 0;
        }

        public void Changemovement(int a)
        {
            this.sterowanie = a;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
