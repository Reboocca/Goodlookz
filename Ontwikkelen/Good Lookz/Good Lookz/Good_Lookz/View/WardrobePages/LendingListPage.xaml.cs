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
    /// JSON response word opgevangen en in een list met variables gestopt.
    /// </summary>
    class lendDelete
    {
        public bool lend_delete { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class LendingListPage : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.LendList> _gets;

        /// <summary>
        /// List maken en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.LendList> response = new List<Models.LendList>();

        public LendingListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                var id = Models.LoginCredentials.loginId;

                var Url = "http://www.good-lookz.com/API/lend/lendDownload.php?users_id={0}&active=1";
                var Url_Full = string.Format(Url, id);

                var content = await _client.GetStringAsync(Url_Full);
                response = JsonConvert.DeserializeObject<List<Models.LendList>>(content);

                _gets = new ObservableCollection<Models.LendList>(response);

                lendingList.ItemsSource = _gets;
            }
            catch
            {
                lblLending.Text = "No lending";
            }
        }

        async void Lend_Tapped(object sender, ItemTappedEventArgs e)
        {
            Models.LendList item = (Models.LendList)e.Item;
            var lend_id = item.lend_id;

            var requestResponse = await DisplayAlert("Delete?", "Delete lending?", "Yes", "No");

            if (requestResponse)
            {
                var url = "http://www.good-lookz.com/API/lend/lendDelete.php";

                var values = new Dictionary<string, string>
                {
                    { "lend_id", lend_id }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await _client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var postMethod = JsonConvert.DeserializeObject<List<lendDelete>>(responseString);

                await DisplayAlert("Message", "Succesfully deleted!", "OK");
                
                // Delete item van ObservableCollection
                foreach (var items in _gets.ToList())
                {
                    if (items.lend_id == lend_id)
                    {
                        _gets.Remove(items);
                    }
                }
            }
        }

		async void LendRequests_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LendRequests(), true);
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
