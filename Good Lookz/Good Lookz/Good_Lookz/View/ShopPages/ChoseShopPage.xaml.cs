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

namespace Good_Lookz.View.ShopPages
{
    /// <summary>
    /// JSON response word opgevangen en in een list met variables gestopt.
    /// </summary>
    class shopsUploaded
    {
        public bool uploaded { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class ChoseShopPage : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.GetShops> _gets_Care;
        private ObservableCollection<Models.GetShops> _gets_Fashion;
        private ObservableCollection<Models.GetShops> _gets_Accessories;

        public ChoseShopPage()
        {
            InitializeComponent();

            DisplayAlert("Hello there!", "Looks like this is your first time!\n\nChose your shops you prefer in order to continue.\nSwipe to your left!\n\n(You can only choose one per row.)", "OK");
        }

        protected override async void OnAppearing()
        {
            string url_Care = "http://www.good-lookz.com/API/shops/shopsDownload.php?rubric_id=1";
            string url_Fashion = "http://www.good-lookz.com/API/shops/shopsDownload.php?rubric_id=2";
            string url_Accessories = "http://www.good-lookz.com/API/shops/shopsDownload.php?rubric_id=3";

            var content_Care = await _client.GetStringAsync(url_Care);
            var content_Fashion = await _client.GetStringAsync(url_Fashion);
            var content_Accessories = await _client.GetStringAsync(url_Accessories);

            var response_Care = JsonConvert.DeserializeObject<List<Models.GetShops>>(content_Care);
            var response_Fashion = JsonConvert.DeserializeObject<List<Models.GetShops>>(content_Fashion);
            var response_Accessories = JsonConvert.DeserializeObject<List<Models.GetShops>>(content_Accessories);

            _gets_Care = new ObservableCollection<Models.GetShops>(response_Care);
            _gets_Fashion = new ObservableCollection<Models.GetShops>(response_Fashion);
            _gets_Accessories = new ObservableCollection<Models.GetShops>(response_Accessories);

            carouselViewCare.ItemsSource = _gets_Care;
            carouselViewFashion.ItemsSource = _gets_Fashion;
            carouselViewAccessories.ItemsSource = _gets_Accessories;

            base.OnAppearing();
        }

        void CarouselViewCare_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.GetShops item_Care = (Models.GetShops)e.SelectedItem;

            var _shops1_id = item_Care.shops_id;
            Models.ShopsChosenSaved.shops1_id = Int32.Parse(_shops1_id);

        }

        void CarouselViewFashion_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.GetShops item_Fashion = (Models.GetShops)e.SelectedItem;

            var _shops2_id = item_Fashion.shops_id;
            Models.ShopsChosenSaved.shops2_id = Int32.Parse(_shops2_id);
        }

        void CarouselViewAccessories_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.GetShops item_Accessories = (Models.GetShops)e.SelectedItem;

            var _shops3_id = item_Accessories.shops_id;
            Models.ShopsChosenSaved.shops3_id = Int32.Parse(_shops3_id);
        }

        async void SaveShop_Clicked(object sender, EventArgs e)
        {
            var shops1_id = Models.ShopsChosenSaved.shops1_id.ToString();
            var shops2_id = Models.ShopsChosenSaved.shops2_id.ToString();
            var shops3_id = Models.ShopsChosenSaved.shops3_id.ToString();
            var users_id = Models.LoginCredentials.loginId;

            string url = "http://www.good-lookz.com/API/shops/shopsChosen.php";

            var value_Care = new Dictionary<string, string>
            {
                { "users_id", users_id },
                { "rubric_id", "1" },
                { "shops_id", shops1_id }
            };

            var value_Fashion = new Dictionary<string, string>
            {
                { "users_id", users_id },
                { "rubric_id", "2" },
                { "shops_id", shops2_id }
            };

            var value_Accessories = new Dictionary<string, string>
            {
                { "users_id", users_id },
                { "rubric_id", "3" },
                { "shops_id", shops3_id }
            };

            var content_Care = new FormUrlEncodedContent(value_Care);
            var response_Care = await _client.PostAsync(url, content_Care);
            var responseString_Care = await response_Care.Content.ReadAsStringAsync();
            var postMethod_Care = JsonConvert.DeserializeObject<List<shopsUploaded>>(responseString_Care);

            var content_Fashion = new FormUrlEncodedContent(value_Fashion);
            var response_Fashion = await _client.PostAsync(url, content_Fashion);
            var responseString_Fashion = await response_Fashion.Content.ReadAsStringAsync();
            var postMethod_Fashion = JsonConvert.DeserializeObject<List<shopsUploaded>>(responseString_Fashion);

            var content_Accessories = new FormUrlEncodedContent(value_Accessories);
            var response_Accessories = await _client.PostAsync(url, content_Accessories);
            var responseString_Accessories = await response_Accessories.Content.ReadAsStringAsync();
            var postMethod_Accessories = JsonConvert.DeserializeObject<List<shopsUploaded>>(responseString_Accessories);

            await DisplayAlert("Success", "Your shop preferences have been saved.", "OK");

            var _id = 1;
            fromPage.id = _id;
			App.Current.MainPage = new SaveSizePage();
		}
    }
}
