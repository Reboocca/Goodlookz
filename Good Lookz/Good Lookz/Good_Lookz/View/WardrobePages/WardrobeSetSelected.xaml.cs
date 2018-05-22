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
    public partial class WardrobeSetSelected : ContentPage
    {
        private HttpClient _client = new HttpClient(new NativeMessageHandler());

        public WardrobeSetSelected()
        {
            InitializeComponent();

            labelName.Text = Models.WardrobeSetsItem.name;
        }

        protected override async void OnAppearing()
        {
            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            loadingHead.IsRunning = true;
            loadingTop.IsRunning = true;
            loadingBottom.IsRunning = true;
            loadingFeet.IsRunning = true;

            string Url_Head = "http://good-lookz.com/API/wardrobe/head/headDownload.php?head_id={0}";

            string data_Head = Models.WardrobeSetsItem.head_id;

            string urlFilled_Head = string.Format(Url_Head, data_Head);

            var content_Head = await _client.GetStringAsync(urlFilled_Head);
            var gets_Head = JsonConvert.DeserializeObject<List<Models.WardrobeHead>>(content_Head);

            loadingHead.IsRunning = false;
            if (gets_Head.Count != 0)
                imageHead.Source = gets_Head[0].picture;

            // ---------------------------

            string Url_Top = "http://good-lookz.com/API/wardrobe/top/topDownload.php?top_id={0}&size={1}";

            string data_Top = Models.WardrobeSetsItem.top_id;
            string size_Top = "";

            string urlFilled_Top = string.Format(Url_Top, data_Top, size_Top);

            var content_Top = await _client.GetStringAsync(urlFilled_Top);
            var gets_Top = JsonConvert.DeserializeObject<List<Models.WardrobeTop>>(content_Top);

            loadingTop.IsRunning = false;
            if (gets_Top.Count != 0)
                imageTop.Source = gets_Top[0].picture;

            // ---------------------------

            string Url_Bottom = "http://good-lookz.com/API/wardrobe/bottom/bottomDownload.php?bottom_id={0}&size={1}";

            string data_Bottom = Models.WardrobeSetsItem.bottom_id;
            string size_Bottom = "";

            string urlFilled_Bottom = string.Format(Url_Bottom, data_Bottom, size_Bottom);

            var content_Bottom = await _client.GetStringAsync(urlFilled_Bottom);
            var gets_Bottom = JsonConvert.DeserializeObject<List<Models.WardrobeBottom>>(content_Bottom);

            loadingBottom.IsRunning = false;
            if (gets_Bottom.Count != 0)
                imageBottom.Source = gets_Bottom[0].picture;

            // ---------------------------

            string Url_Feet = "http://good-lookz.com/API/wardrobe/feet/feetDownload.php?feet_id={0}&size={1}";

            string data_Feet = Models.WardrobeSetsItem.feet_id;
            string size_Feet = "";

            string urlFilled_Feet = string.Format(Url_Feet, data_Feet, size_Feet);

            var content_Feet = await _client.GetStringAsync(urlFilled_Feet);
            var gets_Feet = JsonConvert.DeserializeObject<List<Models.WardrobeFeet>>(content_Feet);

            loadingFeet.IsRunning = true;
            if (gets_Feet.Count != 0)
                imageFeet.Source = gets_Feet[0].picture;

            // ---------------------------

            loadingHead.IsVisible = false;
            loadingTop.IsVisible = false;
            loadingBottom.IsVisible = false;
            loadingFeet.IsVisible = false;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            /// Tijdens het afsluiten van de pagina wordt dit uitgevoerd. 
            /// Clear alle opgeslagen data in het List.
            /// Dit zorgt ervoor dat er geen java error tevoorschijn komt.
            base.OnDisappearing();
            Content = null;
            GC.Collect();
        }
    }
}
