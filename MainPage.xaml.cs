using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace PCClubApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page, IRequestDelegateLogin
    {
        private ClanREST req;
        public MainPage()
        {
            this.InitializeComponent();
            req = new ClanREST(this);
            MainPanel.Visibility = Visibility.Collapsed;
            
        }

        public void LoginResult(bool success)
        {
            if (success)
            {
                LoginView.Visibility = Visibility.Collapsed;
                MainPanel.SetLogin(LoginTextBox.Text);
                MainPanel.Visibility = Visibility.Visible;
                WSManager.StartWS();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.req.Login(LoginTextBox.Text, passwordTextBox.Text);

        }
    }
}
