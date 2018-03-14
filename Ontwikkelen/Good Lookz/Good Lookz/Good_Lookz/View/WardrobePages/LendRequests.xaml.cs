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
    /// <summary>
    /// JSON response word opgevangen en in een list met variables gestopt.
    /// </summary>
    class lendAccept
    {
        public bool lend_accepted { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class LendRequests : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.LendList> _gets;

        /// <summary>
        /// List maken en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.LendList> response = new List<Models.LendList>();

        public LendRequests()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                var id = Models.LoginCredentials.loginId;

                var Url = "http://www.good-lookz.com/API/lend/lendDownload.php?users_id={0}&active=0";
                var Url_Full = string.Format(Url, id);

                var content = await _client.GetStringAsync(Url_Full);
                response = JsonConvert.DeserializeObject<List<Models.LendList>>(content);

                _gets = new ObservableCollection<Models.LendList>(response);

                lendRequests.ItemsSource = _gets;
            }
            catch
            {
                lblRequests.Text = "No requests";
            }
        }

        async void Lend_Tapped(object sender, ItemTappedEventArgs e)
        {
			Models.LendList items = (Models.LendList)e.Item;

			Models.SelectedLend.lend_id			= items.lend_id;
			Models.SelectedLend.borrow_users_id = items.borrow_users_id;
			Models.SelectedLend.owner_users_id	= items.owner_users_id;
			Models.SelectedLend.type			= items.type;
			Models.SelectedLend.item_id			= items.item_id;
			Models.SelectedLend.date			= items.date;
			Models.SelectedLend.days			= items.days;
			Models.SelectedLend.username		= items.username;
			Models.SelectedLend.lending			= items.lending;
			Models.SelectedLend.name			= items.name;
			Models.SelectedLend.picture			= items.picture;

			await Navigation.PushAsync(new SelectedLend(), true);
		}

        async void Delete_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var lend_id = item.CommandParameter.ToString();

			var requestResponse = await DisplayAlert("Warning", "Do you really want to delete the lend request?", "Yes", "No");
			if (requestResponse)
			{
				string webadres = "http://good-lookz.com/API/lend/lendAccept.php?";
				string parameters = "lend_id=" + lend_id + "&accepted=false";

				HttpClient connect = new HttpClient();
				HttpResponseMessage insert = await connect.GetAsync(webadres + parameters);
				insert.EnsureSuccessStatusCode();

				string result = await insert.Content.ReadAsStringAsync();

				if (result == "Success")
				{
					await DisplayAlert("Success", "Lend request has been deleted.", "OK");

					//Navigeer naar vorige pagina
					await Navigation.PushAsync(new LendRequests(), true);
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
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            response.Clear();
            GC.Collect();
        }
    }
}
