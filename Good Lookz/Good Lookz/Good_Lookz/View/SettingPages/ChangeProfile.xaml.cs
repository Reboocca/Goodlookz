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

namespace Good_Lookz.View.SettingPages
{
    public partial class ChangeProfile : ContentPage
    {
        public ChangeProfile()
        {
            InitializeComponent();

            imPicture.Source     = "http://good-lookz.com/img/default_pfp.png";
            loadingPic.IsRunning = false;

            getProfile();
        }

        #region Global
        private MediaFile _mediaFile;
        Models.Settings.Profile pf = new Models.Settings.Profile();
        #endregion

        private async void getProfile()
        {
            string users_id = Models.LoginCredentials.loginId;
            string url      = "http://good-lookz.com/API/account/getProfile.php?users_id=" + users_id;

            HttpClient get = new HttpClient();
            HttpResponseMessage respons = await get.GetAsync(url);

            if (respons.IsSuccessStatusCode)
            {
                string responsecontent  = await respons.Content.ReadAsStringAsync();
                var myobjList           = JsonConvert.DeserializeObject<List<Models.Settings.Profile>>(responsecontent);
                var myObj               = myobjList[0];

                pf.description          = myObj.description;
                pf.picture              = myObj.picture;
                imPicture.Source        = pf.picture;
                editorDesc.Text         = pf.description;
            }
        }

        private async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No PickPhoto", "No PickPhoto availabe", "OK");
                return;
            }

            imPicture.IsVisible     = false;
            loadingPic.IsRunning    = true;
            _mediaFile              = await CrossMedia.Current.PickPhotoAsync();

            if (_mediaFile == null)
            {
                loadingPic.IsRunning    = false;
                imPicture.IsVisible     = true;
                return;
            }

            imPicture.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            loadingPic.IsRunning = false;
            imPicture.IsVisible = true;
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "no camera available.", "OK");
                return;
            }

            imPicture.IsVisible = false;
            loadingPic.IsRunning = true;
            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "myImage.jpg"
            });

            if (_mediaFile == null)
            {
                loadingPic.IsRunning = false;
                imPicture.IsVisible = true;
                return;
            }

            imPicture.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            loadingPic.IsRunning = false;
            imPicture.IsVisible = true;
        }

        private async void uploadToDBS(object sender, EventArgs e)
        {
            btnSave.IsEnabled = false;

            string desc = editorDesc.Text;
            if (string.IsNullOrEmpty(editorDesc.Text))
            {
                desc = "No description added.";
            }

            var content = new MultipartFormDataContent();

            //Stop de file in de content
            content.Add(new StreamContent(_mediaFile.GetStream()),
                        "\"fileToUpload\"",
                        $"\"{_mediaFile.Path}\"");


            var users_id = Models.LoginCredentials.loginId;

            content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(users_id))), "users_id");
            content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(desc))), "description");

            HttpClient client = new HttpClient(new NativeMessageHandler());
            var ResponseMessage = await client.PostAsync("http://good-lookz.com/API/account/updateProfile.php", content);
            string result = await ResponseMessage.Content.ReadAsStringAsync();

            if (result == "Success")
            {
                await DisplayAlert("Success", "You changes have been saved", "Ok");
                await this.Navigation.PopAsync();
            }
            else
            {
                btnSave.IsEnabled = true;
                await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again", "ok");
            }
        }
    }
}
