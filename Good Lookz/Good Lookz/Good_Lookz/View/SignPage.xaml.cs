using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
    public partial class SignPage : ContentPage
    {

        public SignPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            DateTime dt = DateTime.Now;
            lblFooter.Text = dt.Year + " © 4People Communications";

        }

            async void LoginClicked(object sender, EventArgs e)
        {
            // Navigatie van pagina's
            await Navigation.PushAsync(new SignPages.LoginPage(), true);
            //App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        async void CreateClicked(object sender, EventArgs e)
        {
            // Navigatie van pagina's
            await Navigation.PushAsync(new SignPages.CreatePage(), true);
            //App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}