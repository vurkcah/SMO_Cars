using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO_CAR
{
    class Formuls
    {
        public static double ShetRandMent;

        public static void _ShetMent()
        {
            if (Mechanic.nowCar.type == "police")
            {
                ShetRandMent++;

                
            }

            for (int i = 0; i < Queue.profCar.Count; i++)
            {
                if (Queue.profCar[i].type == "police")
                {
                    ShetRandMent++;
                }
            }

            for (int i = 0; i < Queue.fixCar.Count; i++)
            {
                if (Queue.fixCar[i].type == "police")
                {
                    ShetRandMent++;
                }
            }

        }



    }
}
