using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Geolocator;
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
    class distanceUser
    {
        public string users_id { get; set; }
        public string username { get; set; }
        public string distance { get; set; }
    }

    public partial class UsedClothesPage : ContentPage
    {
        private const string url_id = "http://good-lookz.com/API/location/getLocation.php?latitude_origins={0}&longitude_origins={1}&distance={2}&users_id={3}";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.HeadSale> _obsrv_head;
        private ObservableCollection<Models.TopSale> _obsrv_top;
        private ObservableCollection<Models.BottomSale> _obsrv_bottom;
        private ObservableCollection<Models.FeetSale> _obsrv_feet;

        List<Models.HeadSale> gets_Head = new List<Models.HeadSale>();
        List<Models.TopSale> gets_Top = new List<Models.TopSale>();
        List<Models.BottomSale> gets_Bottom = new List<Models.BottomSale>();
        List<Models.FeetSale> gets_Feet = new List<Models.FeetSale>();
        List<distanceUser> gets_id = new List<distanceUser>();

        string latitude_full = "";
        string longitude_full = "";
        string distance = "30";      

        public UsedClothesPage()
        {
            InitializeComponent();
            
            if (Application.Current.Properties.ContainsKey("distance"))
                distance = (string)Application.Current.Properties["distance"];
        }

        protected override async void OnAppearing()
        {
            CarouselviewGrid.IsVisible = false;
            //btnSpecific.IsVisible = false;
            loadingPage.IsEnabled = true;
            loadingPage.IsRunning = true;

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                if (position == null)
                    return;

                var latitude = position.Latitude.ToString();
                var longitude = position.Longitude.ToString();

                latitude_full = latitude.Replace(",", ".");
                longitude_full = longitude.Replace(",", ".");

                loadingPage.IsRunning = false;
                loadingPage.IsEnabled = false;
                CarouselviewGrid.IsVisible = true;
                //btnSpecific.IsVisible = true;
            }
            catch
            {
                loadingPage.IsRunning = false;
                loadingPage.IsEnabled = false;
                await DisplayAlert("Error", "Couldnt get location", "OK");
                return;
            }

            var users_id = Models.LoginCredentials.loginId;
            var full_url_id = string.Format(url_id, latitude_full, longitude_full, distance, users_id);

            var content_id = await client.GetStringAsync(full_url_id);
            gets_id = JsonConvert.DeserializeObject<List<distanceUser>>(content_id);

            if (gets_id.Count == 0)
            {
                await DisplayAlert("Message", "No users found within your chosen range.", "OK");
                App.Current.MainPage = new NavigationPage(new MenuPage());
                return;
            }

            _obsrv_head = new ObservableCollection<Models.HeadSale>(gets_Head);
            _obsrv_top = new ObservableCollection<Models.TopSale>(gets_Top);
            _obsrv_bottom = new ObservableCollection<Models.BottomSale>(gets_Bottom);
            _obsrv_feet = new ObservableCollection<Models.FeetSale>(gets_Feet);

            foreach (var id in gets_id)
            {
                var userID = id.users_id;

                loadingHead.IsRunning = true;
                loadingTop.IsRunning = true;
                loadingBottom.IsRunning = true;
                loadingFeet.IsRunning = true;

                string Url_Head = "http://www.good-lookz.com/API/sale/type/head/headDownload.php?users_id={0}";

                string urlFilled_Head = string.Format(Url_Head, userID);

                var content_Head = await client.GetStringAsync(urlFilled_Head);
                gets_Head = JsonConvert.DeserializeObject<List<Models.HeadSale>>(content_Head);

                foreach (var item in gets_Head)
                {
                    _obsrv_head.Add(item);
                }

                loadingHead.IsRunning = false;
                loadingHead.IsVisible = false;
                loadingHead.IsEnabled = false;

                try
                {
                    // List vullen met de opgehaalde data
                    CarouselViewHead.ItemsSource = _obsrv_head;
                }
                catch
                {

                }

                // ---------------------------

                string Url_Top = "http://www.good-lookz.com/API/sale/type/top/topDownload.php?users_id={0}&size={1}";

                string data_Top = Models.LoginCredentials.loginId;
                string size_Top = "";

                string urlFilled_Top = string.Format(Url_Top, userID, size_Top);

                var content_Top = await client.GetStringAsync(urlFilled_Top);
                gets_Top = JsonConvert.DeserializeObject<List<Models.TopSale>>(content_Top);

                foreach (var item in gets_Top)
                {
                    _obsrv_top.Add(item);
                }

                loadingTop.IsRunning = false;
                loadingTop.IsVisible = false;
                loadingTop.IsEnabled = false;

                try
                {
                    // List vullen met de opgehaalde data
                    CarouselViewTop.ItemsSource = _obsrv_top;
                }
                catch
                {

                }

                // ---------------------------

                string Url_Bottom = "http://www.good-lookz.com/API/sale/type/bottom/bottomDownload.php?users_id={0}&size={1}";

                string data_Bottom = Models.LoginCredentials.loginId;
                string size_Bottom = "";

                string urlFilled_Bottom = string.Format(Url_Bottom, userID, size_Bottom);

                var content_Bottom = await client.GetStringAsync(urlFilled_Bottom);
                gets_Bottom = JsonConvert.DeserializeObject<List<Models.BottomSale>>(content_Bottom);

                foreach (var item in gets_Bottom)
                {
                    _obsrv_bottom.Add(item);
                }

                loadingBottom.IsRunning = false;
                loadingBottom.IsVisible = false;
                loadingBottom.IsEnabled = false;

                try
                {
                    // List vullen met de opgehaalde data
                    CarouselViewBottom.ItemsSource = _obsrv_bottom;
                }
                catch
                {

                }

                // ---------------------------

                string Url_Feet = "http://www.good-lookz.com/API/sale/type/feet/feetDownload.php?users_id={0}&size={1}";

                string data_Feet = Models.LoginCredentials.loginId;
                string size_Feet = "";

                string urlFilled_Feet = string.Format(Url_Feet, userID, size_Feet);

                var content_Feet = await client.GetStringAsync(urlFilled_Feet);
                gets_Feet = JsonConvert.DeserializeObject<List<Models.FeetSale>>(content_Feet);

                foreach (var item in gets_Feet)
                {
                    _obsrv_feet.Add(item);
                }

                loadingFeet.IsRunning = false;
                loadingFeet.IsVisible = false;
                loadingFeet.IsEnabled = false;

                try
                {
                    // List vullen met de opgehaalde data
                    CarouselViewFeet.ItemsSource = _obsrv_feet;
                }
                catch
                {

                }

                // ---------------------------
            }
            if ((gets_Head.Count == 0) && (gets_Top.Count == 0) && (gets_Bottom.Count == 0) && (gets_Feet.Count == 0))
            {
                await DisplayAlert("Message", "Seems like the users within your chosen range have no items for sale.", "OK");
                App.Current.MainPage = new NavigationPage(new MenuPage());
                return;
            }
        }

        void CarouselViewHead_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.HeadSale item = (Models.HeadSale)e.SelectedItem;

            var _head_sale_id = item.head_sale_id;
            var _users_id = item.users_id;
            var _head_id = item.head_id;
            var _picture = item.picture;
            var _date = item.date;
            var _username = item.username;
            var _price = item.price;
            var _desc = item.desc;
            var _fullPrice = item.fullPrice;

            Models.headSaleSelected.head_sale_id = _head_sale_id;
            Models.headSaleSelected.users_id = _users_id;
            Models.headSaleSelected.head_id = _head_id;
            Models.headSaleSelected.picture = _picture;
            Models.headSaleSelected.date = _date;
            Models.headSaleSelected.username = _username;
            Models.headSaleSelected.price = _price;
            Models.headSaleSelected.desc = _desc;
            Models.headSaleSelected.fullPrice = _fullPrice;
        }

        void CarouselViewTop_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.TopSale item = (Models.TopSale)e.SelectedItem;

            var _top_sale_id = item.top_sale_id;
            var _users_id = item.users_id;
            var _top_id = item.top_id;
            var _picture = item.picture;
            var _date = item.date;
            var _size = item.size;
            var _username = item.username;
            var _price = item.price;
            var _desc = item.desc;
            var _fullPrice = item.fullPrice;

            Models.topSaleSelected.top_sale_id = _top_sale_id;
            Models.topSaleSelected.users_id = _users_id;
            Models.topSaleSelected.top_id = _top_id;
            Models.topSaleSelected.picture = _picture;
            Models.topSaleSelected.date = _date;
            Models.topSaleSelected.size = _size;
            Models.topSaleSelected.username = _username;
            Models.topSaleSelected.price = _price;
            Models.topSaleSelected.desc = _desc;
            Models.topSaleSelected.fullPrice = _fullPrice;
        }

        void CarouselViewBottom_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.BottomSale item = (Models.BottomSale)e.SelectedItem;

            var _bottom_sale_id = item.bottom_sale_id;
            var _users_id = item.users_id;
            var _bottom_id = item.bottom_id;
            var _picture = item.picture;
            var _date = item.date;
            var _size = item.size;
            var _username = item.username;
            var _price = item.price;
            var _desc = item.desc;
            var _fullPrice = item.fullPrice;

            Models.bottomSaleSelected.bottom_sale_id = _bottom_sale_id;
            Models.bottomSaleSelected.users_id = _users_id;
            Models.bottomSaleSelected.bottom_id = _bottom_id;
            Models.bottomSaleSelected.picture = _picture;
            Models.bottomSaleSelected.date = _date;
            Models.bottomSaleSelected.size = _size;
            Models.bottomSaleSelected.username = _username;
            Models.bottomSaleSelected.price = _price;
            Models.bottomSaleSelected.desc = _desc;
            Models.bottomSaleSelected.fullPrice = _fullPrice;
        }

        void CarouselViewFeet_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.FeetSale item = (Models.FeetSale)e.SelectedItem;

            var _feet_sale_id = item.feet_sale_id;
            var _users_id = item.users_id;
            var _feet_id = item.feet_id;
            var _picture = item.picture;
            var _date = item.date;
            var _size = item.size;
            var _username = item.username;
            var _price = item.price;
            var _desc = item.desc;
            var _fullPrice = item.fullPrice;

            Models.feetSaleSelected.feet_sale_id = _feet_sale_id;
            Models.feetSaleSelected.users_id = _users_id;
            Models.feetSaleSelected.feet_id = _feet_id;
            Models.feetSaleSelected.picture = _picture;
            Models.feetSaleSelected.date = _date;
            Models.feetSaleSelected.size = _size;
            Models.feetSaleSelected.username = _username;
            Models.feetSaleSelected.price = _price;
            Models.feetSaleSelected.desc = _desc;
            Models.feetSaleSelected.fullPrice = _fullPrice;
        }

        async void headTapped(object sender, EventArgs e)
        {
            var _typeCloth = 1;
            selectedItemType.typeCloth = _typeCloth;

            await Navigation.PushAsync(new SelectedSaleItemPage(), true);
        }

        async void topTapped(object sender, EventArgs e)
        {
            var _typeCloth = 2;
            selectedItemType.typeCloth = _typeCloth;

            await Navigation.PushAsync(new SelectedSaleItemPage(), true);
        }

        async void bottomTapped(object sender, EventArgs e)
        {
            var _typeCloth = 3;
            selectedItemType.typeCloth = _typeCloth;

            await Navigation.PushAsync(new SelectedSaleItemPage(), true);
        }

        async void feetTapped(object sender, EventArgs e)
        {
            var _typeCloth = 4;
            selectedItemType.typeCloth = _typeCloth;

            await Navigation.PushAsync(new SelectedSaleItemPage(), true);
        }

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            gets_Head.Clear();
            gets_Top.Clear();
            gets_Bottom.Clear();
            gets_Feet.Clear();
            GC.Collect();
        }
    }
}
