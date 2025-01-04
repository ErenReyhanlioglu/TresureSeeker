using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public class Wall : StaticObstacle
    {
        public Wall(int x, int y, int obstacleXSize, int obstacleYSize, int seasonType) : 
                    base(x, y, obstacleXSize, obstacleYSize, seasonType)
        {
            obstacleXSize = 10;
            obstacleYSize = 1;

            obstacleName = "Duvar";

            Bounds = new Rectangle(x, y, obstacleXSize, obstacleYSize   );
        }
    }
}
