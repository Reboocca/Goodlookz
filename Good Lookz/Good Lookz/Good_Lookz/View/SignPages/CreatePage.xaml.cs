using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SignPages
{
    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class CreatePage : ContentPage
    {
        private const string Url = "http://good-lookz.com/API/account/CreateUser.php";

        public CreatePage()
        {
            InitializeComponent();

            btnTerms.BackgroundColor = Color.Transparent;
        }

        async void Create_button(object sender, EventArgs e)
        {
            btnCreate.IsVisible = false;
            loadingCreate.IsVisible = true;
            loadingCreate.IsRunning = true;

            var acceptedTerms = termsConditions.IsToggled;
            var Username = username.Text;
            var Password = password.Text;
            var First_name = first_name.Text;
            var Last_name = last_name.Text;
            var Email = email.Text;

            if ((Username == "") || (Password == "") || (First_name == "") || (Last_name == "") || (Email == ""))
            {
                loadingCreate.IsRunning = false;
                loadingCreate.IsVisible = false;
                btnCreate.IsVisible = true;

                await DisplayAlert("Not Filled", "Don't leave empty!", "OK");
            }
			else if (email.TextColor == Color.Red)
			{
				loadingCreate.IsRunning = false;
				loadingCreate.IsVisible = false;
				btnCreate.IsVisible = true;

				await DisplayAlert("Error", "Invalid email address", "OK");
			}
            else
            {
                if (!acceptedTerms)
                {
                    loadingCreate.IsRunning = false;
                    loadingCreate.IsVisible = false;
                    btnCreate.IsVisible = true;

                    await DisplayAlert("Message", "Please accept the Terms of Conditions and Privacy Policy", "OK");
                    return;
                }

                var values = new Dictionary<string, string>
                {
                    { "username", Username },
                    { "password", Password },
                    { "first_name", First_name },
                    { "last_name", Last_name },
                    { "email", Email }
                };

                using (HttpClient client = new HttpClient(new NativeMessageHandler()))
                {
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync(Url, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var postMethod = JsonConvert.DeserializeObject<List<Models.CreateAccount>>(responseString);

                    loadingCreate.IsRunning = false;
                    loadingCreate.IsVisible = false;
                    btnCreate.IsVisible = true;

                    if (postMethod[0].created == null)
                    {
                        await DisplayAlert("Exists", "Username or Email already exists.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Success", "Account created.", "OK");
						await Navigation.PushAsync(new View.SignPage(), true);
					}
                }
            }
        }

        async void Documents_Clicked(object sender, EventArgs e)
        {
            var _id = 0;
            fromPage.id = _id;
            await Navigation.PushAsync(new TermsConditionsPage(), true);
        }
    }
}
