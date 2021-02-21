using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using System.Resources;
using Windows.UI.Xaml.Media.Imaging;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Media;

namespace PCClubApp
{

    class PsevdaShop
    {
        public static string ShopItems()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ShopItems.json");   
            return File.ReadAllText(path);
        }
    }

    public class ShopUnit
    {

        private int id;
        private string description;
        private string name;
        private int cost;
        private int count;
        private ClubUnit club;
        private string category;
        private ClanREST res;
        private ImageSource image;

        private void SetImage()
        {

                /*this.image = null;
                ClanREST res = new ClanREST();
                string img_data = res.GetPicture("/desktop/product/picture/" + this.id.ToString());
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                string convert = regex.Replace(img_data, string.Empty);
                byte[] NewBytes = Convert.FromBase64String(convert);
                MemoryStream ms = new MemoryStream(NewBytes);
                ms.Position = 0;
                this.image = new BitmapImage();
                image.SetSource(ms.AsRandomAccessStream());*/
        }

        public ShopUnit(string jString)
        {
            dynamic result = JObject.Parse(jString);

            id = result.id;
            name = result.name;
            description = result.description;
            cost = result.cost;
            count = result.count;
            category = result.category;
            club = new ClubUnit(result.club);
            //SetImage();
            this.res = new ClanREST();
            this.image = this.res.GetPicture("/desktop/product/picture/" + this.id.ToString());
        }

        public ShopUnit(dynamic jItem)
        {
            id = jItem.id;
            name = jItem.name;
            description = jItem.description;
            cost = jItem.cost;
            //count = jItem.count;
            count = 100;
            category = jItem.category;
            //club = new ClubUnit(jItem.club);
            //SetImage();
            this.res = new ClanREST();
            this.image = this.res.GetPicture("/desktop/product/picture/" + this.id.ToString());
        }

        public string Name
        { get => name; }

        public int Id
        { get => id; }

        public string IdName
        { get => id.ToString(); }

        public string Description
        { get => description; }

        public string Category
        { get => category; }

        public int Cost
        { get => cost; }

        public string CostValue
        { get => String.Format("{0}₽", cost); }

        public int Count
        { get => count; }

        public ImageSource Image
        { get => image; }

    }
}
