using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO_CAR
{
    class Garage
    {
        public static int HOW_WORK; 
        public static int MIN_CAR;
        public static int MAX_CAR;
        public static int MIN_POLICE_TIME;
        public static int MAX_POLICE_TIME;
        public static int carComeInterval;
        public static int policeCarComeInterval;
        public static int Tact;
        public static int workTime;
        public static string _event;
        public static bool sleep;

        public static void _work()
        {
            if (Tact % 48 == 0)
            {
                sleep = true;
                workTime = 0;
                Random random = new Random();
                carComeInterval = Convert.ToInt32(HOW_WORK / random.Next(MIN_CAR, MAX_CAR));
                Queue.profCar.Clear();
            }
            if (policeCarComeInterval != 0)
            {
                policeCarComeInterval--;
                if (policeCarComeInterval == 0)
                {
                    Random random = new Random();
                    policeCarComeInterval = random.Next(MIN_POLICE_TIME, MAX_POLICE_TIME + 1);
                    Car car = new Car();
                    car.timeToFix++;
                    car.type = "police";
                    _event += $"Появилась полицейская машина. Время на ремонт = {car.timeToFix * 30} мин. ";
                    if (Mechanic.nowCar.type == "usual")
                    {
                        Mechanic.nowCar.timeToFix = Mechanic.workingTime;
                        Queue.fixCar.Insert(0, Mechanic.nowCar);
                        Mechanic.nowCar = car;
                        Mechanic.workingTime = car.timeToFix;
                    }
                    else if (Mechanic.nowCar.type == "police")
                    {
                        Mechanic.nowCar.timeToFix = Mechanic.workingTime;
                        Queue.fixCar.Insert(0, car);
                    }
                }
            }

            if (workTime < HOW_WORK)
            {
                workTime++;
                if (workTime % carComeInterval == 0)
                {
                    Car car = new Car();
                    car.type = "usual";
                    Queue.fixCar.Add(car);
                    _event += $"Появилась машина. Время на ремонт = {car.timeToFix*30} мин. ";
                }
                Mechanic._checkBusy();
            }
            else
            {
                _event += "Механик не работает. ";
            }
            Tact++;
        }

        public static string _getDay()
        {
            int day = (int)(Tact / 48);
            int hour = Convert.ToInt32((Tact - day*48) / 2);
            int min = (Tact % 2)*30;
            return $"{day} д. {hour} ч. {min} м.";
        }
    }

    
}
