using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class EditAccount : ContentPage
	{
		public EditAccount()
		{
			InitializeComponent();

			getSettings();
		}

		#region Global
		public class AccountSetting
		{
			public string title { get; set; }
			public int id { get; set; }
		}
		new List<AccountSetting> lstSetting = new List<AccountSetting>();
		#endregion

		//Vul de listview met settings
		private void getSettings()
		{
			lstSetting.Add(new AccountSetting { title = "Personal information", id = 1 });
			lstSetting.Add(new AccountSetting { title = "My size",				id = 2 });
			lstSetting.Add(new AccountSetting { title = "Change password",		id = 3 });
			lstSetting.Add(new AccountSetting { title = "Change e-mail",		id = 4 });

			lvAccSettings.ItemsSource = lstSetting;
		}

		//Wanneer iemand op een setting klikt
		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//Check waar de gebruiker op heeft geklikt en voer de juiste actie uit
			switch (((AccountSetting)(lvAccSettings.SelectedItem)).id)
			{
				case 1:
					editPersonal();
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					break;
			}

			//Code van: https://forums.xamarin.com/discussion/30328/listview-item-selected-disable
			//Zorg ervoor dat een item niet geselecteerd kan worden
			if (e == null) return;
			((ListView)sender).SelectedItem = null;
		}

		private async void editPersonal()
		{
			//Stuur de gebruiker door naar de persoonlijke gegevens pagina
			await Navigation.PushAsync(new View.SettingPages.ChangeInfo(), true);
		}
	}
}
