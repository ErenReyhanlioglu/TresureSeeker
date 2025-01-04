using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public class Bird : DynamicObstacle
    {
        private bool moveDown = true;

        public Bird(int x, int y, int routeXSize, int routeYSize) : base(x, y, routeXSize, routeYSize)
        {
            obstacleSpriteXSize = 2;
            obstacleSpriteYSize = 2;

            obstacleName = "Kuş";

            spriteBounds = new Rectangle(x, y, obstacleSpriteXSize, obstacleSpriteYSize);
        }

        public override void Move()
        {
            if (moveDown)
            {
                if (spriteBounds.Bottom < Bounds.Bottom)
                {
                    SpriteLocation.YLocation++;
                    UpdateBound();
                }
                else
                    moveDown = false;
            }
            else
            {
                if (spriteBounds.Top > Bounds.Top)
                {
                    SpriteLocation.YLocation--;
                    UpdateBound();
                }
                else
                    moveDown = true;
            }
        }

        private void UpdateBound()
        {
            spriteBounds = new Rectangle(
            SpriteLocation.XLocation,
            SpriteLocation.YLocation,
            obstacleSpriteXSize,
            obstacleSpriteYSize);
        }
    }
}
