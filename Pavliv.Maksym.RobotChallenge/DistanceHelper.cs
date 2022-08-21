using System;
using System.Linq;
using Robot.Common;

namespace Pavliv.Maksym.RobotChallenge
{
    public static class DistanceHelper
    {
        private static int Min2D(int x1, int x2)
        {
            int[] nums =
            {
                (int) Math.Pow(x1 - x2, 2),
                (int) Math.Pow(x1 - x2 + 100, 2),
                (int) Math.Pow(x1 - x2 - 100, 2)
            };

            return nums.Min();
        }
        public static int FindDistance(Position a, Position b)
        {
            return (int) (Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static int CalculateLoss(Position a, Position b)
        {
            return Min2D(a.X, b.X) + Min2D(a.Y, b.Y);
        }
    }
}
