using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class SettingsPage : ContentPage
    {
        async void Logout_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Logout?", "Are you sure?", "Yes", "No");

            if (answer == true)
            {
                if (Application.Current.Properties.ContainsKey("loginId"))
                    Application.Current.Properties.Remove("loginId");
                if (Application.Current.Properties.ContainsKey("loginUsername"))
                    Application.Current.Properties.Remove("loginUsername");
                if (Application.Current.Properties.ContainsKey("loginPassword"))
                    Application.Current.Properties.Remove("loginPassword");
                if (Application.Current.Properties.ContainsKey("loginFirstname"))
                    Application.Current.Properties.Remove("loginFirstname");
                if (Application.Current.Properties.ContainsKey("loginLastname"))
                    Application.Current.Properties.Remove("loginLastname");
                if (Application.Current.Properties.ContainsKey("loginEmail"))
                    Application.Current.Properties.Remove("loginEmail");
                if (Application.Current.Properties.ContainsKey("loginDate"))
                    Application.Current.Properties.Remove("loginDate");
                if (Application.Current.Properties.ContainsKey("loginGender"))
                    Application.Current.Properties.Remove("loginGender");
                if (Application.Current.Properties.ContainsKey("loginOffline"))
                    Application.Current.Properties.Remove("loginOffline");
                if (Application.Current.Properties.ContainsKey("loginActive"))
                    Application.Current.Properties.Remove("loginActive");

                wardrobeItems.noItems = false;
                App.Current.MainPage = new NavigationPage(new SignPage());
            }
        }

        /// <summary>
        /// Instellingen worden opgeslagen in Application properties. 
        /// </summary>
        void OnChange1(object sender, System.EventArgs e)
        {
            Application.Current.Properties["friendSwitch"] = friendSwitch.On;
        }
        void OnChange2(object sender, System.EventArgs e)
        {
            Application.Current.Properties["sellSwitch"] = sellSwitch.On;
        }
        void OnChange3(object sender, System.EventArgs e)
        {
            Application.Current.Properties["requestSwitch"] = requestSwitch.On;
        }
        void OnChange4(object sender, System.EventArgs e)
        {
            Application.Current.Properties["vibrationSwitch"] = vibrationSwitch.On;
        }

        public SettingsPage()
        {
            InitializeComponent();
            btnLogout.BackgroundColor = Color.Transparent;

            if (Application.Current.Properties.ContainsKey("friendSwitch"))
                friendSwitch.On = (bool) Application.Current.Properties["friendSwitch"];

            if (Application.Current.Properties.ContainsKey("sellSwitch"))
                sellSwitch.On = (bool)Application.Current.Properties["sellSwitch"];

            if (Application.Current.Properties.ContainsKey("requestSwitch"))
                requestSwitch.On = (bool)Application.Current.Properties["requestSwitch"];

            if (Application.Current.Properties.ContainsKey("vibrationSwitch"))
                vibrationSwitch.On = (bool)Application.Current.Properties["vibrationSwitch"];

            var loginName = Models.LoginCredentials.loginUsername;
            var loginPassword = Models.LoginCredentials.loginPassword;

            UsrOrMail.Text = loginName;
            Password.Text = loginPassword;

            slider.Maximum = 400;
            slider.Minimum = 0;
            string distance = "30";

            if (Application.Current.Properties.ContainsKey("distance"))
                distance = (string)Application.Current.Properties["distance"];

            slider.Value = Double.Parse(distance);
        }

        void Handle_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var distance = slider.Value.ToString();

            int index = distance.IndexOf(".");
            if (index > 0)
                distance = distance.Substring(0, index);

            lblDistance.Text = distance;

            Application.Current.Properties["distance"] = distance;
        }
    }
}
