using System.Windows;
using System.Windows.Controls;

namespace GUIEksamen
{
    public class PlaceholderTextBox : TextBox
    {
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderText",
                typeof(string),
                typeof(PlaceholderTextBox),
                new FrameworkPropertyMetadata("Placeholder"));
    }
}