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
	public partial class ColoursHealthPage : ContentPage
	{
		public ColoursHealthPage()
		{
			InitializeComponent();
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

		protected override void OnAppearing()
		{
			getFeelings();
		}

		private async void getFeelings()
		{
			string webadres = "http://good-lookz.com/API/colourshealth/getcolours.php";
			HttpClient connect = new HttpClient();
			HttpResponseMessage get = await connect.GetAsync(webadres);
			get.EnsureSuccessStatusCode();

			string result	= await get.Content.ReadAsStringAsync();
			var jsonresult	= JsonConvert.DeserializeObject<List<Feeling>>(result);

			foreach(Feeling f in jsonresult)
			{
				lstFeeling.Add(new Feeling { feelingName = f.colourTitle, feelingColour = Color.FromHex(f.colourHex), labelColour = Color.SlateGray });
			}

			lvFeelings.ItemsSource = lstFeeling;
		}

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//Animatie voor achtergrondkleur wijzigen
			this.ColorTo(this.BackgroundColor, ((Feeling)(lvFeelings.SelectedItem)).feelingColour, c => BackgroundColor = c, 700, Easing.CubicInOut);
			
			//Verander de label colours naar wit
			foreach (Feeling f in lstFeeling)
			{
				f.labelColour = Color.White;
			}
			lvFeelings.ItemsSource = null;
			lvFeelings.ItemsSource = lstFeeling;

			//Code van: https://forums.xamarin.com/discussion/30328/listview-item-selected-disable
			//Zorg ervoor dat een item niet geselecteerd kan worden
			if (e == null) return;
			((ListView)sender).SelectedItem = null;
		}
	}
}