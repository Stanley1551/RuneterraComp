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

namespace RuneterraCompanion.UserControls
{
    public partial class CardFilterHeader : UserControl
    {
        private MainWindow mainWindow = null; // Reference to the MainWindow

        public CardFilterHeader()
        {
            InitializeComponent();
        }
    }
}
