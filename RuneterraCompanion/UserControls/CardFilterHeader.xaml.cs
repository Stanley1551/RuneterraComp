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

        private List<ComboRow> regions;
        private List<ComboRow> types;
        private List<ComboRow> rarities;

        private bool AreComboboxesPopulated => regions != null && regions.Count > 0 &&
                                               types != null && types.Count > 0 &&
                                               rarities != null && rarities.Count > 0;

        private List<string> SortingValues = new List<string>() { "Cost", "Attack", "Health" };

        public CardFilterHeader()
        {
            InitializeComponent();
            SortingComboBox.ItemsSource = SortingValues; 
        }

        public List<string> GetSelectedRegions()
        {
            return regions.FindAll(x => x.IsSelected).Select(x => x.Text).ToList();
        }

        public List<string> GetSelectedTypes()
        {
            return types.FindAll(x => x.IsSelected).Select(x => x.Text).ToList();
        }

        public List<string> GetSelectedRarities()
        {
            return rarities.FindAll(x => x.IsSelected).Select(x => x.Text).ToList();
        }

        private void SetItemSources()
        {
            RegionComboBox.ItemsSource = regions;
            RarityComboBox.ItemsSource = rarities;
            TypeComboBox.ItemsSource = types;
        }

        private void PopulateLists()
        {
            regions = ConstructComboRowList(((App)Application.Current).Storage.Regions);
            types = ConstructComboRowList(((App)Application.Current).Storage.Types);
            rarities = ConstructComboRowList(((App)Application.Current).Storage.Rarities);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (((App)Application.Current) != null && !AreComboboxesPopulated)
            {
                PopulateLists();
                SetItemSources();
            }
        }

        private List<ComboRow> ConstructComboRowList(List<string> list)
        {
            List<ComboRow> retVal = new List<ComboRow>();
            list.ForEach(x => retVal.Add(new ComboRow(x)));

            return retVal;
        }
    }

    public class ComboRow
    {
        public bool IsSelected { get; set; }
        public string Text { get; set; }

        public ComboRow(string text)
        {
            Text = text;
            IsSelected = true;
        }

        public ComboRow(string text, bool selected)
        {
            Text = text;
            IsSelected = selected;
        }
    }
}
