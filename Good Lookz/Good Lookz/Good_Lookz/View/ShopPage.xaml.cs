using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class ShopPage : ContentPage
    {
        private const string url = "http://good-lookz.com/API/shops/shopsDownload.php?shops_id={0}";
        private HttpClient _client = new HttpClient(new NativeMessageHandler());

        /// <summary>
        /// List maken voor de 3 type rubrieks en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.GetShops> gets_Care = new List<Models.GetShops>();
        List<Models.GetShops> gets_Fashion = new List<Models.GetShops>();
        List<Models.GetShops> gets_Accessories = new List<Models.GetShops>();

        public ShopPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            imageCareLoader.IsRunning        = true;
            imageFashionLoader.IsRunning     = true;
            imageAccessoriesLoader.IsRunning = true;
            imageForSellLoader.IsRunning     = true;

            string shops1_id    = Models.ShopsChosenSaved.shops1_id.ToString();
            string url_Care     = string.Format(url, shops1_id);

            var content_Care    = await _client.GetStringAsync(url_Care);
            gets_Care           = JsonConvert.DeserializeObject<List<Models.GetShops>>(content_Care);

            // ---------------------- //

            string shops2_id    = Models.ShopsChosenSaved.shops2_id.ToString();
            string url_Fashion  = string.Format(url, shops2_id);

            var content_Fashion = await _client.GetStringAsync(url_Fashion);
            gets_Fashion        = JsonConvert.DeserializeObject<List<Models.GetShops>>(content_Fashion);

            // ---------------------- //

            string shops3_id        = Models.ShopsChosenSaved.shops3_id.ToString();
            string url_Accessories  = string.Format(url, shops3_id);

            var content_Accessories = await _client.GetStringAsync(url_Accessories);
            gets_Accessories        = JsonConvert.DeserializeObject<List<Models.GetShops>>(content_Accessories);

            // ---------------------- //

            imageCare.Source                    = gets_Care[0].picture;
            imageCareLoader.IsRunning           = false;
            imageFashion.Source                 = gets_Fashion[0].picture;
            imageFashionLoader.IsRunning        = false;
            imageAccessories.Source             = gets_Accessories[0].picture;
            imageAccessoriesLoader.IsRunning    = false;
            imageForSell.Source                 = "fashion.png";
            imageForSellLoader.IsRunning        = false;

            imageCare.BindingContext        = gets_Care[0].url;
            imageFashion.BindingContext     = gets_Fashion[0].url;
            imageAccessories.BindingContext = gets_Accessories[0].url;

            lblCare.Text        = gets_Care[0].name;
            lblFashion.Text     = gets_Fashion[0].name;
            lblAccessories.Text = gets_Accessories[0].name;
            lblForSell.Text     = "Used Clothes";

            base.OnAppearing();
        }

        async void imageCare_Tapped(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Redirect", "Visit " + lblCare.Text + " shop?", "Yes", "No");
            if (answer)
                Device.OpenUri(new Uri(imageCare.BindingContext.ToString()));
        }

        async void imageFashion_Tapped(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Redirect", "Visit " + lblFashion.Text + " shop?", "Yes", "No");
            if (answer)
                Device.OpenUri(new Uri(imageFashion.BindingContext.ToString()));
        }

        async void imageAccessories_Tapped(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Redirect", "Visit " + lblAccessories.Text + " shop?", "Yes", "No");
            if (answer)
                Device.OpenUri(new Uri(imageAccessories.BindingContext.ToString()));
        }

        async void imageForSell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShopPages.UsedClothesPage(), true);
        }

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();

            gets_Care.Clear();
            gets_Fashion.Clear();
            gets_Accessories.Clear();
            GC.Collect();
        }
    }
}
