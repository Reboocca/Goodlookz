using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class Privacy : ContentPage
	{
		public Privacy()
		{
			InitializeComponent();

			//Vul de list met settings
			getSettings();
		}

		#region Global
		public class PrivacySettings
		{
			public string title { get; set; }
			public int id { get; set; }
		}
		new List<PrivacySettings> lstSetting = new List<PrivacySettings>();
		#endregion

		//Vul de listview met settings
		private void getSettings()
		{
			lstSetting.Add(new PrivacySettings { title = "Privacy policy", id = 1 });
			lstSetting.Add(new PrivacySettings { title = "Terms of conditions", id = 2 });

			lvPrivacy.ItemsSource = lstSetting;
		}

		//Wanneer iemand op een setting klikt
		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//Check waar de gebruiker op heeft geklikt en voer de juiste actie uit
			switch (((PrivacySettings)(lvPrivacy.SelectedItem)).id)
			{
				case 1:
					GoToPP();
					break;
				case 2:
					GoToTOC();
					break;
			}

			//Code van: https://forums.xamarin.com/discussion/30328/listview-item-selected-disable
			//Zorg ervoor dat een item niet geselecteerd kan worden
			if (e == null) return;
			((ListView)sender).SelectedItem = null;
		}

		private async void GoToPP()
		{
			//Stuur de gebruiker door naar de PP pagina
			await Navigation.PushAsync(new View.SettingPages.PP(), true);
		}

		private async void GoToTOC()
		{
			//Stuur de gebruiker door naar de TOC pagina
			await Navigation.PushAsync(new View.SettingPages.TOC(), true);
		}
	}
}