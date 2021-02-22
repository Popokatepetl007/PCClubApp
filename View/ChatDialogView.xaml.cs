using System;
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
using System.Diagnostics;

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace PCClubApp.View
{
    public sealed partial class ChatDialogView : UserControl
    {

        public Func<string> ReservAction;
        public Action<string> SendMessageAction;
        private List<ChatMessage> MessagesItems;

        public ChatDialogView()
        {

            this.InitializeComponent();
            MessagesItems = new List<ChatMessage>();
        }

        private void SendMessageFromEvent()
        {
            SendMessageAction(TextSendMessage.Text);
            MessagesList.Items.Add(new ChatMessage(TextSendMessage.Text));
            MessagesList.UpdateLayout();
            TextSendMessage.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessageFromEvent();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public void ResiveMessage(ChatMessage chatMessage)
        {
            MessagesList.Items.Add(chatMessage);
        }

        private void TextSendMessage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SendMessageFromEvent();
            }
            
        }
    }
}
