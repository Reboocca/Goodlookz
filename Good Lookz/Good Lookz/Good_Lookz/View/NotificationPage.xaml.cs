using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Good_Lookz.View
{
	public partial class NotificationPage : ContentPage
	{
		public NotificationPage()
		{
			InitializeComponent();
		
			getNotifs();
		}


		#region Global
		List<Models.Notification> lstNotifs = new List<Models.Notification>();
		#endregion

		private async void getNotifs()
		{
			try
			{
				string webadres			= "http://good-lookz.com/API/notifications/getNotifications.php?";
				string parameters		= "users_id=" + Models.LoginCredentials.loginId + "&notif_friends=" + Models.Settings.NotifySettings.notifyfriend + "&notif_lend=" + Models.Settings.NotifySettings.notifyborrow + "&notif_bid=" + Models.Settings.NotifySettings.notifybid;
				HttpClient connect		= new HttpClient();
				HttpResponseMessage get = await connect.GetAsync(webadres+parameters);
				get.EnsureSuccessStatusCode();

				string result = await get.Content.ReadAsStringAsync();

				if(result == "Notifications off" || result == "[]")
				{
					lblNotifications.Text = "No new notifications";
				}
				else
				{
					var jsonresult = JsonConvert.DeserializeObject<List<Models.Notification>>(result);

					foreach (Models.Notification n in jsonresult)
					{
						lstNotifs.Add(n);
					}

					lvNotifications.ItemsSource = lstNotifs;
				}
				
			}
			catch (Exception)
			{
				lblNotifications.Text = "No new notifications";
			}
		}

		private async void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//1 = bid, 2 = lend, 3 = friend request
			switch (((Models.Notification)(lvNotifications.SelectedItem)).type)
			{
				case 1:
					await Navigation.PushAsync(new View.WardrobePages.WardrobeSaleRequests(), true);
					break;
				case 2:
					await Navigation.PushAsync(new View.WardrobePages.LendRequests(), true);
					break;
				case 3:
					await Navigation.PushAsync(new View.FriendsPages.FriendsRequest(), true);
					break;
			}
		}
		
		private async void Delete_Clicked(object sender, EventArgs e)
		{
			try
			{
				var item = ((MenuItem)sender);
				var selected_notif_id = item.CommandParameter.ToString();

				string webadres = "http://good-lookz.com/API/notifications/deleteNotifFromApp.php?id=" + selected_notif_id;
				HttpClient connect = new HttpClient();
				HttpResponseMessage get = await connect.GetAsync(webadres);
				get.EnsureSuccessStatusCode();

				string result = await get.Content.ReadAsStringAsync();

				lstNotifs.Clear();
				lvNotifications.ItemsSource = null;
				getNotifs();
			}
			catch
			{
				throw;
			}
		}
	}
}
