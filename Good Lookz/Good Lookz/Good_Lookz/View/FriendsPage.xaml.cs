using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;

namespace Good_Lookz.View
{
	/// <summary>
	/// JSON response word opgevangen en in een list met variables gestopt.
	/// </summary>
	class Deleted
    {
        public bool friends_delete { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class FriendsPage : ContentPage
    {
        private const string url = "http://www.good-lookz.com/API/friends/friendsCheck.php?users_id={0}";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.FriendsCredentials> _gets;

        List<Models.FriendsRequestCount> response_notification = new List<Models.FriendsRequestCount>();
        List<Models.FriendsCredentials> response = new List<Models.FriendsCredentials>();

        public FriendsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            loadingFriends.IsVisible = true;
            loadingFriends.IsRunning = true;

            string url_notification = "http://www.good-lookz.com/API/notifications/friendsRequests.php?users_id={0}";
            string data = Models.LoginCredentials.loginId;
            string URL_notification = string.Format(url_notification, data);

            var content_notification = await client.GetStringAsync(URL_notification);
            response_notification = JsonConvert.DeserializeObject<List<Models.FriendsRequestCount>>(content_notification);

            if (response_notification[0].count == 0)
            {
                requestsBtn.Text = "Friends Requests";
            }
            else
            {
                var requestsBtnText = "Friends Requests({0})";
                var data_requests = response_notification[0].count;
                requestsBtn.Text = string.Format(requestsBtnText, data_requests);
            }

            //string fastData = "41";
            string URL = string.Format(url, data);

            var content = await client.GetStringAsync(URL);
            response = JsonConvert.DeserializeObject<List<Models.FriendsCredentials>>(content);

            loadingFriends.IsRunning = false;
            loadingFriends.IsVisible = false;

            if (response[0].id != null)
            {
                _gets = new ObservableCollection<Models.FriendsCredentials>(response);

                friendsList.ItemsSource = _gets;
            }
            else
            {
                lblRequests.Text = "No friends";
            }
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var friends_id = item.CommandParameter.ToString();
            var url = "http://www.good-lookz.com/API/friends/friendsDelete.php";

            var values = new Dictionary<string, string>
            {
                { "friends_id", friends_id }
            };

            var requestDelete = await DisplayAlert("Warning", "Are you sure you want to delete this friend?", "Yes", "No");

            if (requestDelete)
            {
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var postMethod = JsonConvert.DeserializeObject<List<Deleted>>(responseString);

                // Delete van items in het ObservableCollection
                foreach (var items in _gets.ToList())
                {
                    if (items.friends_id == friends_id)
                    {
                        _gets.Remove(items);
                    }
                }
            }
        }

        async void AddFriend_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsPages.FriendsAdd(), true);
        }

        async void FriendsRequest_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsPages.FriendsRequest(), true);
        }

        void FriendsList_Tapped(object sender, ItemTappedEventArgs e)
        {
            Models.FriendsCredentials item = (Models.FriendsCredentials)e.Item;

            Models.SelectedFriendsCredentials.id            = item.id;
            Models.SelectedFriendsCredentials.picture       = item.picture;
            Models.SelectedFriendsCredentials.username      = item.username;
            Models.SelectedFriendsCredentials.description   = item.description;
            Models.SelectedFriendsCredentials.fullName      = item.fullName;
            Models.SelectedFriendsCredentials.friends_id    = item.friends_id;

            Navigation.PushAsync(new FriendsPages.FriendsProfilePage(), true);
        }

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            response.Clear();
            response_notification.Clear();
            GC.Collect();
        }
    }
}
