using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
	public partial class ColoursHealthWebview : ContentPage
	{
		public ColoursHealthWebview()
		{
			InitializeComponent();
			wvColoursHealth.Source = "http://colourshealth.com/";
		}
	}
}
