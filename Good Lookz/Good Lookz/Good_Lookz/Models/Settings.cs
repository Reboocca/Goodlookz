using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Lookz.Models
{
	class Settings
	{
		public class Mirror
		{
			public static double adjust_lighting = 1;
		}

		public class Filter
		{
			public class filterHead
			{
				public static bool filteron = false;
				public static string colour { get; set; }
			}

			public class filterTop
			{
				public static bool filteron = false;
				public static string colour { get; set; }
			}

			public class filterBottom
			{
				public static bool filteron = false;
				public static string colour { get; set; }
			}

			public class filterFeet
			{
				public static bool filteron = false;
				public static string colour { get; set; }
			}
		}

		public class NotifySettings
		{
			public static int notifyfriend { get; set; }
			public static int notifyborrow { get; set; }
			public static int notifybid { get; set; }
		}

		public class ResetPWD
		{
			public static string mail { get; set; }
			public static string users_id { get; set; }

			public class JSONresponse
			{
				public string status { get; set; }
				public int tries_left { get; set; }
				public string users_id { get; set; }
			}
		}
	}
}
