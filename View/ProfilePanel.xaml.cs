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
    public sealed partial class ProfilePanel : UserControl
    {
        public ProfilePanel()
        {
            this.InitializeComponent();
            SelectPanel.SelectedIndex = 1;
            SetViewBySelect();
            
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Item list click");
            TextBlock tb = sender as TextBlock;
            Trace.WriteLine(tb.Name);
        }

        private void SelectPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("Item list click");
            SetViewBySelect();
        }

        private void SetViewBySelect()
        {
            AccInfoBlock.Visibility = Visibility.Collapsed;
            AccBalanceBlock.Visibility = Visibility.Collapsed;
            AccReservBlock.Visibility = Visibility.Collapsed;
            if (SelectPanel.SelectedIndex == 0 )
            {
                if (ProfileManager.userRole == EUserRole.Gamer)
                {
                    AccInfoBlock.Visibility = Visibility.Visible;
                    AccInfoBlock.OnActive();
                }
                else
                {
                    SelectPanel.SelectedIndex = 1;
                }
            }
            if (SelectPanel.SelectedIndex == 1)
            {
                AccBalanceBlock.Visibility = Visibility.Visible;
            }
            if (SelectPanel.SelectedIndex == 2)
            {
                AccReservBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
