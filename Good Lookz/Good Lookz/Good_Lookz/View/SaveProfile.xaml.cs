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

namespace Good_Lookz.View
{
    public partial class SaveProfile : ContentPage
    {
        public SaveProfile()
        {
            InitializeComponent();

            imPicture.Source = "http://good-lookz.com/img/default_pfp.png";
            loadingPic.IsRunning = false;
        }

        #region Global
        private MediaFile _mediaFile;
        #endregion

        //code van: https://stackoverflow.com/questions/32163471/confirmation-dialog-on-back-button-press-event-xamarin-forms voor terugknop
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => { await this.DisplayAlert("Message", "Please save your profile before proceeding", "OK"); });
            return true;
        }

        private async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No PickPhoto", "No PickPhoto availabe", "OK");
                return;
            }

            loadingPic.IsRunning = true;
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (_mediaFile == null)
            {
                loadingPic.IsRunning = false;
                return;
            }

            imPicture.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });
            loadingPic.IsRunning = false;
        }

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

            imPicture.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });
            loadingPic.IsRunning = false;
        }

        private async void uploadToDBS(object sender, EventArgs e)
        {
            btnSave.IsEnabled = false;

            string desc = editorDesc.Text;
            if (string.IsNullOrEmpty(editorDesc.Text)){
                desc = "No description added.";
            }

            var content     = new MultipartFormDataContent();

            //Stop de file in de content
            content.Add(new StreamContent(_mediaFile.GetStream()),
                        "\"fileToUpload\"",
                        $"\"{_mediaFile.Path}\"");
            

            var users_id = Models.LoginCredentials.loginId;
            var username = Models.LoginCredentials.loginUsername;
            
            content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(users_id))), "users_id");
            content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(username))), "username");
            content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(desc))), "description");
            
            HttpClient client   = new HttpClient(new NativeMessageHandler());
            var ResponseMessage = await client.PostAsync("http://good-lookz.com/API/account/saveProfile.php", content);
            string result = await ResponseMessage.Content.ReadAsStringAsync();

            if(result == "Success")
            {
                await DisplayAlert("Success", "Profile has been saved", "Ok");
                App.Current.MainPage = new NavigationPage(new MenuPage());
            }
            else
            {
                btnSave.IsEnabled = true;
                await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again", "ok");
            }
        }

        async void LaterClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new View.MenuPage(), true);
            await DisplayAlert("Message", "Alright, we will skip this step for now. You can always go into setting to edit your profile instead!", "ok");

            //Sla de gegevens op in de dbs
            try
            {
                //Haal vereisten gegevens op
                string desc     = "No description added.";
                string users_id = Models.LoginCredentials.loginId;
                string username = Models.LoginCredentials.loginUsername;

                string webadres = "http://good-lookz.com/API/account/saveProfile.php?";
                string parameters = "users_id=" + users_id + "&description=" + desc + "&username=" + username;
                HttpClient connect = new HttpClient();
                HttpResponseMessage uploadToSale = await connect.GetAsync(webadres + parameters);
                uploadToSale.EnsureSuccessStatusCode();

                string result = await uploadToSale.Content.ReadAsStringAsync();

                //Is het resultaat succes
                if (result == "Success")
                {
                    App.Current.MainPage = new NavigationPage(new MenuPage());
                }
                //Is het resultaat failed
                else if (result == "Failed")
                {
                    await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
                }

            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
                throw;
            }
        }
    }
}
