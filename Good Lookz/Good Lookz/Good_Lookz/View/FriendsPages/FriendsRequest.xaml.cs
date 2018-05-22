using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.FriendsPages
{
    /// <summary>
    /// JSON response word opgevangen en in een list met variables gestopt.
    /// </summary>
    class accepted
    {
        public bool friends_accept { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class FriendsRequest : ContentPage
    {
        private const string url = "http://www.good-lookz.com/API/friends/friendsReceiveReq.php?users_id={0}";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.FriendsCredentials> _gets;

        /// <summary>
        /// List maken en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.FriendsCredentials> response = new List<Models.FriendsCredentials>();

        public FriendsRequest()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            try
            {
                loadingFriends.IsVisible = true;
                loadingFriends.IsRunning = true;

                string data = Models.LoginCredentials.loginId;
                //string fastData = "41";
                string URL = string.Format(url, data);

                var content = await client.GetStringAsync(URL);
                response = JsonConvert.DeserializeObject<List<Models.FriendsCredentials>>(content);

                loadingFriends.IsRunning = false;
                loadingFriends.IsVisible = false;

                if (response[0].id != null)
                {
                    _gets = new ObservableCollection<Models.FriendsCredentials>(response);

                    requests.ItemsSource = _gets;
                }
                else
                {
                    lblRequests.Text = "No friend requests.";
                }
            }
            catch (Exception)
            {

                lblRequests.Text = "No friend requests.";
            }
            
        }

        async private void friendAccept(object sender, EventArgs e)
        {
            var button = sender as Button;
            Models.FriendsCredentials item = new Models.FriendsCredentials();

            foreach(Models.FriendsCredentials i in requests.ItemsSource)
            {
                if(button.CommandParameter == i.username)
                {
                    item = i;
                }
            }

            string url_accepted = "http://www.good-lookz.com/API/friends/friendsAcceptReq.php";
            var friends_id = item.friends_id;

            var values = new Dictionary<string, string>
                {
                    { "accepted", "true" },
                    { "friends_id", friends_id }
                };

            var content         = new FormUrlEncodedContent(values);
            var response        = await client.PostAsync(url_accepted, content);
            var responseString  = await response.Content.ReadAsStringAsync();
            var postMethod      = JsonConvert.DeserializeObject<List<accepted>>(responseString);
            
            foreach (var items in _gets.ToList())
            {
                if (items.friends_id == friends_id)
                {
                    _gets.Remove(items);
                }
            }
        }

        async private void friendDecline(object sender, EventArgs e)
        {
            var button = sender as Button;
            Models.FriendsCredentials item = new Models.FriendsCredentials();

            foreach (Models.FriendsCredentials i in requests.ItemsSource)
            {
                if (button.CommandParameter == i.username)
                {
                    item = i;
                }
            }

            var result = await DisplayAlert("Message", "Do you want to report this person for spam?", "Yes", "No");

            if (result)
            {
                //Voeg het spamalert toe
                try
                {
                    string webadres = "http://good-lookz.com/API/friends/friendReportSpam.php?";
                    string parameters = "friend_id=" + item.id;
                    HttpClient connect = new HttpClient();
                    HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
                    get.EnsureSuccessStatusCode();

                    string resultspam = await get.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            //Verwijder het verzoek alsnog
            string url_accepted = "http://www.good-lookz.com/API/friends/friendsAcceptReq.php";

            var values = new Dictionary<string, string>
                {
                    { "accepted", "false" },
                    { "friends_id", item.friends_id }
                };

            var content = new FormUrlEncodedContent(values);
            var responseDel = await client.PostAsync(url_accepted, content);
            var responseString = await responseDel.Content.ReadAsStringAsync();
            var postMethod = JsonConvert.DeserializeObject<List<accepted>>(responseString);

            foreach (var items in _gets.ToList())
            {
                if (items.friends_id == item.friends_id)
                {
                    _gets.Remove(items);
                }
            }
        }

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            response.Clear();
            GC.Collect();
        }
    }
}
