using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace EatCometsClear
{
    class HeadUpDisplay
    {
        System.Collections.ArrayList elements;
        RenderTarget target;

        public HeadUpDisplay()
        {
            target = null;
            this.elements = new System.Collections.ArrayList();
        }

        public HeadUpDisplay(List<Drawable> newElements)
        {
            target = null;

            this.elements = new System.Collections.ArrayList();

            foreach(Drawable element in newElements)
            {
                elements.Add(element);
            }
        }

        public List<Button> GetButtons()
        {
            List<Button> zwracable = new List<Button>();

            var tmp = elements.GetEnumerator();
            while (tmp.MoveNext())
            {
                Button element;
                element = new Button();
                if (tmp.Current.GetType() == element.GetType())
                {
                    element = null;
                    element = (Button)tmp.Current;
                    zwracable.Add(element);
                }
            }
            return zwracable;
        }

        public IDable GetElementByID(int id)
        {
            
            foreach(IDable element in elements)
            {
                if (element.id == id)
                {
                    return element;
                }
            }

            return null;
        }

        public void ChangeCaptionByID(int id, string newString)
        {
            Caption element = (Caption)this.GetElementByID(id);
            element.text.DisplayedString = newString;
        }

        public void ChangeTextColorByID(int id,Color color)
        {
            Caption element = (Caption)this.GetElementByID(id);
            element.text.Color = color;
        }

        public void DrawIDs()
        {
            foreach (IDable elemend in elements)
                Console.WriteLine(elemend.id);
        }

        public void AddElement(Drawable newElement)
        {
            elements.Add(newElement);
        }

        public bool Tick()
        {
            foreach (Button element in this.GetButtons())
            {
                element.Tick();
                if (element.DoAction())
                {
                    element.OnClick();
                    return true;
                }
            }
            return false;
        }

        public void Draw()
        {
            foreach(Drawable element in elements)
            {
                element.Draw( target, new RenderStates());
            }
        }
        
    }
}
