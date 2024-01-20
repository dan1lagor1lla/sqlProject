using System.Windows;

namespace sqlProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Theme CurrentTheme => Resources.MergedDictionaries[2].Source.ToString().Contains("LightTheme.xaml") ? Theme.Light : Theme.Dark;
        public App() { }


        public void ChangeTheme(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries[2] = new() { Source = new Uri("styles\\" + (CurrentTheme == Theme.Dark ? "Light" : "Dark") + "Theme.xaml", UriKind.Relative) };
        }
    }

    public enum Theme
    {
        Dark = 0,
        Light = 1
    }
}
