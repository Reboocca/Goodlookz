using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Lookz.Models
{
    /// <summary>
    /// Values dat opgeslagen moet worden in een class.
    /// </summary>
    class WardrobeSetsItem
    {
        private static string _wardrobe_id;
        private static string _users_id;
        private static string _name;
        private static string _head_id;
        private static string _top_id;
        private static string _bottom_id;
        private static string _feet_id;

        public static string wardrobe_id
        {
            get { return _wardrobe_id; }
            set { _wardrobe_id = value; }
        }

        public static string users_id
        {
            get { return _users_id; }
            set { _users_id = value; }
        }

        public static string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public static string head_id
        {
            get { return _head_id; }
            set { _head_id = value; }
        }

        public static string top_id
        {
            get { return _top_id; }
            set { _top_id = value; }
        }

        public static string bottom_id
        {
            get { return _bottom_id; }
            set { _bottom_id = value; }
        }

        public static string feet_id
        {
            get { return _feet_id; }
            set { _feet_id = value; }
        }
    }
}
