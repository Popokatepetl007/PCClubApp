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
using Windows.UI.Popups;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace PCClubApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page, IRequestDelegateLogin
    {
        private ClanREST req;
        private bool checkLoginPar = false;
        public MainPage()
        {
            this.InitializeComponent();
            req = new ClanREST(this);
            MainPanel.Visibility = Visibility.Collapsed;
            //this.req.Login("test", "qwerty");
        }


        private void LogOutAction()
        {
            LoginView.Visibility = Visibility.Visible;
            MainPanel.Visibility = Visibility.Collapsed;
            req.LogOut();
        }


        public void LoginResult(bool success, int userId, EUserRole userRole)
        {
            StorageManager sm = new StorageManager();

            /*sm.SetClubIdValue(3);
            sm.SetCompIdValue(16);
            sm.SetCompNumberValue(10);*/

            if (success)
            {
                if (userRole == EUserRole.Gamer || userRole == EUserRole.Check)
                {
                    if (sm.ClubIdExist && sm.CompIdExist)
                    {
                        ProfileManager.SetValues(userId, userRole, sm);
                        LoginView.Visibility = Visibility.Collapsed;
                        MainPanel.SetLogin(LoginTextBox.Text);
                        MainPanel.Visibility = Visibility.Visible;
                        LoginTextBox.Text = "";
                        passwordTextBox.Text = "";
                        req.req_delegate_profile = MainPanel;
                        req.ProfileData();
                        UIManager.LogOutAction = LogOutAction;
                        /*MainPanel.LogOutAction = () =>
                        {
                            LoginView.Visibility = Visibility.Visible;
                            MainPanel.Visibility = Visibility.Collapsed;
                            req.LogOut();
                        };*/
                    }
                    else
                    {
                        _ = UIManager.ShoeNeedAdminAsync();
                    }
                }
                
                if (userRole == EUserRole.Admin || userRole == EUserRole.Owner)
                {
                    _ = MainPanel.ShowSettinhsCompAsync();
                }

            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.req.Login(LoginTextBox.Text, checkLoginPar? LoginTextBox.Text : passwordTextBox.Text);
        }


        private void Button_Check(object sender, RoutedEventArgs e)
        {
            LoginTextBox.Text = "";
            passwordTextBox.Text = "";
            if (checkLoginPar)
            {
                passwordTextBox.Visibility = Visibility.Visible;
                LoginTextBox.PlaceholderText = "Login";
                CheckInButton.Content = "У меня чек";
            }
            else
            {
                passwordTextBox.Visibility = Visibility.Collapsed;
                LoginTextBox.PlaceholderText = "Введите номер чека";
                CheckInButton.Content = "У меня Логин/Пароль";
            }
            checkLoginPar = !checkLoginPar;
        }


        private void MainPanel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LoginView_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                this.req.Login(LoginTextBox.Text, checkLoginPar ? LoginTextBox.Text : passwordTextBox.Text);
            }
            
        }
    }
}
