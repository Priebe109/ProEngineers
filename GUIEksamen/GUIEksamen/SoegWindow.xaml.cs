using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GUIEksamen
{
    /// <summary>
    /// Interaction logic for SoegWindow.xaml
    /// </summary>
    public partial class SoegWindow : Window
    {
        private List<Tuple<DateTime, string, string, string>> _fullVittighederInComboBox = new List<Tuple<DateTime, string, string, string>>();
        private List<string> _vittighederInComboBox = new List<string>();

        public SoegWindow()
        {
            InitializeComponent();
            //Show all vittigheder in comboBox
            foreach (var tuple in VittighedsListClass.VittighedsList)
            {
                _fullVittighederInComboBox.Add(tuple);
            }
            //Only show string vittighed
            List<string> vittigheder = new List<string>();

            foreach (var vittighed in VittighedsListClass.VittighedsList)
            {
                vittigheder.Add(vittighed.Item2);
            }

            VittighedsComboBox.ItemsSource = vittigheder;

            TilbageButton.Click += TilbageButtonOnClick;
            SoegButton.Click += SoegButtonOnClick;
            VittighedsComboBox.SelectionChanged += VittighedsComboBoxOnSelectionChanged;
        }

        private void VittighedsComboBoxOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            //Do nothing if no item is selected. Stops null references
            if (VittighedsComboBox.SelectedIndex == -1)
            {
                DatoTextBox.Text = "";
                KildeTextBox.Text = "";
                return;
            }
            
            int i;
            for (i = 0; i < _fullVittighederInComboBox.Count; i++)
            {
                if (_fullVittighederInComboBox[i].Item2 == VittighedsComboBox.SelectedItem.ToString())
                    break;
            }

            DatoTextBox.Text = _fullVittighederInComboBox[i].Item1.ToString().Substring(0, _fullVittighederInComboBox[i].Item1.ToString().IndexOf(" "));
            KildeTextBox.Text = _fullVittighederInComboBox[i].Item3;
        }

        private void TilbageButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            MainWindow window = new MainWindow();
            window.Left = this.Left;
            window.Top = this.Top;
            window.Width = this.Width;
            window.Height = this.Height;

            window.Show();
            this.Close();
        }

        private void SoegButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var list = VittighedsListClass.SearchInVittighedsList(EmneTextBox.Text);

            //Only show string vittighed
            List<string> vittigheder = new List<string>();

            _fullVittighederInComboBox.Clear();
            foreach (var vittighed in list)
            {
                vittigheder.Add(vittighed.Item2);
                _fullVittighederInComboBox.Add(vittighed);
            }

            VittighedsComboBox.ItemsSource = vittigheder;
        }
    }
}
