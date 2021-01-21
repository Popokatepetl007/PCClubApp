﻿using System;
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
    public sealed partial class GameListView : UserControl, IRequestDelegateContentList
    {

        private ClanREST reqClan;
        public GameListView()
        {
            this.InitializeComponent();
            /*Games.Items.Add(GameEnumConversion.AppUnitFromEnum(GameUnits.csgo));
            Games.Items.Add(GameEnumConversion.AppUnitFromEnum(GameUnits.dota2));
            Games.Items.Add(GameEnumConversion.AppUnitFromEnum(GameUnits.gtav));*/
            reqClan = new ClanREST(this);            
        }

        public void OnStart()
        {
            reqClan.GetContent();
        }

        public void ContentResult(List<GameUnit> listResult)
        {
            Trace.WriteLine(listResult);
            Games.Items.Clear();
            listResult.ForEach((i) => Games.Items.Add(i) );
        }

        private void Games_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("---AppRun----");
            SystemController.appRun(GameUnits.csgo);
        }
    }
}
