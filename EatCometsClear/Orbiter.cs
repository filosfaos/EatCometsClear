using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EatCometsClear
{
    class Orbiter :Physical_object
    {

        protected List<Satelite> satelite;

        public Orbiter()
        {
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
            Console.WriteLine(this.objectName + " new planet " + satelite.Count);

        }


        public void NewBall(int i, int newspeed)
        {
            satelite.Add(new Satelite(i, this.position.X, this.position.Y, newspeed));
            //satelite[i] = new satelite(i, this.position.X, this.position.Y, newspeed);
        }
    }
}
