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
		public string borrow_username { get; set; }
		public string owner_users_id { get; set; }
        public string type { get; set; }
        public string item_id { get; set; }
		public string days { get; set; }
		public string date { get; set; }
		public string active { get; set; }
		public string picture { get; set; }
	}
}
