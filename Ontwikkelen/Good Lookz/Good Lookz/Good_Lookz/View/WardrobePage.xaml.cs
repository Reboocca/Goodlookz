using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
    /// <summary>
    /// Values dat opgeslagen moet worden in een class.
    /// </summary>
    class wardrobeItems
    {
        private static bool _noItems = false;

        public static bool noItems
        {
            get { return _noItems; }
            set { _noItems = value; }
        }
    }

    /// <summary>
    /// Code behind voor deze page (backend)
    /// </summary>
    public partial class WardrobePage : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());

        /// <summary>
        /// List maken voor de 4 type clothes en later pas invullen met data.
        /// Dit moet op deze manier zodat er dan de Clear functie uitgevoerd kan worden.
        /// </summary>
        List<Models.WardrobeHead> gets_Head = new List<Models.WardrobeHead>();
        List<Models.WardrobeTop> gets_Top = new List<Models.WardrobeTop>();
        List<Models.WardrobeBottom> gets_Bottom = new List<Models.WardrobeBottom>();
        List<Models.WardrobeFeet> gets_Feet = new List<Models.WardrobeFeet>();

        public WardrobePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
			emptySelectedClothing();

            loadingHead.IsRunning	= true;
            loadingTop.IsRunning	= true;
            loadingBottom.IsRunning = true;
            loadingFeet.IsRunning	= true;

			await getHeads();
			await getTops();
			await getBottoms();
			await getFeet();

			base.OnAppearing();

            var hasHead		= gets_Head.Count;
            var hasTop		= gets_Top.Count;
            var hasBottom	= gets_Bottom.Count;
            var hasFeet		= gets_Feet.Count;

            if ((hasHead == 0) && (hasTop == 0) && (hasBottom == 0) && (hasFeet == 0))
            {
                var _noItems = true;
                wardrobeItems.noItems = _noItems;

				await DisplayAlert("Hello!", "Looks like your wardrobe is empty.\n\nLet's start by adding some clothing. \n(the 'add' button will let you add clothes.)", "OK");
			}
        }

		private async Task getHeads()
		{
			if (Models.Settings.Filter.filterHead.filteron)
			{
				string url = "http://good-lookz.com/API/wardrobe/getFilterResults.php?users_id=" + Models.LoginCredentials.loginId + "&type=Head&colour=" + Models.Settings.Filter.filterHead.colour;

				var content_Head = await _client.GetStringAsync(url);
				gets_Head = JsonConvert.DeserializeObject<List<Models.WardrobeHead>>(content_Head);

				loadingHead.IsRunning = false;
				loadingHead.IsVisible = false;

				try
				{
					// List vullen met de opgehaalde data
					CarouselViewHead.ItemsSource = gets_Head;
				}
				catch
				{

				}
			}
			else
			{
				string Url_Head = "http://good-lookz.com/API/wardrobe/head/headDownload.php?users_id={0}";

				string data_Head = Models.LoginCredentials.loginId;

				string urlFilled_Head = string.Format(Url_Head, data_Head);

				var content_Head = await _client.GetStringAsync(urlFilled_Head);
				gets_Head = JsonConvert.DeserializeObject<List<Models.WardrobeHead>>(content_Head);

				loadingHead.IsRunning = false;
				loadingHead.IsVisible = false;

				try
				{
					// List vullen met de opgehaalde data
					CarouselViewHead.ItemsSource = gets_Head;
				}
				catch
				{

				}
			}
			
		}

		private async Task getTops()
		{
			if (Models.Settings.Filter.filterTop.filteron)
			{
				string url = "http://good-lookz.com/API/wardrobe/getFilterResults.php?users_id=" + Models.LoginCredentials.loginId + "&type=Top&colour=" + Models.Settings.Filter.filterTop.colour;
			
				var content_Top = await _client.GetStringAsync(url);
				gets_Top = JsonConvert.DeserializeObject<List<Models.WardrobeTop>>(content_Top);

				loadingTop.IsRunning = false;
				loadingTop.IsVisible = false;

				try
				{
					// List vullen met de opgehaalde data
					CarouselViewTop.ItemsSource = gets_Top;
				}
				catch
				{

				}

			}
			else
			{
				string Url_Top = "http://good-lookz.com/API/wardrobe/top/topDownload.php?users_id={0}&size={1}";

				string data_Top = Models.LoginCredentials.loginId;
				string size_Top = "";

				string urlFilled_Top = string.Format(Url_Top, data_Top, size_Top);

				var content_Top = await _client.GetStringAsync(urlFilled_Top);
				gets_Top = JsonConvert.DeserializeObject<List<Models.WardrobeTop>>(content_Top);

				loadingTop.IsRunning = false;
				loadingTop.IsVisible = false;

				try
				{
					// List vullen met de opgehaalde data
					CarouselViewTop.ItemsSource = gets_Top;
				}
				catch
				{

				}
			}
		}

		private async Task getBottoms()
		{
			if (Models.Settings.Filter.filterBottom.filteron)
			{
				string url = "http://good-lookz.com/API/wardrobe/getFilterResults.php?users_id=" + Models.LoginCredentials.loginId + "&type=Bottom&colour=" + Models.Settings.Filter.filterBottom.colour;

				var content_Bottom = await _client.GetStringAsync(url);
				gets_Bottom = JsonConvert.DeserializeObject<List<Models.WardrobeBottom>>(content_Bottom);

				loadingBottom.IsRunning = false;
				loadingBottom.IsVisible = false;

				try
				{
					// List vullen met de opgehaalde data
					CarouselViewBottom.ItemsSource = gets_Bottom;
				}
				catch
				{

				}
			}
			else
			{
				string Url_Bottom = "http://good-lookz.com/API/wardrobe/bottom/bottomDownload.php?users_id={0}&size={1}";

				string data_Bottom = Models.LoginCredentials.loginId;
				string size_Bottom = "";

				string urlFilled_Bottom = string.Format(Url_Bottom, data_Bottom, size_Bottom);

				var content_Bottom = await _client.GetStringAsync(urlFilled_Bottom);
				gets_Bottom = JsonConvert.DeserializeObject<List<Models.WardrobeBottom>>(content_Bottom);

				loadingBottom.IsRunning = false;
				loadingBottom.IsVisible = false;

				try
				{
					// List vullen met de opgehaalde data
					CarouselViewBottom.ItemsSource = gets_Bottom;
				}
				catch
				{

				}
			}
			
		}

		private async Task getFeet()
		{
			if (Models.Settings.Filter.filterFeet.filteron)
			{
				string url = "http://good-lookz.com/API/wardrobe/getFilterResults.php?users_id=" + Models.LoginCredentials.loginId + "&type=Feet&colour=" + Models.Settings.Filter.filterFeet.colour;

				var content_Feet = await _client.GetStringAsync(url);
				gets_Feet = JsonConvert.DeserializeObject<List<Models.WardrobeFeet>>(content_Feet);

				loadingFeet.IsRunning = false;
				loadingFeet.IsVisible = false;

				try
				{
					// List vullen met de opgehaalde data
					CarouselViewFeet.ItemsSource = gets_Feet;
				}
				catch
				{

				}
			}
			else
			{
				string Url_Feet = "http://good-lookz.com/API/wardrobe/feet/feetDownload.php?users_id={0}&size={1}";

				string data_Feet = Models.LoginCredentials.loginId;
				string size_Feet = "";

				string urlFilled_Feet = string.Format(Url_Feet, data_Feet, size_Feet);

				var content_Feet = await _client.GetStringAsync(urlFilled_Feet);
				gets_Feet = JsonConvert.DeserializeObject<List<Models.WardrobeFeet>>(content_Feet);

				loadingFeet.IsRunning = false;
				loadingFeet.IsVisible = false;

				try
				{
					// List vullen met de opgehaalde data
					CarouselViewFeet.ItemsSource = gets_Feet;
				}
				catch
				{

				}
			}
		}


		//Cleared alle selecteditems, zodat het outfit opslaan goed gaat
		private void emptySelectedClothing()
		{
			Models.SelectedHead.Clear();
			Models.SelectedTop.Clear();
			Models.SelectedBottom.Clear();
			Models.SelectedFeet.Clear();
		}

		void CarouselViewHead_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.WardrobeHead SelectedHead = (Models.WardrobeHead)e.SelectedItem;

            var _head_id = SelectedHead.head_id;
            var _users_id = SelectedHead.users_id;
            var _picture = SelectedHead.picture;
            var _color = SelectedHead.color;
            var _date = SelectedHead.date;

            Models.SelectedHead.head_id		= _head_id.ToString();
            Models.SelectedHead.users_id	= _users_id.ToString();
            Models.SelectedHead.picture		= _picture;
            Models.SelectedHead.color		= _color;
            Models.SelectedHead.date		= _date;
        }
        void CarouselViewTop_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.WardrobeTop SelectedTop = (Models.WardrobeTop)e.SelectedItem;

            var _top_id = SelectedTop.top_id;
            var _users_id = SelectedTop.users_id;
            var _picture = SelectedTop.picture;
            var _color = SelectedTop.color;
            var _date = SelectedTop.date;
            var _size = SelectedTop.size;

            Models.SelectedTop.top_id		= _top_id.ToString();
            Models.SelectedTop.users_id		= _users_id.ToString();
            Models.SelectedTop.picture		= _picture;
            Models.SelectedTop.color		= _color;
            Models.SelectedTop.date			= _date;
            Models.SelectedTop.size			= _size;
        }
        void CarouselViewBottom_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.WardrobeBottom SelectedBottom = (Models.WardrobeBottom)e.SelectedItem;

            var _bottom_id = SelectedBottom.bottom_id;
            var _users_id = SelectedBottom.users_id;
            var _picture = SelectedBottom.picture;
            var _color = SelectedBottom.color;
            var _date = SelectedBottom.date;
            var _size = SelectedBottom.size;

            Models.SelectedBottom.bottom_id = _bottom_id.ToString();
            Models.SelectedBottom.users_id	= _users_id.ToString();
            Models.SelectedBottom.picture	= _picture;
            Models.SelectedBottom.color		= _color;
            Models.SelectedBottom.date		= _date;
            Models.SelectedBottom.size		= _size;
        }
        void CarouselViewFeet_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.WardrobeFeet SelectedFeet = (Models.WardrobeFeet)e.SelectedItem;

            var _feet_id = SelectedFeet.feet_id;
            var _users_id = SelectedFeet.users_id;
            var _picture = SelectedFeet.picture;
            var _color = SelectedFeet.color;
            var _date = SelectedFeet.date;
            var _size = SelectedFeet.size;

            Models.SelectedFeet.feet_id		= _feet_id.ToString();
            Models.SelectedFeet.users_id	= _users_id.ToString();
            Models.SelectedFeet.picture		= _picture;
            Models.SelectedFeet.color		= _color;
            Models.SelectedFeet.date		= _date;
            Models.SelectedFeet.size		= _size;
        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WardrobePages.WardrobeAdd(), true);
        }

        async void Sets_Clicked(object sender, EventArgs e)
        {
            if (wardrobeItems.noItems == true)
            {
                await DisplayAlert("Wait!", "Add atleast one clothing so you can continue.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new WardrobePages.WardrobeSets(), true);
            }
        }

		private async void btnFilter_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new WardrobePages.FilterWardrobe(), true);
		}
		
		async void SetSave_Clicked(object sender, EventArgs e)
        {
            if (wardrobeItems.noItems == true)
            {
                await DisplayAlert("Wait!", "Add atleast one clothing so you can continue.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new WardrobePages.WardrobeSaveSet(), true);
            }
        }

        async void Friends_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WardrobePages.WardrobeChoose(), true);
        }

        async void headTapped(object sender, EventArgs e)
        {
            var _typeCloth = 1;
            WardrobePages.selectedType.typeCloth = _typeCloth;

            await Navigation.PushAsync(new WardrobePages.WardrobeSelectedItem(), true);
        }

        async void topTapped(object sender, EventArgs e)
        {
            var _typeCloth = 2;
            WardrobePages.selectedType.typeCloth = _typeCloth;

            await Navigation.PushAsync(new WardrobePages.WardrobeSelectedItem(), true);
        }

        async void bottomTapped(object sender, EventArgs e)
        {
            var _typeCloth = 3;
            WardrobePages.selectedType.typeCloth = _typeCloth;

            await Navigation.PushAsync(new WardrobePages.WardrobeSelectedItem(), true);
        }

        async void feetTapped(object sender, EventArgs e)
        {
            var _typeCloth = 4;
            WardrobePages.selectedType.typeCloth = _typeCloth;

            await Navigation.PushAsync(new WardrobePages.WardrobeSelectedItem(), true);
        }

        async void Selling_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WardrobePages.WardrobeSelling(), true);
        }

		async void Lending_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new WardrobePages.LendingListPage(), true);
		}

		protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            gets_Head.Clear();
            gets_Top.Clear();
            gets_Bottom.Clear();
            gets_Feet.Clear();
            GC.Collect();
        }
    }
} 