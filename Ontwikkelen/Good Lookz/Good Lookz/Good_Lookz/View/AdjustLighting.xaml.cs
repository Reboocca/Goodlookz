using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
	public partial class AdjustLighting : ContentPage
	{
		public AdjustLighting()
		{
			InitializeComponent();

			mySlider.Value = Models.LoginCredentials.adjust_lighting;
			btnSave.BackgroundColor = Color.Transparent;
		}

		private async void mySlider_ValueChanged(object sender, EventArgs e)
		{
			//bvLight.Opacity = mySlider.Value;
			await bvLight.FadeTo(mySlider.Value, 300);

			if (mySlider.Value >= 0.8)
			{
				lblText.TextColor = Color.SlateGray;
			}
			else
			{
				lblText.TextColor = Color.White;
			}
		}

		private async void btnSave_Clicked(object sender, EventArgs e)
		{
			Models.LoginCredentials.adjust_lighting = mySlider.Value;
			await this.Navigation.PopAsync();
		}
	}
}
