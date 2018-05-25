using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
            //Haal permissie op voor de camera functie
            getPermission();


            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            btnAdjust.BackgroundColor = Color.Transparent;
			changeLightIntensitivity(Models.Settings.Mirror.adjust_lighting);
		}

        private async void getPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await DisplayAlert("Permission request", "In order for the app to use the camera, permission must first be granted", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
                    status      = results[Permission.Camera];
                }

            }
            catch (Exception)
            {

            }
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
