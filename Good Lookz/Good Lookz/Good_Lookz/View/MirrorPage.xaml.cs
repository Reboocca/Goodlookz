using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
    public partial class MirrorPage : ContentPage
    {
        public MirrorPage()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			btnAdjust.BackgroundColor = Color.Transparent;
			changeLightIntensitivity(Models.Settings.Mirror.adjust_lighting);
		}

		//Verander de achtergrondkleur a.d.v.d. gekozen value
		private void changeLightIntensitivity(double val)
		{
			bvLight.Opacity = val;
		}

		private async void btnAdjust_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new View.AdjustLighting(), true);
		}
    }
}
