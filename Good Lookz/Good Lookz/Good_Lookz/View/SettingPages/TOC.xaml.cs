using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class TOC : ContentPage
	{
		public TOC()
		{
			InitializeComponent();

            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            wvWeb.Source = "http://www.good-lookz.com/articles/terms-of-service.php";
		}
	}
}
