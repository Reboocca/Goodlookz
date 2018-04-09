using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Good_Lookz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            var counter = 0;

            var _loginId = "";
            var _loginUsername = "";
            var _loginPassword = "";
            var _loginFirstname = "";
            var _loginLastname = "";
            var _loginEmail = "";
            var _loginDate = "";
            var _loginGender = "";
            var _loginOffline = "";
            var _loginActive = "";
            var _shops1_id = 0;
            var _shops2_id = 0;
            var _shops3_id = 0;

            if (Application.Current.Properties.ContainsKey("loginId"))
            {
                _loginId = (string)Application.Current.Properties["loginId"];
                counter++;
            }
                
            if (Application.Current.Properties.ContainsKey("loginUsername"))
            {
                _loginUsername = (string)Application.Current.Properties["loginUsername"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("loginPassword"))
            {
                _loginPassword = (string)Application.Current.Properties["loginPassword"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("loginFirstname"))
            {
                _loginFirstname = (string)Application.Current.Properties["loginFirstname"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("loginLastname"))
            {
                _loginLastname = (string)Application.Current.Properties["loginLastname"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("loginEmail"))
            {
                _loginEmail = (string)Application.Current.Properties["loginEmail"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("loginDate"))
            {
                _loginDate = (string)Application.Current.Properties["loginDate"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("loginGender"))
            {
                _loginGender = (string)Application.Current.Properties["loginGender"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("loginOffline"))
            {
                _loginOffline = (string)Application.Current.Properties["loginOffline"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("loginActive"))
            {
                _loginActive = (string)Application.Current.Properties["loginActive"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("shops1_id"))
            {
                _shops1_id = (int)Application.Current.Properties["shops1_id"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("shops2_id"))
            {
                _shops2_id = (int)Application.Current.Properties["shops2_id"];
                counter++;
            }

            if (Application.Current.Properties.ContainsKey("shops3_id"))
            {
                _shops3_id = (int)Application.Current.Properties["shops3_id"];
                counter++;
            }

            if (counter == 13)
            {
                MainPage = new NavigationPage(new View.MenuPage());

                Models.LoginCredentials.loginId = _loginId;
                Models.LoginCredentials.loginUsername = _loginUsername;
                Models.LoginCredentials.loginPassword = _loginPassword;
                Models.LoginCredentials.loginFirstname = _loginFirstname;
                Models.LoginCredentials.loginLastname = _loginLastname;
                Models.LoginCredentials.loginEmail = _loginEmail;
                Models.LoginCredentials.loginDate = _loginDate;
                Models.LoginCredentials.loginGender = _loginGender;
                Models.LoginCredentials.loginOffline = _loginOffline;
                Models.LoginCredentials.loginActive = _loginActive;
                Models.ShopsChosenSaved.shops1_id = _shops1_id;
                Models.ShopsChosenSaved.shops2_id = _shops2_id;
                Models.ShopsChosenSaved.shops3_id = _shops3_id;
            }
            else
            {
                MainPage = new NavigationPage(new View.SignPage());
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
