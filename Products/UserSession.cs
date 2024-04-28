using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products
{
    public static class UserSession
    {
        public static string LoggedInUsername { get; private set; }
        public static string UserType { get; private set; }

        public static string Fname { get; private set; }

        public static string Lname { get; private set; }

        public static string Mname { get; private set; }




        public static void SetLoggedInUser(string username, string userType, string fname, string lname, string mname)
        {
            LoggedInUsername = username;
            UserType = userType;
            Fname = fname;
            Lname = lname;
            Mname = string.IsNullOrEmpty(mname) ? "" : mname[0].ToString();
        }



        public static void ClearLoggedInUser()
        {
            LoggedInUsername = null;
            UserType = null;
            Fname = null;
            Lname=null;
            Mname = null;
        }
    }
}
