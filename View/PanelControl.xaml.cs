﻿using System;
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
    public partial class PanelControl : UserControl
    {

        
        public PanelControl()
        {
            InitializeComponent();
            /*AutoResetEvent autoEvent = new AutoResetEvent(false);
            Timer timer = new Timer(TimeUpdate, autoEvent, 1000, 250);
            autoEvent.WaitOne();
            timer.Change(0, 500);*/

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimeUpdate);
            timer.Interval = 1000;
            timer.Enabled = true;

        }

        private void DisActive()
        {
            ShopPanel.Visibility = Visibility.Collapsed;
            BShop.Background = UIManager.GetImageFromAsset("ShopOff");
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
                Trace.WriteLine("EA SPOrt");
            }
            if (b.Name == BService.Name)
            {
                Trace.WriteLine("EA SPOrt");
            }
            if (b.Name == BShop.Name)
            {
                BShop.Background = UIManager.GetImageFromAsset("ShopOn");
                ShopPanel.Visibility = Visibility.Visible;
                ShopPanel.OnActive();
            }
            if (b.Name == BNews.Name)
            {
                Trace.WriteLine("EA SPOrt");
            }
            if (b.Name == BFavorites.Name)
            {
                Trace.WriteLine("EA SPOrt");
            }
            if (b.Name == BBrowser.Name)
            {
                Trace.WriteLine("EA SPOrt");
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

    }
}
