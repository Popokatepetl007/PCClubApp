using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCClubApp
{
    public class NewsUnit
    {
        private int id;
        private string date;
        private string text;
        private int file_id;

        public NewsUnit(dynamic jItem)
        {
            id = jItem.id;
            date = jItem.date;
            text = jItem.text;

        }

        public int Id
        { get => this.id; }
        public string Text
        { get => this.text; }

    }
}
