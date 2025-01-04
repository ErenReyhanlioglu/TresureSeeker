using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public class Tree : StaticObstacle
    {
        public Tree(int x, int y, int obstacleXSize, int obstacleYSize, int seasonType) : 
                    base(x, y, obstacleXSize, obstacleYSize, seasonType)
        {
            obstacleName = "Ağaç";
        }
    }
}
