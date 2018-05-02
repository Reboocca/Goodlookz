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
            lstSetting.Add(new AccountSetting { title = "Edit your profile",    id = 0 });
            lstSetting.Add(new AccountSetting { title = "Personal information", id = 1 });
            lstSetting.Add(new AccountSetting { title = "Change password",		id = 2 });
			lstSetting.Add(new AccountSetting { title = "Change e-mail",		id = 3 });

			lvAccSettings.ItemsSource = lstSetting;
		}

		//Wanneer iemand op een setting klikt
		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//Check waar de gebruiker op heeft geklikt en voer de juiste actie uit
			switch (((AccountSetting)(lvAccSettings.SelectedItem)).id)
			{
                case 0:
                    editProfile();
                    break;
				case 1:
					editPersonal();
					break;
				case 2:
					editPwd();
					break;
				case 3:
					editMail();
					break;
			}

			//Code van: https://forums.xamarin.com/discussion/30328/listview-item-selected-disable
			//Zorg ervoor dat een item niet geselecteerd kan worden
			if (e == null) return;
			((ListView)sender).SelectedItem = null;
		}

        private async void editProfile()
        {
            //Stuur de gebruiker door naar de profiel pagina
            await Navigation.PushAsync(new View.SettingPages.ChangeProfile(), true);
        }

        private async void editPersonal()
		{
			//Stuur de gebruiker door naar de persoonlijke gegevens pagina
			await Navigation.PushAsync(new View.SettingPages.ChangeInfo(), true);
		}

		private async void editPwd()
		{
			//Stuur de gebruiker door naar de persoonlijke gegevens pagina
			await Navigation.PushAsync(new View.SettingPages.ChangePassword(), true);
		}

		private async void editMail()
		{
			//Stuur de gebruiker door naar de persoonlijke gegevens pagina
			await Navigation.PushAsync(new View.SettingPages.ChangeMail(), true);
		}
	}
}
