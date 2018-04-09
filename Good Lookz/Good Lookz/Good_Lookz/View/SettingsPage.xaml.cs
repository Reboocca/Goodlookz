using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

			getSettings();
        }

		#region Global
		public class Setting
		{
			public string title { get; set; }
			public int id { get; set; }
		}
		new List<Setting> lstSetting = new List<Setting>();
		#endregion

		//Vul de listview met settings
		private void getSettings()
		{
			lstSetting.Add(new Setting { title = "My account",		id = 1 });
			lstSetting.Add(new Setting { title = "Notifications",	id = 2 });
			lstSetting.Add(new Setting { title = "Help",			id = 3 });
			lstSetting.Add(new Setting { title = "Privacy",			id = 4 });
			lstSetting.Add(new Setting { title = "Log out",			id = 5 });

			lvSettings.ItemsSource = lstSetting;
		}

		//Wanneer iemand op een setting klikt
		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//Check waar de gebruiker op heeft geklikt en voer de juiste actie uit
			switch (((Setting)(lvSettings.SelectedItem)).id)
			{
				case 1:
					MyAccount();
					break;
				case 2:
					Notifications();
					break;
				case 3:
					Help();
					break;
				case 4:
					Pricacy();
					break;
				case 5:
					LogOut();
					break;
			}

			//Code van: https://forums.xamarin.com/discussion/30328/listview-item-selected-disable
			//Zorg ervoor dat een item niet geselecteerd kan worden
			if (e == null) return;
			((ListView)sender).SelectedItem = null;
		}

		private async void MyAccount()
		{
			//Stuur de gebruiker door naar de notificatie setting pagina
			await Navigation.PushAsync(new View.SettingPages.EditAccount(), true);
		}

		private async void Notifications()
		{
			//Stuur de gebruiker door naar de notificatie setting pagina
			await Navigation.PushAsync(new View.SettingPages.EditNotifications(), true);
		}

		private async void Help()
		{
			//Stuur de gebruiker door naar de help pagina
			await Navigation.PushAsync(new View.SettingPages.Help(), true);
		}

		private async void Pricacy()
		{
			//Stuur de gebruiker door naar de privacy pagina
			await Navigation.PushAsync(new View.SettingPages.Privacy(), true);
		}

		private async void LogOut()
		{
			//Logout functie overgenomen van oude versie
			var answer = await DisplayAlert("Logout?", "Are you sure?", "Yes", "No");

			if (answer == true)
			{
				if (Application.Current.Properties.ContainsKey("loginId"))
					Application.Current.Properties.Remove("loginId");
				if (Application.Current.Properties.ContainsKey("loginUsername"))
					Application.Current.Properties.Remove("loginUsername");
				if (Application.Current.Properties.ContainsKey("loginPassword"))
					Application.Current.Properties.Remove("loginPassword");
				if (Application.Current.Properties.ContainsKey("loginFirstname"))
					Application.Current.Properties.Remove("loginFirstname");
				if (Application.Current.Properties.ContainsKey("loginLastname"))
					Application.Current.Properties.Remove("loginLastname");
				if (Application.Current.Properties.ContainsKey("loginEmail"))
					Application.Current.Properties.Remove("loginEmail");
				if (Application.Current.Properties.ContainsKey("loginDate"))
					Application.Current.Properties.Remove("loginDate");
				if (Application.Current.Properties.ContainsKey("loginGender"))
					Application.Current.Properties.Remove("loginGender");
				if (Application.Current.Properties.ContainsKey("loginOffline"))
					Application.Current.Properties.Remove("loginOffline");
				if (Application.Current.Properties.ContainsKey("loginActive"))
					Application.Current.Properties.Remove("loginActive");

				wardrobeItems.noItems = false;
				App.Current.MainPage = new NavigationPage(new SignPage());
			}
		}
	}
}
