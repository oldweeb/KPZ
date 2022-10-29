using System.Collections.Generic;
using NUnit.Framework;
using Robot.Common;
using CRobot = Robot.Common.Robot;

namespace Pavliv.Maksym.RobotChallenge.Tests
{
    [TestFixture]
    public class PavlivAlgorithmTests
    {
        private Map _map;

        [OneTimeSetUp]
        public void SetUp()
        {
            _map = new Map(3, 40);
        }
        [Test]
        public void Author_ReturnsPavlivMaksym()
        {
            var alg = new PavlivAlgorithm();
            const string expected = "Pavliv Maksym";

            string actual = alg.Author;
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void Author_IsNotNull()
        {
            var alg = new PavlivAlgorithm();
            Assert.That(alg.Author, Is.Not.Null);
        }

        [Test]
        [TestCase(4, 2)]
        [TestCase(15, 10)]
        [TestCase(2, 44)]
        public void IsCellOccupied_OccupiedCell_ReturnsTrue(int x, int y)
        {
            var alg = new PavlivAlgorithm();

            var robots = new List<CRobot>()
            {
                new CRobot()
                {
                    Position = new Position(4, 2)
                },
                new CRobot()
                {
                    Position = new Position(15, 10)
                },
                new CRobot
                {
                    Position = new Position(2, 44)
                }
            };

            var pos = new Position(x, y);

            bool actual = alg.IsCellOccupied(pos, robots, 0, out _);
            Assert.That(actual, Is.True);
        }
        [Test]
        [TestCase(4, 23)]
        [TestCase(15, 11)]
        [TestCase(2, 4)]
        public void IsCellOccupied_FreeCell_ReturnsFalse(int x, int y)
        {
            var alg = new PavlivAlgorithm();

            var robots = new List<CRobot>()
            {
                new CRobot()
                {
                    Position = new Position(4, 2)
                },
                new CRobot()
                {
                    Position = new Position(15, 10)
                },
                new CRobot
                {
                    Position = new Position(2, 44)
                }
            };

            var pos = new Position(x, y);

            bool actual = alg.IsCellOccupied(pos, robots, 0, out _);
            Assert.That(actual, Is.False);
        }

        [Test]
        [TestCase(1, 1, 3)]
        public void FindOptimalRoute_ReturnsStationPosition(int x, int y, int variant)
        {
            var expectedPosition = new Position(x + 1, y);
            var prev = _map.Stations[0].Position;
            _map.Stations[0].Position = expectedPosition;
            var pos = new Position(x, y);

            var positions = new List<Position>
            {
                new Position(x + 1, y),
                new Position(x, y + 1),
                new Position(x - 1, y),
                new Position(x, y - 1)
            };

            var robots = new List<CRobot>
            {
                new CRobot()
                {
                    Energy = 60,
                    OwnerName = "Test",
                    Position = new Position(x + 10, y + 10)
                },
                new CRobot()
                {
                    Energy = 1000,
                    OwnerName = "Pavliv Maksym",
                    Position = new Position(x + 20, y + 3)
                },
                new CRobot()
                {
                    Energy = 500,
                    OwnerName = "Pavliv Maksym",
                    Position = new Position(x, y)
                }
            };

            var alg = new PavlivAlgorithm();
            var actual = alg.FindOptimalRoute(positions, robots, 2, _map);

            Assert.That(actual, Is.EqualTo(expectedPosition));
            _map.Stations[0].Position = prev;
        }

        [Test]
        [TestCase(1, 1, 3)]
        public void FindOptimalRoute_ReturnsRobotsPosition(int x, int y, int variant)
        {
            var expectedPosition = new Position(x + 1, y);

            var positions = new List<Position>
            {
                new Position(x + 1, y),
                new Position(x, y + 1),
                new Position(x - 1, y),
                new Position(x, y - 1)
            };

            var robots = new List<CRobot>
            {
                new CRobot()
                {
                    Energy = 60,
                    OwnerName = "Test",
                    Position = new Position(x + 10, y + 10)
                },
                new CRobot()
                {
                    Energy = 1000,
                    OwnerName = "Test",
                    Position = expectedPosition
                },
                new CRobot()
                {
                    Energy = 500,
                    OwnerName = "Pavliv Maksym",
                    Position = new Position(x, y)
                }
            };

            var alg = new PavlivAlgorithm();
            var actual = alg.FindOptimalRoute(positions, robots, 2, _map);

            Assert.That(actual, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void DoStep_RobotIsOnStation_ReturnsCollectEnergy()
        {
            var alg = new PavlivAlgorithm();

            var robots = new List<CRobot>
            {
                new CRobot()
                {
                    Energy = 500,
                    OwnerName = "Text",
                    Position = new Position(10, 10)
                },
                new CRobot()
                {
                    Energy = 500,
                    OwnerName = "Pavliv Maksym",
                    Position = _map.Stations[0].Position
                }
            };

            RobotCommand command = alg.DoStep(robots, 1, _map);
            Assert.That(command, Is.InstanceOf<CollectEnergyCommand>());
        }

        [Test]
        public void DoStep_EnoughEnergy_ReturnsCreateNewRobot()
        {
            var alg = new PavlivAlgorithm();
            alg.EnoughRoundCount = 0;

            var robots = new List<CRobot>
            {
                new CRobot()
                {
                    Energy = 500,
                    OwnerName = "Text",
                    Position = new Position(10, 10)
                },
                new CRobot()
                {
                    Energy = 1000,
                    OwnerName = "Pavliv Maksym",
                    Position = _map.Stations[0].Position
                }
            };

            RobotCommand command = alg.DoStep(robots, 1, _map);
            Assert.That(command, Is.InstanceOf<CreateNewRobotCommand>());
        }

        [Test]
        public void DoStep_Phase2_ReturnsCreateRobotPhase2()
        {
            var alg = new PavlivAlgorithm();
            for (var i = 0; i < 35; ++i)
            {
                Logger.LogRound(i);
            }

            var robots = new List<CRobot>()
            {
                new CRobot()
                {
                    Energy = 500,
                    OwnerName = "Text",
                    Position = new Position(10, 10)
                },
                new CRobot()
                {
                    Energy = 1000,
                    OwnerName = "Pavliv Maksym",
                    Position = _map.Stations[0].Position
                }
            };

            RobotCommand command = alg.DoStep(robots, 1, _map);
            Assert.That(command, Is.InstanceOf<CreateNewRobotCommand>());
            Assert.That(command.Description, Is.EqualTo("Not enought robots. Phase 2. Create new"));
        }

        [Test]

        public void DoStep_NotEnoughEnergyNotOnStation_ReturnsMove()
        {
            var alg = new PavlivAlgorithm();
            var robots = new List<CRobot>
            {
                new CRobot()
                {
                    Energy = 500,
                    OwnerName = "Text",
                    Position = new Position(10, 10)
                },
                new CRobot()
                {
                    Energy = 100,
                    OwnerName = "Pavliv Maksym",
                    Position = new Position(0, 0)
                }
            };

            RobotCommand command = alg.DoStep(robots, 0, _map);
            Assert.That(command, Is.InstanceOf<MoveCommand>());
        }
    }
}
