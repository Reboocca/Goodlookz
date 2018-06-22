using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
	public partial class SaveSizePage : ContentPage
	{
		public SaveSizePage()
		{
			InitializeComponent();

			//Zorg dat er een region en gender geselecteerd is
			cbRegion.SelectedIndex = 0;
			cbGender.SelectedIndex = 0;

			//Vul waardes in en laad gegevens in de pickers
			FillList();
			FillPickers("F", "EU");
		}

		#region Globale variabelen
		List<Models.Sizes.AllSizes> lstSizes = new List<Models.Sizes.AllSizes>();

		string _Region = "EU";
		string _Gender = "F";
		#endregion

		//code van: https://stackoverflow.com/questions/32163471/confirmation-dialog-on-back-button-press-event-xamarin-forms voor terugknop
		protected override bool OnBackButtonPressed()
		{
			Device.BeginInvokeOnMainThread(async () => { await this.DisplayAlert("Message", "Please save your sizes before proceeding", "OK"); });
			return true;
		}

		private void FillList()
		{
			lstSizes = Models.Sizes.getAllSizesList();
		}

		private void FillPickers(string g, string r)
		{
			topSize.Items.Clear();
			bottomSize.Items.Clear();
			feetSize.Items.Clear();


			foreach (Models.Sizes.AllSizes i in lstSizes)
			{
				if (i.sGender == g && i.sRegion == r && i.sType == "top")
				{
					topSize.Items.Add(i.sSize);
				}
			}

			foreach (Models.Sizes.AllSizes i in lstSizes)
			{
				if (i.sGender == g && i.sRegion == r && i.sType == "bottom")
				{
					bottomSize.Items.Add(i.sSize);
				}
			}

			foreach (Models.Sizes.AllSizes i in lstSizes)
			{
				if (i.sGender == g && i.sRegion == r && i.sType == "feet")
				{
					feetSize.Items.Add(i.sSize);
				}
			}
		}

		private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbRegion.SelectedIndex)
			{
				case 0:
					_Region = "EU";
					break;
				case 1:
					_Region = "US";
					break;
				case 2:
					_Region = "UK";
					break;
			}

			FillPickers(_Gender, _Region);
		}

		private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbGender.SelectedIndex)
			{
				case 0:
					_Gender = "F";
					break;
				case 1:
					_Gender = "M";
					break;
			}

			FillPickers(_Gender, _Region);
		}

		private async void btnSaveSize_Clicked(object sender, EventArgs e)
		{
			if (topSize.SelectedIndex == -1 || bottomSize.SelectedIndex == -1 || feetSize.SelectedIndex == -1)
			{
				await DisplayAlert("Error", "Please tell us your size before proceeding.", "OK");
			}
			else
			{
				//Sla de gegevens op in de dbs
				try
				{
					//Haal vereisten gegevens op
					var users_id = Models.LoginCredentials.loginId;
					Models.Sizes.AllSizes top = new Models.Sizes.AllSizes();
					Models.Sizes.AllSizes bot = new Models.Sizes.AllSizes();
					Models.Sizes.AllSizes feet = new Models.Sizes.AllSizes();

					foreach (Models.Sizes.AllSizes i in lstSizes)
					{
						if (i.sSize == topSize.SelectedItem.ToString() && i.sRegion == _Region && i.sGender == _Gender && i.sType == "top")
						{
							top = i;
						}
					}
					foreach (Models.Sizes.AllSizes i in lstSizes)
					{
						if (i.sSize == bottomSize.SelectedItem.ToString() && i.sRegion == _Region && i.sGender == _Gender && i.sType == "bottom")
						{
							bot = i;
						}
					}
					foreach (Models.Sizes.AllSizes i in lstSizes)
					{
						if (i.sSize == feetSize.SelectedItem.ToString() && i.sRegion == _Region && i.sGender == _Gender && i.sType == "feet")
						{
							feet = i;
						}
					}

					string webadres = "http://good-lookz.com/API/account/saveSize.php?";
					string parameters = "users_id=" + users_id + "&gender=" + feet.sGender 	+ "&region=" + feet.sRegion + "&topnr=" + top.sNr + "&botnr=" + bot.sNr + "&feetnr=" + feet.sNr;
					HttpClient connect = new HttpClient();
					HttpResponseMessage uploadToSale = await connect.GetAsync(webadres+parameters);
					uploadToSale.EnsureSuccessStatusCode();

					string result = await uploadToSale.Content.ReadAsStringAsync();

					//Is het resultaat succes
					if (result == "Success")
					{
						await DisplayAlert("Success", "Your sizes have been saved! You can always change your size in settings.", "OK");
						App.Current.MainPage = new NavigationPage(new ShopPages.ChoseShopPage());
					}
					//Is het resultaat failed
					else if (result == "Failed")
					{
						await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
					}

				}
				catch (Exception)
				{
					await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
					throw;
				}
			}

			
		}
	}
}
