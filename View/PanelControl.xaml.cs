using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using PCClubApp;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading;
using System.Timers;
using Windows.UI.Core;
using Windows.System;

namespace PCClubApp.View
{
    /// <summary>
    /// Логика взаимодействия для PanelControl.xaml
    /// </summary>
    public partial class PanelControl : UserControl, IViewChatDelegate, IRequestDelegateProfile
    {

        private int commCashValue = 0;
        private ChatClient chatClient;
        private Action<ChatMessage> resiveAction;
        public Action LogOutAction;
        private ClanREST req;
        public PanelControl()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimeUpdate);
            timer.Interval = 1000;
            timer.Enabled = true;
            resiveAction = ChatPanel.ResiveMessage;
            req = new ClanREST(this);
        }

        private void SetComCash(int newValue)
        {
            CommCash.Text = String.Format("{0}₽", newValue);
        }

        private void SetMinCash(int value)
        {
            
            if ((commCashValue - value) >= 0)
            {
                commCashValue = (int)ProfileManager.balance - value;
                ProfileManager.balance = commCashValue;
                SetComCash(commCashValue);
            }
            else
            {
                _ = UIManager.ShoeNeedAdminAsync("Недостаточно средств");
            }
        }

        private void DisActive()
        {
            ShopPanel.Visibility = Visibility.Collapsed;
            AProfilePanel.Visibility = Visibility.Collapsed;
            GamePanel.Visibility = Visibility.Collapsed;
            BrowserPanel.Visibility = Visibility.Collapsed;
            NewsPanel.Visibility = Visibility.Collapsed;
            BShop.Background = UIManager.GetImageFromAsset("ShopOff");
        }

        public void SetLogin(string login)
        {
            ShowLoginNox.Text = login.Length > 16 ? "Accaunt" : login;
            chatClient = new ChatClient(this);
            CompIdBox.Text = ProfileManager.compNumber.ToString();
            LoginHelloView.Visibility = Visibility.Visible;
            WSManager.StartWS(() => {
                WSManager.soc_chat = chatClient;
                IAsyncAction asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, GamePanelStart);
                this.req.ProfileData();
            });
            
        }


        private void TimeUpdate(object source, ElapsedEventArgs e)
        {
            IAsyncAction asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                CurrentTimeBox.Text = DateTime.Now.ToString("hh:mm:ss");
                DateNowBox.Text = DateTime.Now.ToString("dd/MM/yy");
            });
;
        }


        private void GamePanelStart()
        {
            GamePanel.Visibility = Visibility.Visible;
            GamePanel.OnStart();
        }

        private void ShopPanelStart()
        {
            ShopPanel.Visibility = Visibility.Visible;
            ShopPanel.OnActive();
            ShopPanel.ARetCostValue = (int a) =>
            {
                Trace.WriteLine(a);
                SetMinCash(a);
            };
        }

        private void NewsPanelStart()
        {
            NewsPanel.OnActive();
            NewsPanel.Visibility = Visibility.Visible;
        }

        private void BrowserPanelStart()
        {
            BrowserPanel.Visibility = Visibility.Visible;
        }


        private void Button_Click_SelectPanel(object sender, RoutedEventArgs e)
        {
            Windows.UI.Xaml.Controls.Button b = (sender as Windows.UI.Xaml.Controls.Button);
            DisActive();
            

            if (b.Name == BGame.Name)
            {
                GamePanelStart();
            }
            if (b.Name == BService.Name)
            {
                GamePanel.Visibility = Visibility.Visible;
            }
            if (b.Name == BShop.Name)
            {
                //BShop.Background = UIManager.GetImageFromAsset("ShopOn");
                ShopPanelStart();
            }
            if (b.Name == BNews.Name)
            {
                NewsPanelStart();
            }
            if (b.Name == BFavorites.Name)
            {
                Trace.WriteLine("EA SPOrt");
            }
            if (b.Name == BBrowser.Name)
            {
                BrowserPanelStart();
            }


        }


        private void Button_Click_Game(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Service(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Browser(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Shop(object sender, RoutedEventArgs e)
        {
            if (ShopPanel.Visibility == Visibility.Visible)
            {
                ShopPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                ShopPanel.Visibility = Visibility.Visible;
                ShopPanel.OnActive();
            }

        }

        private void Button_Click_Favorits(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_News(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Profile(object sender, RoutedEventArgs e)
        {
            DisActive();
            AProfilePanel.Visibility = Visibility.Visible;
        }

        public async System.Threading.Tasks.Task ShowSettinhsCompAsync()
        {
            CompSettingDialog settingsDialog = new CompSettingDialog();

            ContentDialogResult result = await settingsDialog.ShowAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //_ = ShowSettinhsCompAsync();
            //LogOutAction();
            DisActive();
            UIManager.LogOutAction();
        }



        private async System.Threading.Tasks.Task ShowChat()
        {
            ChatDialog settingsDialog = new ChatDialog();
            settingsDialog.SendMessageAction = (mess) => chatClient.sendMessage(mess);
            ContentDialogResult result = await settingsDialog.ShowAsync();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChatPanel.Visibility = Visibility.Visible;
            ChatPanel.SendMessageAction = (mess) => chatClient.sendMessage(mess);
        }

        public void NewMessageEvent(ChatMessage chatMessage)
        {
            IAsyncAction asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                resiveAction(chatMessage);
            });
            
        }

        public void ProfileResult(ProfileData profile)
        {
            IAsyncAction asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                ShowLoginNox.Text = profile.Name;
                SetComCash((int)profile.Balance);
                ProfileManager.balance = profile.Balance;
                commCashValue = (int)profile.Balance;
            });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoginHelloView.Visibility = Visibility.Collapsed;
        }

        private void B_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Button _b = sender as Button;
            _b.Height *= 1.2;
            _b.Width *= 1.2;

        }

        private void B_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Button _b = sender as Button;
            _b.Height /= 1.2;
            _b.Width /= 1.2;
        }
    }
}
