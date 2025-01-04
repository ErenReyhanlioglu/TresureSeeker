using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public abstract class StaticObstacle : Obstacle
    {
        public int seasonType { get; set; }

        public StaticObstacle(int x, int y ,int obstacleXSize, int obstacleYSize, int seasonType) : base(x, y, obstacleXSize, obstacleYSize)
        {
            this.seasonType = seasonType;
        }
    }
}
