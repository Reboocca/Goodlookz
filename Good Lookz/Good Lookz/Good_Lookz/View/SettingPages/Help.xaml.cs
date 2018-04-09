using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class Help : ContentPage
	{
		public Help()
		{
			InitializeComponent();

			//Vul de list met settings
			getSettings();
		}

		#region Global
		public class HelpSettings
		{
			public string title { get; set; }
			public int id { get; set; }
		}
		new List<HelpSettings> lstSetting = new List<HelpSettings>();
		#endregion

		//Vul de listview met settings
		private void getSettings()
		{
			lstSetting.Add(new HelpSettings { title = "Problem", id = 1 });
			lstSetting.Add(new HelpSettings { title = "Helpdesk", id = 2 });
			lstSetting.Add(new HelpSettings { title = "About us", id = 3 });

			lvHelp.ItemsSource = lstSetting;
		}

		//Wanneer iemand op een setting klikt
		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//Code van: https://forums.xamarin.com/discussion/30328/listview-item-selected-disable
			//Zorg ervoor dat een item niet geselecteerd kan worden
			if (e == null) return;
			((ListView)sender).SelectedItem = null;
		}
	}
}
