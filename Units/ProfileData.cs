using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCClubApp
{
    public class ProfileData
    {
        private int id;
        private string birthday;
        private string login;
        private string name;
        private string phone;
        private string email;

        public ProfileData(dynamic jItem)
        {
            id = jItem.id;
            birthday = jItem.birthday;
            login = jItem.login;
            name = jItem.name;
            phone = jItem.phone;
            email = jItem.email;
        }

        public int Id { get => id; }
        public string Name { get => name; }
        public string Login { get => login; }
        public string Phone { get => phone; }
        public string Birthday { get => birthday; }
        public string Email { get => email; }

    }
}
