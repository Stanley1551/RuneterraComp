using System;
using System.Collections.Generic;
using System.Text;
using RuneterraCompanion.ResponseModels;

namespace RuneterraCompanion.Common.CustomEventArgs
{
    internal class RemainingCardsUpdatedEventArgs : EventArgs
    {
        internal RemainingCardsUpdatedEventArgs(Dictionary<string, int> response) : base()
        {
            RemainingCardsDict = response;
        }

        internal Dictionary<string,int> RemainingCardsDict { get; set; }
    }
}
