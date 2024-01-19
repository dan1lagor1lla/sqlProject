using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sqlProject
{
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(string message, string title) : this(message) => Title = title;

        public NotificationWindow(string message)
        {
            InitializeComponent();
            Messageholder.Text = message;
        }

        private void CloseWindow(object sender, RoutedEventArgs e) => Close();
    }
}
