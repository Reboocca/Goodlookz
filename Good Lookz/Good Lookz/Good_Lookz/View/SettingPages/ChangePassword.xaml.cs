using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class ChangePassword : ContentPage
	{
		public ChangePassword()
		{
			InitializeComponent();
		}

		private async void changePwd(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(cPwd.Text) || string.IsNullOrEmpty(nPwd.Text) || string.IsNullOrEmpty(rPwd.Text))
			{
				await DisplayAlert("Warning", "Please make sure all boxes are filled before changing your password.", "Ok");
			}
			else
			{
				if(rPwd.TextColor == Color.Red || nPwd.Text != rPwd.Text)
				{
					await DisplayAlert("Warning", "New password and confirmed password don't match.", "Ok");
				}
				else
				{
					checkOldPwd();
				}
			}
		}

		private async void checkOldPwd()
		{
			string webadres = "http://good-lookz.com/API/account/checkCurrentPwd.php?";
			string parameters = "users_id=" + Models.LoginCredentials.loginId + "&pwd=" + cPwd.Text;

			HttpClient connect = new HttpClient();
			HttpResponseMessage update = await connect.GetAsync(webadres + parameters);
			update.EnsureSuccessStatusCode();
			string result = await update.Content.ReadAsStringAsync();

			if (result == "Success")
			{
				saveNewPwd();
			}
			else if (result == "Failed")
			{
				await DisplayAlert("Error", "Current password invalid.", "OK");
			}
		}

		private async void saveNewPwd()
		{
			string webadres = "http://good-lookz.com/API/account/updatePwd.php?";
			string parameters = "users_id=" + Models.LoginCredentials.loginId + "&pwd=" + nPwd.Text;

			HttpClient connect = new HttpClient();
			HttpResponseMessage update = await connect.GetAsync(webadres + parameters);
			update.EnsureSuccessStatusCode();
			string result = await update.Content.ReadAsStringAsync();

			if (result == "Success")
			{
				await DisplayAlert("Succes", "Your password has been changed.", "OK");
				await this.Navigation.PopAsync();
			}
			else if (result == "Failed")
			{
				await DisplayAlert("Error", "Something went wrong with updating your password, please check your internet connection and try again.", "OK");
			}
		}
	}
}
