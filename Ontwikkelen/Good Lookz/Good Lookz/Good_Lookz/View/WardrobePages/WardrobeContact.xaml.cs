using System;
using System.Collections.Generic;
using System.Linq;
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
						//Sla akoordverklaring op en maak een mail
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
					break;
				case "WardrobeSelectedSaleRequests":
					id   = Models.SelectedSaleRequests.users_id1;
					name = Models.SelectedSaleRequests.name;
					break;
			}
			lbText.Text = "Make sure you atleast provide your phone number or email-address so " + name + " can contact you.";
		}
	}
}
