using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.SettingPages
{
	public partial class PP : ContentPage
	{
		public PP()
		{
			InitializeComponent();
			wvWeb.Source = "http://www.good-lookz.com/articles/privacy-policy.php";
		}
	}
}
