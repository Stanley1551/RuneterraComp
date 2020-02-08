using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Configuration.Interfaces
{
    public interface IUserConfiguration : IBaseConfiguration
    {
        public string ClassIdentifier { get; }
    }
}
