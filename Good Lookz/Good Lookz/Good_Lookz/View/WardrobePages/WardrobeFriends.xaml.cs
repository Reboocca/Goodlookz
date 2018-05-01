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

namespace Good_Lookz.View.WardrobePages
{
    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class WardrobeFriends : ContentPage
    {
        private const string url    = "http://www.good-lookz.com/API/friends/friendsCheck.php?users_id={0}";
        HttpClient client           = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.FriendsCredentials> _gets;

        /// <summary>
        /// List maken en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.FriendsCredentials> response = new List<Models.FriendsCredentials>();

        public WardrobeFriends()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            string data = Models.LoginCredentials.loginId;
            string URL  = string.Format(url, data);
            var content = await client.GetStringAsync(URL);
            response    = JsonConvert.DeserializeObject<List<Models.FriendsCredentials>>(content);

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

        async void FriendsList_Tapped(object sender, ItemTappedEventArgs e)
        {
            Models.FriendsCredentials item = (Models.FriendsCredentials)e.Item;

            Models.SelectedFriendsCredentials.id            = item.id;
            Models.SelectedFriendsCredentials.fullName      = item.fullName;
            Models.SelectedFriendsCredentials.friends_id    = item.friends_id;

            await Navigation.PushAsync(new WardrobeFriendSelected(), true);
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
