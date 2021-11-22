using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO_CAR
{
    class Car
    {
        public int timeToFix;
        public bool isFixed;
        public string type;

        public void _getTimeToFix()
        {
            Random random = new Random();
            timeToFix = random.Next(3, 6);
        }

        public Car()
        {
            _getTimeToFix();
            isFixed = false;
        }
    }
}
