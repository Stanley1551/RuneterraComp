using RuneterraCompanion.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Configuration
{
    public class UserConfiguration : IUserConfiguration
    {
        public string UserName {
            get { return userNameValue; } 
            set { if (value != userNameValue) { userNameValue = value; } } 
        }

        public int Port {
            get { return portValue; }
            set { if (value != portValue) { portValue = value; } }
        }

        public string ClassIdentifier => GetType().ToString();

        private string userNameValue;
        private int portValue;
    }
}
