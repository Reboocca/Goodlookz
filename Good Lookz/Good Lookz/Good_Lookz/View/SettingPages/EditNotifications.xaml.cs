using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class EditNotifications : ContentPage
	{
		public EditNotifications()
		{
			InitializeComponent();

			//Zet de settings aan of uit aan de hand van de opgeslagen settings
			scFriend.On = Models.Settings.NotifySettings.notifyfriend;
			scLend.On	= Models.Settings.NotifySettings.notifyborrow;
			scSale.On	= Models.Settings.NotifySettings.notifybid;
		}

		private async void Toggled(object sender, ToggledEventArgs e)
		{
			//Switch om te kijken welke switch aan of uit is gezet
			switch (((SwitchCell)sender).Text)
			{
				case "New friend requests":
					Models.Settings.NotifySettings.notifyfriend	= e.Value;
					break;
				case "New borrow requests":
					Models.Settings.NotifySettings.notifyborrow = e.Value;
					break;
				case "New sale requests":
					Models.Settings.NotifySettings.notifybid	= e.Value;
					break;
			}
		}
	}
}
