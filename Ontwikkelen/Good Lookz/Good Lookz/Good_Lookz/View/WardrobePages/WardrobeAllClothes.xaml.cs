using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.WardrobePages
{
    class noDataWardrobeFriends
    {
        public bool wardrobeFriends { get; set; }
    }

    public partial class WardrobeAllClothes : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.WardrobeHeadAll> _obsrv_head;
        private ObservableCollection<Models.WardrobeTopAll> _obsrv_top;
        private ObservableCollection<Models.WardrobeBottomAll> _obsrv_bottom;
        private ObservableCollection<Models.WardrobeFeetAll> _obsrv_feet;

        /// <summary>
        /// List maken voor de 4 type clothes en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.WardrobeHeadAll> gets_Head = new List<Models.WardrobeHeadAll>();
        List<Models.WardrobeTopAll> gets_Top = new List<Models.WardrobeTopAll>();
        List<Models.WardrobeBottomAll> gets_Bottom = new List<Models.WardrobeBottomAll>();
        List<Models.WardrobeFeetAll> gets_Feet = new List<Models.WardrobeFeetAll>();

		//Vrienden check
		private const string url_friends = "http://www.good-lookz.com/API/friends/friendsCheck.php?users_id={0}";
		HttpClient check_friends = new HttpClient(new NativeMessageHandler());
		private ObservableCollection<Models.FriendsCredentials> _gets;
		List<Models.FriendsCredentials> response = new List<Models.FriendsCredentials>();

		//User size
		List<Models.Sizes.AllSizes> lstSizes = new List<Models.Sizes.AllSizes>();
		Models.UserSizes uSize = new Models.UserSizes();

		public WardrobeAllClothes()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
			getUserSize();
			string data = Models.LoginCredentials.loginId;

			string URL = string.Format(url_friends, data);

			var content = await check_friends.GetStringAsync(URL);
			response = JsonConvert.DeserializeObject<List<Models.FriendsCredentials>>(content);

			if (response[0].id != null)
			{
				var ownClothSwitch = ownCloths.IsToggled;
				var ownCloth = 0;

				if (ownClothSwitch)
					ownCloth = 1;

				loadingHead.IsRunning = true;
				loadingTop.IsRunning = true;
				loadingBottom.IsRunning = true;
				loadingFeet.IsRunning = true;

				string Url_Head = "http://www.good-lookz.com/API/wardrobeFriends/headDownload.php?users_id={0}&ownCloth={1}";

				string data_Head = Models.LoginCredentials.loginId;

				string urlFilled_Head = string.Format(Url_Head, data_Head, ownCloth);

				var content_Head = await _client.GetStringAsync(urlFilled_Head);
				if(content_Head != "[]0 results")
				{
					gets_Head = JsonConvert.DeserializeObject<List<Models.WardrobeHeadAll>>(content_Head);

					_obsrv_head = new ObservableCollection<Models.WardrobeHeadAll>(gets_Head);

					loadingHead.IsRunning = false;
					loadingHead.IsVisible = false;

					try
					{
						// List vullen met de opgehaalde data
						CarouselViewHead.ItemsSource = _obsrv_head;
					}
					catch
					{

					}
				}
				else
				{
					loadingHead.IsVisible = false;
				}
				

				// ---------------------------

				string Url_Top = "http://www.good-lookz.com/API/wardrobeFriends/topDownload.php?users_id={0}&ownCloth={1}&size=" + uSize.topnr;

				string data_Top = Models.LoginCredentials.loginId;
				//string size_Top = "";

				string urlFilled_Top = string.Format(Url_Top, data_Top, ownCloth);

				var content_Top = await _client.GetStringAsync(urlFilled_Top);

				if(content_Top != "[]0 results")
				{
					gets_Top = JsonConvert.DeserializeObject<List<Models.WardrobeTopAll>>(content_Top);

					_obsrv_top = new ObservableCollection<Models.WardrobeTopAll>(gets_Top);

					loadingTop.IsRunning = false;
					loadingTop.IsVisible = false;

					try
					{
						// List vullen met de opgehaalde data
						CarouselViewTop.ItemsSource = _obsrv_top;
					}
					catch
					{

					}
				}
				else
				{
					loadingTop.IsVisible = false;
				}


				// ---------------------------

				string Url_Bottom = "http://www.good-lookz.com/API/wardrobeFriends/bottomDownload.php?users_id={0}&ownCloth={1}&size=" + uSize.botnr;

				string data_Bottom = Models.LoginCredentials.loginId;
				//string size_Bottom = "";

				string urlFilled_Bottom = string.Format(Url_Bottom, data_Bottom, ownCloth);

				var content_Bottom = await _client.GetStringAsync(urlFilled_Bottom);

				if(content_Bottom != "[]0 results")
				{
					gets_Bottom = JsonConvert.DeserializeObject<List<Models.WardrobeBottomAll>>(content_Bottom);

					_obsrv_bottom = new ObservableCollection<Models.WardrobeBottomAll>(gets_Bottom);

					loadingBottom.IsRunning = false;
					loadingBottom.IsVisible = false;

					try
					{
						// List vullen met de opgehaalde data
						CarouselViewBottom.ItemsSource = _obsrv_bottom;
					}
					catch
					{

					}
				}
				else
				{
					loadingBottom.IsVisible = false;
				}



				// ---------------------------

				string Url_Feet = "http://www.good-lookz.com/API/wardrobeFriends/feetDownload.php?users_id={0}&ownCloth={1}&size=" + uSize.feetnr;

				string data_Feet = Models.LoginCredentials.loginId;
				//string size_Feet = "";

				string urlFilled_Feet = string.Format(Url_Feet, data_Feet, ownCloth);

				var content_Feet = await _client.GetStringAsync(urlFilled_Feet);

				if(content_Feet != "[]0 results")
				{
					gets_Feet = JsonConvert.DeserializeObject<List<Models.WardrobeFeetAll>>(content_Feet);

					_obsrv_feet = new ObservableCollection<Models.WardrobeFeetAll>(gets_Feet);

					loadingFeet.IsRunning = false;
					loadingFeet.IsVisible = false;

					try
					{
						// List vullen met de opgehaalde data
						CarouselViewFeet.ItemsSource = _obsrv_feet;
					}
					catch
					{

					}
				}
				else
				{
					loadingFeet.IsVisible = false;
				}

				// ---------------------------
			}
			else
			{
				//Gebruiker heeft geen vrienden, navigeer terug naar vorige pagina
				await DisplayAlert("Message", "No friends found, add some friends first so you can look into their wardrobe.", "OK");
				await Application.Current.MainPage.Navigation.PopAsync();
			}

            base.OnAppearing();
        }

        async void ownClothes_Toggled(object sender, ToggledEventArgs e)
        {
            var ownClothSwitch = ownCloths.IsToggled;

            if (ownClothSwitch)
            {
               // gets_Head.Clear();
                //gets_Top.Clear();
                //gets_Bottom.Clear();
                //gets_Feet.Clear();

				//if(_obsrv_head.)
    //            _obsrv_head.Clear();
    //            _obsrv_top.Clear();
    //            _obsrv_bottom.Clear();
    //            _obsrv_feet.Clear();

                loadingHead.IsRunning = true;
                loadingTop.IsRunning = true;
                loadingBottom.IsRunning = true;
                loadingFeet.IsRunning = true;

                string Url_Head = "http://www.good-lookz.com/API/wardrobeFriends/headDownload.php?users_id={0}&ownCloth=1";

                string data_Head = Models.LoginCredentials.loginId;

                string urlFilled_Head = string.Format(Url_Head, data_Head);

                var content_Head = await _client.GetStringAsync(urlFilled_Head);

                if (content_Head != "[]0 results")
                    gets_Head = JsonConvert.DeserializeObject<List<Models.WardrobeHeadAll>>(content_Head);
                    _obsrv_head = new ObservableCollection<Models.WardrobeHeadAll>(gets_Head);

                loadingHead.IsRunning = false;
                loadingHead.IsVisible = false;

                try
                {
                    // List vullen met de opgehaalde data
                    CarouselViewHead.ItemsSource = _obsrv_head;
                }
                catch
                {

                }

                // ---------------------------

                string Url_Top = "http://www.good-lookz.com/API/wardrobeFriends/topDownload.php?users_id={0}&ownCloth=1&size=";

                string data_Top = Models.LoginCredentials.loginId;
                string size_Top = "";

                string urlFilled_Top = string.Format(Url_Top, data_Top, size_Top);

                var content_Top = await _client.GetStringAsync(urlFilled_Top);

                if (content_Top != "[]0 results")
                    gets_Top = JsonConvert.DeserializeObject<List<Models.WardrobeTopAll>>(content_Top);
                    _obsrv_top = new ObservableCollection<Models.WardrobeTopAll>(gets_Top);

                loadingTop.IsRunning = false;
                loadingTop.IsVisible = false;

                try
                {
                    // List vullen met de opgehaalde data
                    CarouselViewTop.ItemsSource = _obsrv_top;
                }
                catch
                {

                }

                // ---------------------------

                string Url_Bottom = "http://www.good-lookz.com/API/wardrobeFriends/bottomDownload.php?users_id={0}&ownCloth=1&size=";

                string data_Bottom = Models.LoginCredentials.loginId;
                string size_Bottom = "";

                string urlFilled_Bottom = string.Format(Url_Bottom, data_Bottom, size_Bottom);

                var content_Bottom = await _client.GetStringAsync(urlFilled_Bottom);

                if (content_Bottom != "[]0 results")
                    gets_Bottom = JsonConvert.DeserializeObject<List<Models.WardrobeBottomAll>>(content_Bottom);
                    _obsrv_bottom = new ObservableCollection<Models.WardrobeBottomAll>(gets_Bottom);

                loadingBottom.IsRunning = false;
                loadingBottom.IsVisible = false;

                try
                {
                    // List vullen met de opgehaalde data
                    CarouselViewBottom.ItemsSource = _obsrv_bottom;
                }
                catch
                {

                }

                // ---------------------------

                string Url_Feet = "http://www.good-lookz.com/API/wardrobeFriends/feetDownload.php?users_id={0}&ownCloth=1&size=";

                string data_Feet = Models.LoginCredentials.loginId;
                string size_Feet = "";

                string urlFilled_Feet = string.Format(Url_Feet, data_Feet, size_Feet);

                var content_Feet = await _client.GetStringAsync(urlFilled_Feet);

                if (content_Feet != "[]0 results")
                    gets_Feet = JsonConvert.DeserializeObject<List<Models.WardrobeFeetAll>>(content_Feet);
                    _obsrv_feet = new ObservableCollection<Models.WardrobeFeetAll>(gets_Feet);

                loadingFeet.IsRunning = false;
                loadingFeet.IsVisible = false;

                try
                {
                    // List vullen met de opgehaalde data
                    CarouselViewFeet.ItemsSource = _obsrv_feet;
                }
                catch
                {

                }

                // ---------------------------
            }
            else
            {
                var userId = short.Parse(Models.LoginCredentials.loginId);

                foreach (var item in _obsrv_head.ToList())
                {
                    if (item.users_id == userId)
                    {
                        _obsrv_head.Remove(item);
                    }
                }

                foreach (var item in _obsrv_top.ToList())
                {
                    if (item.users_id == userId)
                    {
                        _obsrv_top.Remove(item);
                    }
                }

                foreach (var item in _obsrv_bottom.ToList())
                {
                    if (item.users_id == userId)
                    {
                        _obsrv_bottom.Remove(item);
                    }
                }

                foreach (var item in _obsrv_feet.ToList())
                {
                    if (item.users_id == userId)
                    {
                        _obsrv_feet.Remove(item);
                    }
                }
            }

            //await DisplayAlert("Message", "Toggled", "OK");
        }

        void CarouselViewHead_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Models.WardrobeHeadAll SelectedHead = (Models.WardrobeHeadAll)e.SelectedItem;

                var _head_id = SelectedHead.head_id;
                var _users_id = SelectedHead.users_id;
                var _picture = SelectedHead.picture;
                var _date = SelectedHead.date;
                var _id = SelectedHead.users_id;
                var _friends_id = SelectedHead.friends_id;

                Models.SelectedHead.head_id = _head_id;
                Models.SelectedHead.users_id = _users_id;
                Models.SelectedHead.picture = _picture;
                Models.SelectedHead.date = _date;
                selectedUsersIdHead.id = _id;
                selectedUsersIdHead.friends_id = _friends_id;
            }
            catch
            {

            }
        }
        void CarouselViewTop_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Models.WardrobeTopAll SelectedTop = (Models.WardrobeTopAll)e.SelectedItem;

                var _top_id = SelectedTop.top_id;
                var _users_id = SelectedTop.users_id;
                var _picture = SelectedTop.picture;
                var _date = SelectedTop.date;
                var _size = SelectedTop.size;
                var _id = SelectedTop.users_id;
                var _friends_id = SelectedTop.friends_id;

                Models.SelectedTop.top_id = _top_id;
                Models.SelectedTop.users_id = _users_id;
                Models.SelectedTop.picture = _picture;
                Models.SelectedTop.date = _date;
                Models.SelectedTop.size = _size;
                selectedUsersIdTop.id = _id;
                selectedUsersIdTop.friends_id = _friends_id;
            }
            catch
            {

            }
        }
        void CarouselViewBottom_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Models.WardrobeBottomAll SelectedBottom = (Models.WardrobeBottomAll)e.SelectedItem;

                var _bottom_id = SelectedBottom.bottom_id;
                var _users_id = SelectedBottom.users_id;
                var _picture = SelectedBottom.picture;
                var _date = SelectedBottom.date;
                var _size = SelectedBottom.size;
                var _id = SelectedBottom.users_id;
                var _friends_id = SelectedBottom.friends_id;

                Models.SelectedBottom.bottom_id = _bottom_id;
                Models.SelectedBottom.users_id = _users_id;
                Models.SelectedBottom.picture = _picture;
                Models.SelectedBottom.date = _date;
                Models.SelectedBottom.size = _size;
                selectedUsersIdBottom.id = _id;
                selectedUsersIdBottom.friends_id = _friends_id;
            }
            catch
            {

            }
        }
        void CarouselViewFeet_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Models.WardrobeFeetAll SelectedFeet = (Models.WardrobeFeetAll)e.SelectedItem;

                var _feet_id = SelectedFeet.feet_id;
                var _users_id = SelectedFeet.users_id;
                var _picture = SelectedFeet.picture;
                var _date = SelectedFeet.date;
                var _size = SelectedFeet.size;
                var _id = SelectedFeet.users_id;
                var _friends_id = SelectedFeet.friends_id;

                Models.SelectedFeet.feet_id = _feet_id;
                Models.SelectedFeet.users_id = _users_id;
                Models.SelectedFeet.picture = _picture;
                Models.SelectedFeet.date = _date;
                Models.SelectedFeet.size = _size;
                selectedUsersIdFeet.id = _id;
                selectedUsersIdFeet.friends_id = _friends_id;
            }
            catch
            {

            }
        }

		private async void getUserSize()
		{
			string users_id = Models.LoginCredentials.loginId;
			string url = "http://good-lookz.com/API/account/getSizes.php?users_id=" + users_id;

			HttpClient get = new HttpClient();
			HttpResponseMessage respons = await get.GetAsync(url);

			if (respons.IsSuccessStatusCode)
			{
				string responsecontent = await respons.Content.ReadAsStringAsync();
				var myobjList = JsonConvert.DeserializeObject<List<Models.UserSizes>>(responsecontent);
				var myObj = myobjList[0];

				uSize.region = myObj.region;
				uSize.gender = myObj.gender;
				uSize.topnr = myObj.topnr;
				uSize.botnr = myObj.botnr;
				uSize.feetnr = myObj.feetnr;
			}
		}


			async void SetSave_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new WardrobeSaveSet(), true);
        }

        async void headTapped(object sender, EventArgs e)
        {
            var _typeCloth = 1;
            selectedTypeLend.typeCloth = _typeCloth;

            await Navigation.PushAsync(new WardrobePreview(), true);
        }

        async void topTapped(object sender, EventArgs e)
        {
            var _typeCloth = 2;
            selectedTypeLend.typeCloth = _typeCloth;

            await Navigation.PushAsync(new WardrobePreview(), true);
        }

        async void bottomTapped(object sender, EventArgs e)
        {
            var _typeCloth = 3;
            selectedTypeLend.typeCloth = _typeCloth;

            await Navigation.PushAsync(new WardrobePreview(), true);
        }

        async void feetTapped(object sender, EventArgs e)
        {
            var _typeCloth = 4;
            selectedTypeLend.typeCloth = _typeCloth;

            await Navigation.PushAsync(new WardrobePreview(), true);
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
