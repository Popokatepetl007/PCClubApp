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

namespace PCClubApp.View
{
    /// <summary>
    /// Логика взаимодействия для PanelControl.xaml
    /// </summary>
    public partial class PanelControl : UserControl, IViewChatDelegate
    {

        private int commCashValue = 10000;
        private ChatClient chatClient;
        private Action<ChatMessage> resiveAction;
        public PanelControl()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimeUpdate);
            timer.Interval = 1000;
            timer.Enabled = true;
            SetComCash(10000);
            resiveAction = ChatPanel.ResiveMessage;
        }

        private void SetComCash(int newValue)
        {
            CommCash.Text = String.Format("{0}₽", newValue);
        }

        private void SetMinCash(int value)
        {
            if ((commCashValue - value) > 0)
            {
                commCashValue = commCashValue - value;
                SetComCash(commCashValue);
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
            ShowLoginNox.Text = login;
            chatClient = new ChatClient(this);
            WSManager.StartWS();
            WSManager.soc_chat = chatClient;
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


        private void Button_Click_SelectPanel(object sender, RoutedEventArgs e)
        {
            Windows.UI.Xaml.Controls.Button b = (sender as Windows.UI.Xaml.Controls.Button);
            DisActive();

            Trace.WriteLine(b.Name);
            Trace.WriteLine(BGame.Name);
            if (b.Name == BGame.Name)
            {
                GamePanel.Visibility = Visibility.Visible;
                GamePanel.OnStart();
            }
            if (b.Name == BService.Name)
            {
                GamePanel.Visibility = Visibility.Visible;
            }
            if (b.Name == BShop.Name)
            {
                BShop.Background = UIManager.GetImageFromAsset("ShopOn");
                ShopPanel.Visibility = Visibility.Visible;
                ShopPanel.OnActive();
                ShopPanel.ARetCostValue = (int a) =>
                {
                    SetMinCash(a);
                };
            }
            if (b.Name == BNews.Name)
            {
                NewsPanel.Visibility = Visibility.Visible;
            }
            if (b.Name == BFavorites.Name)
            {
                Trace.WriteLine("EA SPOrt");
            }
            if (b.Name == BBrowser.Name)
            {
                BrowserPanel.Visibility = Visibility.Visible;
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

        private async System.Threading.Tasks.Task ShowSettinhsCompAsync()
        {
            CompSettingDialog settingsDialog = new CompSettingDialog();

            ContentDialogResult result = await settingsDialog.ShowAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _ = ShowSettinhsCompAsync();
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
            resiveAction(chatMessage);
        }
    }
}
