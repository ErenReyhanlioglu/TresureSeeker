using ProLabHazine.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public abstract class Tresure
    {
        public int tresurePriority { get; set; }
        public int tresureXSize { get; set; }
        public int tresureYSize { get; set; }
        public string tresureName { get; set; }
        public Rectangle Bounds { get; set; }
        public Location Location { get; set; }

        public Tresure(int xLocation, int yLocation, int _tresureXSize, int _tresureYSize)
        {
            this.tresureXSize = _tresureXSize;
            this.tresureYSize = _tresureYSize;

            Location = new Location(xLocation, yLocation);

            Bounds = new Rectangle(xLocation, yLocation, _tresureXSize, _tresureYSize);
        }


    }
}
