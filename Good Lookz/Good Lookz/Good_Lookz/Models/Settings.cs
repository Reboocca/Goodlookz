using Good_Lookz.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public class FilterFriend
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

        public class Profile
        {
            public string picture { get; set; }
            public string description { get; set; }
        }


        public class Blocked
        {
            public async Task checkBlockedAsync()
            {
                try
                {
                    //Stuur verzoek naar web API
                    string webadres = "http://good-lookz.com/API/account/checkBlocked.php?";
                    string parameters = "users_id=" + Models.LoginCredentials.loginId;
                    HttpClient connect = new HttpClient();
                    HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
                    get.EnsureSuccessStatusCode();

                    //Sla het resultaat van de JSON op
                    string result = await get.Content.ReadAsStringAsync();

                    if (result == "Failed")
                    {
                        if (Application.Current.Properties.ContainsKey("loginId"))
                            Application.Current.Properties.Remove("loginId");
                        if (Application.Current.Properties.ContainsKey("loginUsername"))
                            Application.Current.Properties.Remove("loginUsername");
                        if (Application.Current.Properties.ContainsKey("loginPassword"))
                            Application.Current.Properties.Remove("loginPassword");
                        if (Application.Current.Properties.ContainsKey("loginFirstname"))
                            Application.Current.Properties.Remove("loginFirstname");
                        if (Application.Current.Properties.ContainsKey("loginLastname"))
                            Application.Current.Properties.Remove("loginLastname");
                        if (Application.Current.Properties.ContainsKey("loginEmail"))
                            Application.Current.Properties.Remove("loginEmail");
                        if (Application.Current.Properties.ContainsKey("loginDate"))
                            Application.Current.Properties.Remove("loginDate");
                        if (Application.Current.Properties.ContainsKey("loginGender"))
                            Application.Current.Properties.Remove("loginGender");
                        if (Application.Current.Properties.ContainsKey("loginOffline"))
                            Application.Current.Properties.Remove("loginOffline");
                        if (Application.Current.Properties.ContainsKey("loginActive"))
                            Application.Current.Properties.Remove("loginActive");

                        App.Current.MainPage = new NavigationPage(new SignPage());
                    }
                }
                catch (Exception)
                {

                }
            }       
        }
	}
}
