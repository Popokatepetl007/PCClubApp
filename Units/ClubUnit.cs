using System;
using System.Collections.Generic;
using System.Text;

namespace PCClubApp
{
    public class ClubUnit
    {
        public int id;
        public string address;
        public string name;
        public string phone;
        public string worktime;

        public ClubUnit(dynamic jData)
        {
            id = jData.id;
            address = jData.address;
            name = jData.name;
            phone = jData.phone;
            worktime = jData.worktime;
        }

    }
}
