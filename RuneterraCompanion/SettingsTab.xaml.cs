using Jot;
using RuneterraCompanion.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuneterraCompanion
{
    public partial class SettingsTab : UserControl
    {
        private MainWindow mainWindow = null; // Reference to the MainWindow

        private Regex numberRegex = new Regex("[^0-9]+");

        public SettingsTab()
        {
            InitializeComponent();
            
            // visszaállitani egy objektum értékeit, datacontextnek beadni!
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //quick workaround
            if (mainWindow == null)
            {
                mainWindow = (MainWindow)Application.Current.MainWindow;
                DataContext = mainWindow.Configuration;
            }
        }

        private void CardIntegrityCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckPopup popup = new CheckPopup();
            popup.Show();
        }

        private void PortField_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = numberRegex.IsMatch(e.Text);
        }

        private void PortField_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (numberRegex.IsMatch(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void UserNameField_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            mainWindow.Tracker.Tracker.Persist(mainWindow.Configuration);
        }
    }
}
