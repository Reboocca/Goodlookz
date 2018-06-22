using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
    public class fromPage
    {
        private static int _id;

        public static int id
        {
            get { return _id; }
            set { _id = value; }
        }
    }

    public partial class TermsConditionsPage : ContentPage
    {
        const string sourceTerms = "http://www.good-lookz.com/articles/index.php";

        public TermsConditionsPage()
        {
            InitializeComponent();

            webViewTest.Source = sourceTerms;
        }

        protected override bool OnBackButtonPressed()
        {
            webViewTest.Source = sourceTerms;
            return true;
        }

        private async void Understood_Clicked(object sender, EventArgs e)
        {
            var pageID = fromPage.id;

            switch (pageID)
            {
                case 0:
                    await this.Navigation.PopAsync();
                    break;
                case 1:
                    await this.Navigation.PopAsync();
                    break;
                default:
                    break;
            }
        }
    }
}
