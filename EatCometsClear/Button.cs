using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace EatCometsClear
{
    class Button :Drawabable, Drawable
    {

        float rx, ry, rw, rh;
        RenderWindow window;
        RectangleShape prostokat, tlo;
        public Text tekst;
        //Action akcja;
        public bool doAction;
        Color color, activeColor;
        private int iletyczymasz;
        public delegate void OnClickDelegate();
        public OnClickDelegate onClick;
        public OnClickDelegate onRightClick;
        public delegate void TickDelegate();
        public TickDelegate tick;


        public Button()
        { }

        public Button(float posx, float posy, float width, float height, string napis, RenderWindow okno, Color color, uint textsize, int id )
        {
            this.iletyczymasz = 0;
            this.id = id;
            
            window = okno;
            

            doAction = false;
            rx = posx;
            ry = posy;
            rw = width;
            rh = height;
            prostokat = new RectangleShape();
            prostokat.Position = new Vector2f(rx, ry);
            prostokat.Size = new Vector2f(rw, rh);
            prostokat.FillColor = new Color(255, 0, 0);

            tlo = new RectangleShape();
            tlo.Position = new Vector2f((float)(rx * 0.99), (float)(ry*0.99));
            tlo.Size = new Vector2f(rw, rh);

            this.SetColor(color);
            tekst = new Text();
            tekst.DisplayedString = napis;
            tekst.Font = new Font("fonts/arial.ttf");
            if(tekst.DisplayedString.Length > 10)
                tekst.CharacterSize = textsize-2;
            else
                tekst.CharacterSize = textsize;
            tekst.Position = new Vector2f(rx + (rw - tekst.GetLocalBounds().Width) / 2, ry + (rh - tekst.GetLocalBounds().Height) / 2);

            if ((napis == "-"))
            {
                //tekst.Position = new Vector2f(rx + 15, ry + 2);
                tekst.Position = new Vector2f(rx + (uint)(window.Size.X * 0.01171), ry + (uint)(window.Size.Y * 0.00277777));
            }
            else if ((napis == "+"))
            {
                //tekst.Position = new Vector2f(rx + 12, ry + 3);
                tekst.Position = new Vector2f(rx + (uint)(window.Size.X * 0.009375), ry + (uint)(window.Size.Y * 0.0041666));
            }
            else if (napis.Length == 1)
                tekst.Position = new Vector2f(rx + (uint)(window.Size.X * 0.01171), ry + (uint)(window.Size.Y * 0.00777777));

        }

        public void SetPosition(int x, int y)
        {
            //prostokąt, tło, tekst

            prostokat.Position = new Vector2f(x, y);
            tekst.Position = new Vector2f(x + (rw - tekst.GetLocalBounds().Width) / 2, y + (rh - tekst.GetLocalBounds().Height) / 2);
            tlo.Position = new Vector2f((float)(x * 0.99), (float)(y * 0.99));



        }

        public void ChangeColor(Color coloreg)
        {
            this.prostokat.FillColor = coloreg;
        }

        public void SetColor(Color newColor)
        {

            this.color = newColor;
            int roznica = -32;
            int r, g, b;

            r = color.R + roznica;
            if (r > 255)
                r = 255;
            if (r < 0)
                r = 0;

            g = color.G + roznica;
            if (g > 255)
                g = 255;
            if (g < 0)
                g = 0;
            b = color.B + roznica;
            if (b > 255)
                b = 255;
            if (b < 0)
                b = 0;

            this.activeColor = new Color((byte)r, (byte)g, (byte)b);




            r = activeColor.R + roznica;
            if (r > 255)
                r = 255;
            if (r < 0)
                r = 0;

            g = activeColor.G + roznica;
            if (g > 255)
                g = 255;
            if (g < 0)
                g = 0;
            b = activeColor.B + roznica;
            if (b > 255)
                b = 255;
            if (b < 0)
                b = 0;


            tlo.FillColor = new Color((byte)r, (byte)g, (byte)b);
        }

        public void ChangeText(String text)
        {
            this.tekst.DisplayedString = text;
            tekst.Position = new Vector2f(rx + (rw - tekst.GetGlobalBounds().Width) / 2, ry + (rh - tekst.GetGlobalBounds().Height) / 2);
        }

        public bool IsHoovering()
        {
            float mx = Mouse.GetPosition(window).X;
            float my = Mouse.GetPosition(window).Y;
            if (mx > rx && mx < rx + rw && my > ry && my < ry + rh)
            {
                return true;
            }
            return false;
        }

        public bool DoAction()
        {
            if (IsHoovering())
            {
                this.ChangeColor(activeColor);
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    this.iletyczymasz++;
                    doAction = true;
                }
                else
                    this.iletyczymasz = -1;
                if(Mouse.IsButtonPressed(Mouse.Button.Right))
                {
                    if(this.onRightClick != null)
                    {
                        this.onRightClick();
                    }
                }
            }
            else
                this.ChangeColor(color);

            if ((doAction == true) && (iletyczymasz == 0))
            {
                doAction = false;
                return true;
            }
            else
            {
                if (tekst.DisplayedString.Equals("-") || tekst.DisplayedString.Equals("+"))
                {
                    if (iletyczymasz > 45)
                    {
                        doAction = false;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
        }
       
        public void OnClick()
        {
            if(this.onClick != null)
                this.onClick();
        }

        public void Tick()
        {
            if(this.tick != null)
                this.tick();
        }

        public void Draw()
        {
            window.Draw(tlo);
            window.Draw(prostokat);
            window.Draw(tekst);
        }

        public new void Draw(RenderTarget target, RenderStates states)
        {
            this.Draw();
        }

    }
}
