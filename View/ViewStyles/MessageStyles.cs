using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;

namespace PCClubApp.View
{
    public class MessageContainerStyleSelector : StyleSelector
    {
        public Style SentStyle { get; set; }

        public Style ReceivedStyle { get; set; }

        public string Sender { get; set; }

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            Trace.WriteLine("----------STYLE MESSAGE--------");
            var message = item as ChatMessage;
            Trace.WriteLine(message.Text);
            if (message != null)
            {
                Trace.WriteLine(message.MessageType == EMessageType.Sent ? "SENT" : "RECEVED");
                return message.MessageType == EMessageType.Sent
                           ? this.SentStyle
                           : this.ReceivedStyle;
            }

            return base.SelectStyleCore(item, container);
        }
    }
}
