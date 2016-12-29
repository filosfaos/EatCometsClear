using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Window;


namespace EatCometsClear
{
    class Physic
    {


        public void Gravitation(List<Physical_object> objects)
        {
            foreach ( Physical_object objekt in objects )
            {
                foreach (Physical_object objekt2 in objects)
                {
                    try
                    {
                        if (objekt != objekt2)
                        {
                            if (objekt.enableGravity && objekt2.enableGravity)
                            {
                                float x = 0, y = 0;

                                x = (float)Math.Pow((objekt.position.X - objekt2.position.X), 2);
                                y = (float)Math.Pow((objekt.position.Y - objekt2.position.Y), 2);

                                float c = x + y;
                                //c = kwadrat odległości między obiektami

                                float masa, masa2;
                                masa = (float)objekt.mass * objekt.gravityStrength;
                                masa2 = (float)objekt2.mass * objekt2.gravityStrength;

                                float gravity_strenght;

                                gravity_strenght = (masa + masa2) / c;

                                gravity_strenght /= 2; //na pół bo jest liczone dwa razy

                                if (objekt.mass > objekt2.mass)
                                {
                                    if (objekt.position.X < objekt2.position.X)
                                    {
                                        objekt2.position.X -= gravity_strenght;
                                        if (objekt.position.X > objekt2.position.X)
                                            objekt2.position.X = objekt.position.X;
                                    }

                                    if (objekt.position.X > objekt2.position.X)
                                    {
                                        objekt2.position.X += gravity_strenght;
                                        if (objekt.position.X < objekt2.position.X)
                                            objekt2.position.X = objekt.position.X;
                                    }

                                    if (objekt.position.Y < objekt2.position.Y)
                                    {
                                        objekt2.position.Y -= gravity_strenght;
                                        if (objekt.position.Y > objekt2.position.Y)
                                            objekt2.position.Y = objekt.position.Y;
                                    }

                                    if (objekt.position.Y > objekt2.position.Y)
                                    {
                                        objekt2.position.Y += gravity_strenght;
                                        if (objekt.position.Y < objekt2.position.Y)
                                            objekt2.position.Y = objekt.position.Y;
                                    }


                                }
                                else if (objekt.mass < objekt2.mass)
                                {

                                }
                                else
                                {

                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                
            }


        }

    }

}
