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
	public partial class FilterWardrobe : ContentPage
	{
		public FilterWardrobe()
		{
			InitializeComponent();
		}
		
		//Class voor het opslaan van de JSON gegevens
		class Types
		{
			public string head { get; set; }
			public string top { get; set; }
			public string bottom { get; set; }
			public string feet { get; set; }
		}

        class Found
        {
            public string found { get; set; }
        }

        Models.UserSizes uSize  = new Models.UserSizes();
        List<string> lstColours = new List<string>();

		protected override void OnAppearing()
		{
            //Check of de gebruiker geblokkeerd is
            Models.Settings.Blocked blocked = new Models.Settings.Blocked();
            blocked.checkBlockedAsync();

            if (Models.PreviousPage.page == "wardrobe")
            {
                //Haal de types op vanuit de dbs voor in de picker
                getTypes(Models.LoginCredentials.loginId);
            }
            else
            {
                //CODE VOOR FRIENDS
                getFriendTypes(Models.LoginCredentials.loginId);
            }
        }

		private async void getTypes(string id)
		{
			string webadres = "http://good-lookz.com/API/wardrobe/getFilterOptions.php?";
			string parameters = "users_id=" + id + "&function=types";
			HttpClient connect = new HttpClient();
			HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
			get.EnsureSuccessStatusCode();

			string result = await get.Content.ReadAsStringAsync();
			var jsonResult = JsonConvert.DeserializeObject<List<Types>>(result);

			//Stop de opgehaalde waardes in de picker als de 'true' zijn
			if(jsonResult[0].head == "true")
			{
				pType.Items.Add("Head");
			}

			if (jsonResult[0].top == "true")
			{
				pType.Items.Add("Top");
			}

			if (jsonResult[0].bottom == "true")
			{
				pType.Items.Add("Bottom");
			}

			if (jsonResult[0].feet == "true")
			{
				pType.Items.Add("Feet");
			}

			//Wanneer er geen items gevonden zijn
			if(pType.Items.Count == 0)
			{
				await DisplayAlert("Warning", "There are no clothes found in your wardrobe, please add some beforehand.", "OK");
				await this.Navigation.PopAsync();
			}
		}

		private async void getColours(string type, string id)
		{
            //Haal de opgeslagen kleuren op van het geselecteerde type
			string webadres     = "http://good-lookz.com/API/wardrobe/getFilterOptions.php?";
			string parameters   = "users_id=" + id + "&function=colours&item=" + type;
			HttpClient connect  = new HttpClient();
			HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
			get.EnsureSuccessStatusCode();

			string result       = await get.Content.ReadAsStringAsync();
			string[] colours    = result.Split(',');

            //Stop de kleuren in de picker
			foreach (var colour in colours)
			{
				pColour.Items.Add(colour);
			}
		}

        private async void getFriendTypes(string id)
        {
            await getUserSize();

            string[] arrTypes = new string[] { "head", "top", "bottom", "feet" };
            string[] arrSizes = new string[] { "", uSize.topnr, uSize.botnr, uSize.feetnr };
            int i             = 0;

            while (i <= 3)
            {
                string webadres     = "http://good-lookz.com/API/wardrobeFriends/getFilterFriendOptions.php?";
                string parameters   = "users_id=" + id + "&function=types&type=" + arrTypes[i] + "&size=" + arrSizes[i];
                HttpClient connect  = new HttpClient();
                HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
                get.EnsureSuccessStatusCode();

                string result   = await get.Content.ReadAsStringAsync();
                var jsonResult  = JsonConvert.DeserializeObject<List<Found>>(result);

                if(jsonResult[0].found == "true")
                { 
                    //Stop de opgehaalde waardes in de picker als de 'true' zijn
                    switch (arrTypes[i])
                    {
                        case "head":
                            pType.Items.Add("Head");
                            break;
                        case "top":
                            pType.Items.Add("Top");
                            break;
                        case "bottom":
                            pType.Items.Add("Bottom");
                            break;
                        case "feet":
                            pType.Items.Add("Feet");
                            break;
                    }
                }

                //Next array item
                i++;
            }
            
            //Wanneer er geen items gevonden zijn
            if (pType.Items.Count == 0)
            {
                await DisplayAlert("Warning", "There are no clothes found in your friends wardrobe that are around your size.", "OK");
                await this.Navigation.PopAsync();
            }
        }

        private async void getFriendColours(string type, string id, string size)
        {
            //Haal de opgeslagen kleuren op van het geselecteerde type
            string webadres = "http://good-lookz.com/API/wardrobeFriends/getFilterFriendOptions.php?";
            string parameters = "users_id=" + id + "&function=colours&size=" + size + "&item=" + type;
            HttpClient connect = new HttpClient();
            HttpResponseMessage get = await connect.GetAsync(webadres + parameters);
            get.EnsureSuccessStatusCode();

            string result = await get.Content.ReadAsStringAsync();
            string[] colours = result.Split(',');

            //Stop de kleuren in de picker
            foreach (var colour in colours)
            {
                if(!string.IsNullOrEmpty(colour) && !lstColours.Contains(colour))
                {
                    lstColours.Add(colour);
                    pColour.Items.Add(colour);
                }
            }
        }

        private async Task getUserSize()
        {
            string users_id = Models.LoginCredentials.loginId;
            string url      = "http://good-lookz.com/API/account/getSizes.php?users_id=" + users_id;

            HttpClient get = new HttpClient();
            HttpResponseMessage respons = await get.GetAsync(url);

            if (respons.IsSuccessStatusCode)
            {
                string responsecontent = await respons.Content.ReadAsStringAsync();
                var myobjList   = JsonConvert.DeserializeObject<List<Models.UserSizes>>(responsecontent);
                var myObj       = myobjList[0];

                uSize.region = myObj.region;
                uSize.gender = myObj.gender;
                uSize.topnr  = myObj.topnr;
                uSize.botnr  = myObj.botnr;
                uSize.feetnr = myObj.feetnr;
            }
        }

        private void pType_SelectedIndexChanged(object sender, EventArgs e)
		{
            if(Models.PreviousPage.page == "wardrobe")
            {
                //Als er al items in de colour picker staan, zorg dat deze leeg gemaakt worden
                if (pColour.Items.Count > 0)
                {
                    pColour.Items.Clear();
                }

                //Roep de functie aan om items aan de colour picker toe te voegen
                getColours(pType.Items[pType.SelectedIndex], Models.LoginCredentials.loginId);
            }
            else
            {
                //Als er al items in de colour picker staan, zorg dat deze leeg gemaakt worden
                if (pColour.Items.Count > 0)
                {
                    pColour.Items.Clear();
                    lstColours.Clear();
                }

                //Haal de kleuren van vrienden op
                switch (pType.Items[pType.SelectedIndex])
                {
                    case "Head":
                        getFriendColours(pType.Items[pType.SelectedIndex], Models.LoginCredentials.loginId, "");
                        break;

                    case "Top":
                        getFriendColours(pType.Items[pType.SelectedIndex], Models.LoginCredentials.loginId, uSize.topnr);
                        break;

                    case "Bottom":
                        getFriendColours(pType.Items[pType.SelectedIndex], Models.LoginCredentials.loginId, uSize.botnr);
                        break;

                    case "Feet":
                        getFriendColours(pType.Items[pType.SelectedIndex], Models.LoginCredentials.loginId, uSize.feetnr);
                        break;
                }
            }
		}

		private async void btnFilter_Clicked(object sender, EventArgs e)
		{
            //Check of er een item in beide pickers geselecteerd zijn
			if(pType.SelectedIndex == -1 || pColour.SelectedIndex == -1)
			{
				await DisplayAlert("Warning", "Please select a type and colour before you continue.", "OK");
			}
			else
			{
                if(Models.PreviousPage.page == "wardrobe")
                {
                    //Sla de filtergegevens op a.d.h.v. wat er gekozen is
                    switch (pType.Items[pType.SelectedIndex])
                    {
                        case "Head":
                            Models.Settings.Filter.filterHead.filteron  = true;
                            Models.Settings.Filter.filterHead.colour    = pColour.Items[pColour.SelectedIndex];
                            break;

                        case "Top":
                            Models.Settings.Filter.filterTop.filteron   = true;
                            Models.Settings.Filter.filterTop.colour     = pColour.Items[pColour.SelectedIndex];
                            break;

                        case "Bottom":
                            Models.Settings.Filter.filterBottom.filteron = true;
                            Models.Settings.Filter.filterBottom.colour   = pColour.Items[pColour.SelectedIndex];
                            break;

                        case "Feet":
                            Models.Settings.Filter.filterFeet.filteron  = true;
                            Models.Settings.Filter.filterFeet.colour    = pColour.Items[pColour.SelectedIndex];
                            break;
                    }
                }
                else
                {
                    //Sla de filtergegevens op a.d.h.v. wat er gekozen is
                    switch (pType.Items[pType.SelectedIndex])
                    {
                        case "Head":
                            Models.Settings.FilterFriend.filterHead.filteron    = true;
                            Models.Settings.FilterFriend.filterHead.colour      = pColour.Items[pColour.SelectedIndex];
                            break;

                        case "Top":
                            Models.Settings.FilterFriend.filterTop.filteron     = true;
                            Models.Settings.FilterFriend.filterTop.colour       = pColour.Items[pColour.SelectedIndex];
                            break;

                        case "Bottom":
                            Models.Settings.FilterFriend.filterBottom.filteron  = true;
                            Models.Settings.FilterFriend.filterBottom.colour    = pColour.Items[pColour.SelectedIndex];
                            break;

                        case "Feet":
                            Models.Settings.FilterFriend.filterFeet.filteron    = true;
                            Models.Settings.FilterFriend.filterFeet.colour      = pColour.Items[pColour.SelectedIndex];
                            break;
                    }
                }

                //Terug naar de wardrobe pagina
                await this.Navigation.PopAsync();
            }
		}

        //Reset alle filters
		private async void btnReset_CLicked(object sender, EventArgs e)
		{
            if(Models.PreviousPage.page == "wardrobe")
            {
                //Zet de filters uit
                Models.Settings.Filter.filterHead.filteron      = false;
                Models.Settings.Filter.filterTop.filteron       = false;
                Models.Settings.Filter.filterBottom.filteron    = false;
                Models.Settings.Filter.filterFeet.filteron      = false;
            }
            else
            {
                //Zet de filters uit
                Models.Settings.FilterFriend.filterHead.filteron    = false;
                Models.Settings.FilterFriend.filterTop.filteron     = false;
                Models.Settings.FilterFriend.filterBottom.filteron  = false;
                Models.Settings.FilterFriend.filterFeet.filteron    = false;
            }

            //Terug naar wardrobe pagina
            await this.Navigation.PopAsync();
        }
	}
}
