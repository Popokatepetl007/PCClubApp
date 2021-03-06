﻿using System;
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
    public sealed partial class NewsView : UserControl, IRequestNews
    {
        private ClanREST req;
        public NewsView()
        {
            this.InitializeComponent();
            req = new ClanREST(this);
        }

        public void OnActive()
        {
            req.GetNews();
        }

        public void NewsResult(List<NewsUnit> listNews)
        {
            NewsList.Items.Clear();
            listNews.ForEach((i) => NewsList.Items.Add(i));
        }
    }
}
