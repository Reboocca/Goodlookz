﻿using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
    class uploadLocation
    {
        public bool delete_location { get; set; }
        public bool upload_location { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class MenuPage : ContentPage
    {
        const string url            = "http://www.good-lookz.com/API/notifications/friendsRequests.php?users_id={0}";
        const string locationUrl    = "http://good-lookz.com/API/location/uploadLocation.php";
        HttpClient client           = new HttpClient(new NativeMessageHandler());


        public MenuPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

			//Get notification settings van de gebruiker
			getNotifySettings();

            var data = Models.LoginCredentials.loginId;

            // Request checker
            try
            {
                string URL      = string.Format(url, data);
                var content     = await client.GetStringAsync(URL);
                var response    = JsonConvert.DeserializeObject<List<Models.FriendsRequestCount>>(content);

                if (response[0].count == 0)
                {
                    friendsBtn.Text = "Friends";
                }
                else
                {
                    var friendsText = "Friends({0})";
                    var dataText    = response[0].count;
                    friendsBtn.Text = string.Format(friendsText, dataText);
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "ok");
            }
           

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                if (position == null)
                    return;

                var latitude    = position.Latitude.ToString();
                var longitude   = position.Longitude.ToString();

                var values = new Dictionary<string, string>
                {
                    { "users_id", data },
                    { "latitude", latitude},
                    { "longitude", longitude}
                };

                var content_location    = new FormUrlEncodedContent(values);
                var response_location   = await client.PostAsync(locationUrl, content_location);
                var responseString      = await response_location.Content.ReadAsStringAsync();
                var postMethod          = JsonConvert.DeserializeObject<List<uploadLocation>>(responseString);
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "ok");
            }
        }

		class jsonResultNotification
		{
			public int notifyfriend { get; set; }
			public int notifyborrow { get; set; }
			public int notifybid { get; set; }
		}

        private async void getNotifySettings()
		{
			try
			{
				//Stuur verzoek naar web API om de json op te halen
				string webadres         = "http://good-lookz.com/API/account/getNotifySettings.php?";
				string parameters       = "users_id=" + Models.LoginCredentials.loginId;
				HttpClient connect      = new HttpClient();
				HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
				get.EnsureSuccessStatusCode();

				//Sla het resultaat van de JSON op
				string result   = await get.Content.ReadAsStringAsync();
				var jsonresult  = JsonConvert.DeserializeObject<List<jsonResultNotification>>(result);

				//Stop JSON resultaat in de class met static variabelen
				Models.Settings.NotifySettings.notifyfriend = jsonresult[0].notifyfriend;
				Models.Settings.NotifySettings.notifyborrow = jsonresult[0].notifyborrow;
				Models.Settings.NotifySettings.notifybid    = jsonresult[0].notifybid;

				//Check of de user notitifcaties heeft
				getNotifCount();
			}
			catch (Exception)
			{
            
            }
		}

		private async void getNotifCount()
		{
			//Haal notificatie count op
			try
			{
				string webadres = "http://good-lookz.com/API/notifications/getNotifCount.php?";
				string parameters = "users_id=" + Models.LoginCredentials.loginId + "&notif_friends=" + Models.Settings.NotifySettings.notifyfriend + "&notif_lend=" + Models.Settings.NotifySettings.notifyborrow + "&notif_bid=" + Models.Settings.NotifySettings.notifybid;
				HttpClient connect = new HttpClient();
				HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
				get.EnsureSuccessStatusCode();

				string result = await get.Content.ReadAsStringAsync();
				
				if(!(result == "0" | result == "Notifications off"))
				{
					lblNotif.Text = "(" + result + ")";
				}
				else
				{
					lblNotif.Text = "";
				}
			}
			catch (Exception)
			{
               
            }

		}

		async void MirrorClicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new MirrorPage(), true);
		}

        void WardrobeClicked(object sender, EventArgs e)
        {
            // Platform specific code in een shared library is mogelijk
            Device.OnPlatform(
                WinPhone: () => { Navigation.PushAsync(new WardrobePageWinPhone(), true); },
                Default: () => { Navigation.PushAsync(new View.WardrobePage(), true); }
                );
        }

        async void ShopClicked(object sender, EventArgs e)
        {
            //Check of de shops niet verwijdert zijn
            string care         = await checkShop("1");
            string fashion      = await checkShop("2");
            string accessories  = await checkShop("3");

            if(care == "false" || fashion == "false" || accessories == "false")
            {
                await DisplayAlert("Warning", "Looks like one of your prefered shops has left the app, let's choose a new one!", "ok");
                await Navigation.PushAsync(new SettingPages.ChangeShops(), true);
            }
            else
            {
                Models.ShopsChosenSaved.shops1_id = Int32.Parse(care);
                Models.ShopsChosenSaved.shops2_id = Int32.Parse(fashion);
                Models.ShopsChosenSaved.shops3_id = Int32.Parse(accessories);

                await Navigation.PushAsync(new View.ShopPage(), true);
            }
        }

        private async Task<string> checkShop(string r) {
            string webadres     = "http://good-lookz.com/API/shops/checkShops.php";
            string parameters   = "?users_id=" + Models.LoginCredentials.loginId + "&rubric=" + r;
            HttpClient connect  = new HttpClient();
            HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
            get.EnsureSuccessStatusCode();

            string result = await get.Content.ReadAsStringAsync();
            return result;
        }

        async void SettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(), true);
        }

        async void FriendsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.FriendsPage(), true);
        }

		async void ColoursClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new View.InYourColourPage(), true);
		}

		async void NotificationClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new View.NotificationPage(), true);
		}
	}
}
