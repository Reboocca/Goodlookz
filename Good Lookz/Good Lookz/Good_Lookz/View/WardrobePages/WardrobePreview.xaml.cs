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
    #region classes
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
    #endregion

    public partial class WardrobePreview : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());

        int id          = 0;
        string picture	= "";
        string date		= "";
		string size		= "";
		string region	= "";
		string gender	= "";

		public WardrobePreview()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
		{
			//Geef een min en max datum aan de date picker
			dpDate.MinimumDate = DateTime.Now;
			dpDate.MaximumDate = DateTime.Now.AddMonths(6);

            //Sla gegevens op in strings
			var typeCloth = selectedTypeLend.typeCloth;
            switch (typeCloth)
            {
                case 1:
                    id					= Int32.Parse(Models.SelectedHead.head_id);
                    picture				= Models.SelectedHead.picture;
                    date				= Models.SelectedHead.date;
					stckInfo.IsVisible	= false;
					break;

                case 2:
                    id		= Int32.Parse(Models.SelectedTop.top_id);
                    picture = Models.SelectedTop.picture;
                    date	= Models.SelectedTop.date;
                    size	= Models.SelectedTop.size;
					region	= Models.SelectedTop.region;
					gender	= Models.SelectedTop.gender;
                    break;

                case 3:
                    id		= Int32.Parse(Models.SelectedBottom.bottom_id);
                    picture = Models.SelectedBottom.picture;
                    date	= Models.SelectedBottom.date;
                    size	= Models.SelectedBottom.size;
					region	= Models.SelectedBottom.region;
					gender	= Models.SelectedBottom.gender;
					break;

                case 4:
                    id		= Int32.Parse(Models.SelectedFeet.feet_id);
                    picture = Models.SelectedFeet.picture;
                    date	= Models.SelectedFeet.date;
                    size	= Models.SelectedFeet.size;
					region	= Models.SelectedFeet.region;
					gender	= Models.SelectedFeet.gender;
					break;

                default:
                    break;
            }

            //Vul de image
            selectedCloth.Source = picture;

			//Vul de labels
			lblSize.Text	= size;
			lbGender.Text	= gender;
			lbRegion.Text	= region;

            base.OnAppearing();
        }

        async void AskForLend_Clicked(object sender, EventArgs e)
        {
			if (string.IsNullOrWhiteSpace(enDays.Text))
			{
				await DisplayAlert("Error", "Make sure to give us a number of days you want to borrow the item for.", "OK");
			}
			else
			{
				var typeCloth	= selectedTypeLend.typeCloth;
				string type		= null;
				string owner_id = null;
				string item_id	= null;

				switch (typeCloth)
				{
					case 1:
						type		= "head";
						owner_id	= Models.SelectedHead.users_id.ToString();
						item_id		= Models.SelectedHead.head_id;
						break;
					case 2:
						type		= "top";
						owner_id	= Models.SelectedTop.users_id.ToString();
						item_id		= Models.SelectedTop.top_id;
						break;
					case 3:
						type		= "bottom";
						owner_id	= Models.SelectedBottom.users_id.ToString();
						item_id		= Models.SelectedBottom.bottom_id;
						break;
					case 4:
						type		= "feet";
						owner_id	= Models.SelectedFeet.users_id.ToString();
						item_id		= Models.SelectedFeet.feet_id;
						break;
				}

				string message = editorComments.Text;

				if (string.IsNullOrEmpty(message))
				{
					message = "No message added.";
				}

				string webadres		= "http://good-lookz.com/API/lend/lendUpload.php?";
				string parameters	= "users_id=" + Models.LoginCredentials.loginId + "&owner_id=" + owner_id + "&username=" + Models.LoginCredentials.loginUsername + "&type=" + type + "&item_id=" + item_id + "&date=" + dpDate.Date.ToString("yyyy-MM-dd") + "&days=" + enDays.Text + "&message=" + message;

				HttpClient connect          = new HttpClient();
				HttpResponseMessage insert  = await connect.GetAsync(webadres + parameters);
				insert.EnsureSuccessStatusCode();

				string result = await insert.Content.ReadAsStringAsync();

				if (result == "Success")
				{
					await DisplayAlert("Success", "Your lend request has been sent!", "OK");
					await this.Navigation.PopAsync();
					
				}
				else if (result == "Failed")
				{
					await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
				}
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
