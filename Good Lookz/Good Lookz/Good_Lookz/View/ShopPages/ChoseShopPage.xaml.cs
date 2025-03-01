﻿using ModernHttpClient;
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
        private HttpClient _client  = new HttpClient(new NativeMessageHandler());
        Models.UserSizes uSize      = new Models.UserSizes();

        public ChoseShopPage()
        {
            InitializeComponent();

            DisplayAlert("Hello there!", "Looks like this is your first time!\n\nChose your shops you prefer in order to continue.\nSwipe to your left!\n\n(You can only choose one per row.)", "OK");
        }

        //code van: https://stackoverflow.com/questions/32163471/confirmation-dialog-on-back-button-press-event-xamarin-forms voor terugknop
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => { await this.DisplayAlert("Message", "Please save your favourite shops before proceeding", "OK"); });
            return true;
        }

        protected override async void OnAppearing()
        {
            //Get saved region
            await getUserSize();

            //Get shops
            getShops("1");
            getShops("2");
            getShops("3");

            base.OnAppearing();
        }

        async private void getShops(string rubric)
        {
            try
            {
                string webadres = "http://good-lookz.com/API/shops/getShopsChoose.php?";
                string parameters = "rubric_id=" + rubric + "&region=" + uSize.region;
                HttpClient connect = new HttpClient();
                HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
                get.EnsureSuccessStatusCode();
                
                string result = await get.Content.ReadAsStringAsync();
                var jsonresult = JsonConvert.DeserializeObject<List<Models.GetShops>>(result);

                switch (rubric)
                {
                    case "1":
                        carouselViewCare.ItemsSource        = new ObservableCollection<Models.GetShops>(jsonresult);
                        break;

                    case "2":
                        carouselViewFashion.ItemsSource     = new ObservableCollection<Models.GetShops>(jsonresult);
                        break;
                    case "3":
                        carouselViewAccessories.ItemsSource = new ObservableCollection<Models.GetShops>(jsonresult);
                        break;

                }

            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
                throw;
            }
        }

        //Haal region op
        private async Task getUserSize()
        {
            string users_id = Models.LoginCredentials.loginId;
            string url      = "http://good-lookz.com/API/account/getSizes.php?users_id=" + users_id;

            HttpClient get = new HttpClient();
            HttpResponseMessage respons = await get.GetAsync(url);

            if (respons.IsSuccessStatusCode)
            {
                string responsecontent = await respons.Content.ReadAsStringAsync();
                var myobjList = JsonConvert.DeserializeObject<List<Models.UserSizes>>(responsecontent);
                var myObj = myobjList[0];

                uSize.region = myObj.region;
            }
            else
            {
                uSize.region = "EU";
            }
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

            var content_Care        = new FormUrlEncodedContent(value_Care);
            var response_Care       = await _client.PostAsync(url, content_Care);
            var responseString_Care = await response_Care.Content.ReadAsStringAsync();
            var postMethod_Care     = JsonConvert.DeserializeObject<List<shopsUploaded>>(responseString_Care);

            var content_Fashion         = new FormUrlEncodedContent(value_Fashion);
            var response_Fashion        = await _client.PostAsync(url, content_Fashion);
            var responseString_Fashion  = await response_Fashion.Content.ReadAsStringAsync();
            var postMethod_Fashion      = JsonConvert.DeserializeObject<List<shopsUploaded>>(responseString_Fashion);

            var content_Accessories         = new FormUrlEncodedContent(value_Accessories);
            var response_Accessories        = await _client.PostAsync(url, content_Accessories);
            var responseString_Accessories  = await response_Accessories.Content.ReadAsStringAsync();
            var postMethod_Accessories      = JsonConvert.DeserializeObject<List<shopsUploaded>>(responseString_Accessories);

            await DisplayAlert("Success", "Your shop preferences have been saved.", "OK");

            var _id     = 1;
            fromPage.id = _id;

            App.Current.MainPage = new NavigationPage(new SaveProfile());
        }
    }
}
