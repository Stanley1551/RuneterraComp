using System;
using System.Collections.Generic;
using System.Text;
using Jot.Configuration;

namespace RuneterraCompanion.Configuration.Interfaces
{
    public interface IUserConfiguration : IBaseConfiguration, ITrackingAware<IUserConfiguration>
    {
        public string UserName { get; set; }
        public string ClassIdentifier { get; }
    }
}
