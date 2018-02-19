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

        public WardrobeAdd()
        {
            InitializeComponent();

            //loadingPic.IsVisible = false;
            topSize.IsVisible = false;
            bottomSize.IsVisible = false;
            feetSize.IsVisible = false;
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

                        await DisplayAlert("Message", "Image upload: " + response_head[0].img_upload, "OK");
                        await Application.Current.MainPage.Navigation.PopAsync();

                        break;
                    case 1:
                        URL = "http://good-lookz.com/API/wardrobe/top/topUpload.php";
                        var size_top = topSize.SelectedIndex;

                        switch (size_top)
                        {
                            case -1:
								//await DisplayAlert("Warning", "Please chose size", "OK");
								await DisplayAlert("Warning", "Please choose a clothing size", "OK");
								break;
                            case 0:
                                content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes("S"))), "size");
                                break;
                            case 1:
                                content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes("M"))), "size");
                                break;
                            case 2:
                                content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes("L"))), "size");
                                break;
                            case 3:
                                content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes("XL"))), "size");
                                break;
                            default:
                                break;
                        }

                        if (size_top != -1)
                        {
                            var ResponseMessage_top = await client.PostAsync(URL, content);

                            var sometext_top = await ResponseMessage_top.Content.ReadAsStringAsync();
                            var response_top = JsonConvert.DeserializeObject<List<imageUpload>>(sometext_top);

                            await DisplayAlert("Message", "Image upload: " + response_top[0].img_upload, "OK");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        break;
                    case 2:
                        URL = "http://good-lookz.com/API/wardrobe/bottom/bottomUpload.php";
                        var size_bottom = bottomSize.Text;

                        if (size_bottom != null)
                        {
                            content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(size_bottom))), "size");

                            var ResponseMessage_bottom = await client.PostAsync(URL, content);

                            var sometext_bottom = await ResponseMessage_bottom.Content.ReadAsStringAsync();
                            var response_bottom = JsonConvert.DeserializeObject<List<imageUpload>>(sometext_bottom);

                            await DisplayAlert("Message", "Image upload: " + response_bottom[0].img_upload, "OK");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Warning", "Please insert size", "OK");
                        }
                        break;
                    case 3:
                        URL = "http://good-lookz.com/API/wardrobe/feet/feetUpload.php";
                        var size_feet = feetSize.Text;

                        if (size_feet != null)
                        {
                            content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(size_feet))), "size");

                            var ResponseMessage_feet = await client.PostAsync(URL, content);

                            var sometext_feet = await ResponseMessage_feet.Content.ReadAsStringAsync();
                            var response_feet = JsonConvert.DeserializeObject<List<imageUpload>>(sometext_feet);

                            //await DisplayAlert("Message", "Image upload: " + response_feet[0].img_upload, "OK"); -> overbodig
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Warning", "Please insert size", "OK");
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
				//await DisplayAlert("Error", "Please take or chose picture!", "OK");
				await DisplayAlert("Error", "Please take or choose a picture!", "OK");
			}
		}
    }
}
