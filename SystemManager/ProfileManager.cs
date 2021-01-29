using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCClubApp
{
    public class ProfileManager
    {
        public static int userID = 2;
        public static int compId = 5;
        public static int clubId = 3;
        public static int compNumber = 0;
        public static EUserRole userRole = EUserRole.Gamer;
        public static float balance = 0;

        public static EUserRole GetUserRoleFromString(string string_role)
        {
            if (string_role == "ROLE_ADMIN") return EUserRole.Admin;
            if (string_role == "ROLE_CHECK") return EUserRole.Check;
            if (string_role == "ROLE_OWNER") return EUserRole.Owner;

            return EUserRole.Gamer;
        }

    }

    public enum EUserRole
    {
        Gamer,
        Admin,
        Check,
        Owner
    }
}
