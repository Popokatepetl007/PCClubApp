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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace PCClub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRequestDelegate
    {
        private DataSample dk = new DataSample();
        private ClanREST req;
        public MainWindow()
        {
            InitializeComponent();
            LoginView.Visibility = Visibility.Visible;
            MainPanel.Visibility = Visibility.Hidden;
            req = new ClanREST(this);
        }

        public void LoginResult(bool success)
        {
            if (success)
            {
                LoginView.Visibility = Visibility.Hidden;
                MainPanel.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.req.Login(LoginTextBox.Text, passwordTextBox.Text);
            
        }



    }
}
