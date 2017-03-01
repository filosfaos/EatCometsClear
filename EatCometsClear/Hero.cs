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
        //static System.Collections.ArrayList heromenu;
        private HeadUpDisplay heromenuHUD, menuStatsHUD;
        private RenderWindow okienko;
        public bool enablemovement;
        private int numberofballs;
        private int numberofsatelites;
        private int status;
        private string type;
        public int step;
        public int additionalRange;
        public bool enableRange;
        private bool planetsGravity;
        private bool planetsGravityEnable;
        private bool menuStatistic;
        Button kaczynskiSmiec;
        int sterowanie;
        private bool enableManipulation;
        private bool enableManipulationBuyed;
        int density;
        private bool lastGravity;
        SFML.Audio.Sound collectSound;
        private int soundColldown;
        public bool IsPlaying;

        private Text maximumGet;

        public int sharedGravity;


        public Hero(RenderWindow okienko, float x, float y, Color color, int screenX, int screenY, bool eneblemovementt, int sterowanie)
        {
            IsPlaying = false;

            this.planetsGravity = false;
            this.planetsGravityEnable = false;

            this.enableGravity = true;
            this.sharedGravity = 15;

            soundColldown = 0;
            this.enablemovement = eneblemovementt;
            this.okienko = okienko;

            lastGravity = false;

            density = 100;
            enableManipulation = false;


            kaczynskiSmiec = new Button(1, 1, 1, 1, "1", okienko, new Color(Color.Black), 1, 1);

            List<Drawable> heromenu = new List<Drawable>();


            uint pomX = okienko.Size.X;
            uint pomY = okienko.Size.Y;

            uint buttontextsize = (uint)(pomX * 0.022);
            if (this.okienko.Size.X == 1920)
                buttontextsize = (uint)(pomY * 0.030);
            if (this.okienko.Size.X == 1280)
                buttontextsize = (uint)(pomX * 0.022);
            if (this.okienko.Size.X == 1024)
                buttontextsize = (uint)(pomX * 0.025);
            if (this.okienko.Size.X == 800)
                buttontextsize = (uint)(pomX * 0.022);

            Color buttonscolor = new Color(69, 69, 0);
            Text Gamename;
            Gamename = new Text();
            Gamename.DisplayedString = "Kometa";
            Gamename.Font = new Font("fonts/arial.ttf");
            Gamename.Position = new Vector2f((uint)(pomX * 0.03), (uint)(pomY * 0.03));
            Gamename.Color = new Color(128, 128, 128);
            Gamename.CharacterSize = (uint)(pomY * 0.15);

            heromenu.Add(new Caption(Gamename, 1, okienko));

            heromenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Rozszerzona grawitacja", okienko, buttonscolor, buttontextsize, 21));
            heromenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", okienko, new Color(81, 91, 73), buttontextsize, 23));
            heromenu.Add(new Button((uint)(pomX * 0.12), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "masa->planeta", okienko, new Color(51, 61, 43), buttontextsize, 22));
            heromenu.Add(new Button((uint)(pomX * 0.30), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", okienko, new Color(81, 91, 73), buttontextsize, 24));
            heromenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.25), (uint)(pomX * 0.15), (uint)(pomY * 0.1), "Ewolucja", okienko, buttonscolor, buttontextsize, 25));
            heromenu.Add(new Button((uint)(pomX * 0.24), (uint)(pomY * 0.25), (uint)(pomX * 0.09), (uint)(pomY * 0.1), "X", okienko, new Color(200, 128, 64), buttontextsize, 1));
            heromenu.Add(new Button((uint)(pomX * 0.29), (uint)(pomY * 0.61), (uint)(pomX * 0.04), (uint)(pomY * 0.06), "+", okienko, new Color(81, 91, 73), buttontextsize, 28));
            heromenu.Add(new Button((uint)(pomX * 0.13), (uint)(pomY * 0.61), (uint)(pomX * 0.15), (uint)(pomY * 0.06), "Odblokuj", okienko, buttonscolor, buttontextsize, 29));
            heromenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.61), (uint)(pomX * 0.04), (uint)(pomY * 0.06), "-", okienko, new Color(81, 91, 73), buttontextsize, 27));
            heromenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.53), (uint)(pomX * 0.25), (uint)(pomY * 0.06), "Gęstość", okienko, new Color(51, 61, 43), buttontextsize, 26));
            Gamename = null;

            Gamename = new Text("Menu postaci", new Font("fonts/arial.ttf"), (uint)(pomY * 0.022222));
            Gamename.Position = new Vector2f((uint)(pomX * 0.08), (uint)(pomY * 0.833333));
            Gamename.Color = new Color(Color.White);
            heromenu.Add(new Caption(Gamename, 31, okienko));

            heromenuHUD = new HeadUpDisplay(heromenu);

            this.menuStatistic = false;

            heromenu.Clear();

            Gamename = null;
            Gamename = new Text("Masa :",new Font("fonts/arial.ttf"), (uint)(pomY * 0.05));
            Gamename.Position = new Vector2f((uint)(pomX * 0.65), (uint)(pomY * 0.25));
            Gamename.Color = new Color(Color.White);
            heromenu.Add(new Caption(Gamename, 11, okienko));
            Gamename = null;
            Gamename = new Text("1", new Font("fonts/arial.ttf"), (uint)(pomY * 0.05));
            Gamename.Position = new Vector2f((uint)(pomX * 0.90), (uint)(pomY * 0.25));
            Gamename.Color = new Color(Color.White);
            heromenu.Add(new Caption(Gamename, 12, okienko));


            Gamename = null;
            Gamename = new Text("Ilość planet:", new Font("fonts/arial.ttf"), (uint)(pomY * 0.05));
            Gamename.Position = new Vector2f((uint)(pomX * 0.65), (uint)(pomY * 0.45));
            Gamename.Color = new Color(Color.White);
            heromenu.Add(new Caption(Gamename, 13, okienko));
            Gamename = null;
            Gamename = new Text("0", new Font("fonts/arial.ttf"), (uint)(pomY * 0.05));
            Gamename.Position = new Vector2f((uint)(pomX * 0.90), (uint)(pomY * 0.45));
            Gamename.Color = new Color(Color.White);
            heromenu.Add(new Caption(Gamename, 14, okienko));


            Gamename = null;
            Gamename = new Text("Do awansu:", new Font("fonts/arial.ttf"), (uint)(pomY * 0.05));
            Gamename.Position = new Vector2f((uint)(pomX * 0.65), (uint)(pomY * 0.35));
            Gamename.Color = new Color(Color.White);
            heromenu.Add(new Caption(Gamename, 15, okienko));
            Gamename = null;
            Gamename = new Text("0", new Font("fonts/arial.ttf"), (uint)(pomY * 0.05));
            Gamename.Position = new Vector2f((uint)(pomX * 0.90), (uint)(pomY * 0.35));
            Gamename.Color = new Color(Color.White);
            heromenu.Add(new Caption(Gamename, 16, okienko));


            menuStatsHUD = new HeadUpDisplay(heromenu);

            SetMenu();

            maximumGet = new Text("Osiągnięto maksymalną masę dla tego typu obiektu", new Font("fonts/arial.ttf"), (uint)(pomY * 0.05));

            maximumGet.Position = new Vector2f((uint)(pomX * 0.06), (uint)(pomY * 0.9));
            maximumGet.Color = new Color(255,255,255,128);

            

            this.additionalRange = 10;

            this.step = (int)okienko.Size.X / 180;
            gravityStrength = 10;

            type = "comet";

            this.mass = 40;

            this.sterowanie = sterowanie;
            this.status = 5;
            numberofballs = 0;
            numberofsatelites = 0;

            position = new Vector2f(x, y);
            obwodka = new CircleShape();
            obwodka.FillColor = new Color(121, 121, 121);
            obwodka.Position = position;
            obwodka.Radius = 6;

            kolo = new CircleShape();
            kolo.FillColor = new Color(69, 69, 69);
            kolo.Position = position;
            kolo.Radius = 4;

            this.CalculateRadius();


            zasiegacz = new CircleShape();
            zasiegacz.FillColor = new Color(10, 10, 10);
            zasiegacz.Position = position;
            zasiegacz.Radius = 4;

            this.satelite = new List<Satelite>();
            this.NewBall(0, 1);


            this.Go('x', 0, screenX, screenY);

            this.CalculateRadius();
        }

        public void Draw()
        {
            if (!menuStatistic)
                if (enableRange)
                    okienko.Draw(this.zasiegacz);
            okienko.Draw(this.obwodka);
            okienko.Draw(this.kolo);

            if (satelite != null)
            {
                for (int i = 0; i < this.satelite.Count; i++)
                {
                    if (i != 0)
                    {
                        okienko.Draw(this.satelite[i].obwodka);
                        okienko.Draw(this.satelite[i].kolo);

                    }
                }
            }
            if (menuStatistic)
            {
                heromenuHUD.Draw();
                menuStatsHUD.Draw();
            }
            
            if(IsPlaying)
                if ((this.mass + this.satelite.Count - this.status*0.5 > status) && ((type != "black_hole") && (type != "galaxy_center") ))
                    okienko.Draw(maximumGet);

        }

        public bool Near(Vector2f position, float poprawka, uint range)
        {
            if (range == 0)
                range = (uint)this.obwodka.Radius;
            

            if (this.enableGravity)
            {
                range = 0;
            }

            uint distance = (uint)Math.Pow(range + (uint)this.obwodka.Radius + additionalRange, 2);
            uint distance2 = (uint)Math.Pow(range + (uint)this.obwodka.Radius + additionalRange - poprawka, 2);

            this.zasiegacz.Radius = (float)Math.Sqrt(distance2);
            //this is squared value ( potęgowana wartość )

            uint x = (uint)Math.Pow(this.position.X - (position.X + poprawka), 2);
            uint y = (uint)Math.Pow(this.position.Y - (position.Y + poprawka), 2);

            if ((x + y) <= distance)
                return true;

            if (planetsGravityEnable)
            {
                foreach (Satelite element in satelite)
                {
                    if (Near(element.position, element.kolo.Radius, position, poprawka))
                        return true;
                }
            }

            return false;
        }

        private bool Near(Vector2f pos1, float radius1, Vector2f pos2, float radius2)
        {
            pos2.X -= pos1.X;
            pos1.X = 0;
            pos2.Y -= pos1.Y;
            pos1.Y = 0;
            float c = (float)(Math.Pow(pos2.X, 2) + Math.Pow(pos2.Y, 2) - Math.Pow(radius2,2));

            if (c < radius1)
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
            if(howmuch == 5)
            {
                kolo.FillColor = new Color(255, 180, 60);
                obwodka.FillColor = new Color(255, 70, 0);

                Console.WriteLine("Słońce !");
                type = "small_sun";
                heromenuHUD.ChangeCaptionByID(1, "Małe słońce");
                heromenuHUD.ChangeTextColorByID(1, new Color(255, 70, 0));
                this.minimalMass = 2;
            }
            else if (howmuch == 10)
            {
                Console.WriteLine("Super słońce !");
                kolo.FillColor = new Color(255, 180, 60);
                obwodka.FillColor = new Color(255, 70, 0);
                heromenuHUD.ChangeTextColorByID(1, new Color(255, 75, 5));
                this.minimalMass = 4;
            }
            else if (howmuch == 25)
            {
                Console.WriteLine("Super słońce !");
                type = "medium_sun";

                heromenuHUD.ChangeCaptionByID(1, "Średnie słońce");
                heromenuHUD.ChangeTextColorByID(1, new Color(255, 80, 10));

                kolo.FillColor = new Color(255, 155, 37);
                obwodka.FillColor = new Color(255, 53, 0);

                this.minimalMass = 6;
            }
            else if (howmuch == 50)
            {
                this.kolo.FillColor = new Color(128, 0, 128);
                this.obwodka.FillColor = new Color(255, 0, 255);

                Console.WriteLine("Neutronowy olbrzym !");
                type = "neutron_star";
                heromenuHUD.ChangeCaptionByID(1, "Neutronowy olbrzym");
                heromenuHUD.ChangeTextColorByID(1, new Color(200, 72, 200));

                this.minimalMass = 15;

            }
            else if (howmuch == 100)
            {
                this.kolo.FillColor = new Color(Color.White);
                this.obwodka.FillColor = new Color(200, 200, 200);
                Console.WriteLine("Biały niewypał !");
                type = "white_cancer";

                heromenuHUD.ChangeCaptionByID(1, "Biały niewypał");
                heromenuHUD.ChangeTextColorByID(1, new Color(255, 255, 255));

                this.minimalMass = 25;
            }
            else if (howmuch == 150)
            {
                this.kolo.FillColor = new Color(200, 200, 255);
                this.obwodka.FillColor = new Color(Color.White);
                Console.WriteLine("Supernova !!!!");
                type = "supernova";

                heromenuHUD.ChangeCaptionByID(1, "Supernova");
                heromenuHUD.ChangeTextColorByID(1, new Color(255, 200, 200));
                this.minimalMass = 100;
            }
            else if (howmuch == 300)
            {
                this.kolo.FillColor = new Color(Color.Black);
                this.obwodka.FillColor = new Color(50, 50, 50);

                Console.WriteLine("Czarna dziura !");
                type = "black_hole";

                heromenuHUD.ChangeCaptionByID(1, "Czarna dziura");
                heromenuHUD.ChangeTextColorByID(1, new Color(31, 31, 31));
                this.minimalMass = 99999;
            }
            this.Go('x', 0, 0, 0);
        }

        private void WhatsGoingOn(int numberofframe)
        {

            if (((numberofsatelites > 0) && Keyboard.IsKeyPressed(Keyboard.Key.Q)) || ((numberofsatelites > 0) && this.type == "black_hole"))
            {
                if (numberofframe % 3 == 0) // Co trzecią klatkę, coby za szybko nie było
                {
                    if (numberofsatelites > 0)
                        this.RemoveSatelite();
                }
            }

            if (( Keyboard.IsKeyPressed(Keyboard.Key.E)) && this.type != "black_hole")
            {
                if (numberofframe % 3 == 0) // Co trzecią klatkę, coby za szybko nie było
                {
                    if (mass > 1)
                        this.AddSatelite();
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
                costamekranu = this.okienko.Size.X / this.mass;

            }

            float max_radius = 100;


            if (this.type == "comet")
            {
                max_radius = 5;
            }
            if (this.type == "small_sun")
            {
                max_radius = 10;
            }
            if (this.type == "medium_sun")
            {
                max_radius = 15;
            }
            if(this.type == "red_giant")
            {
                max_radius = 50;

            }
            if (this.type == "neutron_star")
            {
                max_radius = 50;
            }

            if (this.type == "white_cancer")
            {
                max_radius = 5;
            }
            if(this.type == "black_hole")
            {
                max_radius = 100;
            }

            this.kolo.Radius = this.mass;

            if (max_radius < this.mass)
                this.kolo.Radius = max_radius;

            if(enableManipulation)
                this.kolo.Radius = (float)(this.kolo.Radius * 0.01 * density);

            this.obwodka.Radius = this.kolo.Radius + 2;

            Console.WriteLine("radius " + (this.kolo.Radius));
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

        public int Tick(bool movement, int numberofframe, Ball[] ball)
        {
            soundColldown++;

            int distance = 1;

            foreach(Satelite element in this.satelite)
            {
                int pom = distance;
                if (type == "black_hole")
                    pom = (int)(pom * 0.95);
                distance = element.BallLocation(this.numberofsatelites, this.position.X, this.position.Y, pom, this.kolo.Radius);
            }

            int distanceOfLastSatelite = distance;

            if (type == "supernova")
            {
                int magic = satelite.Count;
                this.kolo.Radius = new System.Random().Next(magic - 20, magic);
                this.obwodka.Radius = this.kolo.Radius + 2;
                this.Go('x', 0, (int)okienko.Size.X, (int)okienko.Size.Y);

                if (mass > 1)
                {
                    AddSatelite();
                }

                if (mass + satelite.Count >= status)
                {
                    while (mass > 0)
                    {
                        AddSatelite();
                    }
                    if (mass == 0)
                    {
                        this.Go('x', (int)(okienko.Size.X / 2 - this.position.X), (int)okienko.Size.X, (int)okienko.Size.Y);
                        this.Go('y', (int)(okienko.Size.Y / 2 - this.position.Y), (int)okienko.Size.X, (int)okienko.Size.Y);

                        enablemovement = false;
                        ChangeStatus(300);
                    }
                }
            }
            else if (type == "black_hole")
            {
                if (satelite.Count > 0)
                {
                    if (numberofframe % 4 == 0)
                        RemoveSatelite();
                }
                else
                {
                    satelite.Clear();
                    type = "galaxy_center";
                }

            }





            if (movement)
            {
                menuStatistic = false;
                okienko.SetMouseCursorVisible(false);

                if (lastGravity)
                    enableGravity = true;
                 

                if (Keyboard.IsKeyPressed(Keyboard.Key.Tab) && ( (this.type != "supernova") || (this.type != "black_hole")))
                {
                    if (enableGravity)
                        lastGravity = true;
                    enableGravity = false;
                    menuStatistic = true;
                    okienko.SetMouseCursorVisible(true);
                }
                
                if (menuStatistic)
                {
                    this.heromenuHUD.Tick();
                    menuStatsHUD.ChangeCaptionByID(12, Convert.ToString(this.mass));
                    menuStatsHUD.ChangeCaptionByID(16, Convert.ToString(this.status));
                    if(satelite != null)
                        menuStatsHUD.ChangeCaptionByID(14, Convert.ToString(this.satelite.Count - 1));
                    else
                        menuStatsHUD.ChangeCaptionByID(14, Convert.ToString(0));
                }
                else
                {
                    WhatsGoingOn(numberofframe);

                    if ( (type != "black_hole") && (type != "galaxy_center"))
                    {
                        if ( (mass + this.satelite.Count - this.status*0.5 < this.status) )
                        {
                            for (int i = 0; i < ball.Length; i++)
                            {
                                if (ball[i] != null)
                                {
                                    if (this.Near(ball[i].position, ball[i].kolo.Radius, (uint)(distanceOfLastSatelite)))
                                    {
                                        numberofballs++;
                                        ball[i].Remake();
                                        if (0 == (numberofballs % 10) && (numberofballs > 0))
                                        {
                                            if (soundColldown > 120)
                                            {
                                                if ((collectSound != null) && (collectSound.Status == SFML.Audio.SoundStatus.Stopped))
                                                {
                                                    collectSound.Play();
                                                }
                                            }

                                            soundColldown = 0;
                                            this.mass++;
                                            AddSatelite();
                                            if (this.type == "comet")
                                                RemoveSatelite();
                                        }
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
                }
            }



            return 0;
        }

        private void SetMenu()
        {
            foreach (Button element in heromenuHUD.GetButtons())
            {
                if (element.id == 1)
                {
                    element.onRightClick = delegate ()
                    {
                        heromenuHUD.ChangeCaptionByID(31, "Wskaźnik pokazujący, czy możliwa jest ewolucja");
                    };
                    element.tick = delegate ()
                    {
                        if (this.mass >= status)
                        {
                            element.ChangeText("V");
                            element.SetColor(new Color(14, 128, 0));
                        }
                        else
                        {
                            element.ChangeText("X");
                            element.SetColor(new Color(128, 14, 0));
                        }
                    };
                }

                if (element.id == 21)
                {
                    element.tick = delegate ()
                    {
                        if (this.planetsGravity)
                        {
                            if (this.planetsGravityEnable)
                                element.SetColor(new Color(120, 67, 41));
                            else
                                element.SetColor(new Color(21, 21, 21));
                        }
                        else
                        {
                            if (this.mass >= 100)
                                element.SetColor(new Color(0, 127, 0));
                            else
                                element.SetColor(new Color(127, 0, 0));
                        }
                    };
                    element.onRightClick = delegate ()
                    {
                        if (this.planetsGravity)
                            heromenuHUD.ChangeCaptionByID(31, "Umiejętność sprawiająca, że planety zjadają komety");

                        else
                            heromenuHUD.ChangeCaptionByID(31, "Umiejętność sprawiająca, że planety zjadają komety || koszt 50 masy");
                    };
                    element.onClick = delegate ()
                    {
                        if (this.planetsGravity)
                        {
                            if (this.planetsGravityEnable)
                            {
                                this.planetsGravityEnable = false;
                                heromenuHUD.ChangeCaptionByID(31, "Umiejętność wyłączona");
                            }
                            else
                            {
                                this.planetsGravityEnable = true;
                                heromenuHUD.ChangeCaptionByID(31, "Umiejętność włączona");
                            }
                        }
                        else
                        {
                            if (this.mass >= 50)
                            {
                                this.planetsGravity = true;
                                this.planetsGravityEnable = true;
                                this.mass -= 50;
                            }
                            else
                            {
                                heromenuHUD.ChangeCaptionByID(31, "Potrzeba masy");
                            }
                        }
                    };
                }
                if (element.id == 22)
                {
                    element.onClick = delegate ()
                    {
                        heromenuHUD.ChangeCaptionByID(31, "Zmień masę na planety i odwrotnie");
                    };
                    element.onRightClick = element.onClick;
                }
                if (element.id == 23)
                {
                    element.onRightClick = delegate ()
                    {
                        heromenuHUD.ChangeCaptionByID(31, "Zmiejsz ilość planet");
                    };
                    element.onClick = delegate ()
                    {
                        heromenuHUD.ChangeCaptionByID(31, "Zmiejsz ilość planet");
                        if (numberofsatelites > 0)
                            this.RemoveSatelite();
                    };
                }
                if (element.id == 24)
                {
                    element.onRightClick = delegate ()
                    {
                        heromenuHUD.ChangeCaptionByID(31, "Zwiększ ilość planet");
                    };
                    element.onClick = delegate ()
                    {
                        heromenuHUD.ChangeCaptionByID(31, "Zwiększ ilość planet");
                        if (this.mass > 0)
                        {
                            this.AddSatelite();
                        }
                    };
                }

                if (element.id == 25)
                {
                    element.onRightClick = delegate ()
                    {
                        heromenuHUD.ChangeCaptionByID(31, "Kliknięcie umożliwia ewolucję");
                    };
                    element.onClick = delegate ()
                    {
                        if (this.mass >= status)
                        {
                            this.ChangeStatus(status);
                            this.mass -= status;
                            if (status == 5)
                                status = 10;
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
                    };
                }

                if (element.id == 26)
                {
                    element.onRightClick = delegate ()
                    {
                        if (this.enableManipulation)
                            heromenuHUD.ChangeCaptionByID(31, "Umiejętność pozwalająca manipulować gęstością");
                        else
                            heromenuHUD.ChangeCaptionByID(31, "Umiejętność pozwalająca manipulować gęstością |- koszt 100 masy");
                    };
                    element.onClick += element.onRightClick;
                }
                else if (element.id == 27)
                {element.onRightClick = delegate ()
                    {
                        heromenuHUD.ChangeCaptionByID(31, "Zwiększ gęstość");
                    };
                    element.onClick = delegate ()
                    {

                        if (enableManipulation)
                        {
                            density--;
                            if (density < 10)
                                density = 10;

                            this.CalculateRadius();

                            Console.WriteLine("gęstość = " + density);

                        }
                        else
                        {
                            heromenuHUD.ChangeCaptionByID(31, "Umiejętność pozwalająca manipulować gęstością |- koszt 100 masy");
                        }
                    };
                }
                else if (element.id == 28)
                {
                    element.onRightClick = delegate ()
                    {
                        heromenuHUD.ChangeCaptionByID(31, "Zmniejsz gęstość");
                    };
                    element.onClick = delegate ()
                    {
                        if (enableManipulation)
                        {
                            this.density++;
                            this.CalculateRadius();

                            Console.WriteLine("gęstość = " + density);
                        }
                    };
                }
                else if (element.id == 29)
                {
                    element.tick = delegate ()
                    {
                        if (this.enableManipulationBuyed)
                        {
                            if (this.enableManipulation)
                                element.SetColor(new Color(120, 67, 41));
                            else
                                element.SetColor(new Color(21, 21, 21));
                        }
                        else
                        {
                            if (this.mass >= 100)
                                element.SetColor(new Color(0, 127, 0));
                            else
                                element.SetColor(new Color(127, 0, 0));
                        }
                    };
                    element.onRightClick = delegate ()
                    {
                        if(enableManipulationBuyed)
                            heromenuHUD.ChangeCaptionByID(31, "Włącza / wyłącza gęstość");
                        else
                            heromenuHUD.ChangeCaptionByID(31, "Odblokowuje gęstość");
                    };
                    element.onClick = delegate ()
                    {
                        if (enableManipulationBuyed)
                        {
                            enableManipulation = !enableManipulation;
                            this.CalculateRadius();

                            if (enableManipulation)
                            {
                                heromenuHUD.ChangeCaptionByID(31, "Umiejętność włączona");
                                element.ChangeText("Włączone");
                            }
                            else
                            {
                                heromenuHUD.ChangeCaptionByID(31, "Umiejętność wyłączona");
                                element.ChangeText("Wyłączone");
                            }
                        }
                        else
                        {
                            if (this.mass >= 100)
                            {
                                this.mass -= 100;
                                element.tekst.DisplayedString = "Włączone";
                                enableManipulation = true;
                                enableManipulationBuyed = true;
                            }

                        }
                    };


                }
            }
        }

        public List<Satelite> GetSatelitesOfGravity()
        {
            if (this.satelite.Count == 0)
            {
                List<Satelite> returning = new List<Satelite>();

                for (int i = 0; i < this.sharedGravity; i++)
                {
                    if (this.satelite[i] != null)
                    {
                        returning.Add(this.satelite[i]);
                    }
                }

                return returning;
            }

            return new List<Satelite>();
        }

        public void Changemovement(int a)
        {
            this.sterowanie = a;
        }

        public void AddSound(SFML.Audio.Sound newSound)
        {
            this.collectSound = newSound;
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
