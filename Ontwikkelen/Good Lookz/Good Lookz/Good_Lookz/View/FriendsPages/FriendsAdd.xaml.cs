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
    class checkuser
    {
        public string id { get; set; }
        public string username { get; set; }
    }

    class friendSend
    {
        public bool friends_send { get; set; }
    }

    class friendCheck
    {
        public string id { get; set; }
        public string username { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class FriendsAdd : ContentPage
    {
        private const string url_friendSend = "http://www.good-lookz.com/API/friends/friendsSendReq.php";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<checkuser> _gets;

        /// <summary>
        /// List maken en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<checkuser> response = new List<checkuser>();

        public FriendsAdd()
        {
            InitializeComponent();
        }

        async void Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadingFriends.IsVisible = true;
            loadingFriends.IsRunning = true;

            string url = "http://www.good-lookz.com/API/account/CheckUser.php?username={0}";
            string username = e.NewTextValue;
            string url_Full = string.Format(url, username);

            var content = await client.GetStringAsync(url_Full);
            response = JsonConvert.DeserializeObject<List<checkuser>>(content);

            _gets = new ObservableCollection<checkuser>(response);

            loadingFriends.IsRunning = false;
            loadingFriends.IsVisible = false;
            users.ItemsSource = _gets;
        }

        async void Users_Tapped(object sender, ItemTappedEventArgs e)
        {
            checkuser item = (checkuser)e.Item;

            if (Models.LoginCredentials.loginUsername == item.username)
            {
                await DisplayAlert("Oops", "You need friends...?", "OK");
            }
            else
            {
                string url_friendCheck = "http://www.good-lookz.com/API/friends/friendsCheck.php";
                var users_id = Models.LoginCredentials.loginId;
                var username = item.username;

                var values = new Dictionary<string, string>
                {
                    { "users_id", users_id },
                    { "usrOrMail", username }
                };

                var content_friendsCheck = new FormUrlEncodedContent(values);
                var response_friendsCheck = await client.PostAsync(url_friendCheck, content_friendsCheck);
                var responseString_friendsCheck = await response_friendsCheck.Content.ReadAsStringAsync();
                var postMethod_friendsCheck = JsonConvert.DeserializeObject<List<friendCheck>>(responseString_friendsCheck);

                var alreadyBinded = false;
                var friendcheckUsername = "";
                foreach (var friendCheckId in postMethod_friendsCheck)
                {
                    if (item.id == friendCheckId.id)
                    {
                        alreadyBinded = true;
                        friendcheckUsername = friendCheckId.username;
                        break;
                    }
                }

                if (alreadyBinded)
                {
                    await DisplayAlert("Error", "Already pending or already friends with " + friendcheckUsername, "OK");
                }
                else
                {
                    var sendRequest = await DisplayAlert("Continue", "Send friend request to " + item.username + "?", "Yes", "No");

                    if (sendRequest)
                    {
                        var content = new FormUrlEncodedContent(values);
                        var response = await client.PostAsync(url_friendSend, content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        var postMethod = JsonConvert.DeserializeObject<List<friendSend>>(responseString);

                        await DisplayAlert("Message", "Friend request send!", "OK");
                    }
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
