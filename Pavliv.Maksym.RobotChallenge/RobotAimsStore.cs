using System;
using System.Collections.Generic;
using System.Linq;
using Robot.Common;

namespace Pavliv.Maksym.RobotChallenge
{
    public class RobotAimsStore
    {
        private static readonly Lazy<RobotAimsStore> s_store = new Lazy<RobotAimsStore>(() => new RobotAimsStore());
        private readonly Dictionary<Robot.Common.Robot, Position> _aims;

        private RobotAimsStore()
        {
            _aims = new Dictionary<Robot.Common.Robot, Position>();
        }

        public static RobotAimsStore GetStore()
        {
            return s_store.Value;
        }

        private void Reset()
        {
            _aims.Clear();
        }

        public void SetAim(Robot.Common.Robot robot, Position aim)
        {
            _aims[robot] = aim;
        }

        public Position GetAimOrNull(Robot.Common.Robot robot)
        {
            if (_aims.TryGetValue(robot, out Position aim))
            {
                return aim;
            }

            return null;
        }

        public bool IsOccupied(Position target)
        {
            return _aims.Any(kv => kv.Value == target);
        }

        public static RobotAimsStore GetResetStore()
        {
            var store = s_store.Value;
            store.Reset();
            return store;
        }
    }
}
