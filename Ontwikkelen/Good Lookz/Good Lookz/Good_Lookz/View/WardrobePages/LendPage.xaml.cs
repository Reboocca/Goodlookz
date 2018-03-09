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
        List<Models.WardrobeHeadAll>	gets_Head	= new List<Models.WardrobeHeadAll>();
        List<Models.WardrobeTopAll>		gets_Top	= new List<Models.WardrobeTopAll>();
        List<Models.WardrobeBottomAll>	gets_Bottom = new List<Models.WardrobeBottomAll>();
        List<Models.WardrobeFeetAll>	gets_Feet	= new List<Models.WardrobeFeetAll>();

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
                    string Url_Head = "http://good-lookz.com/API/wardrobeFriends/headDownload.php?users_id={0}&ownCloth=1";
                    string data_Head = Models.SelectedFriendsCredentials.id;
                    string urlFilled_Head = string.Format(Url_Head, data_Head);

                    var content_Head = await _client.GetStringAsync(urlFilled_Head);

					if (content_Head != "[]0 results")
					{
						gets_Head = JsonConvert.DeserializeObject<List<Models.WardrobeHeadAll>>(content_Head);
						Cloths.ItemsSource = gets_Head;
					}
                    break;
                case "Top":
                    string Url_Top = "http://good-lookz.com/API/wardrobeFriends/topDownload.php?users_id={0}&ownCloth=1";
                    string data_Top = Models.SelectedFriendsCredentials.id;
                    string urlFilled_Top = string.Format(Url_Top, data_Top);

                    var content_Top = await _client.GetStringAsync(urlFilled_Top);

					if (content_Top != "[]0 results")
					{
						gets_Top = JsonConvert.DeserializeObject<List<Models.WardrobeTopAll>>(content_Top);
						Cloths.ItemsSource = gets_Top;
					}
					break;
                case "Bottom":
                    string Url_Bottom = "http://good-lookz.com/API/wardrobeFriends/bottomDownload.php?users_id={0}&ownCloth=1";
                    string data_Bottom = Models.SelectedFriendsCredentials.id;
                    string urlFilled_Bottom = string.Format(Url_Bottom, data_Bottom);

                    var content_Bottom = await _client.GetStringAsync(urlFilled_Bottom);

					if (content_Bottom != "[]0 results")
					{
						gets_Bottom = JsonConvert.DeserializeObject<List<Models.WardrobeBottomAll>>(content_Bottom);
						Cloths.ItemsSource = gets_Bottom;
					}
                    break;
                case "Feet":
                    string Url_Feet = "http://good-lookz.com/API/wardrobeFriends/feetDownload.php?users_id={0}&ownCloth=1";
                    string data_Feet = Models.SelectedFriendsCredentials.id;
                    string urlFilled_Feet = string.Format(Url_Feet, data_Feet);

                    var content_Feet = await _client.GetStringAsync(urlFilled_Feet);

					if (content_Feet != "[]0 results")
					{
						gets_Feet = JsonConvert.DeserializeObject<List<Models.WardrobeFeetAll>>(content_Feet);
						Cloths.ItemsSource = gets_Feet;
					}
                    break;
                default:
                    break;
            }

            base.OnAppearing();
        }

        async void Cloths_Tapped(object sender, ItemTappedEventArgs e)
        {

			switch (clothType.clothName)
			{
				case "Head":
					Models.WardrobeHeadAll SelectedHead = (Models.WardrobeHeadAll)e.Item;

					Models.SelectedHead.head_id		= SelectedHead.head_id;
					Models.SelectedHead.users_id	= SelectedHead.users_id;
					Models.SelectedHead.picture		= SelectedHead.picture;
					Models.SelectedHead.date		= SelectedHead.date;
					selectedUsersIdHead.id			= SelectedHead.users_id;
					selectedUsersIdHead.friends_id	= SelectedHead.friends_id;
					selectedTypeLend.typeCloth		= 1;
					selectedTypeLend.previous		= "specifiek";

					await Navigation.PushAsync(new WardrobePreview(), true);
					break;

				case "Top":
					Models.WardrobeTopAll SelectedTop = (Models.WardrobeTopAll)e.Item;

					Models.SelectedTop.top_id		= SelectedTop.top_id;
					Models.SelectedTop.users_id		= SelectedTop.users_id;
					Models.SelectedTop.picture		= SelectedTop.picture;
					Models.SelectedTop.date			= SelectedTop.date;
					Models.SelectedTop.size			= SelectedTop.size;
					Models.SelectedTop.region		= SelectedTop.region;
					Models.SelectedTop.gender		= SelectedTop.gender;
					selectedUsersIdTop.id			= SelectedTop.users_id;
					selectedUsersIdTop.friends_id	= SelectedTop.friends_id;
					selectedTypeLend.typeCloth		= 2;
					selectedTypeLend.previous		= "specifiek";

					await Navigation.PushAsync(new WardrobePreview(), true);
					break;

				case "Bottom":
					Models.WardrobeBottomAll SelectedBottom = (Models.WardrobeBottomAll)e.Item;

					Models.SelectedBottom.bottom_id = SelectedBottom.bottom_id;
					Models.SelectedBottom.users_id	= SelectedBottom.users_id;
					Models.SelectedBottom.picture	= SelectedBottom.picture;
					Models.SelectedBottom.date		= SelectedBottom.date;
					Models.SelectedBottom.size		= SelectedBottom.size;
					Models.SelectedBottom.region	= SelectedBottom.region;
					Models.SelectedBottom.gender	= SelectedBottom.gender;
					selectedUsersIdTop.id			= SelectedBottom.users_id;
					selectedUsersIdTop.friends_id	= SelectedBottom.friends_id;
					selectedTypeLend.typeCloth		= 3;
					selectedTypeLend.previous		= "specifiek";

					await Navigation.PushAsync(new WardrobePreview(), true);
					break;

				case "Feet":
					Models.WardrobeFeetAll SelectedFeet = (Models.WardrobeFeetAll)e.Item;

					Models.SelectedFeet.feet_id		= SelectedFeet.feet_id;
					Models.SelectedFeet.users_id	= SelectedFeet.users_id;
					Models.SelectedFeet.picture		= SelectedFeet.picture;
					Models.SelectedFeet.date		= SelectedFeet.date;
					Models.SelectedFeet.size		= SelectedFeet.size;
					Models.SelectedFeet.region		= SelectedFeet.region;
					Models.SelectedFeet.gender		= SelectedFeet.gender;
					selectedUsersIdTop.id			= SelectedFeet.users_id;
					selectedUsersIdTop.friends_id	= SelectedFeet.friends_id;
					selectedTypeLend.typeCloth		= 4;
					selectedTypeLend.previous		= "specifiek";

					await Navigation.PushAsync(new WardrobePreview(), true);
					break;
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
