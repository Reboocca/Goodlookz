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
    class selectedTypeLend
    {
        private static int _typeCloth;

        public static int typeCloth
        {
            get { return _typeCloth; }
            set { _typeCloth = value; }
        }
    }

    class selectedUsersIdHead
    {
        private static int _id;
        private static int _friends_id;

        public static int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static int friends_id
        {
            get { return _friends_id; }
            set { _friends_id = value; }
        }
    }

    class selectedUsersIdTop
    {
        private static int _id;
        private static int _friends_id;

        public static int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static int friends_id
        {
            get { return _friends_id; }
            set { _friends_id = value; }
        }
    }

    class selectedUsersIdBottom
    {
        private static int _id;
        private static int _friends_id;

        public static int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static int friends_id
        {
            get { return _friends_id; }
            set { _friends_id = value; }
        }
    }

    class selectedUsersIdFeet
    {
        private static int _id;
        private static int _friends_id;

        public static int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static int friends_id
        {
            get { return _friends_id; }
            set { _friends_id = value; }
        }
    }

    public partial class WardrobePreview : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());
        int id = 0;
        string picture = "";
        string date = "";
		string size = "";
		string region = "";
		string gender = "";

		public WardrobePreview()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
		{
			//Geef een min en max datum aan de date picker
			dpDate.MinimumDate = DateTime.Now.AddHours(24);
			dpDate.MaximumDate = DateTime.Now.AddYears(5);

			var typeCloth = selectedTypeLend.typeCloth;


            switch (typeCloth)
            {
                case 1:
                    id					= Models.SelectedHead.head_id;
                    picture				= Models.SelectedHead.picture;
                    date				= Models.SelectedHead.date;
					stckInfo.IsVisible	= false;
					break;

                case 2:
                    id		= Models.SelectedTop.top_id;
                    picture = Models.SelectedTop.picture;
                    date	= Models.SelectedTop.date;
                    size	= Models.SelectedTop.size;
					region	= Models.SelectedTop.region;
					gender	= Models.SelectedTop.gender;
                    break;

                case 3:
                    id		= Models.SelectedBottom.bottom_id;
                    picture = Models.SelectedBottom.picture;
                    date	= Models.SelectedBottom.date;
                    size	= Models.SelectedBottom.size;
					region	= Models.SelectedBottom.region;
					gender	= Models.SelectedBottom.gender;
					break;

                case 4:
                    id		= Models.SelectedFeet.feet_id;
                    picture = Models.SelectedFeet.picture;
                    date	= Models.SelectedFeet.date;
                    size	= Models.SelectedFeet.size;
					region	= Models.SelectedFeet.region;
					gender	= Models.SelectedFeet.gender;
					break;

                default:
                    break;
            }

            selectedCloth.Source = picture;

			//Vul de labels
			lblSize.Text	= size;
			lbGender.Text	= gender;
			lbRegion.Text	= region;

            base.OnAppearing();
        }

        async void AskForLend_Clicked(object sender, EventArgs e)
        {
            var typeCloth = selectedTypeLend.typeCloth;
            var users_id = Models.LoginCredentials.loginId;
            var alreadyLend = false;
            var url_check = "";
            var head_id = "";
            var top_id = "";
            var bottom_id = "";
            var feet_id = "";
            var id = "";
            var friends_id = "";
            var url_full = "";

            switch (typeCloth)
            {
                case 1:
                    url_check = "http://www.good-lookz.com/API/lend/lendDownload.php?id={0}&head_id={1}";
                    head_id = Models.SelectedHead.head_id.ToString();
                    id = selectedUsersIdHead.id.ToString(); ;
                    friends_id = selectedUsersIdHead.friends_id.ToString();
                    url_full = string.Format(url_check, users_id, head_id);
                    break;
                case 2:
                    url_check = "http://www.good-lookz.com/API/lend/lendDownload.php?id={0}&top_id={1}";
                    top_id = Models.SelectedTop.top_id.ToString();
                    id = selectedUsersIdTop.id.ToString(); ;
                    friends_id = selectedUsersIdTop.friends_id.ToString();
                    url_full = string.Format(url_check, users_id, top_id);
                    break;
                case 3:
                    url_check = "http://www.good-lookz.com/API/lend/lendDownload.php?id={0}&bottom_id={1}";
                    bottom_id = Models.SelectedBottom.bottom_id.ToString();
                    id = selectedUsersIdBottom.id.ToString(); ;
                    friends_id = selectedUsersIdBottom.friends_id.ToString();
                    url_full = string.Format(url_check, users_id, bottom_id);
                    break;
                case 4:
                    url_check = "http://www.good-lookz.com/API/lend/lendDownload.php?id={0}&feet_id={1}";
                    feet_id = Models.SelectedFeet.feet_id.ToString();
                    id = selectedUsersIdFeet.id.ToString(); ;
                    friends_id = selectedUsersIdFeet.friends_id.ToString();
                    url_full = string.Format(url_check, users_id, feet_id);
                    break;
                default:
                    break;
            }

            if (users_id == id)
            {
                await DisplayAlert("Error", "Cant lend own clothes!", "OK");
                return;
            }

            var content = await _client.GetStringAsync(url_full);
            var response = JsonConvert.DeserializeObject<List<Models.LendList>>(content);

            if (response.Count != 0)
                alreadyLend = true;

            if (alreadyLend == false)
            {
                //var lendAccept = await DisplayAlert("Lend", "Ask for borrowing?", "Yes", "No");
                //var Url = "http://www.good-lookz.com/API/lend/lendUpload.php";

                //if (lendAccept)
                //{

                //var values = new Dictionary<string, string>
                //{
                //    { "id" , id },
                //    { "friends_id", friends_id },
                //    { "users_id", users_id },
                //    { "head_id", head_id },
                //    { "top_id", top_id },
                //    { "bottom_id", bottom_id },
                //    { "feet_id", feet_id }
                //};

                //    var content_upload = new FormUrlEncodedContent(values);
                //    var response_upload = await _client.PostAsync(Url, content_upload);
                //    var responseString = await response_upload.Content.ReadAsStringAsync();
                //    var postMethod = JsonConvert.DeserializeObject<List<lendUpload>>(responseString);

                //    await DisplayAlert("Message", "Worked!", "OK");
                //}
            }
            else
            {
                await DisplayAlert("Error", "Already on lend or pending!", "OK");
            }
        }

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het pagina.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            Content = null;
            GC.Collect();
        }
    }
}
