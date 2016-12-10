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
        Text textt0, textt1, textt2, textt3, textt4, textt5, textt6, textt7, Gamename, textDifficulty;
        RectangleShape linia;
        Image image;


        Music music;
        bool musicEnabled;

        int numberofframe;
        public bool gamestarted;
        static System.Collections.ArrayList mainmenu, options;
        int showTip;
        bool enableOptions;
        const uint screenX = 1280, screenY = 720;

        int cooldown; //czas odnowienia przycisków

        int sterowanie;
        int difficulty;



        public RPG()
            : base(screenX, screenY, "Żryj komety", Color.Black)
        {
        }

        protected override void LoadContent()
        {
            //tileset = new Texture("Content/tileset.png");
            music = new Music("content/music2.ogg");
            image = new Image("img/icon.ico");
        }

        protected override void Initialize()
        {

            window.SetIcon(image.GetWidth(), image.GetHeight(), image.GetPixelsPtr());

            sterowanie = 2;
            difficulty = 10;

            cooldown = 0;

            musicEnabled = false;
            //music.Play();

            enableOptions = false;
            showTip = 8;
            Gamename = new Text();
            Gamename.DisplayedString = "Żryj komety";
            Gamename.Font = new Font("fonts/arial.ttf");
            Gamename.Position = new Vector2f(100f, 30f);
            Gamename.Color = new Color(138, 7, 7);
            Gamename.CharacterSize = 100;

            linia = new RectangleShape(new Vector2f(1, 300));
            linia.Position = new Vector2f(450, 232);

            uint buttontextsize = 30;
            Color buttonscolor = new Color(127, 112, 0);
            mainmenu = new System.Collections.ArrayList();
            mainmenu.Add(new Button(100, 200, 300, 64, "Graj", window, buttonscolor, buttontextsize));
            mainmenu.Add(new Button(100, 300, 300, 64, "Opcje", window, buttonscolor, buttontextsize));
            mainmenu.Add(new Button(100, 400, 300, 64, "Pokaż wskazówkę", window, buttonscolor, buttontextsize));
            mainmenu.Add(new Button(100, 500, 300, 64, "Wyjdź", window, buttonscolor, buttontextsize));

            buttonscolor = new Color(69, 69, 69);
            options = new System.Collections.ArrayList();
            options.Add(new Button(500, 200, 300, 64, "WSAD + Strzałki", window, buttonscolor, buttontextsize));
            options.Add(new Button(500, 310, 40, 44, "-", window, buttonscolor, buttontextsize));
            options.Add(new Button(550, 300, 200, 64, "Trudność", window, buttonscolor, buttontextsize));
            options.Add(new Button(760, 310, 40, 44, "+", window, buttonscolor, buttontextsize));
            options.Add(new Button(500, 400, 300, 64, "Muzyka", window, buttonscolor, buttontextsize));
            options.Add(new Button(500, 500, 300, 64, "Zamknij", window, buttonscolor, buttontextsize));

            textDifficulty = new Text("0", new Font("fonts/arial.ttf"), 50);
            textDifficulty.Position = new Vector2f(810, 300);
            textDifficulty.Color = new Color(Color.White);
            textDifficulty.DisplayedString = Convert.ToString(difficulty);


            gamestarted = false;

            numberofframe = 0;
            // map = new Tilemap(tileset, 24, 18, 16.0f, 32.0f );

            uint tipsize = 16;

            textt0 = new Text("Poruszanie - dostosuj w opcjach", new Font("fonts/arial.ttf"), tipsize);
            textt0.Position = new Vector2f(100f, 600f);
            textt0.Color = new Color(Color.White);


            textt1 = new Text("Co 10 zdobytych komet otrzymujesz planetę", new Font("fonts/arial.ttf"), tipsize);
            textt1.Position = new Vector2f(100f, 600f);
            textt1.Color = new Color(Color.White);

            textt2 = new Text("Twoja gwiazda ewoluuje", new Font("fonts/arial.ttf"), tipsize);
            textt2.Position = new Vector2f(100f, 600f);
            textt2.Color = new Color(Color.White);

            textt3 = new Text("ESC cofa do menu głównego", new Font("fonts/arial.ttf"), tipsize);
            textt3.Position = new Vector2f(100f, 600f);
            textt3.Color = new Color(Color.White);

            textt4 = new Text("Po ukończeniu gry możesz zacząć od nowa z poziomu menu głównego", new Font("fonts/arial.ttf"), tipsize);
            textt4.Position = new Vector2f(100f, 600f);
            textt4.Color = new Color(Color.White);


            textt5 = new Text("Trudność oznacza dodatkowy zasięg chwytania komet", new Font("fonts/arial.ttf"), tipsize);
            textt5.Position = new Vector2f(100f, 600f);
            textt5.Color = new Color(Color.White);

            textt6 = new Text("Zmienia ustawienia sterowania", new Font("fonts/arial.ttf"), tipsize);
            textt6.Position = new Vector2f(100f, 600f);
            textt6.Color = new Color(Color.White);


            textt7 = new Text("Włącz / Wyłącz  muzykę", new Font("fonts/arial.ttf"), tipsize);
            textt7.Position = new Vector2f(100f, 600f);
            textt7.Color = new Color(Color.White);


            NewGame();
        }

        private void NewGame()
        {
            startNewGame = false;


            hero = null;
            hero = new Hero(window, 1050, 376, new Color(255, 195, 77), (int)screenX, (int)screenY, true, sterowanie);

            menuhero = (Hero)hero.Clone();

            ball = new Ball((int)screenX, (int)screenY);

            Console.Clear();
        }

        protected override void Tick()
        {
            numberofframe++;
            if (numberofframe >= 60)
            {
                numberofframe -= 60;
            }

            if (cooldown > 0)
                cooldown--;

            if (gamestarted == true)
            {
                //Grywalne

                numberofframe++;
                if (numberofframe > 60)
                    numberofframe -= 60;



                int cotamzwracasz = hero.Tick(true, numberofframe, ball, difficulty);
                if (cotamzwracasz == 2)
                {
                    startNewGame = true;
                }
                else if (cotamzwracasz == 1)
                {
                    ball = null;
                    ball = new Ball((int)screenX, (int)screenY);
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

                    menuhero.position = new Vector2f(1050, 376);
                    menuhero.Go('x', 0, (int)screenX, (int)screenY);
                }


                //koniec grywalnego
            }
            else
            {
                hero.Tick(false, numberofframe, ball, difficulty);


                if (startNewGame == true)
                {
                    Console.WriteLine("Tworzenie nowej gry...");

                    NewGame();
                }

                foreach (Button element in mainmenu)
                {
                    if (element.tekst.DisplayedString.Equals("Graj") && element.DoAction())
                    {
                        gamestarted = true;
                        showTip = 0;
                        enableOptions = false;

                        window.SetMouseCursorVisible(false);
                        hero.Go('x', 0, 0, 0);
                    }
                    if (element.tekst.DisplayedString.Equals("Pokaż wskazówkę") && element.DoAction() && cooldown == 0)
                    {
                        cooldown = 30;

                        showTip++;
                        if (showTip > 4)
                            showTip = -1;
                    }
                    if (element.tekst.DisplayedString.Equals("Opcje") && element.DoAction())
                    {
                        enableOptions = true;
                    }

                    if (element.tekst.DisplayedString.Equals("Wyjdź") && element.DoAction())
                    {
                        window.Close();
                    }
                }



                if (enableOptions == true)
                {
                    foreach (Button element in options)
                    {
                        if (element.tekst.DisplayedString.Equals("Muzyka") && element.DoAction() && cooldown == 0)
                        {
                            showTip = 7;
                            cooldown = 30;

                            if (musicEnabled)
                            {
                                music.Pause();
                                musicEnabled = false;
                                Console.WriteLine("Muzyka wyłączona [*]");
                            }
                            else
                            {
                                music.Play();
                                musicEnabled = true;
                                Console.WriteLine("Muzyka włączona (y)");
                            }
                        }

                        if (element.tekst.DisplayedString.Equals("W - A - S - D") && element.DoAction() && cooldown == 0)
                        {
                            cooldown = 45;
                            element.ChangeText("Strzałki");
                            sterowanie = 0;
                            hero.Changemovement(sterowanie);
                            showTip = 6;
                        }

                        if (element.tekst.DisplayedString.Equals("Strzałki") && element.DoAction() && cooldown == 0)
                        {
                            cooldown = 45;
                            element.ChangeText("WSAD + Strzałki");
                            sterowanie = 2;
                            hero.Changemovement(sterowanie);
                            showTip = 6;
                        }

                        if (element.tekst.DisplayedString.Equals("WSAD + Strzałki") && element.DoAction() && cooldown == 0)
                        {
                            cooldown = 45;
                            element.ChangeText("Myszka");
                            sterowanie = 3;
                            hero.Changemovement(sterowanie);
                            showTip = 6;
                        }
                        if (element.tekst.DisplayedString.Equals("Myszka") && element.DoAction() && cooldown == 0)
                        {
                            cooldown = 45;
                            element.ChangeText("WSAD + Strzałki");
                            sterowanie = 2;
                            hero.Changemovement(sterowanie);
                            showTip = 6;
                        }

                        if (element.tekst.DisplayedString.Equals("Zamknij") && element.DoAction())
                        {
                            enableOptions = false;
                        }


                        if (element.tekst.DisplayedString.Equals("Trudność") && element.DoAction())
                        {
                            showTip = 5;
                        }
                        if (element.tekst.DisplayedString.Equals("-") && element.DoAction() && cooldown == 0)
                        {
                            cooldown = 15;
                            difficulty--;
                            if (difficulty < 0)
                                difficulty = 0;
                            Console.WriteLine("Trudność = " + difficulty);

                            textDifficulty.DisplayedString = Convert.ToString(difficulty);

                        }
                        if (element.tekst.DisplayedString.Equals("+") && element.DoAction() && cooldown == 0)
                        {
                            cooldown = 15;
                            difficulty++;
                            Console.WriteLine("Trudność = " + difficulty);

                            textDifficulty.DisplayedString = Convert.ToString(difficulty);
                        }
                    }
                }
            }






            menuhero.Tick(false, numberofframe, ball, difficulty);

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
                    if (element.Draw() == false)
                        break;

                window.Draw(Gamename);
                if (showTip == 0)
                    window.Draw(textt0);
                else if (showTip == 1)
                    window.Draw(textt1);
                else if (showTip == 2)
                    window.Draw(textt2);
                else if (showTip == 3)
                    window.Draw(textt3);
                else if (showTip == 4)
                    window.Draw(textt4);
                else if (showTip == 5)
                    window.Draw(textt5);
                else if (showTip == 6)
                    window.Draw(textt6);
                else if (showTip == 7)
                    window.Draw(textt7);

                if (enableOptions)
                {
                    window.Draw(linia);

                    foreach (Button element in options)
                        if (element.Draw() == false)
                            break;
                    window.Draw(textDifficulty);
                }

            }

        }

    }
}
