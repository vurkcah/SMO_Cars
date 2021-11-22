using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO_CAR
{
    static class Mechanic
    {
       static public int workingTime;
       static public Car nowCar = new Car()
       { type = "usual" };

        static public void _checkBusy()
        {
            if (workingTime != 0)
            {
                workingTime--;
                if (workingTime == 0)
                {
                    if (nowCar.type == "usual")
                    {
                        Garage._event += "Механик починил машину. ";
                    }
                    else
                    {
                        Garage._event += "Механик починил полицейскую машину. ";
                    }
                    nowCar.isFixed = true;
                    Queue.profCar.Add(nowCar);
                }
            }
            else
            {
                if (Queue.fixCar.Count != 0)
                {
                    Garage._event += "Механик начал чинить машину. ";
                    workingTime = Queue.fixCar[0].timeToFix;
                    nowCar = Queue.fixCar[0];
                    Queue.fixCar.RemoveAt(0);
                }
            }
        }
    }
}
