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
    public partial class WardrobeSets : ContentPage
    {
        private const string url = "http://www.good-lookz.com/API/wardrobeSet/wardrobeSetDownload.php";
        private HttpClient _client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.WardrobeSets> _posts;
        private string users_id = Models.LoginCredentials.loginId;

        /// <summary>
        /// List maken en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.WardrobeSets> postMethod = new List<Models.WardrobeSets>();

        public WardrobeSets()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            loadingList.IsVisible = true;
            loadingList.IsRunning = true;

            var values = new Dictionary<string, string>
                {
                    { "users_id", users_id }
                };

            var content = new FormUrlEncodedContent(values);
            var response = await _client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            postMethod = JsonConvert.DeserializeObject<List<Models.WardrobeSets>>(responseString);

            _posts = new ObservableCollection<Models.WardrobeSets>(postMethod);

            loadingList.IsRunning = false;
            loadingList.IsVisible = false;

            postsListView.ItemsSource = _posts;

            base.OnAppearing();
        }

        async void Sets_Tapped(object sender, ItemTappedEventArgs e)
        {
            Models.WardrobeSets items = (Models.WardrobeSets)e.Item;

            var _wardrobe_id = items.wardrobe_id;
            var _users_id = items.users_id;
            var _name = items.name;
            var _head_id = items.head_id;
            var _top_id = items.top_id;
            var _bottom_id = items.bottom_id;
            var _feet_id = items.feet_id;

            Models.WardrobeSetsItem.wardrobe_id = _wardrobe_id;
            Models.WardrobeSetsItem.users_id = _users_id;
            Models.WardrobeSetsItem.name = _name;
            Models.WardrobeSetsItem.head_id = _head_id;
            Models.WardrobeSetsItem.top_id = _top_id;
            Models.WardrobeSetsItem.bottom_id = _bottom_id;
            Models.WardrobeSetsItem.feet_id = _feet_id;

            await Navigation.PushAsync(new WardrobePages.WardrobeSetSelected(), true);
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            string url_delete = "http://www.good-lookz.com/API/wardrobeSet/wardrobeSetDelete.php";

            var item = ((MenuItem)sender);
            var wardrobe_id = item.CommandParameter.ToString();

            var values = new Dictionary<string, string>
            {
                { "wardrobe_id" , wardrobe_id }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await _client.PostAsync(url_delete, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var postMethod = JsonConvert.DeserializeObject<List<Models.WardrobeSetDelete>>(responseString);

            await DisplayAlert("Success", "Deleted", "OK");

            // Delete item van ObservableCollection
            foreach (var items in _posts.ToList())
            {
                if (items.wardrobe_id == wardrobe_id)
                {
                    _posts.Remove(items);
                }
            }
        }

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            postMethod.Clear();
            GC.Collect();
        }
    }
}
