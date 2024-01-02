using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sqlProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Resources.MergedDictionaries[0].Source.ToString().Contains("DarkThemeColors.xaml"))
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("C:\\Users\\taras\\source\\repos\\sqlProject\\sqlProject\\styles\\LightThemeColors.xaml") });
            else
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("C:\\Users\\taras\\source\\repos\\sqlProject\\sqlProject\\styles\\DarkThemeColors.xaml") });
            Application.Current.Resources.MergedDictionaries.RemoveAt(0);
        }
    }
}