using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class ChangeInfo : ContentPage
	{
		public ChangeInfo()
		{
			InitializeComponent();

            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            //Sla alle verschillende maten op in de list die boven is aangemaakt
            lstSizes = Models.Sizes.getAllSizesList();

			//Haal gebruiker gegevens op en vul deze in
			getUserSize();
		}

		#region Global
		List<Models.Sizes.AllSizes> lstSizes = new List<Models.Sizes.AllSizes>();

        Models.UserSizes uSize = new Models.UserSizes();

        string _Gender  = "F";
		string _Region  = "EU";
		#endregion

		//Sla de opgehaalde gebruiker gegevens op in het object
		private async void getUserSize()
		{
			string users_id = Models.LoginCredentials.loginId;
			string url      = "http://good-lookz.com/API/account/getSizes.php?users_id=" + users_id;

			HttpClient get              = new HttpClient();
			HttpResponseMessage respons = await get.GetAsync(url);

			if (respons.IsSuccessStatusCode)
			{
				string responsecontent  = await respons.Content.ReadAsStringAsync();
				var myobjList           = JsonConvert.DeserializeObject<List<Models.UserSizes>>(responsecontent);

				uSize.region = myobjList[0].region;
				uSize.gender = myobjList[0].gender;
				uSize.topnr  = myobjList[0].topnr;
				uSize.botnr  = myobjList[0].botnr;
				uSize.feetnr = myobjList[0].feetnr;
			}

			//Zet de pickers op de goede waardes
			switch (uSize.region)
			{
				case "EU":
					cbRegion.SelectedIndex = 0;
					break;

				case "US":
					cbRegion.SelectedIndex = 1;
					break;

				case "UK":
					cbRegion.SelectedIndex = 2;
					break;

				default:
					cbRegion.SelectedIndex = 0;
					break;
			}

			switch (uSize.gender)
			{
				case "F":
					cbGender.SelectedIndex = 0;
					break;

				case "M":
					cbGender.SelectedIndex = 1;
					break;

				default:
					cbGender.SelectedIndex = 0;
					break;
			}

			//Vul de pickers
			FillPickers(_Gender, _Region);

			//Vul de gegevens in
			fillInfo();
		}

		//Vul de pickers in a.d.h.v. de gekozen waardes
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

			//Selecteer de juiste maten
			topSize.SelectedIndex       = Int32.Parse(uSize.topnr) - 1;
			bottomSize.SelectedIndex    = Int32.Parse(uSize.botnr) - 1;
			feetSize.SelectedIndex      = Int32.Parse(uSize.feetnr) - 1;

		}

		//Vul de gegevens van de gebruiker in in de invoervelden
		private void fillInfo()
		{
			//Vul de entries
			fName.Text = Models.LoginCredentials.loginFirstname;
			lName.Text = Models.LoginCredentials.loginLastname;
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

		private async void UpdateUserInfo(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(fName.Text) | string.IsNullOrEmpty(lName.Text))
			{
				await DisplayAlert("Warning", "Don't leave the first or last name box empty", "OK");
			}
			else
			{
				if (topSize.SelectedIndex == -1 || bottomSize.SelectedIndex == -1 || feetSize.SelectedIndex == -1)
				{
					await DisplayAlert("Warning", "Please fill in your size before proceeding.", "OK");
				}
				else
				{
					//Sla de gegevens op in de dbs
					try
					{
						//Haal vereisten gegevens op
						var users_id = Models.LoginCredentials.loginId;
						Models.Sizes.AllSizes top   = new Models.Sizes.AllSizes();
						Models.Sizes.AllSizes bot   = new Models.Sizes.AllSizes();
						Models.Sizes.AllSizes feet  = new Models.Sizes.AllSizes();

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

						string webadres     = "http://good-lookz.com/API/account/updateUserInfo.php?";
						string parameters   = "users_id=" + Models.LoginCredentials.loginId + "&fname=" + fName.Text + "&lname=" + lName.Text + "&region=" + feet.sRegion + "&gender=" + feet.sGender + "&topsize=" + top.sNr + "&botsize=" + bot.sNr + "&feetsize=" + feet.sNr;

						HttpClient connect          = new HttpClient();
						HttpResponseMessage update  = await connect.GetAsync(webadres + parameters);
						update.EnsureSuccessStatusCode();
						string result               = await update.Content.ReadAsStringAsync();

						if (result == "Success")
						{
							await DisplayAlert("Success", "Changes have been saved!", "OK");

							Models.LoginCredentials.loginFirstname = fName.Text;
							Models.LoginCredentials.loginLastname = lName.Text;

							await this.Navigation.PopAsync();
						}
						else if (result == "Failed")
						{
							await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
						}
					}
					catch
					{
						throw;
					}
				}
			}
		}
	}
}
