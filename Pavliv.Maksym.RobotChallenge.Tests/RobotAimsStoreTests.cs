

using NUnit.Framework;
using Robot.Common;
using CRobot = Robot.Common.Robot;

namespace Pavliv.Maksym.RobotChallenge.Tests
{
    [TestFixture]
    public class RobotAimsStoreTests
    {
        [Test]
        [TestCase(3, 5)]
        [TestCase(10, 10)]
        [TestCase(0, 0)]
        public void SetAim_Position_SetsCorrectPosition(int x, int y)
        {
            var robot = new CRobot()
            {
                Energy = 1000,
                OwnerName = "YA",
                Position = new Position(20, 20)
            };

            var position = new Position(x, y);

            var store = RobotAimsStore.GetResetStore();
            store.SetAim(robot, position);

            Position actual = store.GetAimOrNull(robot);
            Assert.That(actual, Is.Not.Null.And.EqualTo(position));
        }

        [Test]
        [TestCase(3, 5)]
        [TestCase(10, 10)]
        public void IsOccupied_OccupiedCell_ReturnsTrue(int x, int y)
        {
            var robot = new CRobot()
            {
                Energy = 1000,
                OwnerName = "YA",
                Position = new Position(20, 20)
            };

            var position = new Position(x, y);

            var store = RobotAimsStore.GetResetStore();
            store.SetAim(robot, position);

            bool actual = store.IsOccupied(position);
            Assert.That(actual, Is.True);
        }

        [Test]
        [TestCase(3, 5)]
        [TestCase(10, 10)]
        public void IsOccupied_NotOccupiedCell_ReturnsFalse(int x, int y)
        {
            var store = RobotAimsStore.GetResetStore();
            bool actual = store.IsOccupied(new Position(x, y));
            Assert.That(actual, Is.False);
        }
    }
}
