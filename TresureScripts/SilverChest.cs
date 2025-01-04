using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine.Scripts.TresureScripts
{
    public class SilverChest : Tresure
    {
        public SilverChest(int xLocation, int yLocation, int _tresureXSize, int _tresureYSize)
                            : base(xLocation, yLocation, _tresureXSize, _tresureYSize)
        {
            tresurePriority = 2;
            tresureName = "GumusSandik";
        }
    }
}
