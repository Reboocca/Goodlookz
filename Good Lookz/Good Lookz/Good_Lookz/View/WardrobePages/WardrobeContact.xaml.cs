using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Good_Lookz.View.WardrobePages
{
	public partial class WardrobeContact : ContentPage
	{
		public WardrobeContact()
		{
			InitializeComponent();
		}

		#region Global variabels
		string name = null;
		string id	= null;
		string type = null;
		#endregion

		protected override void OnAppearing()
		{
			getInfo();
		}
		private async void btnSend_Clicked(object sender, EventArgs e)
		{
			var accepted = await DisplayAlert("Warning", "By hitting 'send' you accept that your contact information will be send to " + name + ". Do you want to continue?", "Accept", "Decline");
			if (accepted)
			{
				if(!(string.IsNullOrEmpty(enName.Text)) && !(string.IsNullOrEmpty(enMail.Text)) | !(string.IsNullOrEmpty(enPhone.Text)))
				{
					if(!(string.IsNullOrEmpty(enMail.Text)) && enMail.TextColor == Color.Red)
					{
						await DisplayAlert("Warning", "Make sure your e-mail adress is valid, otherwise " + name + " will not be able to contact you.", "OK");
					}
					else
					{
						//Check of de preference goed is ingevuld 
						//Als preference mail is, dan moet het email veld ingevuld zijn. 
						//Dit geldt ook voor de phone preference.
						if (pPreference.SelectedIndex == 0 && string.IsNullOrEmpty(enPhone.Text) || pPreference.SelectedIndex == 1 && string.IsNullOrEmpty(enMail.Text))
						{
							await DisplayAlert("Warning", "Make sure to fill in the field of your preffence.", "OK");
						}
						else
						{
							//Haal preference op
							string prefer = null;
							switch (pPreference.SelectedIndex)
							{
								case 0:
									prefer = "phone";
									break;
								case 1:
									prefer = "mail";
									break;
								default:
									prefer = null;
									break;
							}

							//Verstuur verzoek naar de web API, sla eerst akkoordverklaring op en stuur hierna de mail
							string webadres = "http://good-lookz.com/API/email/emailContact.php?";
							string parameters = "users_id=" + Models.LoginCredentials.loginId + "&reciever_id=" + id + "&type=" + type + "&username=" + Models.LoginCredentials.loginUsername + "&name=" + enName.Text + "&phone=" + enPhone.Text + "&email=" + enMail.Text + "&prefer=" + prefer;

							HttpClient connect = new HttpClient();
							HttpResponseMessage insert = await connect.GetAsync(webadres + parameters);
							insert.EnsureSuccessStatusCode();

							string result = await insert.Content.ReadAsStringAsync();

							//Geef melding weer
							if (result == "Success ")
							{
								if(type == "Sale")
								{
									string web2 = "http://good-lookz.com/API/sale/saleRequestAccept.php?id=" + Models.SelectedSaleRequests.requests_id;
									HttpClient connect2 = new HttpClient();
									HttpResponseMessage delete = await connect.GetAsync(webadres + parameters);
									insert.EnsureSuccessStatusCode();
									string result2 = await insert.Content.ReadAsStringAsync();
								}

								await DisplayAlert("Success", "The mail has been sent to " + name, "OK");
								await this.Navigation.PopAsync();
							}
							else if (result == "Failed " | result == "Couldnt insert to database")
							{
								await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
							}
						}
					}
				}
				else
				{
					await DisplayAlert("Warning", "Make sure to give us all the required information.", "OK");
				}
			}
			
		}

		//Functie om alle gegevens goed op te slaan en labels aan te passen
		private void getInfo()
		{
			switch (Models.PreviousPage.page)
			{
				case "SelectedLend":
					name = Models.SelectedLend.name;
					id	 = Models.SelectedLend.borrow_users_id;
					type = "Lend";
					break;
				case "WardrobeSelectedSaleRequests":
					id   = Models.SelectedSaleRequests.users_id1;
					name = Models.SelectedSaleRequests.name;
					type = "Sale";
					break;
			}
			lbText.Text = "Make sure you atleast provide your phone number or email-address so " + name + " can contact you.";
		}
	}
}
