﻿using System;
using Robot.Common;

namespace Pavliv.Maksym.RobotChallenge
{
    public static class DistanceHelper
    {
        public static int FindDistance(Position a, Position b)
        {
            return (int) (Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
