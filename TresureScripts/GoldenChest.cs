using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine.Scripts.TresureScripts
{
    public class GoldenChest : Tresure
    {
        public GoldenChest(int xLocation, int yLocation, int _tresureXSize, int _tresureYSize)
                            : base(xLocation, yLocation, _tresureXSize, _tresureYSize)
        {
            tresurePriority = 1;
            tresureName = "AltinSandik";
        }
    }
}
