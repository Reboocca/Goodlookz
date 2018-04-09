using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    class saleRequestUpload
    {
        public bool sale_request { get; set; }
    }

    class selectedItemType
    {
        private static int _typeCloth;

        public static int typeCloth
        {
            get { return _typeCloth; }
            set { _typeCloth = value; }
        }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class SelectedSaleItemPage : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());

        public SelectedSaleItemPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var typeCloth = selectedItemType.typeCloth;

            switch (typeCloth)
            {
                case 1:
                    imageItem.Source	= Models.headSaleSelected.picture;
                    lblPrice.Text		= Models.headSaleSelected.fullPrice;
                    lblUsrname.Text		= Models.headSaleSelected.username;
                    lblDesc.Text		= Models.headSaleSelected.desc;
					lblSize.IsVisible	= false;
					lblCity.Text		= Models.headSaleSelected.sale_city;
					break;
                case 2:
                    imageItem.Source	= Models.topSaleSelected.picture;
                    lblPrice.Text		= Models.topSaleSelected.fullPrice;
                    lblUsrname.Text		= Models.topSaleSelected.username;
                    lblDesc.Text		= Models.topSaleSelected.desc;
                    lblSize.Text		= Models.topSaleSelected.size;
					lblCity.Text		= Models.topSaleSelected.sale_city;
                    break;
                case 3:
                    imageItem.Source	= Models.bottomSaleSelected.picture;
                    lblPrice.Text		= Models.bottomSaleSelected.fullPrice;
                    lblUsrname.Text		= Models.bottomSaleSelected.username;
                    lblDesc.Text		= Models.bottomSaleSelected.desc;
                    lblSize.Text		= Models.bottomSaleSelected.size;
					lblCity.Text		= Models.bottomSaleSelected.sale_city;
					break;
                case 4:
                    imageItem.Source	= Models.feetSaleSelected.picture;
                    lblPrice.Text		= Models.feetSaleSelected.fullPrice;
                    lblUsrname.Text		= Models.feetSaleSelected.username;
                    lblDesc.Text		= Models.feetSaleSelected.desc;
                    lblSize.Text		= Models.feetSaleSelected.size;
					lblCity.Text		= Models.feetSaleSelected.sale_city;
					break;
                default:
                    break;
            }           
        }

        async void SendRequest_Clicked(object sender, EventArgs e)
        {
            //await DisplayAlert("Message", "Clicked", "OK");
            var url = "http://www.good-lookz.com/API/sale/saleRequests.php";
            var users_id1 = Models.LoginCredentials.loginId;
            var typeCloth = selectedItemType.typeCloth;
            var sale_id = "";
            var users_id2 = "";

            switch (typeCloth)
            {
                case 1:
                    sale_id = Models.headSaleSelected.head_sale_id;
                    users_id2 = Models.headSaleSelected.users_id;
                    break;
                case 2:
                    sale_id = Models.topSaleSelected.top_sale_id;
                    users_id2 = Models.topSaleSelected.users_id;
                    break;
                case 3:
                    sale_id = Models.bottomSaleSelected.bottom_sale_id;
                    users_id2 = Models.bottomSaleSelected.users_id;
                    break;
                case 4:
                    sale_id = Models.feetSaleSelected.feet_sale_id;
                    users_id2 = Models.feetSaleSelected.users_id;
                    break;
                default:
                    break;
            }

			var username	= Models.LoginCredentials.loginUsername;
            var bidding		= entryBidding.Text;
			var comments	= editorComments.Text;

			if (string.IsNullOrEmpty(editorComments.Text))
			{
				comments = "No message added.";
			}

			var values = new Dictionary<string, string>
            {
                { "sale_id", sale_id },
                { "users_id1", users_id1 },
                { "users_id2", users_id2 },
                { "username", username },
                { "typeCloth", typeCloth.ToString() },
				{ "bidding", bidding },
				{ "comments", comments }
			};

            var content = new FormUrlEncodedContent(values);
            var response = await _client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var postMethod = JsonConvert.DeserializeObject<List<saleRequestUpload>>(responseString);

			if (postMethod[0].sale_request)
			{
				await DisplayAlert("Succes", "Your request has been sent.", "OK");
				await Application.Current.MainPage.Navigation.PopAsync();
			}
			else
			{
				await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
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
