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
    class LendList
    {
        public string lend_id { get; set; }
		public string borrow_users_id { get; set; }
		public string owner_users_id { get; set; }
        public string type { get; set; }
        public string item_id { get; set; }
		public string date { get; set; }
		public string days { get; set; }
		public string username { get; set; }
		public string lending { get; set; }
		public string name { get; set; }
		public string picture { get; set; }
		public string comments { get; set; }
	}

	class SelectedLend
	{
		public static string lend_id { get; set; }
		public static string borrow_users_id { get; set; }
		public static string owner_users_id { get; set; }
		public static string type { get; set; }
		public static string item_id { get; set; }
		public static string date { get; set; }
		public static string days { get; set; }
		public static string username { get; set; }
		public static string lending { get; set; }
		public static string name { get; set; }
		public static string picture { get; set; }
		public static string comments { get; set; }
	}
}
