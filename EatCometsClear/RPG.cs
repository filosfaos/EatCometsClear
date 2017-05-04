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
        /// <summary><param name="startNewGame">określa, czy gra ma zostać uruchomiona ponownie przy kolejnym obiegu głównej pętli</param></summary>
        public bool startNewGame { get; set; }
        /// <summary><param name="hero">obiekt reprezentujący gracza</param></summary>
        Hero hero;
        /// <summary><param name="menuhero">obiekt reprezentujący wizualizację gracza w menu głównym</param></summary>
        Hero menuhero;
        /// <summary><param name="ball">tablica obiektów reprezentująca komety</param></summary>
        Ball[] ball;

        Music music;
        bool musicEnabled;
        Sound clickSound;
        bool clickSoundEnable;
        Sound collectSound;
        bool collectSoundEnable;
        Sound hoverSound;
        bool hoverSoundEnable;
        bool hoverSoundStop;
        bool AdditionaInfosHero;

        int numberofframe;
        public bool gamestarted { get; set; }
        private int showTip { get; set; }
        int enableOptions;
        int lastOption;
        bool escBlock;

        int sterowanie;
        int[] difficulty;

        Text tipText;

        Physic physicCalc;
        RectBlock[] windowBounds;


        bool enableRangeWskaznik;// enableGravity;
        bool enableMusic;

        Image ikona;
        Texture liniaload, sunGif;

        HeadUpDisplay mainMenuHUD, options1HUD, options2HUD, options3HUD, options4HUD, optionbarHUD;

        MyConfig configurancja;

        public RPG()
            : base("Żryj komety", Color.Black)
        {
        }

        protected override void LoadContent()
        {
            //wczytywanie plików gry

            /*
            try
            {
                int pomX = (int)(window.Size.X*0.34);
                int pomY = (int)(window.Size.Y*0.34);
                Vector2i size = new Vector2i((int)(window.Size.X * 0.05), (int)(window.Size.Y * 0.05));
                
            .WriteLine("liapa" + size.X + "gurwa" + size.Y);
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
            try
            {
                music = new Music("content/music.ogg");
                enableMusic = true;
            }
            catch
            {
                enableMusic = false;
            }

            try
            {
                SoundBuffer xD = new SoundBuffer("sounds/click.wav");
                clickSound = new Sound(xD);
                clickSound.Volume = 15;
                clickSoundEnable = true;
            }
            catch
            {
                clickSoundEnable = false;
            }
            try
            {
                SoundBuffer xD = new SoundBuffer("sounds/collect.wav");
                collectSound = new Sound(xD);
                collectSound.Volume = 15;
                collectSoundEnable = true;
            }
            catch
            {
                collectSoundEnable = false;
            }
            try
            {
                SoundBuffer xD = new SoundBuffer("sounds/hover.wav");
                hoverSound = new Sound(xD);
                hoverSound.Volume = 15;
                hoverSoundEnable = true;
            }
            catch
            {
                collectSoundEnable = false;
            }

            try
            {
                ikona = new Image("img/icon.png");
            }
            catch { }
            try
            {
                liniaload = new Texture("img/optionbarbg.png");
            }
            catch { }
        }

        protected override void Initialize()
        {



            //inicjalizacja zmiennych programu

            configurancja = new MyConfig();


            physicCalc = new Physic(); //on liczy fizyke fizyk jeden
            ball = new Ball[5];



            enableRangeWskaznik = false;
            //enableGravity = false;


            sterowanie = 3;
            difficulty = new int[3] { 10, 1000, 100 };
            gamestarted = false;
            numberofframe = 0;

            lastOption = 1;

            if (music != null)
            {
                musicEnabled = false;
                music.Loop = true;
                music.Volume = 10.5f;
                //music.Play();
            }
            enableOptions = 0;
            showTip = 0;

            CreateWindow(gameTitle);
            BuidGUI();

            SetButtonsActions();
            NewGame();
        }

        private void RebuidGUI()
        {
            mainMenuHUD = null;
            optionbarHUD = null;
            options1HUD = null;
            options2HUD = null;
            options3HUD = null;
            options4HUD = null;

            tipText = null;

            BuidGUI();
            SetButtonsActions();

        }

        private void BuidGUI()
        {
            uint pomX = window.Size.X;
            uint pomY = window.Size.Y;

            uint buttontextsize = (uint)(pomX * 0.024);
            uint texttextsize = (uint)(pomY * 0.069);

            if (window.Size.X == 1920)
                buttontextsize = (uint)(pomY * 0.035);


            if (window.Size.X == 1280)
                buttontextsize = (uint)(pomX * 0.024);


            if (window.Size.X == 1024)
                buttontextsize = (uint)(pomX * 0.030);

            if (window.Size.X == 800)
                buttontextsize = (uint)(pomX * 0.020);

            Color buttonscolor = new Color(127, 112, 0);
            System.Collections.ArrayList hudelements = new System.Collections.ArrayList();
            hudelements = new System.Collections.ArrayList();
            hudelements.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.25), (uint)(pomX * 0.15), (uint)(pomY * 0.1), "Graj", window, buttonscolor, buttontextsize, 101));
            hudelements.Add(new Button((uint)(pomX * 0.24), (uint)(pomY * 0.25), (uint)(pomX * 0.09), (uint)(pomY * 0.1), "Nowa", window, new Color(200, 128, 64), buttontextsize, 102));
            hudelements.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.40), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Opcje", window, buttonscolor, buttontextsize, 103));
            hudelements.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Pokaż wskazówkę", window, buttonscolor, buttontextsize, 104));
            hudelements.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Wyjdź", window, buttonscolor, buttontextsize, 105));

            mainMenuHUD = new HeadUpDisplay();
            foreach (Button element in hudelements)
            {
                mainMenuHUD.AddElement(element);
            }
            Caption hudtext;

            hudtext = new Caption(new Text("Żryj komety", new Font("fonts/arial.ttf"), (uint)(pomY * 0.151)), 0, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.028), (uint)(pomY * 0.0305));
            hudtext.text.Color = new Color(255, 128, 128);
            mainMenuHUD.AddElement(hudtext);


            hudtext = new Caption(new Text("Żryj komety", new Font("fonts/arial.ttf"), (uint)(pomY * 0.15)), 0, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.03), (uint)(pomY * 0.03));
            hudtext.text.Color = new Color(138, 7, 7);
            mainMenuHUD.AddElement(hudtext);

            /*
            buttonscolor = new Color(69, 255, 69);
            optionbar = new System.Collections.ArrayList();
            optionbar.Add(new ButtonImg((uint)(pomX * 0.40), (uint)(pomY * 0.25), (uint)(pomX * 0.03), (uint)(pomY * 0.06), window, buttonscolor, (Texture)butimg[0]));
            optionbar.Add(new ButtonImg((uint)(pomX * 0.40), (uint)(pomY * 0.55), (uint)(pomX * 0.03), (uint)(pomY * 0.06), window, buttonscolor, (Texture)butimg[1]));
            optionbar.Add(new ButtonImg((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.03), (uint)(pomY * 0.06), window, buttonscolor, (Texture)butimg[2]));
            */

            /*
            RectangleShape linia = new RectangleShape(new Vector2f(2, (uint)(pomY * 0.45)));
            linia.Position = new Vector2f((uint)(pomX * 0.365), (uint)(pomY * 0.3222));
            linia.FillColor = new Color(255, 255, 255);
            */

            buttonscolor = new Color(69, 128, 69);
            hudelements.Clear();
            hudelements = new System.Collections.ArrayList();
            hudelements.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.265), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "S", window, buttonscolor, buttontextsize, 201));
            buttonscolor = new Color(128, 69, 69);
            hudelements.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.415), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "M", window, buttonscolor, buttontextsize, 202));
            buttonscolor = new Color(69, 69, 128);
            hudelements.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.565), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "G", window, buttonscolor, buttontextsize, 203));
            buttonscolor = new Color(54, 65, 53);
            hudelements.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.715), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "O", window, buttonscolor, buttontextsize, 204));

            hudtext = new Caption(new Text("[__]", new Font("fonts/arial.ttf"), buttontextsize), 911, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.347), (uint)(pomY * 0.277));
            hudtext.text.Color = new Color(Color.White);

            Sprite linia;
            linia = new Sprite();
            linia.Texture = liniaload;
            linia.TextureRect = new IntRect(new Vector2i((int)(pomX * 0.365), (int)(pomY * 0.3222)), new Vector2i((int)(pomX * 0.025), (int)(pomY * 0.45)));
            linia.Position = new Vector2f((int)(pomX * 0.35), (int)(pomY * 0.3222));
            optionbarHUD = new HeadUpDisplay();
            optionbarHUD.AddElement(hudtext);
            optionbarHUD.AddElement(new StaticImage(window, linia));
            foreach (Button element in hudelements)
            {
                optionbarHUD.AddElement(element);
            }
            optionbarHUD.AddElement(hudtext);

            buttonscolor = new Color(69, 128, 69);
            hudelements.Clear();
            hudelements = new System.Collections.ArrayList();
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.25), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Myszka", window, buttonscolor, buttontextsize, 301));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 302));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Czułość", window, buttonscolor, buttontextsize, 303));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 304));

            options1HUD = new HeadUpDisplay();
            foreach (Button element in hudelements)
            {
                options1HUD.AddElement(element);
            }

            buttonscolor = new Color(128, 69, 69);
            hudelements.Clear();
            hudelements = new System.Collections.ArrayList();
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.25), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Muzyka", window, buttonscolor, buttontextsize, 401));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 402));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Głośność", window, buttonscolor, buttontextsize, 403));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 404));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Dźwięki", window, buttonscolor, buttontextsize, 405));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.72), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 406));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.70), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Głośność", window, buttonscolor, buttontextsize, 407));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.72), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 408));

            options2HUD = new HeadUpDisplay();
            foreach (Button element in hudelements)
            {
                options2HUD.AddElement(element);
            }

            buttonscolor = new Color(69, 69, 128);
            hudelements.Clear();
            hudelements = new System.Collections.ArrayList();
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 501));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.25), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Zasięg", window, buttonscolor, buttontextsize, 502));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 503));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 504));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Grawitacja", window, buttonscolor, buttontextsize, 505));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 506));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Informacje w konsoli", window, buttonscolor, buttontextsize, 508));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Wskaźnik zasięgu", window, buttonscolor, buttontextsize, 507));

            options3HUD = new HeadUpDisplay();
            foreach (Button element in hudelements)
            {
                options3HUD.AddElement(element);
            }

            string qwe;
            qwe = Convert.ToString(window.Size.X);
            qwe += "x";
            qwe += Convert.ToString(window.Size.Y);

            buttonscolor = new Color(54, 65, 53);
            hudelements.Clear();
            hudelements = new System.Collections.ArrayList();
            options4HUD = new HeadUpDisplay();
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize, 601));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.25), (uint)(pomX * 0.17), (uint)(pomY * 0.10), qwe, window, buttonscolor, buttontextsize, 602));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 603));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.40), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Tryb okna", window, buttonscolor, buttontextsize, 604));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.17), (uint)(pomY * 0.1), "Zapisz", window, buttonscolor, buttontextsize, 605));
            hudelements.Add(new Button((uint)(pomX * 0.60), (uint)(pomY * 0.715), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "R", window, new Color(128, 54, 13), buttontextsize, 606));

            hudtext = new Caption(new Text("pełny ekran", new Font("fonts/arial.ttf"), texttextsize), 14, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
            hudtext.text.Color = new Color(Color.White);
            hudtext.text.DisplayedString = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["WindowMode"]);
            options4HUD.AddElement(hudtext);

            foreach (Button element in hudelements)
            {
                if (element.tekst.DisplayedString.Equals(qwe))
                    element.DoAction();
                options4HUD.AddElement(element);
            }

            hudtext = new Caption(new Text("0", new Font("fonts/arial.ttf"), texttextsize), 1234, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.27));
            hudtext.text.Color = new Color(Color.White);
            hudtext.text.DisplayedString = Convert.ToString(difficulty[0]);
            options3HUD.AddElement(hudtext);

            hudtext = null;
            hudtext = new Caption(new Text("0", new Font("fonts/arial.ttf"), texttextsize), 1233, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
            hudtext.text.Color = new Color(Color.White);
            hudtext.text.DisplayedString = Convert.ToString(difficulty[1]);
            options3HUD.AddElement(hudtext);

            hudtext = null;
            hudtext = new Caption(new Text("true", new Font("fonts/arial.ttf"), texttextsize), 1235, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.55));
            hudtext.text.Color = new Color(Color.White);
            hudtext.text.DisplayedString = Convert.ToString(AdditionaInfosHero);
            options3HUD.AddElement(hudtext);

            hudtext = null;
            hudtext = new Caption(new Text("0", new Font("fonts/arial.ttf"), texttextsize), 1232, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.72));
            hudtext.text.Color = new Color(Color.White);
            hudtext.text.DisplayedString = Convert.ToString(enableRangeWskaznik);
            options3HUD.AddElement(hudtext);

            hudtext = null;
            hudtext = new Caption(new Text("0", new Font("fonts/arial.ttf"), texttextsize), 25, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
            hudtext.text.Color = new Color(Color.White);
            hudtext.text.DisplayedString = Convert.ToString(difficulty[2]);
            options1HUD.AddElement(hudtext);

            if (enableMusic)
            {

                hudtext = null;
                hudtext = new Caption(new Text("0", new Font("fonts/arial.ttf"), texttextsize), 24, window);
                hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
                hudtext.text.Color = new Color(Color.White);
                hudtext.text.DisplayedString = Convert.ToString((int)music.Volume);
                options2HUD.AddElement(hudtext);

                hudtext = null;
                hudtext = new Caption(new Text("0", new Font("fonts/arial.ttf"), texttextsize), 23, window);
                hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.27));
                hudtext.text.Color = new Color(Color.White);
                hudtext.text.DisplayedString = Convert.ToString(musicEnabled);
                options2HUD.AddElement(hudtext);
            }

            if (clickSoundEnable)
            {

                hudtext = null;
                hudtext = new Caption(new Text("0", new Font("fonts/arial.ttf"), texttextsize), 22, window);
                hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.70));
                hudtext.text.Color = new Color(Color.White);
                hudtext.text.DisplayedString = Convert.ToString((int)clickSound.Volume);
                options2HUD.AddElement(hudtext);
            }


            //uint tipsize = 16;

            tipText = new Text("", new Font("fonts/arial.ttf"), (uint)(pomY * 0.022222));
            tipText.Position = new Vector2f((uint)(pomX * 0.08), (uint)(pomY * 0.833333));
            tipText.Color = new Color(Color.White);

        }

        private void SetButtonsActions()
        {

            foreach (Button element in mainMenuHUD.GetButtons())
            {
                //menu główne
                if (element.id == 101)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Przejdź do gry"; };
                    element.onClick = delegate ()
                    {
                        //zmienia stan gry z menu na gre
                        hero.IsPlaying = true;
                        gamestarted = true;
                        showTip = 0;
                        tipText.DisplayedString = "";
                        enableOptions = 0;

                        window.SetMouseCursorVisible(false);
                        hero.Go('x', 0, 0, 0);
                    };
                    element.onRightClick = delegate ()
                    {
                        tipText.DisplayedString = window.ToString();
                    };
                }
                if (element.id == 102)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Rozpocznij nową grę"; };
                    element.onClick = delegate ()
                    {
                        //przycisk nowej gry
                        startNewGame = true;
                    };
                }
                if (element.id == 104)
                {
                    element.hoverAction = delegate ()
                    {


                        switch (showTip)
                        {
                            case 0:
                                {
                                    tipText.DisplayedString = "";
                                }
                                break;

                            case 1:
                                {
                                    tipText.DisplayedString = "Co 10 zdobytych komet otrzymujesz planetę";
                                }
                                break;

                            case 2:
                                {
                                    tipText.DisplayedString = "Kliknięcie TAB wyświetla menu postaci";
                                }
                                break;
                            case 3:
                                {
                                    tipText.DisplayedString = "Poruszasz się myszką lub klawiszami WSAD/strzałki";
                                }
                                break;
                            case 4:
                                {
                                    tipText.DisplayedString = "ESC cofa do menu głównego";
                                }
                                break;
                            case 5:
                                {
                                    tipText.DisplayedString = "Q wciąga, E wypluwa planety";
                                }
                                break;
                        }
                    };
                    element.onClick = delegate ()
                    {
                        //pokazuje kolejne wskazowki
                        showTip++;
                        if (showTip > 5)
                            showTip = 0;
                    };
                }
                if (element.id == 103)
                {
                    element.hoverAction = delegate ()
                    {
                        if (enableOptions == 0)
                            tipText.DisplayedString = "Pokaż menu opcji";
                        else
                            tipText.DisplayedString = "Zamknij menu opcji";
                    };
                    element.onClick = delegate ()
                    {
                        //przycisk do włączanie menu opcji, po włączeniu zmienia się na zamknij

                        if (enableOptions == 0)
                        {
                            enableOptions = lastOption;
                        }
                        else
                        {
                            enableOptions = 0;
                        }
                    };
                    element.tick = delegate ()
                    {
                        if (enableOptions == 0)
                        {
                            element.tekst.DisplayedString = "Opcje";
                            element.SetColor(new Color(127, 112, 0));
                        }
                        else
                        {
                            element.tekst.DisplayedString = "Zamknij";
                            element.SetColor(new Color(69, 69, 69));
                        }
                    };
                }

                if (element.id == 105)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Wyłącz grę"; };
                    element.onClick = delegate ()
                    {
                        //służy do wyłączania gry
                        window.Close();
                    };
                }
            }
            foreach (Button element in optionbarHUD.GetButtons())
            {
                //panel wyboru podmenu opcji
                if (element.id == 201)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmień ustawienia sterowania"; };
                    element.onClick = delegate ()
                    {
                        //otwiera menu sterowania
                        Caption handelier;
                        handelier = (Caption)optionbarHUD.GetElementByID(911);
                        handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.347), (uint)(window.Size.Y * 0.277));

                        lastOption = 1;
                        enableOptions = 1;
                    };
                }
                if (element.id == 202)
                {
                    element.hoverAction = delegate ()
                    {
                        if (enableMusic)
                            tipText.DisplayedString = "Zmień ustawienia muzyki";
                        else
                            tipText.DisplayedString = "Nie wczytano muzyki";
                    };
                    element.onClick = delegate ()
                    {
                        //otwiera podmenu dźwięków i muzyki
                        if (enableMusic)
                        {
                            Caption handelier;
                            handelier = (Caption)optionbarHUD.GetElementByID(911);
                            handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.347), (uint)(window.Size.Y * 0.427));

                            lastOption = 2;
                            enableOptions = 2;
                        }
                    };
                }
                if (element.id == 203)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmień ustawienia gry"; };
                    element.onClick = delegate ()
                    {
                        //otwiera podmenu opcji gry

                        Caption handelier;
                        handelier = (Caption)optionbarHUD.GetElementByID(911);
                        handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.347), (uint)(window.Size.Y * 0.577));

                        lastOption = 3;
                        enableOptions = 3;
                    };
                }
                if (element.id == 204)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmień ustawienia sterowania"; };
                    element.onClick = delegate ()
                    {
                        //otwiera podmenu opcji obrazu | ekranu

                        Caption handelier;
                        handelier = (Caption)optionbarHUD.GetElementByID(911);
                        handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.347), (uint)(window.Size.Y * 0.727));

                        lastOption = 4;
                        enableOptions = 4;


                        string qwe;
                        qwe = Convert.ToString(configurancja.screenX);
                        qwe += "x";
                        qwe += Convert.ToString(configurancja.screenY);

                        foreach (Button element1 in options4HUD.GetButtons())
                        {
                            if (element1.id == 11)
                            {
                                element1.tekst.DisplayedString = qwe;
                            }
                        }
                    };
                }
            }
            foreach (Button element in options1HUD.GetButtons())
            {
                //podmenu sterownia
                if (element.tekst.DisplayedString.Equals("W - A - S - D"))
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmień ustawienia sterowania"; };
                    element.onClick = delegate ()
                    {
                        //zostawione na potem
                        element.ChangeText("Strzałki");
                        sterowanie = 0;
                        hero.Changemovement(sterowanie);
                    };
                }

                if (element.tekst.DisplayedString.Equals("Strzałki"))
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmień ustawienia sterowania"; };
                    element.onClick = delegate ()
                    {
                        //zostawione na potem
                        element.ChangeText("WSAD + Strzałki");
                        sterowanie = 2;
                        hero.Changemovement(sterowanie);
                    };
                }

                if (element.id == 301)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmień ustawienia sterowania"; };
                    element.onClick = delegate ()
                    {
                        if (element.tekst.DisplayedString.Equals("WSAD / Strzałki"))
                        {
                            //po kliknieciu zmienia sterowanie na myszke
                            element.ChangeText("Myszka");
                            sterowanie = 3;
                            hero.Changemovement(sterowanie);

                        }
                        else if (element.tekst.DisplayedString.Equals("Myszka"))
                        {
                            //po kliknieciu zmienia sterownie na klawiature
                            element.ChangeText("WSAD / Strzałki");
                            sterowanie = 2;
                            hero.Changemovement(sterowanie);
                        }
                    };
                }
                if (element.id == 303)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmień ustawienia sterowania"; };
                    element.onClick = delegate ()
                    {
                        //info
                    };
                }
                if (element.id == 302)
                {
                    element.onClick = delegate ()
                    {
                        //zmniejsza czulosc klawiatury
                        tipText.DisplayedString = "Zmień ustawienia sterowania";

                        difficulty[2]--;
                        if (difficulty[2] < 0)
                            difficulty[2] = 0;
                        Console.WriteLine("Czułość = " + difficulty[2]);

                        Caption handelier;
                        handelier = (Caption)options1HUD.GetElementByID(25);
                        handelier.text.DisplayedString = Convert.ToString(difficulty[2]);
                    };
                }
                if (element.id == 304)
                {
                    element.onClick = delegate ()
                    {
                        //zmniejsza czulość klawiatury
                        tipText.DisplayedString = "Zmień ustawienia sterowania";

                        difficulty[2]++;
                        Console.WriteLine("Czułość = " + difficulty[2]);

                        Caption handelier;
                        handelier = (Caption)options1HUD.GetElementByID(25);
                        handelier.text.DisplayedString = Convert.ToString(difficulty[2]);
                    };
                }
            }
            foreach (Button element in options2HUD.GetButtons())
            {
                //podmenu muzyki i dźwięku
                if (element.id == 401)
                {
                    element.onClick = delegate ()
                    {
                        //włącza|wyłącza muzykę
                        tipText.DisplayedString = "Włącz / Wyłącz  muzykę";

                        if (musicEnabled)
                        {
                            music.Pause();
                            musicEnabled = false;


                            Caption handelier;
                            handelier = (Caption)options2HUD.GetElementByID(23);
                            handelier.text.DisplayedString = Convert.ToString(musicEnabled);
                            //textMusicEnable.DisplayedString = Convert.ToString(musicEnabled);
                            Console.WriteLine("Muzyka wyłączona [*]");
                        }
                        else
                        {
                            music.Play();
                            musicEnabled = true;

                            Caption handelier;
                            handelier = (Caption)options2HUD.GetElementByID(23);
                            handelier.text.DisplayedString = Convert.ToString(musicEnabled);
                            //textMusicEnable.DisplayedString = Convert.ToString(musicEnabled);
                            Console.WriteLine("Muzyka włączona (y)");
                        }
                    };
                }

                if (element.id == 403)
                {
                    element.onClick = delegate ()
                    {
                        //info
                        tipText.DisplayedString = "Zmień głośność muzyki";
                    };
                }
                if (element.id == 402)
                {
                    element.onClick = delegate ()
                    {
                        //zmniejsza głośność muzyki
                        tipText.DisplayedString = "Zmień głośność muzyki";

                        music.Volume--;
                        if (music.Volume < 0)
                            music.Volume = 0;

                        Console.WriteLine("Głośność muzyki = " + music.Volume);


                        Caption handelier;
                        handelier = (Caption)options2HUD.GetElementByID(24);
                        handelier.text.DisplayedString = Convert.ToString((int)music.Volume);
                        //textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);
                    };
                }
                if (element.id == 404)
                {
                    element.onClick = delegate ()
                    {
                        //zwiększa głośność muzyki
                        tipText.DisplayedString = "Zmień głośność muzyki";

                        music.Volume++;
                        if (music.Volume > 100)
                            music.Volume = 100;
                        Console.WriteLine("Głośność muzyki = " + music.Volume);

                        Caption handelier;
                        handelier = (Caption)options2HUD.GetElementByID(24);
                        handelier.text.DisplayedString = Convert.ToString((int)music.Volume);
                        //textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);
                    };
                }
                if (element.id == 407)
                {
                    element.onClick = delegate ()
                    {
                        //info
                        tipText.DisplayedString = "Zmienia głośność dźwięków";
                    };
                }
                if (element.id == 406)
                {
                    element.onClick = delegate ()
                    {
                        //zmniejsza głośność dźwięków
                        tipText.DisplayedString = "Zmienia głośność dźwięków";

                        if (hoverSound != null)
                        {
                            hoverSound.Volume--;
                            if (hoverSound.Volume < 0)
                                hoverSound.Volume = 0;
                        }

                        if (clickSound != null)
                        {
                            clickSound.Volume--;
                            if (clickSound.Volume < 0)
                                clickSound.Volume = 0;


                            Console.WriteLine("Głośność dźwięków = " + clickSound.Volume);


                            Caption handelier;
                            handelier = (Caption)options2HUD.GetElementByID(22);
                            handelier.text.DisplayedString = Convert.ToString((int)clickSound.Volume);
                        }
                        else
                        {
                            tipText.DisplayedString = "Dźwięki nie zostały wczytane :/";
                        }

                        //textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);
                    };
                }
                if (element.id == 408)
                {
                    element.onClick = delegate ()
                    {
                        //zmniejsza głośność dźwięków
                        tipText.DisplayedString = "Zmienia głośność dźwięków";


                        if (hoverSound != null)
                        {
                            hoverSound.Volume++;
                            if (hoverSound.Volume > 100)
                                hoverSound.Volume = 100;
                        }

                        if (clickSound != null)
                        {
                            clickSound.Volume++;
                            if (clickSound.Volume > 100)
                                clickSound.Volume = 100;
                            Console.WriteLine("Głośność dźwięków = " + clickSound.Volume);

                            Caption handelier;
                            handelier = (Caption)options2HUD.GetElementByID(22);
                            handelier.text.DisplayedString = Convert.ToString((int)clickSound.Volume);
                        }
                        else
                        {
                            tipText.DisplayedString = "Dźwięki nie zostały wczytane :/";
                        }
                        //textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);

                    };
                }

                if (element.id == 405)
                {
                    element.hoverAction = delegate ()
                    {
                        if (hoverSound != null)
                            tipText.DisplayedString = "Włącz/wyłącz dźwięki";
                        else
                            tipText.DisplayedString = "Dźwięki nie zostały wczytane :/";
                    };
                    element.onClick = delegate ()
                    {

                        //włącza | wyłącza dźwięki
                        //ta dzika konstrukcja pozwala na bezproblemowe zmienianie stanu dźwięków niezależnie od stanu wczytania tychże
                        if (clickSound != null)
                            clickSoundEnable = !clickSoundEnable;
                        if (hoverSound != null)
                            hoverSoundEnable = !hoverSoundEnable;
                        else
                            tipText.DisplayedString = "Dźwięki nie zostały wczytane :/";
                        //clickSound.SoundBuffer = new SoundBuffer("sounds/click2.wav");
                    };
                }
            }

            foreach (Button element in options3HUD.GetButtons())
            {
                //podmenu zasad gry

                if (element.tekst.DisplayedString.Equals("Wskaźnik zasięgu"))
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Pokaż / ukryj wskaźnik zasięgu połykania / przyciągania"; };
                    element.onClick = delegate ()
                    {
                        //włącza | wyłącza wskaźnik zasięgu
                        if (enableRangeWskaznik)
                        {
                            enableRangeWskaznik = false;

                            Caption handelier;
                            handelier = (Caption)options3HUD.GetElementByID(1232);
                            handelier.text.DisplayedString = Convert.ToString(enableRangeWskaznik);
                            //textRange.DisplayedString = Convert.ToString(enableRangeWskaznik);
                        }
                        else
                        {
                            enableRangeWskaznik = true;

                            Caption handelier;
                            handelier = (Caption)options3HUD.GetElementByID(1232);
                            handelier.text.DisplayedString = Convert.ToString(enableRangeWskaznik);
                            //textRange.DisplayedString = Convert.ToString(enableRangeWskaznik);
                        }
                    };
                }

                if (element.id == 502)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Dodatkowy zasięg połykania komet"; };
                    element.onClick = delegate ()
                    {
                        //info
                    };
                }
                if (element.id == 501)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Dodatkowy zasięg połykania komet"; };
                    element.onClick = delegate ()
                    {
                        //zmniejsza bonusowy zasięg

                        difficulty[0]--;
                        if (difficulty[0] < 0)
                            difficulty[0] = 0;
                        Console.WriteLine("Trudność = " + difficulty[0]);


                        Caption handelier;
                        handelier = (Caption)options3HUD.GetElementByID(1234);
                        handelier.text.DisplayedString = Convert.ToString(difficulty[0]);
                        //textDifficulty.DisplayedString = Convert.ToString(difficulty[0]);

                    };
                }
                if (element.id == 503)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Dodatkowy zasięg połykania komet"; };
                    element.onClick = delegate ()
                    {
                        //zwiększa bonusowy zasięg
                        tipText.DisplayedString = "Dodatkowy zasięg połykania komet";

                        difficulty[0]++;
                        Console.WriteLine("Trudność = " + difficulty[0]);

                        Caption handelier;
                        handelier = (Caption)options3HUD.GetElementByID(1234);
                        handelier.text.DisplayedString = Convert.ToString(difficulty[0]);
                        //textDifficulty.DisplayedString = Convert.ToString(difficulty[0]);
                    };
                }
                if (element.id == 505)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Siła grawitacji"; };
                    element.onClick = delegate ()
                    {
                        //info
                        tipText.DisplayedString = "Siła grawitacji";
                    };
                }
                if (element.id == 504)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Siła grawitacji"; };
                    element.onClick = delegate ()
                    {
                        //zmiejsza siłę grawitacji

                        difficulty[1]--;
                        if (difficulty[1] < 0)
                            difficulty[1] = 0;

                        Console.WriteLine("Grawitacja = " + difficulty[1]);

                        Caption handelier;
                        handelier = (Caption)options3HUD.GetElementByID(1233);
                        handelier.text.DisplayedString = Convert.ToString(difficulty[1]);
                        //textGravity.DisplayedString = Convert.ToString(difficulty[1]);
                    };
                }
                if (element.id == 506)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Siła grawitacji"; };
                    element.onClick = delegate ()
                    {
                        //zwiększa siłę grawitacji

                        difficulty[1]++;

                        Console.WriteLine("Grawitacja = " + difficulty[1]);

                        Caption handelier;
                        handelier = (Caption)options3HUD.GetElementByID(1233);
                        handelier.text.DisplayedString = Convert.ToString(difficulty[1]);
                        //textGravity.DisplayedString = Convert.ToString(difficulty[1]);
                    };
                }

                if (element.id == 508)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Pokaż / ukryj dodatkowe opisy w konsoli"; };
                    element.onClick = delegate ()
                    {
                        AdditionaInfosHero = !AdditionaInfosHero;
                        options3HUD.ChangeCaptionByID(1235, Convert.ToString(AdditionaInfosHero));

                    };
                }
                /*
                if (element.tekst.DisplayedString.Equals("Połykanie") || element.tekst.DisplayedString.Equals("Przyciąganie"))
                {
                    element.onClick = delegate ()
                    {
                        //zmienia tryb na połykanie | włącza grawitacje
                        showTip = 10;
                        if (enableGravity)
                        {
                            enableGravity = false;
                            element.ChangeText("Połykanie");
                        }
                        else
                        {
                            //zmienia tryb na przyciąganie | włącza grawitację
                            enableGravity = true;
                            element.ChangeText("Przyciąganie");
                        }
                    };
                }
                */
            }

            //ustawienia obrazu i ekranu, rozdzielczość i tryb wyświetlania

            foreach (Button element in options4HUD.GetButtons())
            {

                if (element.id.Equals(602))
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmienia rozdzielczość okna gry"; };
                    element.onClick = delegate ()
                    {
                        //info, tylko id bo napis zmienia się w trakcie
                    };
                }
                if (element.id == 603)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmienia rozdzielczość okna gry"; };
                    element.onClick = delegate ()
                    {
                        //zwiększa rozdzielczość



                        if (configurancja.screenX.Equals("800"))
                        {
                            configurancja.screenX = Convert.ToString(1024);
                            configurancja.screenY = Convert.ToString(768);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "1024x768";
                                }
                            }
                        }
                        else if (configurancja.screenX.Equals("1024"))
                        {
                            configurancja.screenX = Convert.ToString(1280);
                            configurancja.screenY = Convert.ToString(720);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "1280x720";
                                }
                            }
                        }
                        else if (configurancja.screenX.Equals("1280"))
                        {
                            configurancja.screenX = Convert.ToString(1366);
                            configurancja.screenY = Convert.ToString(768);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "1366x768";
                                }
                            }
                        }
                        else if (configurancja.screenX.Equals("1366"))
                        {
                            configurancja.screenX = Convert.ToString(1920);
                            configurancja.screenY = Convert.ToString(1080);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "1920x1080";
                                }
                            }
                        }
                        else if (configurancja.screenX.Equals("1920"))
                        {
                            configurancja.screenX = Convert.ToString(800);
                            configurancja.screenY = Convert.ToString(600);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "800x600";
                                }
                            }
                        }

                        foreach (Button elebent in options4HUD.GetButtons())
                        {
                            if (elebent.id == 605)
                                elebent.ChangeText("Zapisz");
                        }
                    };
                }
                if (element.id == 601)
                {
                    element.hoverAction = delegate () { tipText.DisplayedString = "Zmienia rozdzielczość okna gry"; };
                    element.onClick = delegate ()
                    {
                        //zmniejsza rozdzielczość

                        if (configurancja.screenX.Equals("800"))
                        {
                            configurancja.screenX = Convert.ToString(1920);
                            configurancja.screenY = Convert.ToString(1080);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "1920x1080";
                                }
                            }
                        }
                        else if (configurancja.screenX.Equals("1024"))
                        {
                            configurancja.screenX = Convert.ToString(800);
                            configurancja.screenY = Convert.ToString(600);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "800x600";
                                }
                            }
                        }
                        else if (configurancja.screenX.Equals("1280"))
                        {
                            configurancja.screenX = Convert.ToString(1024);
                            configurancja.screenY = Convert.ToString(768);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "1024x768";
                                }
                            }
                        }
                        else if (configurancja.screenX.Equals("1366"))
                        {
                            configurancja.screenX = Convert.ToString(1280);
                            configurancja.screenY = Convert.ToString(720);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "1280x720";
                                }
                            }
                        }
                        else if (configurancja.screenX.Equals("1920"))
                        {
                            configurancja.screenX = Convert.ToString(1366);
                            configurancja.screenY = Convert.ToString(768);
                            foreach (Button element1 in options4HUD.GetButtons())
                            {
                                if (element1.id == 602)
                                {
                                    element1.tekst.DisplayedString = "1366x768";
                                }
                            }
                        }


                        foreach (Button elebent in options4HUD.GetButtons())
                        {
                            if (elebent.id == 605)
                                elebent.ChangeText("Zapisz");
                        }
                    };
                }
                if (element.id == 604)
                {
                    element.hoverAction = delegate ()
                    {
                        tipText.DisplayedString = "Zmienia tryb wyświetlania ekranu, w oknie lub pełny ekran";
                    };
                    element.onClick = delegate ()
                    {
                        //zmienia tryb wyświetlania ekranu między oknem i pełnym ekranem

                        if (configurancja.windowMode.Equals("full"))
                        {
                            configurancja.windowMode = "window";
                        }
                        else if (configurancja.windowMode.Equals("window"))
                        {
                            configurancja.windowMode = "full";
                        }

                        Caption handelier;
                        handelier = (Caption)options4HUD.GetElementByID(14);
                        handelier.text.DisplayedString = Convert.ToString(configurancja.windowMode);


                        foreach (Button elebent in options4HUD.GetButtons())
                        {
                            if (elebent.id == 605)
                                elebent.ChangeText("Zapisz");
                        }
                    };
                }
                if (element.id == 605)
                {
                    element.hoverAction = delegate ()
                    {
                        if (element.tekst.DisplayedString.Equals("Zapisz"))
                        {
                            tipText.DisplayedString = "Kliknij ponownie, aby zmiany weszły w życie";
                        }
                        else if (element.tekst.DisplayedString.Equals("Zastosuj"))
                        {
                            tipText.DisplayedString = "Pomyślnie wprowadzono zmiany";
                        }
                    };
                    element.onClick = delegate ()
                    {
                        //zapisuje kofigurację
                        if (element.tekst.DisplayedString.Equals("Zapisz"))
                        {
                            configurancja.SaveConfig();
                            element.ChangeText("Zastosuj");
                        }
                        else if (element.tekst.DisplayedString.Equals("Zastosuj"))
                        {
                            ReWindow();
                            element.ChangeText("Zapisz");
                        }
                    };
                }
                if (element.id == 606)
                {
                    element.hoverAction = delegate ()
                    {
                        tipText.DisplayedString = "Uruchamia ponownie grę";
                    };
                    element.onClick = delegate ()
                    {
                        //uruchamia ponownie okno z nowymi ustawieniami
                        startNewGame = true;
                        window.Close();
                    };
                }
            }
        }

        private void ReWindow()
        {
            CreateWindow(gameTitle);

            windowBounds = new RectBlock[4];

            for (int i = 0; i < windowBounds.Length; i++)
            {
                windowBounds[i] = new RectBlock();
                windowBounds[i].CollisionShape = new RectangleShape(new Vector2f(3 * window.Size.X, 3 * window.Size.Y));
            }

            windowBounds[0].CollisionShape.Position = new Vector2f(-window.Size.X, -3 * window.Size.Y);
            windowBounds[0].SolidCollisionEffect = new Vector2f(0, 1);
            windowBounds[1].CollisionShape.Position = new Vector2f(3 * window.Size.X, -window.Size.Y);
            windowBounds[1].SolidCollisionEffect = new Vector2f(-1, 0);
            windowBounds[2].CollisionShape.Position = new Vector2f(-window.Size.X, 3 * window.Size.Y);
            windowBounds[2].SolidCollisionEffect = new Vector2f(0, -1);
            windowBounds[3].CollisionShape.Position = new Vector2f(-3 * window.Size.X, -window.Size.Y);
            windowBounds[3].SolidCollisionEffect = new Vector2f(1, 0);

            window.SetActive(true);

            try
            {
                window.SetIcon(ikona.Size.X, ikona.Size.Y, ikona.Pixels);
            }
            catch
            { }


            RebuidGUI();
            hero.RebuidGUI(window);

            foreach (Ball elebent in ball)
            {
                elebent.SetXY((int)window.Size.X, (int)window.Size.Y);
                elebent.Remake();
            }
        }

        private void NewGame()
        {
            //tworzy nową grę

            startNewGame = false;

            hero = null;
            hero = new Hero(window, (uint)(window.Size.X * 0.8203125), (uint)(window.Size.Y * 0.5222222), new Color(255, 195, 77), (int)window.Size.X, (int)window.Size.Y, true, sterowanie);

            if (collectSoundEnable)
            {
                hero.AddSound(collectSound);
            }
            menuhero = (Hero)hero.Clone();

            for (int i = 0; i < ball.Length; i++)
            {
                ball[i] = new Ball((int)window.Size.X, (int)window.Size.Y, i);
                ball[i].cycle = (int)(new Random().Next(0, 40));
            }

            Console.Clear();

        }

        protected override void Tick()
        {
            //obliczanie numeru klatki
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

                //tutaj zbierają się obiekty fizycznie i rydzyk fizyk je rozstawia po kątach
                List<Physical_object> objekty = new List<Physical_object>();
                objekty.Add(hero);

                for (int i = 0; i < ball.Length; i++)
                {
                    if (ball[i] != null)
                    {
                        objekty.Add(ball[i]);
                    }
                }

                physicCalc.Gravitation(objekty);
                physicCalc.Collision(objekty);

                //coby position i pozycja wizualnych shajpów się pokrywała
                this.hero.Go('x', 0, 0, 0);

                for (int i = 0; i < ball.Length; i++)
                {
                    if (ball[i] != null)
                    {
                        if (numberofframe % ball[i].speeder == 0)
                            ball[i].Tick();
                        else
                            ball[i].ReDraw();
                    }
                }

                int cotamzwracasz = hero.Tick(true, numberofframe, ball);
                if (cotamzwracasz == 2)
                {
                    startNewGame = true;
                }




                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    if (!escBlock)
                    {
                        hero.MenuStatistic = false;
                        hero.IsPlaying = false;
                        escBlock = true;
                        window.SetMouseCursorVisible(true);
                        gamestarted = false;

                        //kopiuje no ico?
                        menuhero = (Hero)hero.Clone();
                        menuhero.enablemovement = false;

                        Vector2f newpos = new Vector2f((uint)(window.Size.X * 0.8203125), (uint)(window.Size.Y * 0.5222222));
                        menuhero.position = newpos;
                        menuhero.CalculatePosition();
                    }
                }
                else
                {
                    escBlock = false;
                }

                //koniec grywalnego
            }
            else
            {
                //niegrywalne

                //esc przełącza między grą a mainmenu
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    if (!escBlock)
                    {
                        hero.IsPlaying = true;

                        escBlock = true;

                        gamestarted = true;
                        showTip = 0;
                        enableOptions = 0;

                        window.SetMouseCursorVisible(false);
                        hero.Go('x', 0, 0, 0);
                    }
                }
                else
                {
                    escBlock = false;
                }

                //obliczenia bohatyra
                hero.Tick(false, numberofframe, ball);


                if (startNewGame == true)
                {
                    //tworzy nową grę
                    Console.WriteLine("Tworzenie nowej gry...");

                    NewGame();
                }


                //Tutaj zaczyna się obsługa przycisków menu

                //dźwięk najechania
                if (this.HoverSound())
                {
                    if ((hoverSound != null) && hoverSoundEnable)
                        if (hoverSoundStop)
                        {
                            hoverSound.Stop();
                            hoverSound.Play();
                            hoverSoundStop = false;
                        }
                }
                else
                {
                    hoverSoundStop = true;
                }

                //dźwięk kliknięcia
                //TickButtons obsługuje przyciski
                if (this.TickButtons() && clickSoundEnable)
                {
                    if (clickSound != null)
                        clickSound.Play();
                }

                //aktualizuje ustawienia postaci względem stanu z menus
                hero.additionalRange = difficulty[0];
                hero.gravityStrength = difficulty[1];
                hero.step = difficulty[2] / 10;
                hero.enableRange = enableRangeWskaznik;
                hero.AdditionalInfo = AdditionaInfosHero;
                //hero.enableGravity = enableGravity;

                //coby się planety w menu kręciły
                menuhero.Tick(false, numberofframe, ball);

                //koniec niegrywalnego
            }

        }

        private bool HoverSound()
        {
            //jeżeli się najedzie na jakiś przycisk to odgrywa się dźwięk najechania

            bool playSound = false;

            do
            {
                foreach (Button element in mainMenuHUD.GetButtons())
                {
                    if (element.IsHoovering())
                        return true;
                }
                foreach (Button element in optionbarHUD.GetButtons())
                {
                    if (element.IsHoovering())
                        return true;
                }
                foreach (Button element in options1HUD.GetButtons())
                {
                    if (element.IsHoovering())
                        return true;
                }
                foreach (Button element in options2HUD.GetButtons())
                {
                    if (element.IsHoovering())
                        return true;
                }
                foreach (Button element in options3HUD.GetButtons())
                {
                    if (element.IsHoovering())
                        return true;
                }
                foreach (Button element in options4HUD.GetButtons())
                {
                    if (element.IsHoovering())
                        return true;
                }
            } while (false);

            return playSound;
        }

        private bool TickButtons()
        {
            //zmiennia zwracana, jeżeli zwroci true zostanie zagrany dźwięk kliknięcia.
            bool playSound = false;

            tipText.DisplayedString = "";

            playSound = mainMenuHUD.Tick();

            if (!playSound)
            {
                if ((enableOptions >= 1) && (!playSound))
                {
                    playSound = optionbarHUD.Tick();

                    if (!playSound)
                    {
                        switch (enableOptions)
                        {
                            case 1:
                                playSound = options1HUD.Tick();
                                break;
                            case 2:
                                playSound = options2HUD.Tick();
                                break;
                            case 3:
                                playSound = options3HUD.Tick();
                                break;
                            case 4:
                                playSound = options4HUD.Tick();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return playSound;
        }

        protected override void Render()
        {
            //generuje klatkę do wyświetlenia

            if (gamestarted == true)
            {
                //jeżeli stan gry oznacza uruchomioną grę, wyświetla komety i gracza
                for (int i = 0; i < ball.Length; i++)
                {
                    if (ball[i] != null)
                        window.Draw(ball[i].kolo);
                }

                hero.Draw();
            }
            else
            {
                //jeży stan gry oznacza menu główne, rysuje interfejs menu

                //te słoneczko po prawej
                menuhero.Draw();

                //główne menu główne
                mainMenuHUD.Draw();


                //wskazowka
                window.Draw(tipText);


                //jeżeli jakieś podmenu jest otwarte
                if (enableOptions >= 1)
                {
                    optionbarHUD.Draw();

                    if (enableOptions == 1)
                    {
                        options1HUD.Draw();
                    }

                    if (enableOptions == 2)
                    {
                        options2HUD.Draw();
                    }
                    if (enableOptions == 3)
                    {
                        options3HUD.Draw();
                    }
                    if (enableOptions == 4)
                    {
                        options4HUD.Draw();
                    }
                }

                //yesnodialog.Draw();
            }

        }

        protected override void Window_resized(object sender, EventArgs e)
        {
            RebuidGUI();
        }
    }
}