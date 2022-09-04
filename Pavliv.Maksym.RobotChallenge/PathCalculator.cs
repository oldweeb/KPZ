using System;
using System.Collections.Generic;
using Robot.Common;

namespace Pavliv.Maksym.RobotChallenge
{
    internal class PathCalculator
    {
        public int AttackEnergyLoss { get; set; } = 50;

        public IReadOnlyList<Position> CalculateCheapestRoute(Robot.Common.Robot robot, Map map, Position endPoint)
        {
            Position current = robot.Position;
            Tuple<int, int> offset = Tuple.Create(Math.Abs(current.X - endPoint.X), Math.Abs(current.Y - endPoint.Y));
            Robot.Common.Robot clone = robot.GetCopy();
            var path = new List<Position>();

            while (!current.Equals(endPoint))
            {
                Position next = GetNext(current, endPoint, robot, map);
                clone.Energy -= DistanceHelper.FindDistance(current, next);
                current = next;
                path.Add(current);
            }

            return path.AsReadOnly();
        }

        private Position GetNext(
            Position current, 
            Position endPoint,
            Robot.Common.Robot robot,
            Map map
            )
        {
            if (robot.Energy >= DistanceHelper.FindDistance(current, endPoint) + AttackEnergyLoss)
            {
                return new Position(endPoint.X, endPoint.Y);
            }

            throw new NotImplementedException();
        }

        public IEnumerable<Position> CalculateFastestRoute(Robot.Common.Robot robot, Map map, Position endPoint)
        {
            throw new NotImplementedException();
        }


    }
}
