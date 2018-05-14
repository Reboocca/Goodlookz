using Good_Lookz.MarkupExtensions;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SignPages
{
    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class LoginPage : ContentPage
    {
        private const string Url = "http://good-lookz.com/API/account/CheckUser.php";
        public LoginPage()
        {
            InitializeComponent();

			//REMOVE LATER
			usrOrMail.Text  = "rebecca";
			password.Text   = "123";
        }

        async void LoginClicked(object sender, EventArgs e)
        {
            btnLogin.IsVisible = false;
            loadingLogin.IsVisible = true;
            loadingLogin.IsRunning = true;

            var values = new Dictionary<string, string>
            {
                { "loginName", usrOrMail.Text },
                { "password", password.Text }
            };

            using (HttpClient client = new HttpClient(new NativeMessageHandler()))
            {
                try
                {
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync(Url, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var postMethod = JsonConvert.DeserializeObject<List<Models.LoginAccount>>(responseString);

                    Models.LoginCredentials.loginId         = postMethod[0].id;
                    Models.LoginCredentials.loginUsername   = postMethod[0].username;
                    Models.LoginCredentials.loginFirstname  = postMethod[0].first_name;
                    Models.LoginCredentials.loginLastname   = postMethod[0].last_name;
                    Models.LoginCredentials.loginEmail      = postMethod[0].email;
                    Models.LoginCredentials.loginDate       = postMethod[0].date;
                    Models.LoginCredentials.loginOffline    = postMethod[0].offline;
                    Models.LoginCredentials.loginActive     = postMethod[0].active;

                    Application.Current.Properties["loginId"]           = postMethod[0].id;
                    Application.Current.Properties["loginUsername"]     = postMethod[0].username;
                    Application.Current.Properties["loginFirstname"]    = postMethod[0].first_name;
                    Application.Current.Properties["loginLastname"]     = postMethod[0].last_name;
                    Application.Current.Properties["loginEmail"]        = postMethod[0].email;
                    Application.Current.Properties["loginDate"]         = postMethod[0].date;
                    Application.Current.Properties["loginOffline"]      = postMethod[0].offline;
                    Application.Current.Properties["loginActive"]       = postMethod[0].active;

                    if (postMethod[0].username == null)
                    {
                        loadingLogin.IsRunning = false;
                        loadingLogin.IsVisible = false;
                        btnLogin.IsVisible     = true;

                        if (postMethod[0].blocked)
                        {
                            await DisplayAlert("Error", "Your account has been blocked, please contact our support for more information.", "OK");
                        }
                        else{
                            await DisplayAlert("Failed", "The given login credentials are incorrect", "OK");
                        }

                    }
                    else
                    {
                        string Url_shops = "http://www.good-lookz.com/API/shops/shopsChosen.php?users_id={0}";
                        string users_id = Models.LoginCredentials.loginId;
                        string Url_shops_full = string.Format(Url_shops, users_id);

                        var content_shops = await client.GetStringAsync(Url_shops_full);
                        var gets_shopsChosen = JsonConvert.DeserializeObject<List<Models.ShopsChosen>>(content_shops);

                        var _hasChosen = gets_shopsChosen.Count;

                        loadingLogin.IsRunning = false;
                        loadingLogin.IsVisible = false;
                        btnLogin.IsVisible = true;

                        //Check of de gebruiker een profiel heeft opgeslagen
                        bool setupcomplete = await checkProfile(Models.LoginCredentials.loginId);

                        if (setupcomplete)
                        {
                            var _shops1_id = gets_shopsChosen[0].shops_id;
                            var _shops2_id = gets_shopsChosen[1].shops_id;
                            var _shops3_id = gets_shopsChosen[2].shops_id;

                            Models.ShopsChosenSaved.shops1_id = _shops1_id;
                            Models.ShopsChosenSaved.shops2_id = _shops2_id;
                            Models.ShopsChosenSaved.shops3_id = _shops3_id;

                            Application.Current.Properties["shops1_id"] = _shops1_id;
                            Application.Current.Properties["shops2_id"] = _shops2_id;
                            Application.Current.Properties["shops3_id"] = _shops3_id;

                            App.Current.MainPage = new NavigationPage(new MenuPage());
                        }
                        else
                        {
                            App.Current.MainPage = new ShopPages.ChoseShopPage();
                        }
                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "ok");
                    throw;
                }


            }
        }


        private void forgotPWD(object sender, EventArgs e)
	    {
		    Navigation.PushAsync(new View.SignPages.ForgotPassword(), true);
	    }

        private async Task<bool> checkProfile(string id)
        {
            bool r = false;
            try
            {
                string webadres         = "http://good-lookz.com/API/account/CheckProfileExists.php?";
                string parameters       = "users_id=" + Models.LoginCredentials.loginId;
                HttpClient connect      = new HttpClient();
                HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
                get.EnsureSuccessStatusCode();

                string result = await get.Content.ReadAsStringAsync();

                if (result == "True")
                {
                    r = true;
                }
                else
                {
                    r = false;
                }
            }
            catch (Exception)
            {
                r = false;
            }

            return r;
        }
    }
}
