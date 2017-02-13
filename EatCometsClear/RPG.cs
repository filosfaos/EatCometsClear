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

        int numberofframe;
        public bool gamestarted;
        int showTip;
        int enableOptions;
        int lastOption;
        //uint screenX = 1280, screenY = 720;
        bool escBlock;

        int sterowanie;
        int[] difficulty;

        List<Text> texty;


        Physic rydzykFizyk;

        bool enableRangeWskaznik, enableGravity;
        bool enableMusic;

        Image ikona;
        Texture liniaload;

        HeadUpDisplay mainMenuHUD, options1HUD, options2HUD, options3HUD, options4HUD, optionbarHUD;

        MyConfig configurancja;

        public RPG(uint x, uint y)
            : base(x, y, "Żryj komety", Color.Black)
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
            try{
                music = new Music("content/music.ogg");
                enableMusic = true;
            }
            catch{
                enableMusic = false;
            }

            try{
                SoundBuffer xD = new SoundBuffer("sounds/click.wav");
                clickSound = new Sound(xD);
                clickSound.Volume = 15;
                clickSoundEnable = true;
            }
            catch {
                clickSoundEnable = false;
            }
            try{
                SoundBuffer xD = new SoundBuffer("sounds/collect.wav");
                collectSound = new Sound(xD);
                collectSound.Volume = 15;
                collectSoundEnable = true;
            }
            catch{
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
            try{
                liniaload = new Texture("img/optionbarbg.png");
            }
            catch { }
        }

        protected override void Initialize()
        {
            configurancja = new MyConfig();

            window.SetActive(true);
            try
            {
                window.SetIcon(ikona.Size.X, ikona.Size.Y, ikona.Pixels);
            }
            catch
            { }

            rydzykFizyk = new Physic(); //on liczy fizyke fizyk jeden
            ball = new Ball[5];



            enableRangeWskaznik = true;
            enableGravity = false;
            uint pomX = window.Size.X;
            uint pomY = window.Size.Y;

            
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
            hudelements.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.25), (uint)(pomX * 0.15), (uint)(pomY * 0.1), "Graj", window, buttonscolor, buttontextsize, 0 ));
            hudelements.Add(new Button((uint)(pomX * 0.24), (uint)(pomY * 0.25), (uint)(pomX * 0.09), (uint)(pomY * 0.1), "Nowa", window, new Color(200, 128, 64), buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.40), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Opcje", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Pokaż wskazówkę", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.08), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.1), "Wyjdź", window, buttonscolor, buttontextsize,  0));

            mainMenuHUD = new HeadUpDisplay();
            foreach(Button element in hudelements)
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
            hudelements.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.265), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "S", window, buttonscolor, buttontextsize,  0));
            buttonscolor = new Color(128, 69, 69);
            hudelements.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.415), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "M", window, buttonscolor, buttontextsize,  0));
            buttonscolor = new Color(69, 69, 128);
            hudelements.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.565), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "G", window, buttonscolor, buttontextsize,  0));
            buttonscolor = new Color(54, 65, 53);
            hudelements.Add(new Button((uint)(pomX * 0.345), (uint)(pomY * 0.715), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "O", window, buttonscolor, buttontextsize,  0));

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
            optionbarHUD.AddElement(new StaticImage(window,linia));
            foreach (Button element in hudelements)
            {
                optionbarHUD.AddElement(element);
            }
            optionbarHUD.AddElement(hudtext);

            buttonscolor = new Color(69, 128, 69);
            hudelements.Clear();
            hudelements = new System.Collections.ArrayList();
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.25), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Myszka", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Czułość", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Zamknij", window, new Color(69, 69, 69), buttontextsize,  0));

            options1HUD = new HeadUpDisplay();
            foreach (Button element in hudelements)
            {
                options1HUD.AddElement(element);
            }

            buttonscolor = new Color(128, 69, 69);
            hudelements.Clear();
            hudelements = new System.Collections.ArrayList();
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.25), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Muzyka", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize,  1));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Głośność", window, buttonscolor, buttontextsize,  1));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize,  1));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Dźwięki", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.72), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize,  2));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.70), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Głośność", window, buttonscolor, buttontextsize,  2));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.72), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize, 2));

            options2HUD = new HeadUpDisplay();
            foreach (Button element in hudelements)
            {
                options2HUD.AddElement(element);
            }

            buttonscolor = new Color(69, 69, 128);
            hudelements.Clear();
            hudelements = new System.Collections.ArrayList();
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize,  1));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.25), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Zasięg", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize,  1));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize,  2));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.40), (uint)(pomX * 0.17), (uint)(pomY * 0.10), "Grawitacja", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.42), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize,  2));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.55), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Połykanie", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Wskaźnik zasięgu", window, buttonscolor, buttontextsize,  0));

            options3HUD = new HeadUpDisplay();
            foreach (Button element in hudelements)
            {
                options3HUD.AddElement(element);
            }

            string dupa;
            dupa = Convert.ToString(window.Size.X);
            dupa += "x";
            dupa += Convert.ToString(window.Size.Y);

            buttonscolor = new Color(54, 65, 53);
            hudelements.Clear();
            hudelements = new System.Collections.ArrayList();
            options4HUD = new HeadUpDisplay();
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "-", window, buttonscolor, buttontextsize,  1));
            hudelements.Add(new Button((uint)(pomX * 0.44), (uint)(pomY * 0.25), (uint)(pomX * 0.17), (uint)(pomY * 0.10), dupa, window, buttonscolor, buttontextsize,  11));
            hudelements.Add(new Button((uint)(pomX * 0.62), (uint)(pomY * 0.27), (uint)(pomX * 0.03), (uint)(pomY * 0.06), "+", window, buttonscolor, buttontextsize,  1));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.40), (uint)(pomX * 0.25), (uint)(pomY * 0.10), "Tryb okna", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.40), (uint)(pomY * 0.70), (uint)(pomX * 0.17), (uint)(pomY * 0.1), "Zapisz", window, buttonscolor, buttontextsize,  0));
            hudelements.Add(new Button((uint)(pomX * 0.60), (uint)(pomY * 0.715), (uint)(pomX * 0.04), (uint)(pomY * 0.07), "R", window, new Color(128,54,13), buttontextsize,  0));

            hudtext = new Caption(new Text("pełny ekran", new Font("fonts/arial.ttf"), texttextsize), 14, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
            hudtext.text.Color = new Color(Color.White);
            hudtext.text.DisplayedString = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["WindowMode"]);
            options4HUD.AddElement(hudtext);

            foreach (Button element in hudelements)
            {
                if (element.tekst.DisplayedString.Equals(dupa))
                    element.DoAction();
                options4HUD.AddElement(element);
            }

            hudtext = new Caption( new Text("0", new Font("fonts/arial.ttf"), texttextsize), 1234, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.27));
            hudtext.text.Color = new Color(Color.White);
            hudtext.text.DisplayedString = Convert.ToString(difficulty[0]);
            options3HUD.AddElement(hudtext);

            hudtext = null;
            hudtext = new Caption(new Text("0", new Font("fonts/arial.ttf"), texttextsize),1233, window);
            hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
            hudtext.text.Color = new Color(Color.White);
            hudtext.text.DisplayedString = Convert.ToString(difficulty[1]);
            options3HUD.AddElement(hudtext);

            hudtext = null;
            hudtext = new Caption( new Text("0", new Font("fonts/arial.ttf"), texttextsize), 1232, window);
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
                hudtext = new Caption( new Text("0", new Font("fonts/arial.ttf"), texttextsize), 24, window);
                hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.41));
                hudtext.text.Color = new Color(Color.White);
                hudtext.text.DisplayedString = Convert.ToString((int)music.Volume);
                options2HUD.AddElement(hudtext);

                hudtext = null;
                hudtext = new Caption( new Text("0", new Font("fonts/arial.ttf"), texttextsize), 23, window);
                hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.27));
                hudtext.text.Color = new Color(Color.White);
                hudtext.text.DisplayedString = Convert.ToString(musicEnabled);
                options2HUD.AddElement(hudtext);
            }

            if(clickSoundEnable)
            {

                hudtext = null;
                hudtext = new Caption(new Text("0", new Font("fonts/arial.ttf"), texttextsize), 22, window);
                hudtext.text.Position = new Vector2f((uint)(pomX * 0.66), (uint)(pomY * 0.70));
                hudtext.text.Color = new Color(Color.White);
                hudtext.text.DisplayedString = Convert.ToString((int)clickSound.Volume);
                options2HUD.AddElement(hudtext);
            }


            //uint tipsize = 16;
            uint tipsize = (uint)(pomY * 0.022222);
            texty = new List<Text>();

            for( int i = 0; i < 22; i++)
            {
                if( i == 0)
                    texty.Add( new Text("Poruszanie - dostosuj w opcjach", new Font("fonts/arial.ttf"), tipsize) );
                if( i == 1)
                    texty.Add(new Text("Co 10 zdobytych komet otrzymujesz planetę", new Font("fonts/arial.ttf"), tipsize));
                if(i == 2)
                    texty.Add(new Text("Przytrzymanie TAB wyświetla menu postaci", new Font("fonts/arial.ttf"), tipsize));
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
                if (i == 16)
                    texty.Add(new Text("Nie wczytano muzyki", new Font("fonts/arial.ttf"), tipsize));
                if (i == 17)
                    texty.Add(new Text("Zmiany wejdą w życie po ponownym uruchomieniu gry. Kliknij R aby uruchomić ponownie", new Font("fonts/arial.ttf"), tipsize));
                if (i == 18)
                    texty.Add(new Text("Zmienia tryb wyświetlania ekranu, w oknie lub pełny ekran", new Font("fonts/arial.ttf"), tipsize));
                if (i == 19)
                    texty.Add(new Text("Zmienia rozdzielczość okna gry", new Font("fonts/arial.ttf"), tipsize));
                if (i == 20)
                    texty.Add(new Text("Zmienia głośność dźwięków", new Font("fonts/arial.ttf"), tipsize));
                if (i == 21)
                    texty.Add(new Text("Dźwięki nie zostały wczytane :/", new Font("fonts/arial.ttf"), tipsize));

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

            if(collectSoundEnable)
            {
                hero.AddSound(collectSound);
            }
            menuhero = (Hero)hero.Clone();

            for (int i = 0; i < ball.Length; i++)
            {
                ball[i] = new Ball((int)window.Size.X, (int)window.Size.Y, i);
            }

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

                List<Physical_object> objekty = new List<Physical_object>();
                objekty.Add(hero);

                for (int i = 0; i < ball.Length; i++)
                {
                    if (ball[i] != null)
                        objekty.Add(ball[i]);
                }

                rydzykFizyk.Gravitation(objekty);

                this.hero.Go('x', 0, 0, 0);

                for (int i = 0; i < ball.Length; i++)
                {
                    if (ball[i] != null)
                        ball[i].ReDraw();
                }


                int cotamzwracasz = hero.Tick(true, numberofframe, ball);
                if (cotamzwracasz == 2)
                {
                    startNewGame = true;
                }



                menuhero = (Hero)hero.Clone();
                menuhero.enablemovement = false;


                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    if (!escBlock)
                    {
                        escBlock = true;
                        window.SetMouseCursorVisible(true);
                        gamestarted = false;

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

                if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    if(!escBlock)
                    {
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


                hero.Tick(false, numberofframe, ball);


                if (startNewGame == true)
                {
                    Console.WriteLine("Tworzenie nowej gry...");

                    NewGame();
                }


                //Tutaj zaczyna się obsługa przycisków menu

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

                if(this.TickButtons() && clickSoundEnable)
                {
                    if (clickSound != null)
                        clickSound.Play();
                }

                menuhero.Tick(false, numberofframe, ball);

            }

        }

        bool HoverSound()
        {
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

        bool TickButtons()
        {
            bool playSound;
            playSound = false;

            foreach (Button element in mainMenuHUD.GetButtons())
            {
                if (element.tekst.DisplayedString.Equals("Graj") && element.DoAction())
                {
                    playSound = true;
                    gamestarted = true;
                    showTip = 0;
                    enableOptions = 0;

                    window.SetMouseCursorVisible(false);
                    hero.Go('x', 0, 0, 0);
                }
                if (element.tekst.DisplayedString.Equals("Nowa") && element.DoAction())
                {
                    playSound = true;
                    startNewGame = true;
                }
                if (element.tekst.DisplayedString.Equals("Pokaż wskazówkę") && element.DoAction())
                {
                    playSound = true;
                    showTip++;
                    if (showTip > 4)
                        showTip = -1;
                }
                if (element.tekst.DisplayedString.Equals("Opcje"))
                {
                    if (element.DoAction())
                    {
                        playSound = true;
                        if (enableOptions == 0)
                        {
                            enableOptions = lastOption;
                        }
                        else
                        {
                            enableOptions = 0;
                        }
                    }

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
                }
                if (element.tekst.DisplayedString.Equals("Zamknij"))
                {
                    if (element.DoAction())
                    {
                        playSound = true;
                        if (enableOptions != 0)
                        {
                            enableOptions = 0;
                        }
                    }

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
                }

                if (element.tekst.DisplayedString.Equals("Wyjdź") && element.DoAction())
                {
                    playSound = true;
                    window.Close();
                }


            }



            if (enableOptions >= 1)
            {
                foreach (Button element in optionbarHUD.GetButtons())
                {
                    if (element.tekst.DisplayedString.Equals("S") && element.DoAction())
                    {
                        playSound = true;

                        Caption handelier;
                        handelier = (Caption)optionbarHUD.GetElementByID(911);
                        handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.347), (uint)(window.Size.Y * 0.277));

                        lastOption = 1;
                        enableOptions = 1;
                        showTip = 13;
                    }
                    if (element.tekst.DisplayedString.Equals("M") && element.DoAction())
                    {
                        playSound = true;
                        if (enableMusic)
                        {
                            Caption handelier;
                            handelier = (Caption)optionbarHUD.GetElementByID(911);
                            handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.347), (uint)(window.Size.Y * 0.427));

                            lastOption = 2;
                            enableOptions = 2;
                            showTip = 12;
                        }
                        else
                        {
                            showTip = 16;
                        }
                    }
                    if (element.tekst.DisplayedString.Equals("G") && element.DoAction())
                    {
                        playSound = true;

                        Caption handelier;
                        handelier = (Caption)optionbarHUD.GetElementByID(911);
                        handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.347), (uint)(window.Size.Y * 0.577));

                        lastOption = 3;
                        enableOptions = 3;
                        showTip = 11;
                    }
                    if (element.tekst.DisplayedString.Equals("O") && element.DoAction())
                    {
                        playSound = true;

                        Caption handelier;
                        handelier = (Caption)optionbarHUD.GetElementByID(911);
                        handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.347), (uint)(window.Size.Y * 0.727));

                        lastOption = 4;
                        enableOptions = 4;


                        string dupa;
                        dupa = Convert.ToString(configurancja.screenX);
                        dupa += "x";
                        dupa += Convert.ToString(configurancja.screenY);


                        foreach (Button element1 in options4HUD.GetButtons())
                        {
                            if (element1.id == 11)
                            {
                                element1.tekst.DisplayedString = dupa;
                            }
                        }


                    }

                }
                if (enableOptions == 1)
                {
                    foreach (Button element in options1HUD.GetButtons())
                    {
                        if (element.tekst.DisplayedString.Equals("W - A - S - D") && element.DoAction())
                        {
                            playSound = true;
                            element.ChangeText("Strzałki");
                            sterowanie = 0;
                            hero.Changemovement(sterowanie);
                            showTip = 6;
                        }

                        if (element.tekst.DisplayedString.Equals("Strzałki") && element.DoAction())
                        {
                            playSound = true;
                            element.ChangeText("WSAD + Strzałki");
                            sterowanie = 2;
                            hero.Changemovement(sterowanie);
                            showTip = 6;
                        }

                        if (element.tekst.DisplayedString.Equals("WSAD / Strzałki") && element.DoAction())
                        {
                            playSound = true;
                            element.ChangeText("Myszka");
                            sterowanie = 3;
                            hero.Changemovement(sterowanie);
                            showTip = 6;
                        }
                        if (element.tekst.DisplayedString.Equals("Myszka") && element.DoAction())
                        {
                            playSound = true;
                            element.ChangeText("WSAD / Strzałki");
                            sterowanie = 2;
                            hero.Changemovement(sterowanie);
                            showTip = 6;
                        }

                        if (element.tekst.DisplayedString.Equals("Zamknij") && element.DoAction())
                        {
                            playSound = true;
                            enableOptions = 0;
                        }


                        if (element.tekst.DisplayedString.Equals("Czułość") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 14;
                        }
                        if (element.tekst.DisplayedString.Equals("-") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 14;

                            difficulty[2]--;
                            if (difficulty[2] < 0)
                                difficulty[2] = 0;
                            Console.WriteLine("Czułość = " + difficulty[2]);

                            Caption handelier;
                            handelier = (Caption)options1HUD.GetElementByID(25);
                            handelier.text.DisplayedString = Convert.ToString(difficulty[2]);

                        }
                        if (element.tekst.DisplayedString.Equals("+") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 14;

                            difficulty[2]++;
                            Console.WriteLine("Czułość = " + difficulty[2]);

                            Caption handelier;
                            handelier = (Caption)options1HUD.GetElementByID(25);
                            handelier.text.DisplayedString = Convert.ToString(difficulty[2]);
                        }
                    }
                }
                if (enableOptions == 2)
                {
                    foreach (Button element in options2HUD.GetButtons())
                    {
                        if (element.tekst.DisplayedString.Equals("Muzyka") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 7;

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
                        }
                        if (element.id.Equals(1))
                        {
                            if (element.tekst.DisplayedString.Equals("Głośność") && element.DoAction())
                            {
                                playSound = true;
                                showTip = 15;
                            }
                            if (element.tekst.DisplayedString.Equals("-") && element.DoAction())
                            {
                                playSound = true;
                                showTip = 15;

                                music.Volume--;
                                if (music.Volume < 0)
                                    music.Volume = 0;

                                Console.WriteLine("Głośność muzyki = " + music.Volume);


                                Caption handelier;
                                handelier = (Caption)options2HUD.GetElementByID(24);
                                handelier.text.DisplayedString = Convert.ToString((int)music.Volume);
                                //textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);

                            }
                            if (element.tekst.DisplayedString.Equals("+") && element.DoAction())
                            {
                                playSound = true;
                                showTip = 15;

                                music.Volume++;
                                if (music.Volume > 100)
                                    music.Volume = 100;
                                Console.WriteLine("Głośność muzyki = " + music.Volume);

                                Caption handelier;
                                handelier = (Caption)options2HUD.GetElementByID(24);
                                handelier.text.DisplayedString = Convert.ToString((int)music.Volume);
                                //textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);
                            }

                        }
                        if (element.id.Equals(2))
                        {
                            if (element.tekst.DisplayedString.Equals("Głośność") && element.DoAction())
                            {
                                playSound = true;
                                showTip = 20;
                            }
                            if (element.tekst.DisplayedString.Equals("-") && element.DoAction())
                            {
                                playSound = true;
                                showTip = 20;

                                if(hoverSound != null)
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
                                    showTip = 21;
                                }
                                
                                //textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);

                            }
                            if (element.tekst.DisplayedString.Equals("+") && element.DoAction())
                            {
                                playSound = true;
                                showTip = 20;
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
                                    showTip = 21;
                                }
                                //textMusicVolume.DisplayedString = Convert.ToString((int)music.Volume);
                            }



                        }
                        if (element.tekst.DisplayedString.Equals("Dźwięki") && element.DoAction())
                        {
                            playSound = true;
                            if (clickSound != null)
                                clickSoundEnable = !clickSoundEnable;
                            if (hoverSound != null)
                                hoverSoundEnable = !hoverSoundEnable;
                            else
                                showTip = 21;
                            //clickSound.SoundBuffer = new SoundBuffer("sounds/click2.wav");
                        }
                    }
                }
                if (enableOptions == 3)
                {
                    foreach (Button element in options3HUD.GetButtons())
                    {


                        if (element.tekst.DisplayedString.Equals("Wskaźnik zasięgu") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 9;
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
                        }

                        if (element.tekst.DisplayedString.Equals("Zasięg") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 5;
                        }
                        if (element.tekst.DisplayedString.Equals("-") && (element.id == 1))
                        {

                            if (element.DoAction())
                            {
                                playSound = true;
                                showTip = 5;

                                difficulty[0]--;
                                if (difficulty[0] < 0)
                                    difficulty[0] = 0;
                                Console.WriteLine("Trudność = " + difficulty[0]);


                                Caption handelier;
                                handelier = (Caption)options3HUD.GetElementByID(1234);
                                handelier.text.DisplayedString = Convert.ToString(difficulty[0]);
                                //textDifficulty.DisplayedString = Convert.ToString(difficulty[0]);
                            }
                        }
                        if (element.tekst.DisplayedString.Equals("+") && (element.id == 1))
                        {
                            if (element.DoAction())
                            {
                                playSound = true;
                                showTip = 5;

                                difficulty[0]++;
                                Console.WriteLine("Trudność = " + difficulty[0]);

                                Caption handelier;
                                handelier = (Caption)options3HUD.GetElementByID(1234);
                                handelier.text.DisplayedString = Convert.ToString(difficulty[0]);
                                //textDifficulty.DisplayedString = Convert.ToString(difficulty[0]);
                            }
                        }
                        if (element.tekst.DisplayedString.Equals("Grawitacja") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 8;
                        }
                        if (element.tekst.DisplayedString.Equals("-") && (element.id == 2))
                        {
                            if (element.DoAction())
                            {
                                playSound = true;
                                showTip = 8;

                                difficulty[1]--;
                                if (difficulty[1] < 0)
                                    difficulty[1] = 0;

                                Console.WriteLine("Grawitacja = " + difficulty[1]);

                                Caption handelier;
                                handelier = (Caption)options3HUD.GetElementByID(1233);
                                handelier.text.DisplayedString = Convert.ToString(difficulty[1]);
                                //textGravity.DisplayedString = Convert.ToString(difficulty[1]);
                            }
                        }
                        if (element.tekst.DisplayedString.Equals("+") && (element.id == 2))
                        {
                            if (element.DoAction())
                            {
                                playSound = true;
                                showTip = 8;

                                difficulty[1]++;

                                Console.WriteLine("Grawitacja = " + difficulty[1]);

                                Caption handelier;
                                handelier = (Caption)options3HUD.GetElementByID(1233);
                                handelier.text.DisplayedString = Convert.ToString(difficulty[1]);
                                //textGravity.DisplayedString = Convert.ToString(difficulty[1]);
                            }
                        }
                        if (element.tekst.DisplayedString.Equals("Przyciąganie") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 10;
                            enableGravity = false;
                            element.ChangeText("Połykanie");
                        }
                        if (element.tekst.DisplayedString.Equals("Połykanie") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 10;
                            enableGravity = true;
                            element.ChangeText("Przyciąganie");

                        }
                    }
                }


                if (enableOptions == 4)
                {
                    /*
                    if (configurancja.screenX.Equals("1024"))
                    {
                        Caption handelier;
                        handelier = (Caption)options4HUD.GetElementByID(14);
                        handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.66), (uint)(window.Size.Y * 0.27));
                    }
                    if (configurancja.screenX.Equals("1280"))
                    {
                        Caption handelier;
                        handelier = (Caption)options4HUD.GetElementByID(14);
                        handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.66), (uint)(window.Size.Y * 0.41));
                    }
                    if (configurancja.screenX.Equals("1920"))
                    {
                        Caption handelier;
                        handelier = (Caption)options4HUD.GetElementByID(14);
                        handelier.text.Position = new Vector2f((uint)(window.Size.X * 0.66), (uint)(window.Size.Y * 0.56));
                    }
                    */

                    foreach (Button element in options4HUD.GetButtons())
                    {

                        if (element.id.Equals(11) && element.DoAction())
                        {
                            playSound = true;
                            showTip = 19;
                        }
                        if (element.tekst.DisplayedString.Equals("+") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 19;
                            if (configurancja.screenX.Equals("800"))
                            {
                                configurancja.screenX = Convert.ToString(1024);
                                configurancja.screenY = Convert.ToString(768);
                                foreach (Button element1 in options4HUD.GetButtons())
                                {
                                    if (element1.id == 11)
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
                                    if (element1.id == 11)
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
                                    if (element1.id == 11)
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
                                    if (element1.id == 11)
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
                                    if (element1.id == 11)
                                    {
                                        element1.tekst.DisplayedString = "800x600";
                                    }
                                }
                            }
                        }

                        if (element.tekst.DisplayedString.Equals("-") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 19;
                            if (configurancja.screenX.Equals("800"))
                            {
                                configurancja.screenX = Convert.ToString(1920);
                                configurancja.screenY = Convert.ToString(1080);
                                foreach (Button element1 in options4HUD.GetButtons())
                                {
                                    if (element1.id == 11)
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
                                    if (element1.id == 11)
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
                                    if (element1.id == 11)
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
                                    if (element1.id == 11)
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
                                    if (element1.id == 11)
                                    {
                                        element1.tekst.DisplayedString = "1366x768";
                                    }
                                }
                            }
                        }
                        if (element.tekst.DisplayedString.Equals("Tryb okna") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 18;
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
                        }
                        if (element.tekst.DisplayedString.Equals("Zapisz") && element.DoAction())
                        {
                            playSound = true;
                            showTip = 17;
                            configurancja.SaveConfig();

                        }
                        if (element.tekst.DisplayedString.Equals("R") && element.DoAction())
                        {
                            playSound = true;
                            startNewGame = true;
                            window.Close();
                        }

                    }
                }
            }

            hero.additionalRange = difficulty[0];
            hero.gravityStrength = difficulty[1];
            hero.step = difficulty[2] / 10;
            hero.enableRange = enableRangeWskaznik;
            hero.enableGravity = enableGravity;

            return playSound;
        }
        
        protected override void Render()
        {
            if (gamestarted == true)
            {
                // window.Draw(map);

                for (int i = 0; i < ball.Length; i++)
                {
                    if (ball[i] != null)
                        window.Draw(ball[i].kolo);
                }
                

                hero.Draw();
            }
            else
            {
                menuhero.Draw();
                
                mainMenuHUD.Draw();
                

                if ( (showTip >= 0) && (showTip <= texty.Count))
                    window.Draw(texty[showTip]);

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

    }
}
