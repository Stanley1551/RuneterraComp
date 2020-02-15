using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Jot;
using RuneterraCompanion.Configuration;
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

        //TODO az async void annyira nem jó
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //TODO: ha nem sikeres, lekezelni
            await Storage.TryInitializeAsync();
        }
    }
}
