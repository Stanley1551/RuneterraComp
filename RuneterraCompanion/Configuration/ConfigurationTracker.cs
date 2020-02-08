using Jot;
using RuneterraCompanion.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Configuration
{
    public class ConfigurationTracker : IConfigurationTracker
    {
        private Tracker Tracker = new Tracker();

        public ConfigurationTracker()
        {
            //TODO: nem itt kéne!
            Tracker.Configure<UserConfiguration>()
                .Id(x => x.ClassIdentifier)
                .Properties(x => new { x.UserName, x.Port });
        }

        Tracker IConfigurationTracker.Tracker => Tracker;
    }
}
