using Good_Lookz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
	public partial class InYourColourPage : ContentPage
	{
		public InYourColourPage()
		{
			InitializeComponent();
			
			getFeelings();
		}

		class Feeling
		{
			public string colourTitle { get; set; }
			public string colourHex { get; set; }
			public string feelingName { get; set; }
			public Color feelingColour { get; set; }
			public Color labelColour { get; set; }
		}

		List<Feeling> lstFeeling = new List<Feeling>();


		//Haal de gevoelens op vanuit de database en stop deze in de lijst en listview
		private async void getFeelings()
		{
			string webadres = "http://good-lookz.com/API/inyourcolour/getcolours.php";
			HttpClient connect = new HttpClient();
			HttpResponseMessage get = await connect.GetAsync(webadres);
			get.EnsureSuccessStatusCode();

			string result = await get.Content.ReadAsStringAsync();
			var jsonresult = JsonConvert.DeserializeObject<List<Feeling>>(result);

			foreach (Feeling f in jsonresult)
			{
				lstFeeling.Add(new Feeling { feelingName = f.colourTitle, feelingColour = Color.FromHex(f.colourHex), labelColour = Color.SlateGray });
			}

			await lvFeelings.FadeTo(0, 1);
			lvFeelings.ItemsSource = lstFeeling;
			lvFeelings.FadeTo(1, 500);
		}

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//Animatie voor achtergrondkleur wijzigen
			this.ColorTo(this.BackgroundColor, ((Feeling)(lvFeelings.SelectedItem)).feelingColour, c => BackgroundColor = c, 750);

			//Verander de label colours naar wit
			foreach (Feeling f in lstFeeling)
			{
				f.labelColour = Color.White;
			}

			//Check of de het logo gewijzigd is, zo niet verander de image source
			var source = imgLogo.Source as FileImageSource;
			if (source != null)
			{
				var filename = source.File;

				if(source.File == "iyc_logo.png")
				{
					imgLogo.FadeTo(0, 500);
					imgLogo.Source = "iyc_logo_white.png";
					imgLogo.FadeTo(1, 500);
				}
			}

			lblTitle.TextColor = Color.White;

			lblTitle.FadeTo(0, 500);
			lvFeelings.FadeTo(0, 500);
			lvFeelings.ItemsSource = null;
			lvFeelings.ItemsSource = lstFeeling;
			lvFeelings.FadeTo(1, 500);
			lblTitle.FadeTo(1, 500);

			//Code van: https://forums.xamarin.com/discussion/30328/listview-item-selected-disable
			//Zorg ervoor dat een item niet geselecteerd kan worden
			if (e == null) return;
			((ListView)sender).SelectedItem = null;
		}

		private async void logoClicked(object sender, EventArgs e)
		{
			//Open de pagina met de webview van inyourcolour.com
			await Navigation.PushAsync(new View.InYourColourWebview(), true);
		}

	}
}