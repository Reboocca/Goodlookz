using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.WardrobePages
{
	public partial class FilterWardrobe : ContentPage
	{
		public FilterWardrobe()
		{
			InitializeComponent();
		}
		
		//Class voor het opslaan van de JSON gegevens
		class Types
		{
			public string head { get; set; }
			public string top { get; set; }
			public string bottom { get; set; }
			public string feet { get; set; }
		}

		protected override void OnAppearing()
		{
			getTypes(Models.LoginCredentials.loginId);
		}

		private async void getTypes(string id)
		{
			string webadres = "http://good-lookz.com/API/wardrobe/getFilterOptions.php?";
			string parameters = "users_id=" + id + "&function=types";
			HttpClient connect = new HttpClient();
			HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
			get.EnsureSuccessStatusCode();

			string result = await get.Content.ReadAsStringAsync();
			var jsonResult = JsonConvert.DeserializeObject<List<Types>>(result);

			//Stop de opgehaalde waardes in de picker als de 'true' zijn
			if(jsonResult[0].head == "true")
			{
				pType.Items.Add("Head");
			}

			if (jsonResult[0].top == "true")
			{
				pType.Items.Add("Top");
			}

			if (jsonResult[0].bottom == "true")
			{
				pType.Items.Add("Bottom");
			}

			if (jsonResult[0].feet == "true")
			{
				pType.Items.Add("Feet");
			}

			//Wanneer er geen items gevonden zijn
			if(pType.Items.Count == 0)
			{
				await DisplayAlert("Warning", "There are no clothes found in your wardrobe, please add some beforehand.", "OK");
				await this.Navigation.PopAsync();
			}
		}

		private async void getColours(string type, string id)
		{
			string webadres = "http://good-lookz.com/API/wardrobe/getFilterOptions.php?";
			string parameters = "users_id=" + id + "&function=colours&item=" + type;
			HttpClient connect = new HttpClient();
			HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
			get.EnsureSuccessStatusCode();

			string result = await get.Content.ReadAsStringAsync();
			string[] colours = result.Split(',');

			foreach (var colour in colours)
			{
				pColour.Items.Add(colour);
			}
		}

		private void pType_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Als er al items in de colour picker staan, zorg dat deze leeg gemaakt worden
			if(pColour.Items.Count > 0)
			{
				pColour.Items.Clear();
			}

			//Roep de functie aan om items aan de colour picker toe te voegen
			getColours(pType.Items[pType.SelectedIndex], Models.LoginCredentials.loginId);
		}

		private async void btnFilter_Clicked(object sender, EventArgs e)
		{
			if(pType.SelectedIndex == -1 || pColour.SelectedIndex == -1)
			{
				await DisplayAlert("Warning", "Please select a type and colour before you continue.", "OK");
			}
			else
			{
				switch (pType.Items[pType.SelectedIndex])
				{
					case "Head":
						Models.Settings.Filter.filterHead.filteron	 = true;
						Models.Settings.Filter.filterHead.colour	 = pColour.Items[pColour.SelectedIndex];
						break;

					case "Top":
						Models.Settings.Filter.filterTop.filteron	 = true;
						Models.Settings.Filter.filterTop.colour		 = pColour.Items[pColour.SelectedIndex];
						break;

					case "Bottom":
						Models.Settings.Filter.filterBottom.filteron = true;
						Models.Settings.Filter.filterBottom.colour	 = pColour.Items[pColour.SelectedIndex];
						break;

					case "Feet":
						Models.Settings.Filter.filterFeet.filteron	 = true;
						Models.Settings.Filter.filterFeet.colour	 = pColour.Items[pColour.SelectedIndex];
						break;
				}
				await this.Navigation.PopAsync();
			}
			
		}

		private async void btnReset_CLicked(object sender, EventArgs e)
		{
			Models.Settings.Filter.filterHead.filteron		= false;
			Models.Settings.Filter.filterTop.filteron		= false;
			Models.Settings.Filter.filterBottom.filteron	= false;
			Models.Settings.Filter.filterFeet.filteron		= false;

			await this.Navigation.PopAsync();
		}
	}
}
