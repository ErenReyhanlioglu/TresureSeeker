using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public class Location
    {
        public int XLocation { get; set; }
        public int YLocation { get; set; }

        public Location(int XPosition, int YPosition)
        {
            XLocation = XPosition;
            YLocation = YPosition;
        }
    }
}
