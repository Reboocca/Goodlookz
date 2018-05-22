using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public class setUpload
    {
        public bool set_upload { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class WardrobeSaveSet : ContentPage
    {
        private const string url = "http://www.good-lookz.com/API/wardrobeSet/wardrobeSetUpload.php";

        public WardrobeSaveSet()
        {
            InitializeComponent();

            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            ImageHead.Source    = Models.SelectedHead.picture;
            ImageTop.Source     = Models.SelectedTop.picture;
            ImageBottom.Source  = Models.SelectedBottom.picture;
            ImageFeet.Source    = Models.SelectedFeet.picture;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(setName.Text) || setName.Text[0].ToString() == " ")
            {
                await DisplayAlert("Warning", "Please give a name for your set. \nDo not leave it empty.", "OK");
            }
            else
            {
                var users_id    = Models.LoginCredentials.loginId;
                var name        = setName.Text;
                var head_id     = Models.SelectedHead.head_id.ToString();
                var top_id      = Models.SelectedTop.top_id.ToString();
                var bottom_id   = Models.SelectedBottom.bottom_id.ToString();
                var feet_id     = Models.SelectedFeet.feet_id.ToString();

                if (name == null)
                {
                    await DisplayAlert("Error", "Do not leave the name empty!", "OK");
                }
                else
                {
                    var values = new Dictionary<string, string>
                {
                    { "users_id", users_id },
                    { "name", name },
                    { "head_id", head_id },
                    { "top_id", top_id },
                    { "bottom_id", bottom_id },
                    { "feet_id", feet_id }
                };

                    using (HttpClient client = new HttpClient(new NativeMessageHandler()))
                    {
                        var content = new FormUrlEncodedContent(values);
                        var response = await client.PostAsync(url, content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        var postMethod = JsonConvert.DeserializeObject<List<setUpload>>(responseString);

                        if (postMethod[0].set_upload == true)
                        {
                            await DisplayAlert("Success", "Set has been saved.", "OK");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
                        }
                    }
                }
            }
        }

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            Content = null;
            GC.Collect();
        }
    }
}
