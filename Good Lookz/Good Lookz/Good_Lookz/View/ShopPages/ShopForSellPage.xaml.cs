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

namespace Good_Lookz.View.ShopPages
{
    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class ShopForSellPage : ContentPage
    {
        private const string url = "http://www.good-lookz.com/API/sale/saleDownload.php?users_id={0}";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.SaleList> _gets;

        /// <summary>
        /// List maken en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.SaleList> response = new List<Models.SaleList>();

        public ShopForSellPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            string data = Models.LoginCredentials.loginId;

            string URL = string.Format(url, data);

            var content = await client.GetStringAsync(URL);
            response = JsonConvert.DeserializeObject<List<Models.SaleList>>(content);

            _gets = new ObservableCollection<Models.SaleList>(response);

            sales.ItemsSource = _gets;
        }

        async void Sales_Tapped(object sender, ItemTappedEventArgs e)
        {
            Models.SaleList item = (Models.SaleList)e.Item;

            Models.SelectedSaleList.sale_id		= item.sale_id;
            Models.SelectedSaleList.users_id	= item.users_id;
            Models.SelectedSaleList.head_id		= item.head_id;
			Models.SelectedSaleList.top_id		= item.top_id;
            Models.SelectedSaleList.bottom_id	= item.bottom_id;
			Models.SelectedSaleList.feet_id		= item.feet_id;
			Models.SelectedSaleList.size		= item.size;
			Models.SelectedSaleList.username	= item.username;
			Models.SelectedSaleList.picture		= item.picture;
			Models.SelectedSaleList.price		= item.price;
            Models.SelectedSaleList.desc		= item.desc;
            Models.SelectedSaleList.fullPrice	= item.fullPrice;

            await Navigation.PushAsync(new SelectedSaleItemPage(), true);
        }

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            response.Clear();
            GC.Collect();
        }
    }
}
