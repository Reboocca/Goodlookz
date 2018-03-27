using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
	class imageUpload
	{
		public bool img_upload { get; set; }
	}

	/// <summary>
	/// Code behind voor deze page (backend)
	/// </summary>
	public partial class WardrobeAdd : ContentPage
	{
		private MediaFile _mediaFile;
		HttpClient client = new HttpClient(new NativeMessageHandler());
		string color = "";

		//Globale variabelen
		List<Models.Sizes.AllSizes> lstSizes = new List<Models.Sizes.AllSizes>();
		Models.UserSizes uSize = new Models.UserSizes();

		string _Region = "EU";
		string _Gender = "F";

		public WardrobeAdd()
		{
			InitializeComponent();

			loadingPic.IsVisible = false;
			topSize.IsVisible = false;
			bottomSize.IsVisible = false;
			feetSize.IsVisible = false;

			//Sla alle verschillende maten op in de list die boven is aangemaakt
			lstSizes = Models.Sizes.getAllSizesList();

			//Haal gebruiker gegevens op
			getUserSize();
		}

		//Sla de opgehaalde gebruiker gegevens op in het object
		private async void getUserSize()
		{
			string users_id = Models.LoginCredentials.loginId;
			string url = "http://good-lookz.com/API/account/getSizes.php?users_id=" + users_id;

			HttpClient get = new HttpClient();
			HttpResponseMessage respons = await get.GetAsync(url);

			if (respons.IsSuccessStatusCode)
			{
				string responsecontent = await respons.Content.ReadAsStringAsync();
				var myobjList = JsonConvert.DeserializeObject<List<Models.UserSizes>>(responsecontent);
				var myObj = myobjList[0];

				uSize.region = myObj.region;
				uSize.gender = myObj.gender;
				uSize.topnr = myObj.topnr;
				uSize.botnr = myObj.botnr;
				uSize.feetnr = myObj.feetnr;
			}

			//Zet de pickers op de goede waardes
			switch (uSize.region)
			{
				case "EU":
					cbRegion.SelectedIndex = 0;
					break;

				case "US":
					cbRegion.SelectedIndex = 1;
					break;

				case "UK":
					cbRegion.SelectedIndex = 2;
					break;

				default:
					cbRegion.SelectedIndex = 0;
					break;
			}

			switch (uSize.gender)
			{
				case "F":
					cbGender.SelectedIndex = 0;
					break;

				case "M":
					cbGender.SelectedIndex = 1;
					break;

				default:
					cbGender.SelectedIndex = 0;
					break;
			}
		}

		//Vul de pickers in a.d.h.v. de gekozen waardes
		private void FillPickers(string g, string r)
		{
			topSize.Items.Clear();
			bottomSize.Items.Clear();
			feetSize.Items.Clear();


			foreach (Models.Sizes.AllSizes i in lstSizes)
			{
				if (i.sGender == g && i.sRegion == r && i.sType == "top")
				{
					topSize.Items.Add(i.sSize);
				}
			}

			foreach (Models.Sizes.AllSizes i in lstSizes)
			{
				if (i.sGender == g && i.sRegion == r && i.sType == "bottom")
				{
					bottomSize.Items.Add(i.sSize);
				}
			}

			foreach (Models.Sizes.AllSizes i in lstSizes)
			{
				if (i.sGender == g && i.sRegion == r && i.sType == "feet")
				{
					feetSize.Items.Add(i.sSize);
				}
			}
		}

		/// <summary>
		/// Foto kiezen uit galerij. Als er geen galerij bestaat geeft die een DisplayAlert aan.
		/// </summary>
		private async void PickPhoto_Clicked(object sender, EventArgs e)
		{
			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsPickPhotoSupported)
			{
				await DisplayAlert("No PickPhoto", "no PickPhoto availabe", "OK");
				return;
			}

			loadingPic.IsRunning = true;
			_mediaFile = await CrossMedia.Current.PickPhotoAsync();

			if (_mediaFile == null)
			{
				loadingPic.IsRunning = false;
				return;
			}

			FileImage.Source = ImageSource.FromStream(() =>
			{
				return _mediaFile.GetStream();
			});
			loadingPic.IsRunning = false;
		}

		/// <summary>
		/// Foto maken vanuit de camera functie. Als er geen camera bestaat geeft die een DisplayAlert aan.
		/// </summary>
		private async void TakePhoto_Clicked(object sender, EventArgs e)
		{
			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				await DisplayAlert("No Camera", "no camera available.", "OK");
				return;
			}

			loadingPic.IsRunning = true;
			_mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
			{
				Directory = "Sample",
				Name = "myImage.jpg"
			});

			if (_mediaFile == null)
			{
				loadingPic.IsRunning = false;
				return;
			}

			FileImage.Source = ImageSource.FromStream(() =>
			{
				return _mediaFile.GetStream();
			});
			loadingPic.IsRunning = false;
		}

		void WardrobeItem_Changed(object sender, EventArgs e)
		{
			var selected = wardorbeItem.SelectedIndex;

			switch (selected)
			{
				case 0:
					feetSize.IsVisible = false;
					bottomSize.IsVisible = false;
					topSize.IsVisible = false;
					break;
				case 1:
					feetSize.IsVisible = false;
					bottomSize.IsVisible = false;

					topSize.IsVisible = true;
					break;
				case 2:
					topSize.IsVisible = false;
					feetSize.IsVisible = false;

					bottomSize.IsVisible = true;
					break;
				case 3:
					topSize.IsVisible = false;
					bottomSize.IsVisible = false;

					feetSize.IsVisible = true;
					break;
				default:
					break;
			}
		}

		void ColorItem_Changed(object sender, EventArgs e)
		{
			var selected = colorItem.SelectedIndex;

			switch (selected)
			{
				case -1:
					break;
				case 0:
					colorSelected.Color = Color.Red;
					color = "red";
					break;
				case 1:
					colorSelected.Color = Color.Green;
					color = "green";
					break;
				case 2:
					colorSelected.Color = Color.Blue;
					color = "blue";
					break;
				case 3:
					colorSelected.Color = Color.Black;
					color = "black";
					break;
				case 4:
					colorSelected.Color = Color.Gray;
					color = "gray";
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Foto uploaden naar het WEB-API
		/// </summary>
		private async void UploadFile_Clicked(object sender, EventArgs e)
		{
			try
			{
				loadingPic.IsVisible = true;
				loadingPic.IsRunning = true;

				var selectedType = wardorbeItem.SelectedIndex;
				var users_id = Models.LoginCredentials.loginId;

				var content = new MultipartFormDataContent();

				content.Add(new StreamContent(_mediaFile.GetStream()),
						"\"fileToUpload\"",
						$"\"{_mediaFile.Path}\"");

				content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(users_id))), "users_id");
				content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(color))), "color");

				var URL = "";

				switch (selectedType)
				{
					case -1:
						//await DisplayAlert("Warning", "Please chose cloth type!", "OK");
						await DisplayAlert("Warning", "Please a choose clothing type!", "OK");
						break;
					case 0:
						URL = "http://good-lookz.com/API/wardrobe/head/headUpload.php";
						var ResponseMessage_head = await client.PostAsync(URL, content);

						var sometext_head = await ResponseMessage_head.Content.ReadAsStringAsync();
						var response_head = JsonConvert.DeserializeObject<List<imageUpload>>(sometext_head);
						
						await Application.Current.MainPage.Navigation.PopAsync();

						break;
					case 1:
						URL = "http://good-lookz.com/API/wardrobe/top/topUpload.php";

						if (topSize.SelectedIndex == -1)
						{
							await DisplayAlert("Warning", "Please choose a size", "OK");
						}
						else
						{
							Models.Sizes.AllSizes top = new Models.Sizes.AllSizes();

							foreach (Models.Sizes.AllSizes i in lstSizes)
							{
								if (i.sSize == topSize.SelectedItem.ToString() && i.sRegion == _Region && i.sGender == _Gender && i.sType == "top")
								{
									top = i;
								}
							}

							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(top.sSize))), "size");
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(top.sRegion))), "region");
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(top.sGender))), "gender");
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(top.sNr))), "nr");

							var ResponseMessage_top = await client.PostAsync(URL, content);
							var sometext_top = await ResponseMessage_top.Content.ReadAsStringAsync();
							var response_top = JsonConvert.DeserializeObject<List<imageUpload>>(sometext_top);
							
							await Application.Current.MainPage.Navigation.PopAsync();
						}
						break;
					case 2:
						URL = "http://good-lookz.com/API/wardrobe/bottom/bottomUpload.php";

						if (bottomSize.SelectedIndex == -1)
						{
							await DisplayAlert("Warning", "Please choose a size", "OK");
						}
						else
						{
							Models.Sizes.AllSizes bot = new Models.Sizes.AllSizes();

							foreach (Models.Sizes.AllSizes i in lstSizes)
							{
								if (i.sSize == bottomSize.SelectedItem.ToString() && i.sRegion == _Region && i.sGender == _Gender && i.sType == "bottom")
								{
									bot = i;
								}
							}

							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(bot.sSize))), "size");
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(bot.sRegion))), "region");
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(bot.sGender))), "gender");
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(bot.sNr))), "nr");

							var ResponseMessage_bottom = await client.PostAsync(URL, content);
							var sometext_bottom = await ResponseMessage_bottom.Content.ReadAsStringAsync();
							var response_bottom = JsonConvert.DeserializeObject<List<imageUpload>>(sometext_bottom);
							
							await Application.Current.MainPage.Navigation.PopAsync();
						}
						break;
					case 3:
						URL = "http://good-lookz.com/API/wardrobe/feet/feetUpload.php";

						if (feetSize.SelectedIndex == -1)
						{
							await DisplayAlert("Warning", "Please choose a size", "OK");
						}
						else
						{
							Models.Sizes.AllSizes feet = new Models.Sizes.AllSizes();

							foreach (Models.Sizes.AllSizes i in lstSizes)
							{
								if (i.sSize == feetSize.SelectedItem.ToString() && i.sRegion == _Region && i.sGender == _Gender && i.sType == "feet")
								{
									feet = i;
								}
							}
							
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(feet.sSize))), "size");
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(feet.sRegion))), "region");
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(feet.sGender))), "gender");
							content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(feet.sNr))), "nr");

							var ResponseMessage_feet = await client.PostAsync(URL, content);
							var sometext_feet = await ResponseMessage_feet.Content.ReadAsStringAsync();
							var response_feet = JsonConvert.DeserializeObject<List<imageUpload>>(sometext_feet);
							
							await Application.Current.MainPage.Navigation.PopAsync();
						}
						break;
					default:
						break;
				}
				loadingPic.IsRunning = false;
				loadingPic.IsVisible = false;
			}
			catch
			{
				//Zorg ervoor dat de Activity Indicator uitstaat
				loadingPic.IsRunning = false;
				loadingPic.IsVisible = false;
				
				await DisplayAlert("Error", "Please take or choose a picture!", "OK");
			}
		}

		private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbRegion.SelectedIndex)
			{
				case 0:
					_Region = "EU";
					break;
				case 1:
					_Region = "US";
					break;
				case 2:
					_Region = "UK";
					break;
			}

			FillPickers(_Gender, _Region);
		}

		private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbGender.SelectedIndex)
			{
				case 0:
					_Gender = "F";
					break;
				case 1:
					_Gender = "M";
					break;
			}

			FillPickers(_Gender, _Region);
		}

	}
}
