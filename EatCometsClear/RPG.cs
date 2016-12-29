using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

namespace EatCometsClear
{
    class RPG : Game
    {
        public bool startNewGame;
        Hero hero, menuhero;
        Ball ball;
        Text  Gamename, Gamename2, textDifficulty, textGravity, textRange, textSteps, textMusicVolume, textMusicEnable;
        RectangleShape linia;
        

        Music music;
        bool musicEnabled;

        int numberofframe;
        public bool gamestarted;
        static System.Collections.ArrayList mainmenu, options1, options2, options3, optionbar;
        int showTip;
        int enableOptions;
        //uint screenX = 1280, screenY = 720;
        

        int sterowanie;
        int[] difficulty;

        List<Text> texty;


        Physic rydzykFizyk;

        bool enableRangeWskaznik, enableGravity;

        Image ikona;

        public RPG()
            : base(1280, 720, "Żryj komety", Color.Black)
        {
        }

        protected override void LoadContent()
        {

            /*
            try
            {
                int pomX = (int)(window.Size.X*0.34);
                int pomY = (int)(window.Size.Y*0.34);
                Vector2i size = new Vector2i((int)(window.Size.X * 0.05), (int)(window.Size.Y * 0.05));
                Console.WriteLine("liapa" + size.X + "gurwa" + size.Y);
                butimg = new System.Collections.ArrayList();
                butimg.Add(new Texture("img/1pad.jpg"  , new IntRect(pomX, pomY, 1/size.X, 1/size.Y) ));
                butimg.Add(new Texture("img/1logo.jpg" , new IntRect(pomX, pomY, 1/size.X, 1/size.Y) ));
                butimg.Add(new Texture("img/1head.png" , new IntRect(pomX, pomY, 1/size.X, 1/size.Y) ));

            }
            catch
            {
                Console.WriteLine("Nie wczytano obrazkow :(");
            }

            */
            music = new Music("content/music2.ogg");
            ikona = new Image("img/icon.png");
            
        }

        protected override void Initialize()
        {
            window.SetActive(true);
            window.SetIcon(ikona.Size.X, ikona.Size.Y, ikona.Pixels);

            rydzykFizyk = new Physic(); //on liczy fizyke fizyk jeden

            enableRangeWskaznik = true;
            enableGravity = false;

            uint pomX = window.Size.X;
            uint pomY = window.Size.Y;

            
            sterowanie = 3;
            difficulty = new int[3] { 10, 1000, 100 };
            gamestarted = false;
            numberofframe = 0;

            musicEnabled = false;
            music.Loop = true;
            music.Volume = 100;
            //music.Play();

            enableOptions = 0;
            showTip = 0;
            Gamename = new Text();
            Gamename.DisplayedString = "Żryj komety";
            Gamename.Font = new Font("fonts/arial.ttf");
            Gamename.Position = new Vector2f((uint)(pomX * 0.03), (uint)(pomY * 0.03));
            Gamename.Color = new Color(138, 7, 7);
            Gamename.CharacterSize = (uint)(pomY * 0.15);
            
            Gamename2 = new Text();
            Gamename2.DisplayedString = "Żryj komety";
            Gamename2.Font = new Font("fonts/arial.ttf");
            Gamename2.Position = new Vector2f((uint)(pomX * 0.028), (uint)(pomY * 0.0305));
            Gamename2.Color = new Color(255, 255, 255);
            Gamename2.CharacterSize = (uint)(pomY * 0.151);

            linia = new RectangleShape(new Vector2f(2, (uint)(pomY * 0.45)));
            linia.Position = new Vector2f((uint)(pomX * 0.365), (uint)(pomY * 0.3222));
            linia.FillColor = new Color(255,255,255);

            uint buttontextsize = (uint)(pomY * 0.040);
            Color buttonscolor = new Color(127, 112, 0);
            mainmenu = new System.Collections.ArrayList();
            mainmenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.25), (uint)(pomX * 0.15), (uint)(pomY * 0.1), "Graj", window, buttonscolor, buttontextsize, 0 ));
            mainmenu.Add(new Button((uint)(pomX * 0.24), (uint)(pomY * 0.25), (uint)(pomX * 0.09), (uint)(pomY * 0.1), "Nowa", window, new Color(200, 128, 64), buttontextsize, 0));
            mainmenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.40), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Opcje", window, buttonscolor, buttontextsize, 0));
            mainmenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Pokaż wskazówkę", window, buttonscolor, buttontextsize, 0));
            mainmenu.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Wyjdź", window, buttonscolor, buttontextsize, 0));

            /*
            buttonscolor = new Color(69, 255, 69);
            optionbar = new System.Collections.ArrayList();
            optionbar.Add(new ButtonImg((uint)(pomX * 0.40), (uint)(pomY * 0.25), (uint)(pomX * 0.03), (uint)(pomY * 0.06), window, buttonscolor, (Texture)butimg[0]));
            optionbar.Add(new ButtonImg((uint)(pomX * 0.40), (uint)(pomY * 0.55), (uint)(pomX * 0.03), (uint)(pomY * 0.06), window, buttonscolor, (Texture)butimg[1]));
            optionbar.Add(new ButtonImg((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.03), (uint)(pomY * 0.06), window, buttonscolor, (Texture)butimg[2]));
            */
            buttonscolor = new Color(69, 128, 69);
            optionbar = new System.Collections.ArrayList();
            optionbar.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.265), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "S", window, buttonscolor, buttontextsize, 0));
            buttonscolor = new Color(128, 69, 69);
            optionbar.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.415), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "M", window, buttonscolor, buttontextsize, 0));
            buttonscolor = new Color(69, 69, 128);
            optionbar.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.565), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "G", window, buttonscolor, buttontextsize, 0));
            buttonscolor = new Color(69, 69, 69);
            optionbar.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.715), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "X", window, buttonscolor, buttontextsize, 0));



            buttonscolor = new Color(69, 128, 69);
            options1 = new System.Collections.ArrayList();
            options1.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.25), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Myszka", window, buttonscolor, buttontextsize, 0));
            options1.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 0));
            options1.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Czułość", window, buttonscolor, buttontextsize, 0));
            options1.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 0));
            options1.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "przycisk", window, buttonscolor, buttontextsize, 0));
            options1.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Zamknij", window, buttonscolor, buttontextsize, 0));


            buttonscolor = new Color(128, 69, 69);
            options2 = new System.Collections.ArrayList();
            options2.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.25), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Muzyka", window, buttonscolor, buttontextsize, 0));
            options2.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 0));
            options2.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Głośność", window, buttonscolor, buttontextsize, 0));
            options2.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 0));
            options2.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "przycisk", window, buttonscolor, buttontextsize, 0));
            options2.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Zamknij", window, buttonscolor, buttontextsize, 0));

            buttonscolor = new Color(69, 69, 128);
            options3 = new System.Collections.ArrayList();
            options3.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 1));
            options3.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.25), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Zasięg", window, buttonscolor, buttontextsize, 0));
            options3.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 1));
            options3.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 2));
            options3.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Grawitacja", window, buttonscolor, buttontextsize, 0));
            options3.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 2));
            options3.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Połykanie", window, buttonscolor, buttontextsize, 0));
            options3.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Wskaźnik zasięgu", window, buttonscolor, buttontextsize, 0));


            textDifficulty = new Text("0", new Font("fonts/arial.ttf"), 50);
            textDifficulty.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.27));
            textDifficulty.Color = new Color(Color.White);
            textDifficulty.DisplayedString = Convert.ToString(difficulty[0]);


            textGravity = new Text("0", new Font("fonts/arial.ttf"), 50);
            textGravity.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
            textGravity.Color = new Color(Color.White);
            textGravity.DisplayedString = Convert.ToString(difficulty[1]);


            textRange = new Text("0", new Font("fonts/arial.ttf"), 50);
            textRange.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.71));
            textRange.Color = new Color(Color.White);
            textRange.DisplayedString = Convert.ToString(enableRangeWskaznik);

            textSteps = new Text("0", new Font("fonts/arial.ttf"), 50);
            textSteps.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
            textSteps.Color = new Color(Color.White);
            textSteps.DisplayedString = Convert.ToString(difficulty[2]);

            textMusicVolume = new Text("0", new Font("fonts/arial.ttf"), 50);
            textMusicVolume.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
            textMusicVolume.Color = new Color(Color.White);
            textMusicVolume.DisplayedString = Convert.ToString(music.Volume);


            textMusicEnable = new Text("0", new Font("fonts/arial.ttf"), 50);
            textMusicEnable.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.27));
            textMusicEnable.Color = new Color(Color.White);
            textMusicEnable.DisplayedString = Convert.ToString(musicEnabled);


            //uint tipsize = 16;
            uint tipsize = (uint)(pomY * 0.022222);
            texty = new List<Text>();

            for( int i = 0; i < 16; i++)
            {
                if( i == 0)
                    texty.Add( new Text("Poruszanie - dostosuj w opcjach", new Font("fonts/arial.ttf"), tipsize) );
                if( i == 1)
                    texty.Add(new Text("Co 10 zdobytych komet otrzymujesz planetę", new Font("fonts/arial.ttf"), tipsize));
                if(i == 2)
                    texty.Add(new Text("Twoja gwiazda ewoluuje", new Font("fonts/arial.ttf"), tipsize));
                if( i == 3)
                    texty.Add(new Text("ESC cofa do menu głównego", new Font("fonts/arial.ttf"), tipsize));
                if (i == 4)
                    texty.Add(new Text("Po ukończeniu gry możesz zacząć od nowa z poziomu menu głównego", new Font("fonts/arial.ttf"), tipsize));
                if (i == 5)
                    texty.Add(new Text("Dodatkowy zasięg połykania komet", new Font("fonts/arial.ttf"), tipsize));
                if (i == 6)
                    texty.Add(new Text("Zmienia ustawienia sterowania", new Font("fonts/arial.ttf"), tipsize));
                if (i == 7)
                    texty.Add(new Text("Włącz / Wyłącz  muzykę", new Font("fonts/arial.ttf"), tipsize));
                if (i == 8)
                    texty.Add(new Text("Siła grawitacji", new Font("fonts/arial.ttf"), tipsize));
                if (i == 9)
                    texty.Add(new Text("Pokaż / ukryj wskaźnik zasięgu połykania / przyciągania", new Font("fonts/arial.ttf"), tipsize));
                if (i == 10)
                    texty.Add(new Text("Zmienia przelicznik zasięgu między masą a ilością planet", new Font("fonts/arial.ttf"), tipsize));
                if (i == 11)
                    texty.Add(new Text("Zmień ustawienia gry", new Font("fonts/arial.ttf"), tipsize));
                if (i == 12)
                    texty.Add(new Text("Zmień ustawienia muzyki", new Font("fonts/arial.ttf"), tipsize));
                if (i == 13)
                    texty.Add(new Text("Zmień ustawienia sterowania", new Font("fonts/arial.ttf"), tipsize));
                if (i == 14)
                    texty.Add(new Text("Zmień \"czułość klawiatury\" ", new Font("fonts/arial.ttf"), tipsize));
                if (i == 15)
                    texty.Add(new Text("Zmień głośność muzyki", new Font("fonts/arial.ttf"), tipsize));

                texty[i].Position = new Vector2f((uint)(pomX * 0.08), (uint)(pomY * 0.833333));
                texty[i].Color = new Color(Color.White);
            }

            NewGame();
        }

        private void NewGame()
        {
            startNewGame = false;


            hero = null;
            hero = new Hero(window, (uint)(window.Size.X * 0.8203125), (uint)(window.Size.Y * 0.5222222), new Color(255, 195, 77), (int)window.Size.X, (int)window.Size.Y, true, sterowanie);

            menuhero = (Hero)hero.Clone();

            ball = new Ball((int)window.Size.X, (int)window.Size.Y);

            Console.Clear();
        }

        protected override void Tick()
        {
            numberofframe++;
            if (numberofframe >= 60)
            {
                numberofframe -= 60;
            }
            

            if (gamestarted == true)
            {
                //Grywalne

                numberofframe++;
                if (numberofframe > 60)
                    numberofframe -= 60;



                int cotamzwracasz = hero.Tick(true, numberofframe, ball);
                if (cotamzwracasz == 2)
                {
                    startNewGame = true;
                }
                else if (cotamzwracasz == 1)
                {
                    ball = null;
                    ball = new Ball((int)window.Size.X, (int)window.Size.Y);
                }
                else if (cotamzwracasz == 3)
                {
                    ball = null;
                }



                menuhero = (Hero)hero.Clone();
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.SetMouseCursorVisible(true);
                    gamestarted = false;

                    menuhero.position = new Vector2f((uint)(window.Size.X * 0.8203125), (uint)(window.Size.Y * 0.5222222));
                    menuhero.Go('x', 0, (int)window.Size.X, (int)window.Size.Y);
                }

                List<Physical_object> objekty = new List<Physical_object>();
                objekty.Add(hero);
                objekty.Add(ball);

                rydzykFizyk.Gravitation(objekty);
                try
                {
                    ball.kolo.Position = ball.position;
                }
                catch
                {

                }
                    this.hero.Go('x', 0, 0, 0);

                //koniec grywalnego
            }
            else
            {
                hero.Tick(false, numberofframe, ball);


                if (startNewGame == true)
                {
                    Console.WriteLine("Tworzenie nowej gry...");

                    NewGame();
                }

                //Tutaj zaczyna się obsługa przycisków menu
                foreach (Button element in mainmenu)
                {
                    if (element.tekst.DisplayedString.Equals("Graj") && element.DoAction())
                    {
                        gamestarted = true;
                        showTip = 0;
                        enableOptions = 0;

                        window.SetMouseCursorVisible(false);
                        hero.Go('x', 0, 0, 0);
                    }
                    if (element.tekst.DisplayedString.Equals("Nowa") && element.DoAction())
                    {
                        startNewGame = true;
                    }
                    if (element.tekst.DisplayedString.Equals("Pokaż wskazówkę") && element.DoAction() )
                    {
                        showTip++;
                        if (showTip > 4)
                            showTip = -1;
                    }
                    if (element.tekst.DisplayedString.Equals("Opcje") && element.DoAction())
                    {
                        enableOptions = 3;
                    }

                    if (element.tekst.DisplayedString.Equals("Wyjdź") && element.DoAction())
                    {
                        window.Close();
                    }


                }



                if (enableOptions >= 1)
                {
                    foreach (Button element in optionbar)
                    {
                        if (element.tekst.DisplayedString.Equals("S") && element.DoAction())
                        {
                            enableOptions = 1;
                            showTip = 13;
                        }
                        if (element.tekst.DisplayedString.Equals("M") && element.DoAction())
                        {
                            enableOptions = 2;
                            showTip = 12;
                        }
                        if (element.tekst.DisplayedString.Equals("G") && element.DoAction())
                        {
                            enableOptions = 3;
                            showTip = 11;
                        }
                        if (element.tekst.DisplayedString.Equals("X") && element.DoAction())
                        {
                            enableOptions = 0;
                        }

                    }
                    if (enableOptions == 1)
                    {
                        foreach (Button element in options1)
                        {
                            if (element.tekst.DisplayedString.Equals("W - A - S - D") && element.DoAction())
                            {
                                element.ChangeText("Strzałki");
                                sterowanie = 0;
                                hero.Changemovement(sterowanie);
                                showTip = 6;
                            }

                            if (element.tekst.DisplayedString.Equals("Strzałki") && element.DoAction())
                            {
                                element.ChangeText("WSAD + Strzałki");
                                sterowanie = 2;
                                hero.Changemovement(sterowanie);
                                showTip = 6;
                            }

                            if (element.tekst.DisplayedString.Equals("WSAD / Strzałki") && element.DoAction())
                            {
                                element.ChangeText("Myszka");
                                sterowanie = 3;
                                hero.Changemovement(sterowanie);
                                showTip = 6;
                            }
                            if (element.tekst.DisplayedString.Equals("Myszka") && element.DoAction())
                            {
                                element.ChangeText("WSAD / Strzałki");
                                sterowanie = 2;
                                hero.Changemovement(sterowanie);
                                showTip = 6;
                            }

                            if (element.tekst.DisplayedString.Equals("Zamknij") && element.DoAction())
                            {
                                enableOptions = 0;
                            }


                            if (element.tekst.DisplayedString.Equals("Czułość") && element.DoAction())
                            {
                                showTip = 14;
                            }
                            if (element.tekst.DisplayedString.Equals("-") && element.DoAction())
                            {
                                showTip = 14;

                                difficulty[2]--;
                                if (difficulty[2] < 0)
                                    difficulty[2] = 0;
                                Console.WriteLine("Czułość = " + difficulty[2]);

                                textSteps.DisplayedString = Convert.ToString(difficulty[2]);

                            }
                            if (element.tekst.DisplayedString.Equals("+") && element.DoAction())
                            {
                                showTip = 14;

                                difficulty[2]++;
                                Console.WriteLine("Czułość = " + difficulty[2]);

                                textSteps.DisplayedString = Convert.ToString(difficulty[2]);
                            }
                        }
                    }
                    if (enableOptions == 2)
                    {
                        foreach (Button element in options2)
                        {
                            if (element.tekst.DisplayedString.Equals("Muzyka") && element.DoAction())
                            {
                                showTip = 7;

                                if (musicEnabled)
                                {
                                    music.Pause();
                                    musicEnabled = false;
                                    textMusicEnable.DisplayedString = Convert.ToString(musicEnabled);
                                    Console.WriteLine("Muzyka wyłączona [*]");
                                }
                                else
                                {
                                    music.Play();
                                    musicEnabled = true;
                                    textMusicEnable.DisplayedString = Convert.ToString(musicEnabled);
                                    Console.WriteLine("Muzyka włączona (y)");
                                }
                            }
                            if (element.tekst.DisplayedString.Equals("Głośność") && element.DoAction())
                            {
                                showTip = 15;
                            }
                            if (element.tekst.DisplayedString.Equals("-") && element.DoAction())
                            {
                                showTip = 15;

                                music.Volume--;
                                if (music.Volume < 0)
                                    music.Volume = 0;
                                Console.WriteLine("Głośność muzyki = " + music.Volume);

                                textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);

                            }
                            if (element.tekst.DisplayedString.Equals("+") && element.DoAction())
                            {
                                showTip = 15;

                                music.Volume++;
                                if (music.Volume > 100)
                                    music.Volume = 100;
                                Console.WriteLine("Głośność muzyki = " + music.Volume);

                                textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);
                            }

                            if (element.tekst.DisplayedString.Equals("Zamknij") && element.DoAction())
                            {
                                enableOptions = 0;
                            }
                        }
                    }
                    if (enableOptions == 3)
                    {
                        foreach (Button element in options3)
                        {
                            

                            if (element.tekst.DisplayedString.Equals("Wskaźnik zasięgu") && element.DoAction())
                            {
                                showTip = 9;
                                if(enableRangeWskaznik)
                                {
                                    enableRangeWskaznik = false;
                                    textRange.DisplayedString = Convert.ToString(enableRangeWskaznik);
                                }
                                else
                                {
                                    enableRangeWskaznik = true;
                                    textRange.DisplayedString = Convert.ToString(enableRangeWskaznik);
                                }
                            }

                            if (element.tekst.DisplayedString.Equals("Zasięg") && element.DoAction())
                            {
                                showTip = 5;
                            }
                            if (element.tekst.DisplayedString.Equals("-") && (element.id == 1) )
                            {

                                if (element.DoAction())
                                {
                                    showTip = 5;

                                    difficulty[0]--;
                                    if (difficulty[0] < 0)
                                        difficulty[0] = 0;
                                    Console.WriteLine("Trudność = " + difficulty[0]);

                                    textDifficulty.DisplayedString = Convert.ToString(difficulty[0]);
                                }
                            }
                            if (element.tekst.DisplayedString.Equals("+") && (element.id == 1) )
                            {
                                if ( element.DoAction())
                                {
                                    showTip = 5;

                                    difficulty[0]++;
                                    Console.WriteLine("Trudność = " + difficulty[0]);

                                    textDifficulty.DisplayedString = Convert.ToString(difficulty[0]);
                                }
                            }
                            if (element.tekst.DisplayedString.Equals("Grawitacja") && element.DoAction())
                            {
                                showTip = 8;
                            }
                            if (element.tekst.DisplayedString.Equals("-") &&  (element.id == 2) )
                            {
                                if (element.DoAction())
                                {
                                    showTip = 8;

                                    difficulty[1]--;
                                    if (difficulty[1] < 0)
                                        difficulty[1] = 0;

                                    Console.WriteLine("Grawitacja = " + difficulty[1]);

                                    textGravity.DisplayedString = Convert.ToString(difficulty[1]);
                                }
                            }
                            if (element.tekst.DisplayedString.Equals("+") && (element.id == 2))
                            {
                                if (element.DoAction())
                                {
                                    showTip = 8;

                                    difficulty[1]++;

                                    Console.WriteLine("Grawitacja = " + difficulty[1]);

                                    textGravity.DisplayedString = Convert.ToString(difficulty[1]);
                                }
                            }
                            if (element.tekst.DisplayedString.Equals("Przyciąganie") && element.DoAction())
                            {
                                showTip = 10;
                                enableGravity = false;
                                element.ChangeText("Połykanie");
                            }
                            if (element.tekst.DisplayedString.Equals("Połykanie") && element.DoAction())
                            {
                                showTip = 10;
                                enableGravity = true;
                                element.ChangeText("Przyciąganie");

                            }
                        }
                    }
                }

                hero.additionalRange = difficulty[0];
                hero.gravityStrength = difficulty[1];
                hero.step = difficulty[2]/10;
                hero.enableRange = enableRangeWskaznik;
                hero.enableGravity = enableGravity;
            }






            menuhero.Tick(false, numberofframe, ball);

        }

        protected override void Render()
        {
            if (gamestarted == true)
            {
                // window.Draw(map);
                if (ball != null)
                    window.Draw(ball.kolo);

                hero.Draw();
            }
            else
            {
                menuhero.Draw();


                foreach (Button element in mainmenu)
                    element.Draw();

                window.Draw(Gamename2);
                window.Draw(Gamename);
                if ( (showTip >= 0) && (showTip <= texty.Count))
                    window.Draw(texty[showTip]);

                if (enableOptions >= 1)
                {
                    window.Draw(linia);

                    foreach (Button element in optionbar)
                        element.Draw();

                    if (enableOptions == 1)
                    {
                        foreach (Button element in options1)
                            element.Draw();

                        window.Draw(textSteps);
                    }

                    if (enableOptions == 2)
                    {
                        foreach (Button element in options2)
                            element.Draw();

                        window.Draw(textMusicVolume);
                        window.Draw(textMusicEnable);
                    }
                    if (enableOptions == 3)
                    {
                        foreach (Button element in options3)
                            element.Draw();

                        window.Draw(textDifficulty);
                        window.Draw(textGravity);
                        window.Draw(textRange);
                    }
                }

            }

        }

    }
}
