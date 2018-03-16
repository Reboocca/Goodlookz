using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.WardrobePages
{
	public partial class WardrobeSelectedSaleItem : ContentPage
	{
		public WardrobeSelectedSaleItem()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			entryPrice.Text		= Models.SelectedSaleList.price;
			editorDesc.Text		= Models.SelectedSaleList.desc;
			imageItem.Source	= Models.SelectedSaleList.picture;
		}

		private async void btnEdit_Clicked(object sender, EventArgs e)
		{
			string webadres = "http://good-lookz.com/API/sale/saleEdit.php?";
			string parameters = "item_id=" + Models.SelectedSaleList.item_id + "&cloth_id=" + Models.SelectedSaleList.clothID + "&price=" + entryPrice.Text + "&desc=" + editorDesc.Text;

			HttpClient connect			= new HttpClient();
			HttpResponseMessage update	= await connect.GetAsync(webadres + parameters);
			update.EnsureSuccessStatusCode();
			string result				= await update.Content.ReadAsStringAsync();

			if (result == "Success")
			{
				await DisplayAlert("Success", "Changes have been saved!", "OK");
				await this.Navigation.PopAsync();
			}
			else if (result == "Failed")
			{
				await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
			}
		}
	}
}
