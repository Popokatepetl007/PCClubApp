using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace PCClubApp.View
{
    public sealed partial class ProfileInfo : UserControl, IRequestDelegateProfile
    {
        private ClanREST req;
        private ProfileData current_profile;

        public ProfileInfo()
        {
            this.InitializeComponent();
            req = new ClanREST(this);
            AccPicsView.picAction = (_is) => {
                ProfileImageRect.Source = _is;
            };
        }

        public void OnActive()
        {
            req.ProfileData();
        }

        public void ProfileResult(ProfileData profile)
        {
            current_profile = profile;
            AccLoginBox.Text = profile.Login;
            AccNameBox.Text = profile.Name;
            AccPhoneBox.Text = profile.Phone;
            AccMailBox.Text = profile.Email;
            AccBDayBox.Text = profile.Birthday;
            ProfileImageRect.Source = UIManager.BitmapImageFromUrl("/profile/picture");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccPicsView.Visibility = Visibility.Visible;
        }
    }
}
