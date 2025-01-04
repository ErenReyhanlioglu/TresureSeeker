using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public class Bee : DynamicObstacle
    {
        private bool moveRight = true;

        public Bee(int x, int y, int routeXSize, int routeYSize) : base(x, y, routeXSize, routeYSize)
        {
            obstacleSpriteXSize = 2;
            obstacleSpriteYSize = 2;

            obstacleName = "Arı";

            spriteBounds = new Rectangle(x, y, obstacleSpriteXSize, obstacleSpriteYSize);
        }

        public override void Move()
        {
            if (moveRight)
            {
                if (spriteBounds.Right < Bounds.Right)
                {
                    SpriteLocation.XLocation++;
                    UpdateSpriteBound();
                }
                else
                    moveRight = false; 
            }
            else
            {
                if (spriteBounds.Left > Bounds.Left)
                {
                    SpriteLocation.XLocation--;
                    UpdateSpriteBound();
                }
                else
                    moveRight = true;
            }
        }

        private void UpdateSpriteBound()
        {
            spriteBounds = new Rectangle(
            SpriteLocation.XLocation,
            SpriteLocation.YLocation,
            obstacleSpriteXSize,
            obstacleSpriteYSize);
        }
    }
}
