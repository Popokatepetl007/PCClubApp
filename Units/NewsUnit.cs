using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PCClubApp
{
    public class NewsUnit
    {
        private int id;
        private string date;
        private string text;
        private int file_id;
        private string file_type;

        public NewsUnit(dynamic jItem)
        {
            id = jItem.id;
            date = jItem.date;
            text = jItem.text;
            JArray files_arr = jItem.files as JArray;
            if (files_arr.Count() > 0)
            {
                file_id = jItem.files[0].id;
                file_type = jItem.files[0].type;
                Trace.WriteLine(file_type);
            }
        }

        public int Id
        { get => this.id; }
        public string Text
        { get => this.text; }

        public object SourceFile
        { get => file_type == null ? null : UIManager.GetMediaSourceByUrl(String.Format("/desktop/news/file/{0}", this.file_id), file_type); }

    }
}
