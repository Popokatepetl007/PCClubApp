using System;
using System.Collections.Generic;
using System.Text;

namespace PCClubApp
{
    public class ClubUnit
    {
        private int id;
        private string address;
        private string name;
        private string phone;
        private string worktime;

        public ClubUnit(dynamic jData)
        {
            id = jData.id;
            address = jData.address;
            name = jData.name;
            phone = jData.phone;
            worktime = jData.worktime;
        }

        public string Name
        { get => name; }

        public string Address
        { get => address; }
        public int Id
        { get => id; }

    }
}
