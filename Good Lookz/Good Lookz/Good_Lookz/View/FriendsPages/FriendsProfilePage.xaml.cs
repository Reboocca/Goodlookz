using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.FriendsPages
{
    public partial class FriendsProfilePage : ContentPage
    {
        public FriendsProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            lblUsername.Text    = Models.SelectedFriendsCredentials.username;
            lblDesc.Text        = Models.SelectedFriendsCredentials.description;
            imPicture.Source    = Models.SelectedFriendsCredentials.picture;
        }

        async void View_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WardrobePages.WardrobeFriendSelected(), true);
        }

    }
}
