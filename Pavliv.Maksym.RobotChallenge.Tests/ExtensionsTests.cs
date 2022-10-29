using NUnit.Framework;
using Robot.Common;

namespace Pavliv.Maksym.RobotChallenge.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void RobotGetCopy_Robot_ReturnsCopyDifferentReferences()
        {
            var robot = new Robot.Common.Robot()
            {
                Energy = 1000,
                OwnerName = "Pavliv Maksym",
                Position = new Position(25, 10)
            };

            var copy = robot.GetCopy();
            Assert.That(object.ReferenceEquals(robot, copy), Is.False);
            Assert.That(robot.Energy, Is.EqualTo(copy.Energy));
            Assert.That(robot.OwnerName, Is.EqualTo(copy.OwnerName));
            Assert.That(robot.Position, Is.EqualTo(copy.Position));
        }

        [Test]
        [TestCase(1000)]
        [TestCase(50)]
        [TestCase(200)]
        public void StationIsEmpty_EmptyStation_ReturnsTrue(int energy)
        {
            var station = new EnergyStation()
            {
                Energy = energy
            };

            bool actual = station.IsEmpty();
            Assert.That(actual, Is.True);
        }

        [Test]
        public void StationIsEmpty_NotEmptyStation_ReturnsFalse()
        {
            var station = new EnergyStation()
            {
                Energy = 0
            };

            bool actual = station.IsEmpty();
            Assert.That(actual, Is.False);
        }
    }
}
