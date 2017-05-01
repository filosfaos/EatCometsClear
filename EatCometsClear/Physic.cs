using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Window;
using SFML.Graphics;


namespace EatCometsClear
{
    class Physic
    {
        enum Dimension{
            Top = 0,
            Right = 1,
            Bottom = 2,
            Left = 3,
            Null = 4
        }

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

        private Dimension IsCollision(Physical_object obj1, Physical_object obj2)
        {
            if (obj1.GetCollisionType() == obj2.GetCollisionType())
            {
                if(obj1.GetCollisionType() == new RectangleShape().GetType())
                {
                    if (obj1.CollisionShape.GetGlobalBounds().Contains(obj2.CollisionShape.GetLocalBounds().Left, obj2.CollisionShape.GetLocalBounds().Top))
                        return Dimension.Bottom;

                }
            }

            return Dimension.Null;
        }

        public void Collision(List<Physical_object> objects)
        {
            foreach(Physical_object element in objects)
            {
                if (element.CollisionShape == null)
                    continue;

                foreach(Physical_object element2 in objects)
                {
                    //czy obiekty nie są tym samym obiektem i są różne od null
                    if( (element != element2) && (element2.CollisionShape != null) )
                    {
                        //po której stronie koliduje obiekt
                        Dimension dimension = IsCollision(element, element2);

                        //w którą stronę ma działać kolizja
                        switch (dimension)
                        {
                            case Dimension.Top:
                                {

                                }
                                break;
                            case Dimension.Right:
                                {

                                }
                                break;
                            case Dimension.Bottom:
                                {
                                    
                                }
                                break;
                            case Dimension.Left:
                                {
                                    
                                }
                                break;
                            case Dimension.Null:
                                {

                                }
                                break;
                        }
                    }
                    //Jeżeli obiekty są takie same nic sie nie dzieje
                }

            }
        }
    }

}
