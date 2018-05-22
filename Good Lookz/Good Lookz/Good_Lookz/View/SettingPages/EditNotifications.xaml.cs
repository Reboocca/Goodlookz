using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class EditNotifications : ContentPage
	{
		public EditNotifications()
		{
			InitializeComponent();

            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            toggleSwitches();
		}


		private void toggleSwitches()
		{
			//Zet de settings aan of uit aan de hand van de opgeslagen settings
			if (Models.Settings.NotifySettings.notifyfriend == 1)
			{
				scFriend.On = true;
			}

			if (Models.Settings.NotifySettings.notifybid == 1)
			{
				scSale.On = true;
			}

			if (Models.Settings.NotifySettings.notifyborrow == 1)
			{
				scLend.On = true;
			}
		}

		private void Toggled(object sender, ToggledEventArgs e)
		{
			string column = null;
			int val		  = 0;

			if(e.Value == true)
			{
				val = 1;
			}

			//Switch om te kijken welke switch aan of uit is gezet
			switch (((SwitchCell)sender).Text)
			{
				case "New friend requests":
					Models.Settings.NotifySettings.notifyfriend	= val;
					column = "notify_new_friend";
					break;
				case "New borrow requests":
					Models.Settings.NotifySettings.notifyborrow = val;
					column = "notify_new_lend";
					break;
				case "New sale requests":
					Models.Settings.NotifySettings.notifybid	= val;
					column = "notify_new_bid";
					break;
			}

			//Update de settings in de database
			updateSettingsDBS(column, val);
		}

		private async void updateSettingsDBS(string c, int v)
		{
			string webadres = "http://good-lookz.com/API/account/updateNotifySetting.php?";
			string parameters = "users_id=" + Models.LoginCredentials.loginId + "&notify=" + c + "&value=" + v;

			HttpClient connect = new HttpClient();
			HttpResponseMessage update = await connect.GetAsync(webadres + parameters);
			update.EnsureSuccessStatusCode();
			string result = await update.Content.ReadAsStringAsync();
		}
	}
}
