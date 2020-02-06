using Jot;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Configuration
{
    public static class ConfigurationTracker
    {
        internal static Tracker Tracker = new Tracker();

        static ConfigurationTracker()
        {
            Tracker.Configure<UserConfiguration>()
                .Id(x => x.ClassIdentifier)
                .Properties(x => new { x.UserName, x.Port });
        }
    }
}
