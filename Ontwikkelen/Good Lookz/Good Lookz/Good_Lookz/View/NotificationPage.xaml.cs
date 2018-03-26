using System;
using System.Collections.Generic;
using System.Linq;
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

			lvNotifications.IsRefreshing = true;
			getNotifs();
		}


		#region Global
		List<Models.Notification> lstNotifs = new List<Models.Notification>();
		#endregion

		private void getNotifs()
		{
			lstNotifs.Add(new Models.Notification { notif_id = "123",	type = 1,	message = "has made an offer",			username = "LBahdu",		picture = "shop.png" });
			lstNotifs.Add(new Models.Notification { notif_id = "78",	type = 2,	message = "wants to borrow something",	username = "Kols",			picture = "wardrobe.png" });
			lstNotifs.Add(new Models.Notification { notif_id = "378",	type = 1,	message = "has made an offer",			username = "DAnvsiu",		picture = "shop.png" });
			lstNotifs.Add(new Models.Notification { notif_id = "512",	type = 3,	message = "wants to be your friend",	username = "LBahdu",		picture = "friends.png" });
			lstNotifs.Add(new Models.Notification { notif_id = "521",	type = 3,	message = "wants to be your friend",	username = "DiemsDjape",	picture = "friends.png" });
			lstNotifs.Add(new Models.Notification { notif_id = "72",	type = 2,	message = "wants to borrow something",	username = "Lolads",		picture = "wardrobe.png" });
			lstNotifs.Add(new Models.Notification { notif_id = "127",	type = 1,	message = "has made an offer",			username = "Burank",		picture = "shop.png" });
			lstNotifs.Add(new Models.Notification { notif_id = "767",	type = 2,	message = "wants to borrow something",	username = "GMSosodElal",	picture = "wardrobe.png" });

			lvNotifications.IsRefreshing	= false;
			lvNotifications.ItemsSource		= lstNotifs;
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
					await Navigation.PushAsync(new View.FriendsPage(), true);
					break;

			}

			//Code van: https://forums.xamarin.com/discussion/30328/listview-item-selected-disable
			//Zorg ervoor dat een item niet geselecteerd kan worden
			if (e == null) return;
			((ListView)sender).SelectedItem = null;

		}
	}
}
