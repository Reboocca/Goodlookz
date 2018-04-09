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
	public partial class ResetCode : ContentPage
	{
		public ResetCode()
		{
			InitializeComponent();
			if (!string.IsNullOrEmpty(Models.Settings.ResetPWD.mail))
			{
				enMail.Text = Models.Settings.ResetPWD.mail;
			}
		}

		#region Global
		Models.Settings.ResetPWD.JSONresponse _result = new Models.Settings.ResetPWD.JSONresponse();
		public static class ResetPWDUser
		{
			public static string users_id { get; set; }
		}
		#endregion

		private void Next(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(enMail.Text) || string.IsNullOrEmpty(enCode.Text))
			{
				DisplayAlert("Warning", "Make sure you fill in the email and code before proceeding.", "ok");
			}
			else
			{
				if (enMail.TextColor == Color.Red)
				{
					DisplayAlert("Warning", "Invalid e-mail address.", "ok");
				}
				else
				{
					checkCode();
				}
			}
		}

		private async void checkCode()
		{
			try
			{
				string url = "http://www.good-lookz.com/API/account/forgotPwd.php?email=" + enMail.Text + "&code=" + enCode.Text;

				HttpClient get = new HttpClient();
				HttpResponseMessage response = await get.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					string responsecontent = await response.Content.ReadAsStringAsync();
					var myobjList = JsonConvert.DeserializeObject<List<Models.Settings.ResetPWD.JSONresponse>>(responsecontent);

					_result.status		= myobjList[0].status;
					_result.tries_left	= myobjList[0].tries_left;
					_result.users_id	= myobjList[0].users_id;

					//Switch om te kijken wat de status is en geef een juiste melding terug aan de gebruiker
					switch (_result.status)
					{
						case "wrong":
							await DisplayAlert("Wrong code", "Given code is unkown, you have " + _result.tries_left.ToString() + " left before this recovery code gets blocked.", "ok");
							break;

						case "blocked":
							await DisplayAlert("Out of tries", "The recovery code ran out of tries and has been blocked. If you still want to reset your password, request a new recovery code from us.", "ok");
							break;

						case "expired":
							await DisplayAlert("Expired", "The recovery code has been expired. Recovery codes last for 30 minutes after their request. You can always request a new code from us to reset your password.", "ok");
							break;

						case "invalid":
							await DisplayAlert("Not found", "The given email has not been found", "ok");
							break;

						case "used":
							await DisplayAlert("Already been used", "This recovery code has already been used. To create a new one, go back to the previous page and simply make a request.", "ok");
							break;

						case "active":
							Models.Settings.ResetPWD.users_id = _result.users_id;
							await Navigation.PushAsync(new View.SignPages.ResetPassword(), true);
							break;
					}
				}
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "ok");
				throw;
			}
		}
	}
}
