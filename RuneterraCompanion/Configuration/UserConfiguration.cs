using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Configuration
{
    public class UserConfiguration
    {
        public string UserName { 
            get { return userNameValue; } 
            set { if (value != userNameValue) { userNameValue = value; } } 
        }

        public string Port {
            get { return portValue; }
            set { if (value != portValue) { portValue = value; } }
        }

        public string ClassIdentifier => GetType().ToString();

        private string userNameValue;
        private string portValue;
    }
}
