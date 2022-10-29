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
        private const int MinAllowedEnergy = 800;
        private const int NewRobotEnergy = 600;
        private const int OffsetX = 4;
        private const int OffsetY = 4;
        private int _roundCounter = 0;
        private int _myRobotCount = 10;

        private readonly RobotAimsStore _store;


        private void IncrementRoundCounter(object sender, LogRoundEventArgs e) 
        {
            _roundCounter++;
        }

        public PavlivAlgorithm()
        {
            Logger.OnLogRound += IncrementRoundCounter;
            _store = RobotAimsStore.GetStore();
        }

        public int MaxMyRobotCount { get; set; } = 20;
        public int Phase2MaxMyRobotCount { get; set; } = 80;
        public int EnoughRoundCount { get; set; } = 10;
        public int Phase2StartRound { get; set; } = 35;

        public RobotCommand DoStep(IList<CRobot> robots, int robotToMoveIndex, Map map)
        {
            MaxMyRobotCount = map.Stations.Count / 2;
            Phase2MaxMyRobotCount = (int)(map.Stations.Count / 1.5);
            CRobot robot = robots[robotToMoveIndex];
            
            if (_roundCounter >= EnoughRoundCount &&
                _roundCounter < Phase2StartRound &&
                _myRobotCount < MaxMyRobotCount &&
                _myRobotCount < 100 &&
                robot.Energy >= MinAllowedEnergy &&
                robot.IsOnStation(map, out _))
            {
                _myRobotCount++;
                return new CreateNewRobotCommand()
                {
                    Description = "Not enough robots. Create new",
                    NewRobotEnergy = NewRobotEnergy
                };
            }

            if (_roundCounter >= Phase2StartRound && 
                _roundCounter < 40 &&
                robot.IsOnStation(map, out _) && 
                _myRobotCount < Phase2MaxMyRobotCount && 
                _myRobotCount < 100 &&
                robot.Energy >= MinAllowedEnergy)
            {
                _myRobotCount++;
                return new CreateNewRobotCommand()
                {
                    Description = "Not enought robots. Phase 2. Create new",
                    NewRobotEnergy = NewRobotEnergy
                };
            }
            
            if (robot.IsOnStation(map, out _))
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

        public bool IsCellOccupied(Position pos, IList<CRobot> robots, int robotToMoveIndex, out CRobot occupant)
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

        public Position FindOptimalRoute(
            List<Position> positions, 
            IList<CRobot> robots, 
            int robotToMoveIndex,
            Map map)
        {
            CRobot robot = robots[robotToMoveIndex];

            CRobot robotBestToAttack = null;
            Position optimalPosition = null;
            EnergyStation optimalStation = null;

            positions.ToList().ForEach(pos =>
            {
                if (IsCellOccupied(pos, robots, robotToMoveIndex, out CRobot occupant))
                {
                    if (occupant.OwnerName != robot.OwnerName && occupant.Energy * StealRate >= AttackLoss)
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
                           return (!isOccupied || putin.OwnerName != robot.OwnerName);
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
                    //_store.SetAim(robot, optimalPosition);
                }
            });

            return robotBestToAttack == null ? optimalPosition : robotBestToAttack.Position;
        }

        public string Author => "Pavliv Maksym";
    }
}
