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
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;
using PCClubApp;
using Windows.UI.Xaml.Automation;

namespace PCClubApp.View
{
    /// <summary>
    /// Логика взаимодействия для ShopListView.xaml
    /// </summary>
    public partial class ShopListView : UserControl, IRequestDelegateShop
    {
        private ObservableCollection<ShopUnit> _shopCollection = new ObservableCollection<ShopUnit>();

        public Action<int> ARetCostValue;

        public ObservableCollection<ShopUnit> ShopListCollection
        {
            get { return this._shopCollection; }
        }

        private ClanREST req;
        public ShopListView()
        {
            InitializeComponent();
            req = new ClanREST(this);
        }

        public void OnActive()
        {
            req.ShopList();
        }

        public void ShopListResult<ShopUnit>(List<ShopUnit> shopList)
        {
            Products.Items.Clear();
            shopList.ForEach((i) =>
            {
                PCClubApp.ShopUnit ss = i as PCClubApp.ShopUnit;
                _shopCollection.Add(ss);
                Products.Items.Add(ss);
            });
        }

        public void ShopResultBuyProduct(bool succes)
        {

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Xaml.Controls.Button b = (sender as Windows.UI.Xaml.Controls.Button);
            Trace.WriteLine(b.GetValue(AutomationProperties.NameProperty).GetType());
            foreach (var i in ShopListCollection)
            {
                if (i.Id.ToString().Replace(" ", "") == b.GetValue(AutomationProperties.NameProperty).ToString().Replace(" ", ""))
                {
                    ARetCostValue(i.Cost);
                    req.BuyProduct(i.Id);
                }
            }
            
        }

    }
}
