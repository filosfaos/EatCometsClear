using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

//2k16

namespace EatCometsClear
{
    class Hero : Physical_object, ICloneable, Drawable
    {
        public CircleShape kolo;
        public CircleShape obwodka;
        public CircleShape zasiegacz;
        List<Satelite> satelite;
        static System.Collections.ArrayList heromenu;
        private RenderWindow okienko;
        public bool enablemovement;
        private int numberofballs;
        private int numberofsatelites;
        private int status;
        private string type;
        public int step;
        public int additionalRange;
        public bool enableRange;
        private bool menuStatistic;
        Button kaczynskiSmiec;
        int sterowanie;
        private bool enableManipulation;
        int density;
        private bool lastGravity;
        
        public Hero(RenderWindow okienko, float x, float y, Color color, int screenX, int screenY, bool eneblemovementt, int sterowanie)
        {

            this.enablemovement = eneblemovementt;
            this.okienko = okienko;

            lastGravity = false;

            density = 1000;
            enableManipulation = false;

            kaczynskiSmiec = new Button(1, 1, 1, 1, "1", okienko, new Color(Color.Black), 1, 1);

            heromenu = new System.Collections.ArrayList();


            uint pomX = okienko.Size.X;
            uint pomY = okienko.Size.Y;

            uint buttontextsize = (uint)(pomX * 0.040);
            if (this.okienko.Size.X == 1920)
                buttontextsize = (uint)(pomY * 0.030);
            if (this.okienko.Size.X == 1280)
                buttontextsize = (uint)(pomX * 0.022);
            if (this.okienko.Size.X == 1024)
                buttontextsize = (uint)(pomX * 0.025);
            if (this.okienko.Size.X == 800)
                buttontextsize = (uint)(pomX * 0.015);

            Color buttonscolor = new Color(69, 69, 0);
            heromenu = new System.Collections.ArrayList();
            Text Gamename;
            Gamename = new Text();
            Gamename.DisplayedString = "Małe słońce";
            Gamename.Font = new Font("fonts/arial.ttf");
            Gamename.Position = new Vector2f((uint)(pomX * 0.03), (uint)(pomY * 0.03));
            Gamename.Color = new Color(138, 7, 7);
            Gamename.CharacterSize = (uint)(pomY * 0.15);

            heromenu.Add(new Caption(Gamename, 1, okienko));
            heromenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.25), (uint)(pomX * 0.15), (uint)(pomY * 0.1), "Awansuj", okienko, buttonscolor, buttontextsize, 0));
            heromenu.Add(new Button((uint)(pomX * 0.24), (uint)(pomY * 0.25), (uint)(pomX * 0.09), (uint)(pomY * 0.1), "X", okienko, new Color(200, 128, 64), buttontextsize, 1));
            heromenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", okienko, buttonscolor, buttontextsize, 2));
            heromenu.Add(new Button((uint)(pomX * 0.12), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "masa->planeta", okienko, buttonscolor, buttontextsize, 0));
            heromenu.Add(new Button((uint)(pomX * 0.30), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", okienko, buttonscolor, buttontextsize, 2));
            heromenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.57), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "?", okienko, buttonscolor, buttontextsize, 3));
            heromenu.Add(new Button((uint)(pomX * 0.12), (uint)(pomY * 0.55), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "odblokuj", okienko, buttonscolor, buttontextsize, 3));
            heromenu.Add(new Button((uint)(pomX * 0.30), (uint)(pomY * 0.57), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "X", okienko, new Color(128,0,0), buttontextsize, 4));
            heromenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "przycisk", okienko, buttonscolor, buttontextsize, 0));

            Gamename = null;

            Gamename = new Text("Menu postaci", new Font("fonts/arial.ttf"), (uint)(pomY * 0.022222));
            Gamename.Position = new Vector2f((uint)(pomX * 0.08), (uint)(pomY * 0.833333));
            Gamename.Color = new Color(Color.White);
            heromenu.Add(new Caption(Gamename, 2, okienko));


            this.menuStatistic = false;

            this.additionalRange = 10;

            this.step = (int)okienko.Size.X / 180;
            gravityStrength = 10;

            type = "small_sun";

            this.mass = 2;

            this.sterowanie = sterowanie;
            this.status = 10;
            numberofballs = 0;
            numberofsatelites = 0;

            position = new Vector2f(x, y);
            obwodka = new CircleShape();
            obwodka.FillColor = new Color(229, 83, 0);
            obwodka.Position = position;
            obwodka.Radius = 6;

            kolo = new CircleShape();
            kolo.FillColor = color;
            kolo.Position = position;
            kolo.Radius = 4;


            zasiegacz = new CircleShape();
            zasiegacz.FillColor = new Color(10,10,10);
            zasiegacz.Position = position;
            zasiegacz.Radius = 4;

            this.satelite = new List<Satelite>();
            this.NewBall(0, 1);


            this.Go('x', 0, screenX, screenY);
        }


        public void Draw()
        {
            if (!menuStatistic)
                if (enableRange)
                    okienko.Draw(this.zasiegacz);
            okienko.Draw(this.obwodka);
            okienko.Draw(this.kolo);

            for (int i = 0; i < this.satelite.Count; i++)
            {
                if (i != 0)
                {
                    okienko.Draw(this.satelite[i].obwodka);
                    okienko.Draw(this.satelite[i].kolo);

                }
            }
            if (menuStatistic)
            {
                foreach (Drawable rysownik in heromenu)
                    rysownik.Draw(null, new RenderStates());
            }
        }

        public bool Near(Vector2f position, float poprawka, uint howmanyplanets)
        {
            if (howmanyplanets == 0)
                howmanyplanets = 1;

            
            howmanyplanets *= 8;

            if(this.enableGravity)
            {
                howmanyplanets = 0;
            }

            uint distance = (uint)Math.Pow(howmanyplanets + (uint)this.obwodka.Radius + additionalRange, 2);
            uint distance2 = (uint)Math.Pow(howmanyplanets + (uint)this.obwodka.Radius + additionalRange - poprawka, 2);

            this.zasiegacz.Radius = (float)Math.Sqrt(distance2);
            //this is squared value ( potęgowana wartość )
            
            uint x = (uint)Math.Pow( this.position.X - (position.X + poprawka), 2);
            uint y = (uint)Math.Pow( this.position.Y - (position.Y + poprawka), 2);
            
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
            }

            this.CalculatePosition();
        }

        public void NewBall(int i, int newspeed)
        {
            satelite.Add(new Satelite(i, this.position.X, this.position.Y, newspeed));
            //satelite[i] = new satelite(i, this.position.X, this.position.Y, newspeed);

        }

        public void ChangeStatus(int howmuch)
        {
            if (howmuch == 10)
            {
                Console.WriteLine("Super słońce !");
                kolo.FillColor = new Color(255, 180, 60);
                obwodka.FillColor = new Color(255, 70, 0);
            }
            else if (howmuch == 25)
            {
                Console.WriteLine("Super słońce !");
                type = "medium_sun";

                var tmp = heromenu.GetEnumerator();
                while (tmp.MoveNext())
                {
                    Caption element;
                    element = new Caption();
                    if (tmp.Current.GetType() == element.GetType())
                    {
                        element = null;
                        element = (Caption)tmp.Current;

                        if (element.id == 1)
                            element.text.DisplayedString = "Średnie słońce";
                    }
                }

                kolo.FillColor = new Color(255, 155, 37);
                obwodka.FillColor = new Color(255, 53, 0);

            }
            else if (howmuch == 50)
            {
                this.kolo.FillColor = new Color(128, 0, 128);
                this.obwodka.FillColor = new Color(255, 0, 255);

                Console.WriteLine("Neutronowy olbrzym !");
                type = "neutron_star";
                var tmp = heromenu.GetEnumerator();
                while (tmp.MoveNext())
                {
                    Caption element;
                    element = new Caption();
                    if (tmp.Current.GetType() == element.GetType())
                    {
                        element = null;
                        element = (Caption)tmp.Current;

                        if (element.id == 1)
                            element.text.DisplayedString = "Neutronowy olbrzym";
                    }
                }

            }
            else if (howmuch == 100)
            {
                this.kolo.FillColor =  new Color(Color.White);
                this.obwodka.FillColor =new Color(200, 200, 200);
                Console.WriteLine("Biały niewypał !");
                type = "white_cancer";
                var tmp = heromenu.GetEnumerator();
                while (tmp.MoveNext())
                {
                    Caption element;
                    element = new Caption();
                    if (tmp.Current.GetType() == element.GetType())
                    {
                        element = null;
                        element = (Caption)tmp.Current;

                        if (element.id == 1)
                            element.text.DisplayedString = "Biały niewypał";
                    }
                }
            }
            else if (howmuch == 150)
            {
                this.kolo.FillColor = new Color(200, 200, 255);
                this.obwodka.FillColor = new Color(Color.White);
                Console.WriteLine("Supernova !!!!");
                type = "supernova";
                var tmp = heromenu.GetEnumerator();
                while (tmp.MoveNext())
                {
                    Caption element;
                    element = new Caption();
                    if (tmp.Current.GetType() == element.GetType())
                    {
                        element = null;
                        element = (Caption)tmp.Current;

                        if (element.id == 1)
                            element.text.DisplayedString = "Supernova";
                    }
                }
            }
            else if (howmuch == 300)
            {
                this.kolo.FillColor = new Color(Color.Black);
                this.obwodka.FillColor = new Color(50, 50, 50);

                Console.WriteLine("Czarna dziura !");
                type = "black_hole";
                var tmp = heromenu.GetEnumerator();
                while (tmp.MoveNext())
                {
                    Caption element;
                    element = new Caption();
                    if (tmp.Current.GetType() == element.GetType())
                    {
                        element = null;
                        element = (Caption)tmp.Current;

                        if (element.id == 1)
                            element.text.DisplayedString = "Czarna dziura";
                    }
                }
            }


            this.Go('x', 0, 800, 600);
        }

        private void WhatsGoingOn(int numberofframe)
        {

            if ( ((numberofsatelites > 0) && Keyboard.IsKeyPressed(Keyboard.Key.Space)) || ((numberofsatelites > 0) && this.type == "black_hole" ))
            {
                if (numberofframe % 3 == 0) // Co trzecią klatkę, coby za szybko nie było
                {
                    this.RemoveSatelite();
                }
            }


            if (sterowanie == 3)
            {
                if ((Mouse.GetPosition(okienko).X >= 0) && (Mouse.GetPosition(okienko).X <= okienko.Size.X))
                    if ((Mouse.GetPosition(okienko).Y >= 0) && (Mouse.GetPosition(okienko).Y <= okienko.Size.Y))
                    {
                        this.Go('x', Mouse.GetPosition(okienko).X - (int)this.position.X, (int)okienko.Size.X, (int)okienko.Size.Y);
                        this.Go('y', Mouse.GetPosition(okienko).Y - (int)this.position.Y, (int)okienko.Size.X, (int)okienko.Size.Y);
                    }
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
        }

        private void AddSatelite()
        {
            this.mass--;
            numberofsatelites++;
            Console.WriteLine("planeta numer " + numberofsatelites);
            this.NewBall(numberofsatelites, numberofsatelites);

            CalculateRadius();

        }

        private void CalculateRadius()
        {
            float costamekranu = this.mass / this.okienko.Size.X;

            if (this.mass > this.okienko.Size.X)
            {
                costamekranu = this.okienko.Size.X / this.mass ;

            }


            if (this.type == "small_sun")
            {
                this.kolo.Radius = (float)( this.mass * 0.001 * density);
                this.obwodka.Radius = this.kolo.Radius + 2;
            }
            if (this.type == "medium_sun")
            {
                this.kolo.Radius = (float)( this.mass * 0.0009 * density);
                this.obwodka.Radius = this.kolo.Radius + 2;
            }
            if (this.type == "neutron_star")
            {
                this.kolo.Radius = (float)( this.mass * 0.0005 * density);
                this.obwodka.Radius = this.kolo.Radius + 2;
            }

            if (this.type == "white_cancer")
            {
                this.kolo.Radius = (float)( this.mass * 0.00005 * density);
                this.obwodka.Radius = this.kolo.Radius + 2;
            }

            Console.WriteLine("radius " +( this.mass * 0.1 * density));
        }

        public void CalculatePosition()
        {
            Vector2f newposition = new Vector2f(this.position.X - this.obwodka.Radius, this.position.Y - this.obwodka.Radius);
            this.obwodka.Position = newposition;
            this.kolo.Position = new Vector2f(this.position.X - this.kolo.Radius, this.position.Y - this.kolo.Radius);
            this.zasiegacz.Position = new Vector2f(this.position.X - this.zasiegacz.Radius, this.position.Y - this.zasiegacz.Radius);
        }

        private void RemoveSatelite()
        {
            /*
                    for (int i = 0; i < numberofsatelites; i++)
                    {
                        int j = i;
                        j++;
                        this.satelite[i] = null;
                        this.satelite[i].Add(this.satelite[j]);
                    }
                    this.satelite.RemoveAt(numberofsatelites);
                    */
            this.satelite.RemoveAt(0);
            numberofsatelites--;

            this.mass++;


            Console.WriteLine("masa = " + mass);

            this.CalculateRadius();

        }

        public int Tick(bool movement, int numberofframe, Ball[] ball )
        {


            for (int i = 0; i < this.satelite.Count ; i++)
            {
                this.satelite[i].BallLocation(this.numberofsatelites, this.position.X, this.position.Y, i, this.kolo.Radius);
            }


            if (type == "supernova")
            {
                int magic = numberofballs;
                magic /= 10;
                this.kolo.Radius = new System.Random().Next(magic - 20, magic);
                this.obwodka.Radius = this.kolo.Radius + 2;
                this.Go('x', 0, (int)okienko.Size.X, (int)okienko.Size.Y);
            }





            if (movement)
            {
                menuStatistic = false;
                okienko.SetMouseCursorVisible(false);

                if (lastGravity)
                    enableGravity = true;
                 

                if (Keyboard.IsKeyPressed(Keyboard.Key.Tab))
                {
                    if (enableGravity)
                        lastGravity = true;
                    enableGravity = false;
                    menuStatistic = true;
                    okienko.SetMouseCursorVisible(true);
                }
                
                if (menuStatistic)
                {
                    this.MenuTick();
                }
                else
                {
                    WhatsGoingOn(numberofframe);

                    if (type != "black_hole")
                    {
                        for (int i = 0; i < ball.Length; i++)
                        {
                            if (ball[i] != null)
                            {
                                if (this.Near(ball[i].position, ball[i].kolo.Radius, (uint)(this.satelite.Count - 1)))
                                {
                                    numberofballs++;
                                    ball[i].Remake();
                                    if (0 == (numberofballs % 7) && (numberofballs > 0))
                                    {
                                        this.mass++;
                                        AddSatelite();
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int i = 0; i < ball.Length; i++)
                        {
                            if (ball[i] != null)
                            {
                                if (this.Near(ball[i].position, ball[i].kolo.Radius, (uint)(this.satelite.Count)))
                                {
                                    numberofballs++;
                                    ball[i] = null;
                                    if (0 == (numberofballs % 10) && (numberofballs > 0))
                                    {
                                        this.mass++;
                                        AddSatelite();
                                    }
                                }
                            }
                        }
                    }
                    if (status == 300)
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
                        if (numberofsatelites == 0)
                        {
                            this.satelite.Clear();
                            numberofsatelites--;
                            Console.WriteLine("Wsiorbałeś cały kosmos xD");
                            Console.WriteLine("Pozdro i papatki");
                            ball = null;

                            return 3;
                        }
                    }
                }
            }



            return 0;
        }

        private void MenuTick()
        {
            var tmp = heromenu.GetEnumerator();

            while (tmp.MoveNext())
            {
                Button element;
                element = kaczynskiSmiec;
                if (tmp.Current.GetType() == element.GetType())
                {
                    element = null;
                    element = (Button)tmp.Current;

                    if (element.id == 1)
                    {
                        if (this.mass >= status)
                        {
                            element.ChangeText("V");
                            element.ChangeColor(new Color(0, 128, 0));
                        }
                        else
                        {
                            element.ChangeText("X");
                            element.ChangeColor(new Color(128, 0, 0));
                        }
                    }

                    if (element.id == 3 || element.id == 4)
                    {

                        if (element.id == 4)
                        {
                            if (element.tekst.DisplayedString.Equals("+"))
                            {
                                if (element.DoAction())
                                {
                                    this.density++;

                                    this.CalculateRadius();

                                    Console.WriteLine("plusiczek");
                                    Console.WriteLine(density);
                                }
                            }
                            else
                            {
                                if (!enableManipulation)
                                {
                                    if (this.mass >= 100)
                                    {
                                        element.ChangeText("V");
                                        element.ChangeColor(new Color(0, 128, 0));
                                    }
                                    else
                                    {
                                        element.ChangeText("X");
                                        element.ChangeColor(new Color(128, 0, 0));
                                    }
                                }
                                else
                                {
                                    element.tekst.DisplayedString = "+";
                                    element.ChangeColor(new Color(127, 112, 0));
                                }
                            }
                        }


                        if (element.tekst.DisplayedString.Equals("-") && element.DoAction())
                        {
                            density--;
                            if (density < 10)
                                density = 10;

                            this.CalculateRadius();

                            Console.WriteLine("minusiczek");
                            Console.WriteLine(density);

                        }

                        if (element.tekst.DisplayedString.Equals("?"))
                        {
                            if (enableManipulation)
                            {
                                element.tekst.DisplayedString = "-";
                                element.ChangeColor(new Color(127, 112, 0));
                            }

                            if (element.DoAction())
                            {
                                var tmp2 = heromenu.GetEnumerator();
                                while (tmp2.MoveNext())
                                {
                                    Caption element2;
                                    element2 = new Caption();
                                    if (tmp2.Current.GetType() == element2.GetType())
                                    {
                                        element2 = null;
                                        element2 = (Caption)tmp2.Current;

                                        if (element2.id == 2)
                                            element2.text.DisplayedString = "Umiejętność pozwalająca manipulować gęstością |- koszt 100 masy";
                                    }

                                }
                            }

                        }

                        if (element.tekst.DisplayedString.Equals("odblokuj") && element.DoAction())
                        {
                            if (this.mass >= 100)
                            {
                                element.tekst.DisplayedString = "Gęstość";
                                enableManipulation = true;
                            }
                            else
                            {
                                var tmp3 = heromenu.GetEnumerator();
                                while (tmp3.MoveNext())
                                {
                                    Caption element3;
                                    element3 = new Caption();
                                    if (tmp3.Current.GetType() == element3.GetType())
                                    {
                                        element3 = null;
                                        element3 = (Caption)tmp3.Current;

                                        if (element3.id == 2)
                                            element3.text.DisplayedString = "Umiejętność pozwalająca manipulować gęstością |- koszt 100 masy";
                                    }
                                }
                            }
                            if (element.tekst.DisplayedString.Equals("Gęstość") && element.DoAction())
                            {
                                var tmp3 = heromenu.GetEnumerator();
                                while (tmp3.MoveNext())
                                {
                                    Caption element3;
                                    element3 = new Caption();
                                    if (tmp3.Current.GetType() == element3.GetType())
                                    {
                                        element3 = null;
                                        element3 = (Caption)tmp3.Current;

                                        if (element3.id == 2)
                                            element3.text.DisplayedString = "Umiejętność pozwalająca manipulować gęstością";
                                    }
                                }
                            }
                        }

                    }




                    if (element.tekst.DisplayedString.Equals("masa->planeta") && element.DoAction())
                    {
                        var tmp3 = heromenu.GetEnumerator();
                        while (tmp3.MoveNext())
                        {
                            Caption element3;
                            element3 = new Caption();
                            if (tmp3.Current.GetType() == element3.GetType())
                            {
                                element3 = null;
                                element3 = (Caption)tmp3.Current;

                                if (element3.id == 2)
                                    element3.text.DisplayedString = "Zmień masę na planety i odwrotnie";
                            }
                        }
                    }

                    if (element.tekst.DisplayedString.Equals("+") && (element.id == 2))
                    {
                        if (element.DoAction())
                        {
                            if (this.mass > 0)
                            {
                                this.AddSatelite();
                            }
                        }
                    }
                    if (element.tekst.DisplayedString.Equals("-") && (element.id == 2))
                    {
                        if (element.DoAction())
                        {
                            if (numberofsatelites > 0)
                                this.RemoveSatelite();
                        }
                    }

                    if (element.tekst.DisplayedString.Equals("Awansuj") && element.DoAction())
                    {
                        if (this.mass >= status)
                        {
                            this.ChangeStatus(status);
                            this.mass -= status;
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
                                AddSatelite();
                                //numberofsatelites--;
                            }

                            this.CalculateRadius();


                        }
                    }
                }
            }
        }

        public void Changemovement(int a)
        {
            this.sterowanie = a;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            this.Draw();
        }


    }

}
