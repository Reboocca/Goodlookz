using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.WardrobePages
{
    public partial class WardrobeChoose : ContentPage
    {
        public WardrobeChoose()
        {
            InitializeComponent();

            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();
        }

        async void AllClothes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WardrobeAllClothes(), true);
        }

        async void Specific_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WardrobeFriends(), true);
        }
    }
}
