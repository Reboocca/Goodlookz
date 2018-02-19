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
    class lendUpload
    {
        public bool set_update { get; set; }
    }

    /// <summary>
    /// Values dat opgeslagen moet worden in een class.
    /// </summary>
    class clothType
    {
        private static string _clothName;
        public static string clothName
        {
            get { return _clothName; }
            set { _clothName = value; }
        }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class LendPage : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());

        /// <summary>
        /// List maken voor de 4 type clothes en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.WardrobeHead> gets_Head = new List<Models.WardrobeHead>();
        List<Models.WardrobeTop> gets_Top = new List<Models.WardrobeTop>();
        List<Models.WardrobeBottom> gets_Bottom = new List<Models.WardrobeBottom>();
        List<Models.WardrobeFeet> gets_Feet = new List<Models.WardrobeFeet>();

        public LendPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var clothName = clothType.clothName;
            lblCloth.Text = clothName;

            switch (clothName)
            {
                case "Head":
                    string Url_Head = "http://good-lookz.com/API/wardrobe/head/headDownload.php?users_id={0}";

                    string data_Head = Models.SelectedFriendsCredentials.id;

                    string urlFilled_Head = string.Format(Url_Head, data_Head);

                    var content_Head = await _client.GetStringAsync(urlFilled_Head);
                    gets_Head = JsonConvert.DeserializeObject<List<Models.WardrobeHead>>(content_Head);

                    Cloths.ItemsSource = gets_Head;
                    break;
                case "Top":
                    string Url_Top = "http://good-lookz.com/API/wardrobe/top/topDownload.php?users_id={0}&size={1}";

                    string data_Top = Models.SelectedFriendsCredentials.id;
                    string size_Top = "";

                    string urlFilled_Top = string.Format(Url_Top, data_Top, size_Top);

                    var content_Top = await _client.GetStringAsync(urlFilled_Top);
                    gets_Top = JsonConvert.DeserializeObject<List<Models.WardrobeTop>>(content_Top);

                    Cloths.ItemsSource = gets_Top;
                    break;
                case "Bottom":
                    string Url_Bottom = "http://good-lookz.com/API/wardrobe/bottom/bottomDownload.php?users_id={0}&size={1}";

                    string data_Bottom = Models.SelectedFriendsCredentials.id;
                    string size_Bottom = "";

                    string urlFilled_Bottom = string.Format(Url_Bottom, data_Bottom, size_Bottom);

                    var content_Bottom = await _client.GetStringAsync(urlFilled_Bottom);
                    gets_Bottom = JsonConvert.DeserializeObject<List<Models.WardrobeBottom>>(content_Bottom);

                    Cloths.ItemsSource = gets_Bottom;
                    break;
                case "Feet":
                    string Url_Feet = "http://good-lookz.com/API/wardrobe/feet/feetDownload.php?users_id={0}&size={1}";

                    string data_Feet = Models.SelectedFriendsCredentials.id;
                    string size_Feet = "";

                    string urlFilled_Feet = string.Format(Url_Feet, data_Feet, size_Feet);

                    var content_Feet = await _client.GetStringAsync(urlFilled_Feet);
                    gets_Feet = JsonConvert.DeserializeObject<List<Models.WardrobeFeet>>(content_Feet);

                    Cloths.ItemsSource = gets_Feet;
                    break;
                default:
                    break;
            }

            base.OnAppearing();
        }

        async void Cloths_Tapped(object sender, ItemTappedEventArgs e)
        {
            var clothName = clothType.clothName;
            var head_id = "";
            var top_id = "";
            var bottom_id = "";
            var feet_id = "";
            var users_id = Models.LoginCredentials.loginId;
            var alreadyLend = false;

            switch (clothName)
            {
                case "Head":
                    Models.WardrobeHead item_head = (Models.WardrobeHead)e.Item;
                    head_id = item_head.head_id.ToString();

                    var url_check_head = "http://www.good-lookz.com/API/lend/lendDownload.php?id={0}&head_id={1}";
                    var url_full_head = string.Format(url_check_head, users_id, head_id);

                    var content_head = await _client.GetStringAsync(url_full_head);
                    var response_head = JsonConvert.DeserializeObject<List<Models.LendList>>(content_head);

                    if (response_head.Count != 0)
                        alreadyLend = true;

                    break;
                case "Top":
                    Models.WardrobeTop item_top = (Models.WardrobeTop)e.Item;
                    top_id = item_top.top_id.ToString();

                    var url_check_top = "http://www.good-lookz.com/API/lend/lendDownload.php?id={0}&top_id={1}";
                    var url_full_top = string.Format(url_check_top, users_id, top_id);

                    var content_top = await _client.GetStringAsync(url_full_top);
                    var response_top = JsonConvert.DeserializeObject<List<Models.LendList>>(content_top);

                    if (response_top.Count != 0)
                        alreadyLend = true;

                    break;
                case "Bottom":
                    Models.WardrobeBottom item_bottom = (Models.WardrobeBottom)e.Item;
                    bottom_id = item_bottom.bottom_id.ToString();

                    var url_check_bottom = "http://www.good-lookz.com/API/lend/lendDownload.php?id={0}&bottom_id={1}";
                    var url_full_bottom = string.Format(url_check_bottom, users_id, bottom_id);

                    var content_bottom = await _client.GetStringAsync(url_full_bottom);
                    var response_bottom = JsonConvert.DeserializeObject<List<Models.LendList>>(content_bottom);

                    if (response_bottom.Count != 0)
                        alreadyLend = true;

                    break;
                case "Feet":
                    Models.WardrobeFeet item_feet = (Models.WardrobeFeet)e.Item;
                    feet_id = item_feet.feet_id.ToString();

                    var url_check_feet = "http://www.good-lookz.com/API/lend/lendDownload.php?id={0}&feet_id={1}";
                    var url_full_feet = string.Format(url_check_feet, users_id, feet_id);

                    var content_feet = await _client.GetStringAsync(url_full_feet);
                    var response_feet = JsonConvert.DeserializeObject<List<Models.LendList>>(content_feet);

                    if (response_feet.Count != 0)
                        alreadyLend = true;

                    break;
                default:
                    break;
            }

            if (alreadyLend == false)
            {
                var id = Models.SelectedFriendsCredentials.id;
                var friends_id = Models.SelectedFriendsCredentials.friends_id;


                var lendAccept = await DisplayAlert("Lend", "Ask for borrowing?", "Yes", "No");
                var Url = "http://www.good-lookz.com/API/lend/lendUpload.php";

                if (lendAccept)
                {
                    var values = new Dictionary<string, string>
                {
                    { "id" , id },
                    { "friends_id", friends_id },
                    { "users_id", users_id },
                    { "head_id", head_id },
                    { "top_id", top_id },
                    { "bottom_id", bottom_id },
                    { "feet_id", feet_id }
                };

                    var content = new FormUrlEncodedContent(values);
                    var response = await _client.PostAsync(Url, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var postMethod = JsonConvert.DeserializeObject<List<lendUpload>>(responseString);

                    await DisplayAlert("Message", "Worked!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Already on lend or pending!", "OK");
            }
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
