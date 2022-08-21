using System;
using System.Collections.Generic;
using System.Linq;
using Robot.Common;

using CRobot = Robot.Common.Robot;

namespace Pavliv.Maksym.RobotChallenge
{
    public class PavlivAlgorithm : IRobotAlgorithm
    {
        private const double StealRate = 0.05;
        private const int AttackLoss = 50;
        private const int MinEnergyBonus = 50;
        private const int MaxEnergyBonus = 100;
        private const int NewRobotPrice = 200;
        private const int MinAllowedEnergy = 600;
        private const int NewRobotEnergy = 400;
        private const int MaxRobotCount = 30;
        private const int OffsetX = 2;
        private const int OffsetY = 2;
        private int _roundCounter = 0;
        private int _myRobotCount = 10;

        public PavlivAlgorithm()
        {
            Logger.OnLogRound += (sender, e) => _roundCounter++;
        }

        public RobotCommand DoStep(IList<CRobot> robots, int robotToMoveIndex, Map map)
        {
            CRobot robot = robots[robotToMoveIndex];
            if (_myRobotCount < MaxRobotCount && robot.Energy >= MinAllowedEnergy && robot.IsOnStation(map, out _))
            {
                _myRobotCount++;
                return new CreateNewRobotCommand()
                {
                    Description = "Not enough robots. Create new",
                    NewRobotEnergy = PavlivAlgorithm.NewRobotEnergy
                };
            }

            if (robot.IsOnStation(map, out EnergyStation currentStation))
            {
                return new CollectEnergyCommand()
                {
                    Description = "Decided to collect energy"
                };
            }

            Position oldPosition = robot.Position;
            List<Position> availablePositions = map.GetCellsWithOffset(oldPosition, OffsetX, OffsetY).ToList();
            Position newPosition = FindOptimalRoute(availablePositions, robots, robotToMoveIndex, map);

            return new MoveCommand()
            {
                Description = $"Found optimal route: ({newPosition.X}; {newPosition.Y})",
                NewPosition = newPosition
            };
        }

        private bool IsCellOccupied(Position pos, IList<CRobot> robots, int robotToMoveIndex, out CRobot occupant)
        {
            CRobot robot = robots[robotToMoveIndex];

            occupant = null;
            foreach (CRobot r in robots)
            {
                if (r.Position != pos) continue;

                occupant = r;
                return true;
            }

            return false;
        }

        private Position FindOptimalRoute(
            List<Position> positions, 
            IList<CRobot> robots, 
            int robotToMoveIndex,
            Map map)
        {
            CRobot robot = robots[robotToMoveIndex];

            CRobot robotBestToAttack = null;
            Position optimalPosition = null;
            EnergyStation optimalStation = null;

            positions.ForEach(pos =>
            {
                if (IsCellOccupied(pos, robots, robotToMoveIndex, out CRobot occupant))
                {
                    if (occupant.OwnerName != robot.OwnerName)
                    {
                        robotBestToAttack = robotBestToAttack != null &&
                                            robotBestToAttack.Energy * StealRate > occupant.Energy * StealRate
                            ? robotBestToAttack
                            : occupant;

                        positions.Remove(pos);
                    }
                }
                
                List<EnergyStation> stations = map.GetNearbyResources(pos, OffsetX);
                var tryCounter = 0;
                while (!(stations = map.GetNearbyResources(pos, OffsetX + tryCounter).Where(st =>
                       {
                           bool isOccupied = IsCellOccupied(st.Position, robots, robotToMoveIndex, out CRobot putin);
                           return !isOccupied || putin.OwnerName != robot.OwnerName;
                       }).ToList()).Any())
                {
                    tryCounter++;
                }

                var closestStation = stations.MinBy(st => DistanceHelper.FindDistance(pos, st.Position));
                if (optimalPosition == null || 
                    DistanceHelper.FindDistance(optimalPosition, optimalStation.Position) > DistanceHelper.FindDistance(pos, closestStation.Position))
                {
                    optimalPosition = pos;
                    optimalStation = closestStation;
                }
            });

            return robotBestToAttack == null ? optimalPosition : robotBestToAttack.Position;
        }

        public string Author => "Pavliv Maksym";
    }
}
