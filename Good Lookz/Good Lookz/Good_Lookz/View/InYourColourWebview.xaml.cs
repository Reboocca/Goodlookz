using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
	public partial class InYourColourWebview : ContentPage
	{
		public InYourColourWebview()
		{
			InitializeComponent();

            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            wvWebView.Source = "http://www.inyourcolour.com";
		}
	}
}
