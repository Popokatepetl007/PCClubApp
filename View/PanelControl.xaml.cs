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


namespace PCClubApp.View
{
    /// <summary>
    /// Логика взаимодействия для PanelControl.xaml
    /// </summary>
    public partial class PanelControl : UserControl
    {
        
        public PanelControl()
        {
            InitializeComponent();
            
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

    }
}
