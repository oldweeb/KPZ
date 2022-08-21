using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Robot.Common;
using CRobot = Robot.Common.Robot;

namespace Pavliv.Maksym.RobotChallenge
{
    public static class Extensions
    {
        #region RobotExtensions

        public static bool IsOnStation(this CRobot robot, Map map, out EnergyStation station)
        {
            Position pos = robot.Position;

            foreach (EnergyStation st in map.Stations)
            {
                if (st.Position == pos)
                {
                    station = st;
                    return true;
                }
            }

            station = null;
            return false;
        }

        #endregion

        #region StationExtensions

        public static bool IsEmpty(this EnergyStation station)
        {
            return station.Energy > 0;
        }

        #endregion

        #region MapExtensions

        public static IEnumerable<Position> GetCellsWithOffset(this Map map, Position center, int offsetX, int offsetY)
        {
            for (var i = -offsetX; i <= offsetX; ++i)
            {
                for (var j = -offsetY; j <= offsetY; ++j)
                {
                    if (i == j) continue;
                    var pos = new Position()
                    {
                        X = center.X + i,
                        Y = center.Y + j
                    };

                    if (map.IsValid(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }

        #endregion

        #region IEnumerable<T>Extensions

        public static TSource MinBy<TSource, TSelector>(this IEnumerable<TSource> source, Func<TSource, TSelector> selector)
        {
            return source.OrderBy(selector).First();
        }

        #endregion
    }
}
