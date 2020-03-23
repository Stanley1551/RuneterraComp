using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Jot;
using RuneterraCompanion.Configuration;
using RuneterraCompanion.Configuration.Interfaces;
using RuneterraCompanion.CustomModels;
using RuneterraCompanion.Storage;

namespace RuneterraCompanion
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public CardDataStorage<Card> Storage { get; } = new CardDataStorage<Card>();
        public IConfigurationTracker Tracker { get; set; }
        public IUserConfiguration Configuration { get; set; }

        //TODO az async void annyira nem jó
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Tracker = new ConfigurationTracker();
            Configuration = new UserConfiguration();

            Tracker.Tracker.Track(Configuration);

            //TODO: ha nem sikeres, lekezelni
            Storage.TryInitializeAsync();
        }
    }
}
