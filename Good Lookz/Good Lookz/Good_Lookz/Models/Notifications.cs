﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Lookz.Models
{
    /// <summary>
    /// JSON response word opgevangen en in een list met variables gestopt.
    /// </summary>
    class FriendsRequestCount
    {
        public int count { get; set; }
    }

	class Notification
	{
		public string notif_id { get; set; }
		public int type { get; set; }
		public string username { get; set; }
		public string message { get; set; }
		public string picture { get; set; }
	}
}
