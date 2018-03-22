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
    public class WardrobeHead
    {
        public int head_id { get; set; }
        public int users_id { get; set; }
        public string picture { get; set; }
        public string color { get; set; }
        public string date { get; set; }
    }

    public class WardrobeHeadAll
    {
        public int head_id { get; set; }
        public int users_id { get; set; }
        public string picture { get; set; }
        public string date { get; set; }
        public int friends_id { get; set; }
        public string username { get; set; }
    }

    public class HeadSale
    {
        public string head_sale_id { get; set; }
        public string users_id { get; set; }
        public string head_id { get; set; }
        public string picture { get; set; }
        public string date { get; set; }
        public string username { get; set; }
        public string price { get; set; }
		public string desc { get; set; }
		public string sale_city { get; set; }
		public string fullPrice { get { return price + "€"; } }
    }

    public class headDelete
    {
        public bool head_delete { get; set; }
    }

    /// <summary>
    /// Values dat opgeslagen moet worden in een class.
    /// </summary>
    class SelectedHead
    {
        private static string _head_id;
        private static string _users_id;
        private static string _picture;
        private static string _color;
        private static string _date;

        public static string head_id
        {
            get { return _head_id; }
            set { _head_id = value; }
        }

        public static string users_id
        {
            get { return _users_id; }
            set { _users_id = value; }
        }

        public static string picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        public static string color
        {
            get { return _color; }
            set { _color = value; }
        }

        public static string date
        {
            get { return _date; }
            set { _date = value; }
        }

		public static void Clear()
		{
			_head_id	= string.Empty;
			_users_id	= string.Empty;
			_picture	= string.Empty;
			_color		= string.Empty;
			_date		= string.Empty;
		}
	}

    class headSaleSelected
    {
        private static string _head_sale_id;
        private static string _users_id;
        private static string _head_id;
        private static string _picture;
        private static string _date;
        private static string _username;
        private static string _price;
        private static string _desc;
        private static string _fullPrice;
		public static string sale_city;

        public static string head_sale_id
        {
            get { return _head_sale_id; }
            set { _head_sale_id = value; }
        }

        public static string users_id
        {
            get { return _users_id; }
            set { _users_id = value; }
        }

        public static string head_id
        {
            get { return _head_id; }
            set { _head_id = value; }
        }

        public static string picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        public static string date
        {
            get { return _date; }
            set { _date = value; }
        }

        public static string username
        {
            get { return _username; }
            set { _username = value; }
        }

        public static string price
        {
            get { return _price; }
            set { _price = value; }
        }

        public static string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public static string fullPrice
        {
            get { return _fullPrice; }
            set { _fullPrice = value; }
        }
    }
}
