using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public abstract class DynamicObstacle : Obstacle
    {
        public Rectangle spriteBounds { get; set; }
        public int obstacleSpriteXSize { get; set; }
        public int obstacleSpriteYSize { get; set; }
        public Location SpriteLocation { get; set; }

        public DynamicObstacle(int XPosittion, int YPosition, int routeXSize, int routeYSize) : 
                                base (XPosittion, YPosition, routeXSize, routeYSize)
        {
            SpriteLocation = new Location (XPosittion, YPosition);
        }

        public virtual void Move() { }
    }
}
