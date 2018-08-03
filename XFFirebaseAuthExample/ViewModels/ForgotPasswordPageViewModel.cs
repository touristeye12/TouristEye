using Firebase.Xamarin.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TouristEye.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        App app = (Application.Current as App);
        public ICommand navigateCmd { get; }
        public INavigation _navigation { get; set; }

        public ForgotPasswordViewModel(INavigation navigation)
        {
            _navigation = navigation;
            navigateCmd = new Command(async () => await navigate());
            Error = false;
        }



        async Task navigate()
        {
            try
            {
                IsBusy = true;
                Error = false;
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCrepjXrkXJ4tWxVLZrH-UaoYFs271eG-8"));

                await authProvider.SendPasswordResetEmailAsync(Email);
                ErrorMsg = "Your request has been submitted! please check your email";
                //IFolder folder = FileSystem.Current.LocalStorage;
                //IFolder log = await folder.CreateFolderAsync("log", CreationCollisionOption.OpenIfExists);
                //IFile ALFile = await log.CreateFileAsync("AuthLink", CreationCollisionOption.OpenIfExists);
                //var ALFieResult = await ALFile.ReadAllTextAsync();
                //if (ALFieResult != "")
                //{
                //    app.AuthLink = JsonConvert.DeserializeObject<FirebaseAuthLink>(ALFieResult);
                //    IFile UPFile = await log.CreateFileAsync("UserProfile", CreationCollisionOption.OpenIfExists);
                //    var UPFileResult = await ALFile.ReadAllTextAsync();
                //    app.UserProfile = JsonConvert.DeserializeObject<UserProfile>(UPFileResult);
                //    app.MainPage = new NavigationPage(new Home());
                //}
                //else
                //{
                //    app.MainPage = new NavigationPage(new LoginPage());

                //}
            }
            catch (Exception e)
            {
                ErrorMsg = "Error occured while processing your request!";
            }
            finally
            {
                Error = true;
                IsBusy = false;
            }

        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

    }
}
