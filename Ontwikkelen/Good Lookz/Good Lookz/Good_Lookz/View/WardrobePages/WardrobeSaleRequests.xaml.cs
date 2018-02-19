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
    class SaleRequestsDeleted
    {
        public bool requests_deleted { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class WardrobeSaleRequests : ContentPage
    {
        private const string url = "http://www.good-lookz.com/API/sale/saleRequests.php?users_id={0}";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.SaleRequests> _gets;

        /// <summary>
        /// List maken en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.SaleRequests> response = new List<Models.SaleRequests>();

        public WardrobeSaleRequests()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            string data = Models.LoginCredentials.loginId;

            string URL = string.Format(url, data);

            var content = await client.GetStringAsync(URL);
            response = JsonConvert.DeserializeObject<List<Models.SaleRequests>>(content);

            _gets = new ObservableCollection<Models.SaleRequests>(response);

            saleRequests.ItemsSource = _gets;
        }

        async void SaleRequests_Tapped(object sender, ItemTappedEventArgs e)
        {
            Models.SaleRequests items = (Models.SaleRequests)e.Item;

            var _requests_id = items.requests_id;
            var _sale_id = items.sale_id;
            var _users_id1 = items.users_id1;
            var _users_id2 = items.users_id2;
            var _username = items.username;
            var _bidding = items.bidding;
            var _picture = items.picture;
            var _price = items.price;
            var _desc = items.desc;
            var _fullPrice = items.fullPrice;

            Models.SelectedSaleRequests.requests_id = _requests_id;
            Models.SelectedSaleRequests.sale_id = _sale_id;
            Models.SelectedSaleRequests.users_id1 = _users_id1;
            Models.SelectedSaleRequests.users_id2 = _users_id2;
            Models.SelectedSaleRequests.username = _username;
            Models.SelectedSaleRequests.bidding = _bidding;
            Models.SelectedSaleRequests.picture = _picture;
            Models.SelectedSaleRequests.price = _price;
            Models.SelectedSaleRequests.desc = _desc;
            Models.SelectedSaleRequests.fullPrice = _fullPrice;

            await Navigation.PushAsync(new WardrobeSelectedSaleRequests(), true);
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var requests_id = item.CommandParameter.ToString();

            var url_delete = "http://www.good-lookz.com/API/sale/saleRequests.php";

            var values = new Dictionary<string, string>
            {
                { "requests_id", requests_id }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url_delete, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var postMethod = JsonConvert.DeserializeObject<List<SaleRequestsDeleted>>(responseString);

            // Delete item van ObservableCollection
            foreach (var items in _gets.ToList())
            {
                if (requests_id == items.requests_id)
                {
                    _gets.Remove(items);
                }
            }

            await DisplayAlert("Message", "Deleted: " + postMethod[0].requests_deleted, "OK");
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
