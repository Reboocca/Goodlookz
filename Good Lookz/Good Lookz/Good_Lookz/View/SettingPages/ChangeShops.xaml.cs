﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Good_Lookz.View.SettingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeShops : ContentPage
    {
        public ChangeShops()
        {
            InitializeComponent();
        }

        class Shops
        {
            public string shops_id { get; set; }
            public string name { get; set; }
            public string picture { get; set; }
            public string url { get; set; }
            public string active { get; set; }
            public int position = 0;
        }

        #region Global
        Models.UserSizes uSize = new Models.UserSizes();
        #endregion

        protected override async void OnAppearing()
        {
            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            //Haal region op
            await getUserSize();

            //Check of de shops niet verwijdert zijn
            string care         = await checkShop("1");
            string fashion      = await checkShop("2");
            string accessories  = await checkShop("3");

            if (care == "false" || fashion == "false" || accessories == "false")
            {
                if(care == "false")
                {
                    getShopMissing("1");
                    getShopsAsync("2");
                    getShopsAsync("3");
                }
                else if(fashion == "false")
                {
                    getShopsAsync("1");
                    getShopMissing("2");
                    getShopsAsync("3");
                }
                else
                {
                    getShopsAsync("1");
                    getShopsAsync("2");
                    getShopMissing("3");

                }
            }
            else
            {
                //Haal shops op en stop ze in de carouselviews
                getShopsAsync("1");
                getShopsAsync("2");
                getShopsAsync("3");
            }

            
        }

        private async Task<string> checkShop(string r)
        {
            string webadres = "http://good-lookz.com/API/shops/checkShops.php";
            string parameters = "?users_id=" + Models.LoginCredentials.loginId + "&rubric=" + r;
            HttpClient connect = new HttpClient();
            HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
            get.EnsureSuccessStatusCode();

            string result = await get.Content.ReadAsStringAsync();
            return result;
        }

        private async void getShopMissing(string r)
        {
            try
            {
                string webadres = "http://good-lookz.com/API/shops/getShopsChoose.php?";
                string parameters = "rubric_id=" + r + "&region=" + uSize.region;
                HttpClient connect = new HttpClient();
                HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
                get.EnsureSuccessStatusCode();

                string result = await get.Content.ReadAsStringAsync();
                var jsonresult = JsonConvert.DeserializeObject<List<Shops>>(result);

                switch (r)
                {
                    case "1":
                        cvCare.ItemsSource = new ObservableCollection<Shops>(jsonresult);
                        break;

                    case "2":
                        cvFashion.ItemsSource = new ObservableCollection<Shops>(jsonresult);
                        break;
                    case "3":
                        cvAccessories.ItemsSource = new ObservableCollection<Shops>(jsonresult);
                        break;

                }


            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
                throw;
            }
        }

        private async void getShopsAsync(string id)
        {
            try
            {
                string webadres         = "http://www.good-lookz.com/API/shops/getShopsChange.php?";
                string parameters       = "rubric_id=" + id + "&region=" + uSize.region;
                HttpClient connect      = new HttpClient();
                HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
                get.EnsureSuccessStatusCode();

                string result   = await get.Content.ReadAsStringAsync();
                var jsonresult  = JsonConvert.DeserializeObject<List<Shops>>(result);
                string shop     = null;

                switch (id)
                {
                    case "1":
                        cvCare.ItemsSource  = new ObservableCollection<Shops>(jsonresult);
                        shop                = await getShopSaved("1");

                        //Selecteer de juiste Fashion Shop
                        foreach (Shops s in jsonresult)
                        {
                            if (s.shops_id == shop)
                            {
                                cvCare.Position = s.position;
                            }
                        }
                        break;
                    case "2":
                        cvFashion.ItemsSource   = new ObservableCollection<Shops>(jsonresult);
                        shop                    = await getShopSaved("2");

                        //Selecteer de juiste Fashion Shop
                        foreach (Shops s in jsonresult)
                        {
                            if (s.shops_id == shop)
                            {
                                cvFashion.Position = s.position;
                            }
                        }
                        break;
                    case "3":
                        cvAccessories.ItemsSource   = new ObservableCollection<Shops>(jsonresult);
                        shop                        = await getShopSaved("3");

                        //Selecteer de juiste Fashion Shop
                        foreach (Shops s in jsonresult)
                        {
                            if (s.shops_id == shop)
                            {
                                cvAccessories.Position = s.position;
                            }
                        }
                        break;
                }
            }
            catch (Exception)
            {
               
            }
        }

        private async Task getUserSize()
        {
            string users_id = Models.LoginCredentials.loginId;
            string url      = "http://good-lookz.com/API/account/getSizes.php?users_id=" + users_id;

            HttpClient get = new HttpClient();
            HttpResponseMessage response = await get.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responsecontent = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<List<Models.UserSizes>>(responsecontent);
                var result = results[0];

                uSize.region = result.region;
            }
            else
            {
                uSize.region = "EU";
            }

        }

        private async Task<string> getShopSaved(string id)
        {
            string result = "Failed";
            try
            {
                //Stuur verzoek naar web API om de json op te halen
                string webadres = "http://good-lookz.com/API/account/getShopUser.php?";
                string parameters = "users_id=" + Models.LoginCredentials.loginId + "&rubric_id=" + id;
                HttpClient connect = new HttpClient();
                HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
                get.EnsureSuccessStatusCode();

                //Sla het resultaat op in een string
                result = await get.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                
            }

            return result;
        }

        private async Task<bool> saveShopAsync(string shop_id, string rubric_id)
        {
            bool r = false;
            try
            {
                string webadres         = "http://www.good-lookz.com/API/account/updateShopPreference.php?";
                string parameters       = "users_id=" + Models.LoginCredentials.loginId + "&shop_id=" + shop_id + "&rubric_id=" + rubric_id;
                HttpClient connect      = new HttpClient();
                HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
                get.EnsureSuccessStatusCode();

                string result = await get.Content.ReadAsStringAsync();
                if(result == "Success")
                {
                    r = true;
                }
            }
            catch (Exception)
            {

            }

            return r;
        }

        private void cvCare_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Sla id op van de winkel in de class
            Shops item                          = (Shops)e.SelectedItem;
            Models.ShopsChosenSaved.shops1_id   = Int32.Parse(item.shops_id);
        }

        private void cvFashion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Sla id op van de winkel in de class
            Shops item                          = (Shops)e.SelectedItem;
            Models.ShopsChosenSaved.shops2_id   = Int32.Parse(item.shops_id);
        }

        private void cvAccessories_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Sla id op van de winkel in de class
            Shops item                          = (Shops)e.SelectedItem;
            Models.ShopsChosenSaved.shops3_id   = Int32.Parse(item.shops_id);
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            bool i1 = await saveShopAsync(Models.ShopsChosenSaved.shops1_id.ToString(), "1");
            bool i2 = await saveShopAsync(Models.ShopsChosenSaved.shops2_id.ToString(), "2");
            bool i3 = await saveShopAsync(Models.ShopsChosenSaved.shops3_id.ToString(), "3");

            if(i1 && i2 && i3)
            {
                await DisplayAlert("Succes", "Your shop preferences have been saved", "OK");
                await this.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Something went wrong with updating your preferences, please check your internet connection and try again.", "OK");
            }
        }
    }
}