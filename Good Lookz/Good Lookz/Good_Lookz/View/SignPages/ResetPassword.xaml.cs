using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SignPages
{
	public partial class ResetPassword : ContentPage
	{
		public ResetPassword()
		{
			InitializeComponent();

		}
		private async void changePwd(object sender, EventArgs e)
		{ 
            //Check of de invoervelden leeg zijn
			if (string.IsNullOrEmpty(nPwd.Text) || string.IsNullOrEmpty(rPwd.Text))
			{
				await DisplayAlert("Warning", "Please make sure all boxes are filled before resetting your password.", "Ok");
			}
			else
			{
                //Check of de wachtwoorden overeenkomen
				if (rPwd.TextColor == Color.Red || nPwd.Text != rPwd.Text)
				{
					await DisplayAlert("Warning", "New password and confirmed password don't match.", "Ok");
				}
				else
				{
					saveNewPwd();
				}
			}
		}

		private async void saveNewPwd()
		{
            //Verander het wachtwoord met de hulp van de webserver
			string webadres = "http://good-lookz.com/API/account/updatePwd.php?";
			string parameters = "users_id=" + Models.Settings.ResetPWD.users_id + "&pwd=" + nPwd.Text + "&reset=1";

			HttpClient connect = new HttpClient();
			HttpResponseMessage update = await connect.GetAsync(webadres + parameters);
			update.EnsureSuccessStatusCode();
			string result = await update.Content.ReadAsStringAsync();

            //Check het resultaat een weergeef een melding
			if (result == "Success")
			{
				this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
				await DisplayAlert("Succes", "Your password has been changed", "OK");
				await this.Navigation.PopAsync();
			}
			else if (result == "Failed")
			{
				await DisplayAlert("Error", "Something went wrong with updating your password, please check your internet connection and try again.", "OK");
			}
		}
	}
}
