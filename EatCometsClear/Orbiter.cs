using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EatCometsClear
{
    abstract class Orbiter :Physical_object
    {

        public CircleShape kolo { get; set; }
        public CircleShape obwodka;
        public CircleShape zasiegacz;
        public bool AdditionalInfo { get; set; }

        protected List<Satelite> satelite;

        public Orbiter()
        {
            AdditionalInfo = false;
            this.objectName = "orbiter_type";
        }

        protected void RemoveSatelite()
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

            this.mass++;
        }

        protected void AddSatelite()
        {
            this.mass--;
            this.NewBall(satelite.Count+1, satelite.Count+1);
            if(AdditionalInfo)
                Console.WriteLine(this.objectName + " new planet " + satelite.Count);

        }

        public void NewBall(int i, int newspeed)
        {
            satelite.Add(new Satelite(i, this.position.X, this.position.Y, newspeed, AdditionalInfo));
            //satelite[i] = new satelite(i, this.position.X, this.position.Y, newspeed);
        }

        public void CalculatePosition()
        {
            this.obwodka.Origin = new Vector2f(obwodka.Radius, obwodka.Radius);
            this.obwodka.Position = this.position;
            //this.kolo.Position = new Vector2f(this.position.X - this.kolo.Radius, this.position.Y - this.kolo.Radius);
            this.kolo.Origin = new Vector2f(kolo.Radius, kolo.Radius);
            kolo.Position = this.position;
            zasiegacz.Origin = new Vector2f(zasiegacz.Radius, zasiegacz.Radius);
            zasiegacz.Position = this.position;
        }

        protected virtual void CalculateRadius()
        {
            float max_radius = 100;

            
            this.kolo.Radius = this.mass;

            if (max_radius < this.mass)
                this.kolo.Radius = max_radius;
            
            this.obwodka.Radius = this.kolo.Radius + 2;

            if (AdditionalInfo)
                Console.WriteLine("radius " + (this.kolo.Radius));
        }

        protected bool Near(Physical_object PObject)
        {
            Vector2f PosDiff = this.position - PObject.position;



            return false;
        }

    }
}
