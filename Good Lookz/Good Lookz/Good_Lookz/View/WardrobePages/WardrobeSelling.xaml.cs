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

namespace Good_Lookz.View.WardrobePages
{
    /// <summary>
    /// JSON response word opgevangen en in een list met variables gestopt.
    /// </summary>
    class saleDelete
    {
        public bool sale_delete { get; set; }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class WardrobeSelling : ContentPage
    {
        private const string url = "http://www.good-lookz.com/API/sale/saleDownload.php?id={0}";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Models.SaleList> _gets;

        public WardrobeSelling()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                string data = Models.LoginCredentials.loginId;

                string URL = string.Format(url, data);

                var content = await client.GetStringAsync(URL);
                var response = JsonConvert.DeserializeObject<List<Models.SaleList>>(content);

                _gets = new ObservableCollection<Models.SaleList>(response);

                sales.ItemsSource = _gets;
            }
            catch
            {
                await DisplayAlert("Message", "First time here? \nTry setting items for sale from your wardrobe to get started.", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        async void Sales_Tapped(object sender, ItemTappedEventArgs e)
        {
			Models.SaleList select = (Models.SaleList)e.Item;

			//Check welk type het is en vul het id en clothid in
			if (!string.IsNullOrEmpty(select.head_id))
			{
				Models.SelectedSaleList.clothID	= "1";
				Models.SelectedSaleList.item_id	= select.head_id;
			}
			else if(!string.IsNullOrEmpty(select.top_id))
			{
				Models.SelectedSaleList.clothID	= "2";
				Models.SelectedSaleList.item_id	= select.top_id;
			}
			else if (!string.IsNullOrEmpty(select.bottom_id))
			{
				Models.SelectedSaleList.clothID	= "3";
				Models.SelectedSaleList.item_id = select.bottom_id;
			}
			else if (!string.IsNullOrEmpty(select.feet_id))
			{
				Models.SelectedSaleList.clothID	= "4";
				Models.SelectedSaleList.item_id	= select.feet_id;
			}

			//Vul de overige gegevens in die nodig zijn op de volgende pagina
			Models.SelectedSaleList.picture			= select.picture;
			Models.SelectedSaleList.sale_id			= select.sale_id;
			Models.SelectedSaleList.desc			= select.desc;
			Models.SelectedSaleList.price			= select.price;
			Models.SelectedSaleList.sale_city		= select.sale_city;

			await Navigation.PushAsync(new WardrobeSelectedSaleItem(), true);
		}

        async void Delete_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
			var sale_id = item.CommandParameter.ToString();

			try
			{
				string webadres = "http://www.good-lookz.com/API/sale/saleDelete.php?sale_id=" + sale_id;
				HttpClient connect = new HttpClient();
				HttpResponseMessage deleteItemSale = await connect.GetAsync(webadres);
				deleteItemSale.EnsureSuccessStatusCode();

				string result = await deleteItemSale.Content.ReadAsStringAsync();
				if (result == "Delete function success.")
				{
					// Delete item van ObserveAbleCollection
					foreach (var items in _gets.ToList())
					{
						if (items.sale_id == sale_id)
						{
							_gets.Remove(items);
						}
					}
				}
				else if (result == "Delete function failed.")
				{
					await DisplayAlert("Error", "Unable delete the item from sale, please try again later.", "OK");
				}
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Unable to connect with our servers.", "OK");
			}			
        }

        async void SaleRequests_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WardrobeSaleRequests(), true);
        }

        //protected override void OnDisappearing()
        //{
        //    Content = null;
        //}
    }
}
