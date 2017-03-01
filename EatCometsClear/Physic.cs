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
                                float o1x = objekt.position.X, o1y = objekt.position.Y;
                                float o2x = objekt2.position.X, o2y = objekt2.position.Y;



                                //różnice między obiektami w odległości
                                float rozx = 0, rozy = 0;
                                rozx = (float)Math.Pow((o1x - o2x), 2);
                                rozy = (float)Math.Pow((o1y - o2y), 2);

                                float c = rozx + rozy;
                                //c = kwadrat odległości między obiektami

                                float masa, masa2;
                                masa = (float)objekt.GetMass() * objekt.gravityStrength;
                                masa2 = (float)objekt2.GetMass() * objekt2.gravityStrength;

                                float gravity_strenght;

                                gravity_strenght = (masa + masa2) / c;

                                gravity_strenght /= 2; //na pół bo jest liczone dwa razy


                                c = (float)Math.Sqrt(c);

                                float xChangeMultipler = Math.Abs( objekt.position.X - objekt2.position.X);
                                float yChangeMultipler = Math.Abs( objekt.position.Y - objekt2.position.Y);

                                float xChange;
                                xChange = gravity_strenght * (xChangeMultipler / c);

                                float yChange;
                                yChange = gravity_strenght * (yChangeMultipler / c);


                                if (objekt.GetMass() > objekt2.GetMass())
                                {
                                    if (objekt.position.X < objekt2.position.X)
                                    {
                                        objekt2.position.X -= xChange;
                                        if (objekt.position.X > objekt2.position.X)
                                            objekt2.position.X = objekt.position.X;
                                    }

                                    if (objekt.position.X > objekt2.position.X)
                                    {
                                        objekt2.position.X += xChange;
                                        if (objekt.position.X < objekt2.position.X)
                                            objekt2.position.X = objekt.position.X;
                                    }

                                    if (objekt.position.Y < objekt2.position.Y)
                                    {
                                        objekt2.position.Y -= yChange;
                                        if (objekt.position.Y > objekt2.position.Y)
                                            objekt2.position.Y = objekt.position.Y;
                                    }

                                    if (objekt.position.Y > objekt2.position.Y)
                                    {
                                        objekt2.position.Y += yChange;
                                        if (objekt.position.Y < objekt2.position.Y)
                                            objekt2.position.Y = objekt.position.Y;
                                    }
                                }
                                else if (objekt.GetMass() < objekt2.GetMass())
                                {
                                    if (objekt2.position.X < objekt.position.X)
                                    {
                                        objekt.position.X -= gravity_strenght;
                                        if (objekt2.position.X > objekt.position.X)
                                            objekt.position.X = objekt2.position.X;
                                    }

                                    if (objekt2.position.X > objekt.position.X)
                                    {
                                        objekt.position.X += gravity_strenght;
                                        if (objekt2.position.X < objekt.position.X)
                                            objekt.position.X = objekt2.position.X;
                                    }

                                    if (objekt2.position.Y < objekt.position.Y)
                                    {
                                        objekt.position.Y -= gravity_strenght;
                                        if (objekt2.position.Y > objekt.position.Y)
                                            objekt.position.Y = objekt2.position.Y;
                                    }

                                    if (objekt2.position.Y > objekt.position.Y)
                                    {
                                        objekt.position.Y += gravity_strenght;
                                        if (objekt2.position.Y < objekt.position.Y)
                                            objekt.position.Y = objekt2.position.Y;
                                    }

                                }
                                else
                                {
                                    //jak ta sama maso to się nic nie powinno dziać xD
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
