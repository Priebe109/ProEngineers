using System;
using System.Windows;

namespace GUIEksamen
{
    /// <summary>
    /// Interaction logic for IndtastWindow.xaml
    /// </summary>
    public partial class IndtastWindow : Window
    {
        public IndtastWindow()
        {
            InitializeComponent();

            GemButton.Click += GemButtonOnClick;
            TilbageButton.Click += TilbageButtonOnClick;
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

        private void GemButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            //Check if no input field is empty
            var isAllFieldsFilled = false;
            if (VittighedTextBox.Text.Length != 0)
                if (DatoTextBox.Text.Length != 0)
                    if (KildeTextBox.Text.Length != 0)
                        if (EmneTextBox.Text.Length != 0)
                            isAllFieldsFilled = true;
            if (!isAllFieldsFilled)
            {
                MessageBox.Show("Udfyld alle tekstfelter.", "Vittigheder - Fejl");
                return;
            }

            //Try to parse dato into DataTime object
            DateTime dato;
            try
            {
                dato = DateTime.Parse(DatoTextBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Dato er ikke indtastet i gyldigt format.\nEksempel: 01.01.1999", "Vittigheder - Fejl");
                return;
            }

            //Save Vittighed
            VittighedsListClass.AddVittighed(dato, VittighedTextBox.Text, KildeTextBox.Text, EmneTextBox.Text);

            MessageBox.Show("Vittighed gemt.", "Vittigheder - Succes");
            //Clear textBoxes, so user is less likely to save the same vittighed twice
            DatoTextBox.Text = "";
            VittighedTextBox.Text = "";
            KildeTextBox.Text = "";
            EmneTextBox.Text = "";
        }
    }
}
