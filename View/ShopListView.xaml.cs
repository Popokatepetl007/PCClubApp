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

        readonly string all_cats = "Все";

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
            CategorySwitchView.Items.Clear();
            _shopCollection.Clear();
            List<string> categoryNamesList = new List<string>();
            categoryNamesList.Add(all_cats);
            CategorySwitchView.Items.Add(new CategoryProduct(all_cats));
            shopList.ForEach((i) =>
            {
                PCClubApp.ShopUnit ss = i as PCClubApp.ShopUnit;
                _shopCollection.Add(ss);

                if (!categoryNamesList.Contains(ss.Category))
                {
                    categoryNamesList.Add(ss.Category);
                    CategorySwitchView.Items.Add(new CategoryProduct(ss.Category));
                }
            });
            CategorySwitchView.SelectedIndex = 0;
            FilterItemsByCategoryname(all_cats);
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
                    break;
                }
            }
            
        }

        private void CategorySwitchView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            CategoryProduct _t = CategorySwitchView.SelectedItem as CategoryProduct;
            FilterItemsByCategoryname(_t.Name);
        }

        private void FilterItemsByCategoryname(string name)
        {
            Products.Items.Clear();
            foreach (var i in _shopCollection) {
                if (i.Category == name || name == all_cats) Products.Items.Add(i);
            }
        }

    }

    public class CategoryProduct
    {
        private string category_name;

        public CategoryProduct(string _name)
        {
            this.category_name = _name;
        }

        public string Name
        { get => this.category_name; }

    }

}
