using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PCClubApp
{
    public class StorageManager
    {

        private Windows.Storage.ApplicationDataContainer localSettings;
        private Windows.Storage.StorageFolder localFolder;
        readonly string compIdKey = "compIdKey";
        readonly string clubIdKey = "clubIdKey";
        readonly string compNimberKey = "compNumberKey";

        public StorageManager()
        {
            this.localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            this.localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        }

        public void SetCompIdValue(int value)
        {
            localSettings.Values[compIdKey] = value;
        }

        public void SetClubIdValue(int value)
        {
            localSettings.Values[clubIdKey] = value;
        }

        public void SetCompNumberValue(int value)
        {
            localSettings.Values[compNimberKey] = value;
        }


        public bool CompIdExist
        { get => localSettings.Values[compIdKey] != null; }

        public bool ClubIdExist
        { get => localSettings.Values[clubIdKey] != null; }

        public int ComtId => CompIdExist ? Convert.ToInt32(localSettings.Values[compIdKey]) : -1;
        public int ClubId => CompIdExist ? Convert.ToInt32(localSettings.Values[clubIdKey]) : -1;

        public int CompNumber => CompIdExist ? Convert.ToInt32(localSettings.Values[compNimberKey]) : -1;

    }
}
