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
    class emailSend
    {
        public bool send { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class WardrobeSelectedSaleRequests : ContentPage
    {
        private const string url = "http://www.good-lookz.com/API/email/emailSend.php";
        HttpClient client = new HttpClient(new NativeMessageHandler());

        public WardrobeSelectedSaleRequests()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            lblUsrname.Text = Models.LoginCredentials.loginUsername;
            imageItem.Source = Models.SelectedSaleRequests.picture;
            lblPrice.Text = Models.SelectedSaleRequests.price;
            lblBidding.Text = Models.SelectedSaleRequests.bidding;
            lblUsrname_buyer.Text = Models.SelectedSaleRequests.username;
        }

        async void SendRequest_Clicked(object sender, EventArgs e)
        {
            var id = Models.SelectedSaleRequests.users_id1;
            var users_id = Models.LoginCredentials.loginId;
            var username = Models.LoginCredentials.loginUsername;
            var email = Models.LoginCredentials.loginEmail;

            var values = new Dictionary<string, string>
            {
                { "id", id },
                { "users_id", users_id },
                { "username", username },
                { "email", email }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var postMethod = JsonConvert.DeserializeObject<List<emailSend>>(responseString);

            await DisplayAlert("Message", "Mail has been sent!", "OK");
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
