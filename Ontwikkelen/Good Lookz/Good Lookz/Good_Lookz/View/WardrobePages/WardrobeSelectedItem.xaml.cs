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
    class saleUpload
    {
        public bool sale_update { get; set; }
    }

    /// <summary>
    /// Values dat opgeslagen moet worden in een class.
    /// </summary>
    class selectedType
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
    public partial class WardrobeSelectedItem : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());

        public WardrobeSelectedItem()
        {
            InitializeComponent();
        }


		int typeCloth = selectedType.typeCloth;
		int id = 0;
		string picture = "";
		string color = "";
		string date = "";
		string size = "";
		string type = "";


		protected override void OnAppearing()
        {
            switch (typeCloth)
            {
                case 1:
                    id = Int32.Parse(Models.SelectedHead.head_id);
                    picture = Models.SelectedHead.picture;
                    color = Models.SelectedHead.color;
                    date = Models.SelectedHead.date;
					type = "head";
                    break;

                case 2:
                    id = Int32.Parse(Models.SelectedTop.top_id);
                    picture = Models.SelectedTop.picture;
                    color = Models.SelectedTop.color;
                    date = Models.SelectedTop.date;
                    size = Models.SelectedTop.size;
					type = "top";
					break;

                case 3:
                    id = Int32.Parse(Models.SelectedBottom.bottom_id);
                    picture = Models.SelectedBottom.picture;
                    color = Models.SelectedBottom.color;
                    date = Models.SelectedBottom.date;
                    size = Models.SelectedBottom.size.ToString();
					type = "bottom";
					break;

                case 4:
                    id = Int32.Parse(Models.SelectedFeet.feet_id);
                    picture = Models.SelectedFeet.picture;
                    color = Models.SelectedFeet.color;
                    date = Models.SelectedFeet.date;
                    size = Models.SelectedFeet.size.ToString();
					type = "feet";
					break;

                default:
                    break;
            }

            selectedCloth.Source = picture;
            lblClothDate.Text = "Upload date: " + date;
            lblSize.Text = size;
            lblColor.Text = color;

            base.OnAppearing();
        }

        async void SetSale_Clicked(object sender, EventArgs e)
        {
            var price = entryPrice.Text;
            var desc = editorDesc.Text;

            if (((price == null) || (price == "")) || ((desc == null) || (desc == "")))
            {
                await DisplayAlert("Error", "Please fill in the price and description!", "OK");
            }
            else
            {
                var setSaleAgreed = await DisplayAlert("Message", "Set item for sale?\n Price: " + price, "Yes", "No");

				if (setSaleAgreed)
				{
					var typeCloth = selectedType.typeCloth;
					var users_id = Models.LoginCredentials.loginId;
					var username = Models.LoginCredentials.loginUsername;

					try {
						string webadres = "http://good-lookz.com/API/sale/saleUpload.php?user_id=" + users_id + "&item_id=" + id + "&type=" + type + "&size=" + size + "&price=" + price + "&desc=" + desc;
						HttpClient connect = new HttpClient();
						HttpResponseMessage uploadToSale = await connect.GetAsync(webadres);
						uploadToSale.EnsureSuccessStatusCode();

						string result = await uploadToSale.Content.ReadAsStringAsync();

						if (result == "Item has been set for sale.") {
							//Item is toegevoegd aan de "sales" tabel en bestaat niet twee keer
							//Ga door met de oude code:
							switch (typeCloth)
							{
								case 1:
									var head_id = Models.SelectedHead.head_id.ToString();
									var url_head = "http://www.good-lookz.com/API/sale/type/head/headUpload.php";
									var values_head = new Dictionary<string, string>
									{
										{ "users_id", users_id },
										{ "head_id", head_id },
										{ "username", username },
										{ "price", price },
										{ "desc", desc }
									};

									var content_head = new FormUrlEncodedContent(values_head);
									var response_head = await _client.PostAsync(url_head, content_head);
									var responseString_head = await response_head.Content.ReadAsStringAsync();
									var postMethod_head = JsonConvert.DeserializeObject<List<saleUpload>>(responseString_head);

									await DisplayAlert("Message", "Set for sale: " + postMethod_head[0].sale_update.ToString(), "OK");
									break;

								case 2:
									var top_id = Models.SelectedTop.top_id.ToString();
									var size_top = Models.SelectedTop.size;
									var url_top = "http://www.good-lookz.com/API/sale/type/top/topUpload.php";

									var values_top = new Dictionary<string, string>
									{
										{ "users_id", users_id },
										{ "top_id", top_id },
										{ "username", username },
										{ "size", size_top },
										{ "price", price },
										{ "desc", desc }
									};

									var content_top = new FormUrlEncodedContent(values_top);
									var response_top = await _client.PostAsync(url_top, content_top);
									var responseString_top = await response_top.Content.ReadAsStringAsync();
									var postMethod_top = JsonConvert.DeserializeObject<List<saleUpload>>(responseString_top);

									await DisplayAlert("Message", "Set for sale: " + postMethod_top[0].sale_update.ToString(), "OK");
									break;

								case 3:
									var bottom_id = Models.SelectedBottom.bottom_id.ToString();
									var size_bottom = Models.SelectedBottom.size.ToString();
									var url_bottom = "http://www.good-lookz.com/API/sale/type/bottom/bottomUpload.php";

									var values_bottom = new Dictionary<string, string>
									{
										{ "users_id", users_id },
										{ "bottom_id", bottom_id },
										{ "username", username },
										{ "size", size_bottom },
										{ "price", price },
										{ "desc", desc }
									};

									var content_bottom = new FormUrlEncodedContent(values_bottom);
									var response_bottom = await _client.PostAsync(url_bottom, content_bottom);
									var responseString_bottom = await response_bottom.Content.ReadAsStringAsync();
									var postMethod_bottom = JsonConvert.DeserializeObject<List<saleUpload>>(responseString_bottom);

									await DisplayAlert("Message", "Set for sale: " + postMethod_bottom[0].sale_update.ToString(), "OK");
									break;

								case 4:
									var feet_id = Models.SelectedFeet.feet_id.ToString();
									var size_feet = Models.SelectedFeet.size.ToString();
									var url_feet = "http://www.good-lookz.com/API/sale/type/feet/feetUpload.php";

									var values_feet = new Dictionary<string, string>
									{
										{ "users_id", users_id },
										{ "feet_id",feet_id  },
										{ "username", username },
										{ "size", size_feet },
										{ "price", price },
										{ "desc", desc }
									};

									var content_feet = new FormUrlEncodedContent(values_feet);
									var response_feet = await _client.PostAsync(url_feet, content_feet);
									var responseString_feet = await response_feet.Content.ReadAsStringAsync();
									var postMethod_feet = JsonConvert.DeserializeObject<List<saleUpload>>(responseString_feet);

									await DisplayAlert("Message", "Set for sale: " + postMethod_feet[0].sale_update.ToString(), "OK");
									break;

								default:
									break;
							}
							Application.Current.MainPage = new NavigationPage(new MenuPage());
						}
						else if (result == "Can't set item on sale.") {
							await DisplayAlert("Error", "Unable to set this item for sale at the moment, please try again later", "OK");
						}
						else if (result == "Item already on sale.") {
							await DisplayAlert("Already on sale", "This item has already been set for sale.", "OK");
						}
						else if (result == "Item not found.") {
							await DisplayAlert("Error", "Item not found", "OK");
						}
					}
					catch (Exception) {
						await DisplayAlert("Error", "Unable to connect with our servers.", "OK");
					}
				}
            }
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            var typeCloth = selectedType.typeCloth;

            var head_id = "";
            var top_id = "";
            var bottom_id = "";
            var feet_id = "";

            switch (typeCloth)
            {
                case 1:
                    head_id = Models.SelectedHead.head_id.ToString();
                    var url_Head = "http://www.good-lookz.com/API/wardrobe/head/img/delete.php";

                    var values_Head = new Dictionary<string, string>
                    {
                        { "head_id", head_id }
                    };

                    var content_Head = new FormUrlEncodedContent(values_Head);
                    var response_Head = await _client.PostAsync(url_Head, content_Head);
                    var responseString_Head = await response_Head.Content.ReadAsStringAsync();
                    var postMethod_Head = JsonConvert.DeserializeObject<List<Models.headDelete>>(responseString_Head);

                    await DisplayAlert("Message", "Deleted successfully!", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();

                    break;
                case 2:
                    top_id = Models.SelectedTop.top_id.ToString();
                    var url_Top = "http://www.good-lookz.com/API/wardrobe/top/img/delete.php";

                    var values_Top = new Dictionary<string, string>
                    {
                        { "top_id", top_id }
                    };

                    var content_Top = new FormUrlEncodedContent(values_Top);
                    var response_Top = await _client.PostAsync(url_Top, content_Top);
                    var responseString_Top = await response_Top.Content.ReadAsStringAsync();
                    var postMethod_Top = JsonConvert.DeserializeObject<List<Models.topDelete>>(responseString_Top);

                    await DisplayAlert("Message", "Deleted successfully!", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();

                    break;
                case 3:
                    bottom_id = Models.SelectedBottom.bottom_id.ToString();
                    var url_Bottom = "http://www.good-lookz.com/API/wardrobe/bottom/img/delete.php";

                    var values_Bottom = new Dictionary<string, string>
                    {
                        { "bottom_id", bottom_id }
                    };

                    var content_Bottom = new FormUrlEncodedContent(values_Bottom);
                    var response_Bottom = await _client.PostAsync(url_Bottom, content_Bottom);
                    var responseString_Bottom = await response_Bottom.Content.ReadAsStringAsync();
                    var postMethod_Bottom = JsonConvert.DeserializeObject<List<Models.bottomDelete>>(responseString_Bottom);

                    await DisplayAlert("Message", "Deleted successfully!", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();

                    break;
                case 4:
                    feet_id = Models.SelectedFeet.feet_id.ToString();
                    var url_Feet = "http://www.good-lookz.com/API/wardrobe/feet/img/delete.php";

                    var values_Feet = new Dictionary<string, string>
                    {
                        { "feet_id", feet_id }
                    };

                    var content_Feet = new FormUrlEncodedContent(values_Feet);
                    var response_Feet = await _client.PostAsync(url_Feet, content_Feet);
                    var responseString_Feet = await response_Feet.Content.ReadAsStringAsync();
                    var postMethod_Feet = JsonConvert.DeserializeObject<List<Models.feetDelete>>(responseString_Feet);

                    await DisplayAlert("Message", "Deleted successfully!", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();

                    break;
                default:
                    break;
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
