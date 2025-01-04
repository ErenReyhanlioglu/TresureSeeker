using ProLabHazine.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public abstract class Obstacle
    {
        public int obstacleXSize { get; set; }
        public int obstacleYSize { get; set; }
        public Rectangle Bounds { get; set; }
        public Location Location { get; set; }
        public string obstacleName { get; set; }

        public Obstacle(int x, int y, int obstacleXSize, int obstacleYSize)
        {
            this.obstacleXSize = obstacleXSize;
            this.obstacleYSize = obstacleYSize;
            Location = new Location(x, y);

            Bounds = new Rectangle(x, y, obstacleXSize, obstacleYSize);
        }
    }
}
