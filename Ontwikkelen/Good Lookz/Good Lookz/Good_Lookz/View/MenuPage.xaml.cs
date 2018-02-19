using ModernHttpClient;
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
        const string url = "http://www.good-lookz.com/API/notifications/friendsRequests.php?users_id={0}";
        const string locationUrl = "http://good-lookz.com/API/location/uploadLocation.php";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        //private static int _hasChosen;

        //public static int hasChosen
        //{
        //    get { return _hasChosen; }
        //    set { _hasChosen = value; }
        //}

        public MenuPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            // Request checker
            var data = Models.LoginCredentials.loginId;
            string URL = string.Format(url, data);

            var content = await client.GetStringAsync(URL);
            var response = JsonConvert.DeserializeObject<List<Models.FriendsRequestCount>>(content);

            if (response[0].count == 0)
            {
                friendsBtn.Text = "Friends";
            }
            else
            {
                var friendsText = "Friends({0})";
                var dataText = response[0].count;
                friendsBtn.Text = string.Format(friendsText, dataText);
            }

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                if (position == null)
                    return;

                var latitude = position.Latitude.ToString();
                var longitude = position.Longitude.ToString();

                var values = new Dictionary<string, string>
                {
                    { "users_id", data },
                    { "latitude", latitude},
                    { "longitude", longitude}
                };

                var content_location = new FormUrlEncodedContent(values);
                var response_location = await client.PostAsync(locationUrl, content_location);
                var responseString = await response_location.Content.ReadAsStringAsync();
                var postMethod = JsonConvert.DeserializeObject<List<uploadLocation>>(responseString);
            }
            catch
            {
                await DisplayAlert("Message", "Unable to get location", "OK");
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
            await Navigation.PushAsync(new View.ShopPage(), true);
        }

        async void SettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(), true);
        }

        async void FriendsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.FriendsPage(), true);
        }
    }
}
