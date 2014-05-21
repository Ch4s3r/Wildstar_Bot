using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Wildstar_Bot
{
    class Movement
    {
        public int move_forward()
        {
            
            return 0;
        }

        public void turn_left(float radians)
        {

        }

        public void turn_right(float radians)
        {

        }

        public double getAngle(Vector3D current, Vector3D next)
        {
            return Vector3D.AngleBetween(current, next);
        }
    }
}
