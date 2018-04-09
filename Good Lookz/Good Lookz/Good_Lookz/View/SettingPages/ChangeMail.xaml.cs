using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class ChangeMail : ContentPage
	{
		public ChangeMail()
		{
			InitializeComponent();

			//Vul de placeholder van de email entry
			getCurrentMail();
		}

		private void getCurrentMail()
		{
			newMail.Placeholder = Models.LoginCredentials.loginEmail;
		}

		private async Task<bool> validPwd()
		{
			bool i = false;
			string webadres = "http://good-lookz.com/API/account/checkCurrentPwd.php?";
			string parameters = "users_id=" + Models.LoginCredentials.loginId + "&pwd=" + cPwd.Text;

			HttpClient connect = new HttpClient();
			HttpResponseMessage update = await connect.GetAsync(webadres + parameters);
			update.EnsureSuccessStatusCode();
			string result = await update.Content.ReadAsStringAsync();

			if (result == "Success")
			{
				i = true;
			}
			else if(result == "Failed")
			{
				i = false;
			}

			return i;
		}

		private async Task<bool> checkMail()
		{
			bool r = false;
			try
			{
				string webadres = "http://good-lookz.com/API/account/checkMail.php?";
				string parameters = "mail=" + newMail.Text;
				HttpClient connect = new HttpClient();
				HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
				get.EnsureSuccessStatusCode();

				string result = await get.Content.ReadAsStringAsync();

				if (result == "0")
				{
					r = true;
				}
				else
				{
					r = false;
				}
			}
			catch (Exception)
			{
				r = false;
			}

			return r;
		}

		private async Task<bool> updateMail()
		{
			bool r = false;
			try
			{
				string webadres = "http://good-lookz.com/API/account/updateMail.php?";
				string parameters = "mail=" + newMail.Text + "&users_id=" + Models.LoginCredentials.loginId;
				HttpClient connect = new HttpClient();
				HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
				get.EnsureSuccessStatusCode();

				string result = await get.Content.ReadAsStringAsync();

				if (result == "Success")
				{
					Models.LoginCredentials.loginEmail = newMail.Text;
					r = true;
				}
				else
				{
					r = false;
				}
			}
			catch (Exception)
			{
				r = false;
			}

			return r;
		}

		private async void changeMail(object sender, EventArgs e)
		{
			//Check of de mail entry gevuld is
			if (string.IsNullOrEmpty(newMail.Text))
			{
				await DisplayAlert("Warning", "Please fill in a new mail address before proceeding.", "ok");
			}
			else
			{
				//Check of het wachtwoord correct is
				if (await validPwd())
				{
					//Check of de mail uniek is
					if (await checkMail())
					{
						if(await updateMail())
						{
							await DisplayAlert("Success", "Changes have been saved!", "ok");
							await this.Navigation.PopAsync();
						}
						else
						{
							await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "ok");
						}
					}
					else
					{
						await DisplayAlert("Error", "E-mail address is already being used", "ok");
					}
				}
				else
				{
					await DisplayAlert("Error", "Password invalid.", "OK");
				}
			}	
		}
	}
}
