using Jot;
using RuneterraCompanion.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Configuration
{
    //ennek itt nem sok értelme van így, valamit felé kéne húzni
    public class ConfigurationTracker : IConfigurationTracker
    {
        private Tracker Tracker = new Tracker();

        public ConfigurationTracker()
        {
        }

        Tracker IConfigurationTracker.Tracker => Tracker;
    }
}
