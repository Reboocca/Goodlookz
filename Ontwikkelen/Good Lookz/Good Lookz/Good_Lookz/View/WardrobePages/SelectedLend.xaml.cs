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
	public partial class SelectedLend : ContentPage
	{
		public SelectedLend()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			getInfo();
		}

		private void getInfo()
		{
			lbUsername.Text = Models.SelectedLend.username;
			lbDate.Text		= Models.SelectedLend.date;
			lbDays.Text		= Models.SelectedLend.days;
			lbName.Text		= Models.SelectedLend.name;
			img.Source		= Models.SelectedLend.picture;

			if(Models.SelectedLend.lending == "0")
			{
				btnContact.IsVisible	= false;
				lbTitle.Text			= "wants to borrow this item";
				lbText.Text				= "Is interested in borrowing this item. Make sure to tell us wether you've accepted to borrow this item to " + Models.SelectedLend.name + " with the button below.";
			}
			else
			{
				btnContact.Text			= "Contact " + Models.SelectedLend.username;
				btnAccept.IsVisible		= false;
				btnDecline.IsVisible	= false;
				lbTitle.Text			= "is borrowing this item";
				lbText.Text				= "Is borrowing this item from you. If " + Models.SelectedLend.name + " contacted yet, then click the button below to see to send a reminder";
			}
		}

		private async void btnAccept_Clicked(object sender, EventArgs e)
		{
			string webadres = "http://good-lookz.com/API/lend/lendAccept.php?";
			string parameters = "lend_id=" + Models.SelectedLend.lend_id+ "&accepted=true";

			HttpClient connect = new HttpClient();
			HttpResponseMessage insert = await connect.GetAsync(webadres + parameters);
			insert.EnsureSuccessStatusCode();

			string result = await insert.Content.ReadAsStringAsync();

			if (result == "Success")
			{
				await DisplayAlert("Success", "Lend request has been accepted.", "OK");

				Models.PreviousPage.page = "SelectedLend";
				await Navigation.PushAsync(new WardrobeContact(), true);

			}
			else if (result == "Failed")
			{
				await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
			}
		}

		private async void btnDecline_Clicked(object sender, EventArgs e)
		{
			var requestResponse = await DisplayAlert("Warning", "Do you really want to delete the lend request?", "Yes", "No");
			if (requestResponse)
			{
				string webadres = "http://good-lookz.com/API/lend/lendAccept.php?";
				string parameters = "lend_id=" + Models.SelectedLend.lend_id + "&accepted=false";

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

		private async void btnContact_Clicked(object sender, EventArgs e)
		{
			Models.PreviousPage.page = "SelectedLend";
			await Navigation.PushAsync(new WardrobeContact(), true);
		}
	}
}
