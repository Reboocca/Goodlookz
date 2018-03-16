using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View.WardrobePages
{
    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class WardrobeSelectedSaleRequests : ContentPage
    {
        public WardrobeSelectedSaleRequests()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
			lbBidderUsername.Text	= Models.SelectedSaleRequests.username;
            imageItem.Source		= Models.SelectedSaleRequests.picture;
            lblPrice.Text			= Models.SelectedSaleRequests.price;
            lblBidding.Text			= Models.SelectedSaleRequests.bidding;
            lblUsrname_buyer.Text	= Models.SelectedSaleRequests.name;
			btnContact.Text			= "Start communicating with " + Models.SelectedSaleRequests.username;
        }

        async void btnContact_Clicked(object sender, EventArgs e)
        {
			Models.PreviousPage.page = "WardrobeSelectedSaleRequests";
			await Navigation.PushAsync(new WardrobeContact(), true);
		}

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();

			///Content = null momenteel weggehaald om terug navigatie te kunnen gebruiken bij het contactformulier
            //Content = null;
            GC.Collect();
        }
    }
}
