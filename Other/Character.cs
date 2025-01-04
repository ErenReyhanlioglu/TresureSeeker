using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProLabHazine.Character;

namespace ProLabHazine
{
    public class Character
    {
        #region PROPS, VALUES AND OBJECTS

        public int ID { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public Rectangle Bounds { get; set; }

        AnaForm anaForm;
        Runtime runtime;

        private HashSet<Location> visitedArea = new HashSet<Location>();

        List<Tresure> tresures;
        List<StaticObstacle> staticObstacles;
        List<DynamicObstacle> dynamicObstacles;

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        }
        public struct Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        #endregion

        public Character(int id, string name, int x, int y, int playerXSize, int playerYSize, AnaForm _anaForm,
            List<Tresure> _tresures, List<StaticObstacle> _staticObstacles, List<DynamicObstacle> _dynamicObstacles)
        {
            ID = id;
            Name = name;
            Location = new Location(x, y);

            Bounds = new Rectangle(x, y, playerXSize, playerYSize);
            anaForm = _anaForm;

            tresures = _tresures;
            staticObstacles = _staticObstacles;
            dynamicObstacles = _dynamicObstacles;

            runtime = new Runtime(tresures, anaForm);
        }

        public void ShortestPath(Location targetLocation)
        {

        }

        public Direction SelectDirection()
        {
            bool right = true, left = true, top = true, bottom = true;

            bool isRightVisited = visitedArea.Any(P =>
            P.XLocation == Location.XLocation + Bounds.Width + 1 &&
            (P.YLocation == Location.YLocation) // || P.YLocation == Location.YLocation + Bounds.Height
            );

            bool isLeftVisited = visitedArea.Any(P =>
                P.XLocation == Location.XLocation - 1 &&
                (P.YLocation == Location.YLocation) // || P.YLocation == Location.YLocation + Bounds.Height
            );

            bool isTopVisited = visitedArea.Any(P =>
                P.YLocation == Location.YLocation - 1 &&
                (P.XLocation == Location.XLocation) // || P.XLocation == Location.XLocation + Bounds.Width
            );

            bool isBottomVisited = visitedArea.Any(P =>
                P.YLocation == Location.YLocation + Bounds.Height + 1 &&
                (P.XLocation == Location.XLocation) // || P.XLocation == Location.XLocation + Bounds.Width
            );

            right = !isRightVisited;
            left = !isLeftVisited;
            top = !isTopVisited;
            bottom = !isBottomVisited;

            Point tresurePoint = RotateForTresure();

            List<Direction> availableDirections = new List<Direction>();

            if (right && IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap))
                availableDirections.Add(Direction.Right);

            if (left && IsValidMove(Location.XLocation - 1, Location.YLocation, AnaForm.fogMap))
                availableDirections.Add(Direction.Left);

            if (top && IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
                availableDirections.Add(Direction.Up);

            if (bottom && IsValidMove(Location.XLocation, Location.YLocation + 1, AnaForm.fogMap))
                availableDirections.Add(Direction.Down);

            if (tresurePoint.X != -1)
            {
                availableDirections.Clear();

                if (Location.XLocation < tresurePoint.X)
                {
                    availableDirections.Add(Direction.Right);
                }
                else if (Location.XLocation > tresurePoint.X)
                {
                    availableDirections.Add(Direction.Left);
                }

                if (Location.YLocation < tresurePoint.Y)
                {
                    availableDirections.Add(Direction.Down);
                }
                else if (Location.YLocation > tresurePoint.Y)
                {
                    availableDirections.Add(Direction.Up);
                }

                Random random = new Random();
                int index = random.Next(0, availableDirections.Count);
                return availableDirections[index];
            }
            else if (availableDirections.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(0, availableDirections.Count);
                return availableDirections[index];
            }
            else
                return SelectNearestUnDiscoverdDirection();
        }

        private Direction SelectNearestUnDiscoverdDirection()
        {
            int newX = Location.XLocation;
            int newY = Location.YLocation;

            Point playerLocation = new Point(newX, newY);

            Point closestFogyLocation = FindClosestFoggyArea(playerLocation, AnaForm.fogMap);

            while (Location.XLocation != closestFogyLocation.X || Location.YLocation != closestFogyLocation.Y)
            {
                if (Location.XLocation < closestFogyLocation.X)
                {
                    if (IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap))
                        return Direction.Right;
                    else
                        return IfSelectDirectionWayIsBlocked(Direction.Right);
                }
                else if (Location.XLocation > closestFogyLocation.X)
                {
                    if (IsValidMove(Location.XLocation - 1, Location.YLocation, AnaForm.fogMap))
                        return Direction.Left;
                    else
                        return IfSelectDirectionWayIsBlocked(Direction.Left);
                }

                if (Location.YLocation < closestFogyLocation.Y)
                {
                    if (IsValidMove(Location.XLocation, Location.YLocation + 1, AnaForm.fogMap))
                        return Direction.Down;
                    else
                        return IfSelectDirectionWayIsBlocked(Direction.Down);
                }
                else if (Location.YLocation > closestFogyLocation.Y)
                {
                    if (IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
                        return Direction.Up;
                    else
                        return IfSelectDirectionWayIsBlocked(Direction.Up);
                }
            }

            return Direction.None;
        }

        int movementValue = 0;
        Direction nextDirection = Direction.None;
        private Direction IfSelectDirectionWayIsBlocked(Direction direction) // Örneğin aşağı gitmesi lazım fakat sağ kapalı olduğu için sola gidiyor diyelim bir sol gidince açılan sağa gidecek sonrasında sağ tekrar kapalı oldugu için bir birim sola gidecek böylece sonsuz döngüye girecek hareketlerin bir queue'su oluşturulmalı
        {

            nextDirection = Direction.None;
            nextDirection = direction;

            if (direction == Direction.Right && !IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap)) // sadeleşebilir. IsValidM
            {
                if (IsValidMove(Location.XLocation, Location.YLocation + 1, AnaForm.fogMap))
                {
                    while (!IsValidMove(Location.XLocation + 1, Location.YLocation + movementValue, AnaForm.fogMap) && IsValidMove(Location.XLocation, Location.YLocation + 1 + movementValue, AnaForm.fogMap))
                    {
                        //Location.YLocation += 1;
                        movementValue += 1;
                    }
                }
                else if (IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
                {
                    while (!IsValidMove(Location.XLocation + 1, Location.YLocation - movementValue, AnaForm.fogMap) && IsValidMove(Location.XLocation, Location.YLocation - 1 - movementValue, AnaForm.fogMap))
                    {
                        //Location.YLocation -= 1;
                        movementValue -= 1;
                    }
                }

                //if (IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
                //    nextDirection = Direction.Up;
                //else if (IsValidMove(Location.XLocation - 1, Location.YLocation, AnaForm.fogMap))
                //    nextDirection = Direction.Left;
                //else
                //    nextDirection = Direction.None;
            }
            else if (direction == Direction.Left && !IsValidMove(Location.XLocation - 1, Location.YLocation, AnaForm.fogMap))
            {
                if (IsValidMove(Location.XLocation, Location.YLocation + 1, AnaForm.fogMap))
                {
                    while (!IsValidMove(Location.XLocation - 1, Location.YLocation + movementValue, AnaForm.fogMap) && IsValidMove(Location.XLocation, Location.YLocation + 1 + movementValue, AnaForm.fogMap))
                    {
                        //Location.YLocation += 1;
                        movementValue += 1;
                    }
                }
                else if (IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
                {
                    while (!IsValidMove(Location.XLocation - 1, Location.YLocation - movementValue, AnaForm.fogMap) && IsValidMove(Location.XLocation, Location.YLocation - 1 - movementValue, AnaForm.fogMap))
                    {
                        //Location.YLocation -= 1;
                        movementValue -= 1;
                    }
                }
                //if (IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
                //    nextDirection = Direction.Up;
                //else if (IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap))
                //    nextDirection = Direction.Right;
                //else
                //    nextDirection = Direction.None;
            }
            else if (direction == Direction.Down && !IsValidMove(Location.XLocation, Location.YLocation + 1, AnaForm.fogMap))
            {
                if (IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap))
                {
                    while (!IsValidMove(Location.XLocation + movementValue, Location.YLocation + 1, AnaForm.fogMap) && IsValidMove(Location.XLocation + 1 + movementValue, Location.YLocation, AnaForm.fogMap))
                    {
                        //Location.XLocation += 1;
                        movementValue += 1;
                    }
                }
                else if (IsValidMove(Location.XLocation - 1, Location.YLocation, AnaForm.fogMap))
                {
                    while (!IsValidMove(Location.XLocation - movementValue, Location.YLocation + 1, AnaForm.fogMap) && IsValidMove(Location.XLocation - 1 - movementValue, Location.YLocation, AnaForm.fogMap))
                    {
                        //Location.XLocation -= 1;
                        movementValue -= 1;
                    }
                }
                //if (IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap))
                //    nextDirection = Direction.Right;
                //else if (IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
                //    nextDirection = Direction.Up;
                //else
                //    nextDirection = Direction.None;
            }
            else if (direction == Direction.Up && !IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
            {
                if (IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap))
                {
                    while (!IsValidMove(Location.XLocation + movementValue, Location.YLocation - 1, AnaForm.fogMap) && IsValidMove(Location.XLocation + 1 + movementValue, Location.YLocation, AnaForm.fogMap))
                    {
                        //Location.XLocation += 1;
                        movementValue += 1;
                    }
                }
                else if (IsValidMove(Location.XLocation - 1, Location.YLocation, AnaForm.fogMap))
                {
                    while (!IsValidMove(Location.XLocation - movementValue, Location.YLocation - 1, AnaForm.fogMap) && IsValidMove(Location.XLocation - 1 - movementValue, Location.YLocation, AnaForm.fogMap))
                    {
                        //Location.XLocation -= 1;
                        movementValue -= 1;
                    }
                }
                //if (IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap))
                //    nextDirection = Direction.Right;
                //else if (IsValidMove(Location.XLocation, Location.YLocation + 1, AnaForm.fogMap))
                //    nextDirection = Direction.Down;
                //else
                //    nextDirection = Direction.None;
            }

            return Direction.None;
        }

        public void Move(bool[,] fogMap, List<StaticObstacle> staticObstacles, List<DynamicObstacle> dynamicObstacles, List<Tresure> tresures)
        {
            int newX = Location.XLocation;
            int newY = Location.YLocation;

            switch (SelectDirection())
            {
                case Direction.Up:
                    newY -= 1;
                    break;
                case Direction.Down:
                    newY += 1;
                    break;
                case Direction.Left:
                    newX -= 1;
                    break;
                case Direction.Right:
                    newX += 1;
                    break;
                case Direction.None:
                    if (nextDirection == Direction.Right)
                    {
                        if (IsValidMove(Location.XLocation, Location.YLocation + 1, AnaForm.fogMap))
                        {
                            newY = newY + movementValue;
                            movementValue = 0;
                            break;
                        }
                        else if (IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
                        {
                            newY = newY - movementValue;
                            movementValue = 0;
                            break;
                        }
                    }
                    else if (nextDirection == Direction.Left)
                    {
                        if (IsValidMove(Location.XLocation, Location.YLocation + 1, AnaForm.fogMap))
                        {
                            newY = newY + movementValue;
                            movementValue = 0;
                            break;
                        }
                        else if (IsValidMove(Location.XLocation, Location.YLocation - 1, AnaForm.fogMap))
                        {
                            newY = newY - movementValue;
                            movementValue = 0;
                            break;
                        }
                    }
                    else if (nextDirection == Direction.Down)
                    {
                        if (IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap))
                        {
                            newX = newX + movementValue;
                            movementValue = 0;
                            break;
                        }
                        else if (IsValidMove(Location.XLocation - 1, Location.YLocation, AnaForm.fogMap))
                        {
                            newX = newX - movementValue;
                            movementValue = 0;
                            break;
                        }
                    }
                    else if (nextDirection == Direction.Up)
                    {
                        if (IsValidMove(Location.XLocation + 1, Location.YLocation, AnaForm.fogMap))
                        {
                            newX = newX + movementValue;
                            movementValue = 0;
                            break;
                        }
                        else if (IsValidMove(Location.XLocation - 1, Location.YLocation, AnaForm.fogMap))
                        {
                            newX = newX - movementValue;
                            movementValue = 0;
                            break;
                        }
                    }
                    break;
            }

            if (IsValidMove(newX, newY, fogMap))
            {
                Location.XLocation = newX;
                Location.YLocation = newY;
                Runtime.totalStepValue++;
                anaForm.lblTotalStepValue.Text = $"Toplam adım sayısı: {Runtime.totalStepValue}";
            }

            runtime.CollectTresures(newX, newY);

            UpdateVisitedArea();
        }
        private void UpdateVisitedArea()
        {
            for (int i = Location.XLocation; i <= Location.XLocation + Bounds.Width; i++)
            {
                for (int j = Location.YLocation; j <= Location.YLocation + Bounds.Height; j++)
                {
                    Location tempLocation = new Location(i, j);
                    if (!visitedArea.Contains(tempLocation))
                    {
                        visitedArea.Add(tempLocation);
                    }
                }
            }
        }

        private bool IsValidMove(int nextXLocation, int nextYLocation, bool[,] fogMap)
        {
            if (nextXLocation < 0 || nextYLocation < 0 || (nextXLocation) >= fogMap.GetLength(0) || (nextYLocation) >= fogMap.GetLength(1))
                return false;

            foreach (StaticObstacle obstacle in staticObstacles)
                if (obstacle.Bounds.Contains(nextXLocation, nextYLocation))
                {
                    runtime.DiscoveredStaticObstacles(obstacle);
                    return false;
                }

            foreach (DynamicObstacle obstacle in dynamicObstacles)
                if (obstacle.Bounds.Contains(nextXLocation, nextYLocation))
                {
                    runtime.DiscoveredDynamicObstacles(obstacle);
                    return false;
                }

            return true;
        }

        public static Point FindClosestFoggyArea(Point playerLocation, bool[,] fogMap)
        {
            int minDistance = int.MaxValue;
            Point closestFoggyArea = new Point(-1, -1);

            for (int i = 0; i < fogMap.GetLength(0); i++)
            {
                for (int j = 0; j < fogMap.GetLength(1); j++)
                {
                    if (fogMap[i, j])
                    {
                        int distance = CalculateDistance(playerLocation, new Point(i, j));
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestFoggyArea = new Point(i, j);
                        }
                    }
                }
            }

            return closestFoggyArea;
        }

        private static int CalculateDistance(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        private Point RotateForTresure()
        {
            Point tresurePoint = new Point(-1, -1);
            Tresure tresureWithLowestPriority = null;

            foreach (Tresure tresure in tresures)
            {
                if (!AnaForm.fogMap[tresure.Location.XLocation, tresure.Location.YLocation])
                {
                    if (tresureWithLowestPriority == null || tresure.tresurePriority < tresureWithLowestPriority.tresurePriority)
                    {
                        tresurePoint.X = tresure.Location.XLocation;
                        tresurePoint.Y = tresure.Location.YLocation;
                        tresureWithLowestPriority = tresure;
                    }
                }
            }

            return tresurePoint;
        }
    }
}

