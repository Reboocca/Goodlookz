using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.WardrobePages
{
    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class WardrobeFriendSelected : ContentPage
    {
        public WardrobeFriendSelected()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            fullNameLbl.Text = Models.SelectedFriendsCredentials.fullName;
        }

        async void Head_Clicked(object sender, EventArgs e)
        {
            var _clothName = btnHead.Text;
            clothType.clothName = _clothName;
            await Navigation.PushAsync(new LendPage(), true);
        }

        async void Top_Clicked(object sender, EventArgs e)
        {
            var _clothName = btnTop.Text;
            clothType.clothName = _clothName;
            await Navigation.PushAsync(new LendPage(), true);
        }

        async void Bottom_Clicked(object sender, EventArgs e)
        {
            var _clothName = btnBottom.Text;
            clothType.clothName = _clothName;
            await Navigation.PushAsync(new LendPage(), true);
        }

        async void Feet_Clicked(object sender, EventArgs e)
        {
            var _clothName = btnFeet.Text;
            clothType.clothName = _clothName;
            await Navigation.PushAsync(new LendPage(), true);
        }
    }
}
