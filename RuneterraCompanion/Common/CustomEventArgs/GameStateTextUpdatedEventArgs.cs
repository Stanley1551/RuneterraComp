using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Common.CustomEventArgs
{
    internal class GameStateTextUpdatedEventArgs : EventArgs
    {
        internal GameStateTextUpdatedEventArgs(string updatedText) : base()
        {
            UpdatedText = updatedText;
        }

        internal string UpdatedText { get; set; }
        internal string PreviousText { get; set; }
    }
}
