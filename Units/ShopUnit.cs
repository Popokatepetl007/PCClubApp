using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using System.Resources;

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
        }

        public ShopUnit(dynamic jItem)
        {
            id = jItem.id;
            name = jItem.name;
            description = jItem.description;
            cost = jItem.cost;
            count = jItem.count;
            category = jItem.category;
            club = new ClubUnit(jItem.club);
        }

        public string Name
        { get => name; }

        public int Id
        { get => id; }

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

    }
}
