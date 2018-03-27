using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Lookz.Models
{
    /// <summary>
    /// JSON response word opgevangen en in een list met variables gestopt.
    /// </summary>
    public class FriendsCredentials
    {
        public string id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string friends_id { get; set; }
        public string fullName { get { return first_name + " " + last_name; } }
    }
    
    /// <summary>
    /// Values dat opgeslagen moet worden in een class.
    /// </summary>
    public class SelectedFriendsCredentials
    {
        private static string _id;
        private static string _friends_id;
        private static string _fullName;

        public static string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static string friends_id
        {
            get { return _friends_id; }
            set { _friends_id = value; }
        }

        public static string fullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
    }
}
