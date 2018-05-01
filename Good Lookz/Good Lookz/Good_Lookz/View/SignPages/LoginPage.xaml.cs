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
            btnLogin.IsVisible      = false;
            loadingLogin.IsVisible  = true;
            loadingLogin.IsRunning  = true;
            
            var values = new Dictionary<string, string>
            {
                { "loginName", usrOrMail.Text },
                { "password", password.Text }
            };

            using (HttpClient client = new HttpClient(new NativeMessageHandler()))
            {
                var content         = new FormUrlEncodedContent(values);
                var response        = await client.PostAsync(Url, content);
                var responseString  = await response.Content.ReadAsStringAsync();
                var postMethod      = JsonConvert.DeserializeObject<List<Models.LoginAccount>>(responseString);

                var _loginId        = postMethod[0].id;
                var _loginUsername  = postMethod[0].username;
                var _loginPassword  = postMethod[0].password;
                var _loginFirstname = postMethod[0].first_name;
                var _loginLastname  = postMethod[0].last_name;
                var _loginEmail     = postMethod[0].email;
                var _loginDate      = postMethod[0].date;
                var _loginGender    = postMethod[0].gender;
                var _loginOffline   = postMethod[0].offline;
                var _loginActive    = postMethod[0].active;

                Models.LoginCredentials.loginId         = _loginId;
                Models.LoginCredentials.loginUsername   = _loginUsername;
                Models.LoginCredentials.loginPassword   = _loginPassword;
                Models.LoginCredentials.loginFirstname  = _loginFirstname;
                Models.LoginCredentials.loginLastname   = _loginLastname;
                Models.LoginCredentials.loginEmail      = _loginEmail;
                Models.LoginCredentials.loginDate       = _loginDate;
                Models.LoginCredentials.loginGender     = _loginGender;
                Models.LoginCredentials.loginOffline    = _loginOffline;
                Models.LoginCredentials.loginActive     = _loginActive;

                Application.Current.Properties["loginId"]           = _loginId;
                Application.Current.Properties["loginUsername"]     = _loginUsername;
                Application.Current.Properties["loginPassword"]     = _loginPassword;
                Application.Current.Properties["loginFirstname"]    = _loginFirstname;
                Application.Current.Properties["loginLastname"]     = _loginLastname;
                Application.Current.Properties["loginEmail"]        = _loginEmail;
                Application.Current.Properties["loginDate"]         = _loginDate;
                Application.Current.Properties["loginGender"]       = _loginGender;
                Application.Current.Properties["loginOffline"]      = _loginOffline;
                Application.Current.Properties["loginActive"]       = _loginActive;

				if (postMethod[0].username == null)
                {
                    loadingLogin.IsRunning  = false;
                    loadingLogin.IsVisible  = false;
                    btnLogin.IsVisible      = true;

					await DisplayAlert("Failed", "The given login credentials are incorrect", "OK");
				}
				else
                {
                    string Url_shops        = "http://www.good-lookz.com/API/shops/shopsChosen.php?users_id={0}";
                    string users_id         = Models.LoginCredentials.loginId;
                    string Url_shops_full   = string.Format(Url_shops, users_id);

                    var content_shops       = await client.GetStringAsync(Url_shops_full);
                    var gets_shopsChosen    = JsonConvert.DeserializeObject<List<Models.ShopsChosen>>(content_shops);

                    var _hasChosen          = gets_shopsChosen.Count;

                    loadingLogin.IsRunning  = false;
                    loadingLogin.IsVisible  = false;
                    btnLogin.IsVisible      = true;

                    if (_hasChosen == 0)
                    {
                        App.Current.MainPage = new ShopPages.ChoseShopPage();
                    }
                    else
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
                }
            }
        }


		private void forgotPWD(object sender, EventArgs e)
		{
			Navigation.PushAsync(new View.SignPages.ForgotPassword(), true);
		}
    }
}
