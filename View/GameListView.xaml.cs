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
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Automation;

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace PCClubApp.View
{
    public sealed partial class GameListView : UserControl, IRequestDelegateContentList
    {

        private ClanREST reqClan;
        private ObservableCollection<GameUnit> _gameCollection = new ObservableCollection<GameUnit>();

        public ObservableCollection<GameUnit> GameListCollection
        {
            get { return this._gameCollection; }
        }
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
            _gameCollection.Clear();
            listResult.ForEach((i) => {
                Games.Items.Add(i);
                _gameCollection.Add(i);
            } );
        }

        private void Games_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("---AppRun----");
            Windows.UI.Xaml.Controls.Grid b = (sender as Windows.UI.Xaml.Controls.Grid);
            foreach (var i in GameListCollection)
            {
                if (i.Id.ToString().Replace(" ", "") == b.GetValue(AutomationProperties.NameProperty).ToString().Replace(" ", ""))
                {
                    _ = SystemController.appRunAsync(i);
                }
            }

            
        }
    }
}
