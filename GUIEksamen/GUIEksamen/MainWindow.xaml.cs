using System.Windows;

namespace GUIEksamen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IndtastButton.Click += IndtastButtonOnClick;
            SoegButton.Click += SoegButtonOnClick;
        }

        private void IndtastButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            IndtastWindow window = new IndtastWindow();
            window.Left = this.Left;
            window.Top = this.Top;
            window.Width = this.Width;
            window.Height = this.Height;

            window.Show();
            this.Close();
        }

        private void SoegButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            SoegWindow window = new SoegWindow();
            window.Left = this.Left;
            window.Top = this.Top;
            window.Width = this.Width;
            window.Height = this.Height;

            window.Show();
            this.Close();
        }
    }
}
