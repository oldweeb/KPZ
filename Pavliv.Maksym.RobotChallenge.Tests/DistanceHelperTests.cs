using NUnit.Framework;
using Robot.Common;

namespace Pavliv.Maksym.RobotChallenge.Tests
{
    [TestFixture]
    public class DistanceHelperTests
    {
        [Test]
        [TestCase(0, 0, 3, 4, 25)]
        [TestCase(0, 0, 0, 10, 100)]
        [TestCase(0, 0, 0, 0, 0)]
        public void FindDistance_2Positions_ReturnsCorrectDistance(
            int x1,
            int y1,
            int x2, 
            int y2,
            int expectedDistance) 
        {
            var p1 = new Position(x1, y1);
            var p2 = new Position(x2, y2);

            int actualDistance = DistanceHelper.FindDistance(p1, p2);

            Assert.That(actualDistance, Is.EqualTo(expectedDistance));
        }
    }
}
