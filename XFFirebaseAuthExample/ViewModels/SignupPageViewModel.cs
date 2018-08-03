using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Firebase.Xamarin.Auth;
using TouristEye.Models;
using Newtonsoft.Json;
using PCLStorage;
using TouristEye.Views;

namespace TouristEye.ViewModels
{
    class SignupPageViewModel : BaseViewModel
    {
        public INavigation _navigation { get; set; }
        public ICommand LoginCmd { get; }

        Action propChangedCallBack => (LoginCmd as Command).ChangeCanExecute;

        public SignupPageViewModel(INavigation navigation)
        {
            userProfile = new UserProfile();
            _navigation = navigation;
            LoginCmd = new Command(async () => await Login());
            Error = false;
        }

        async Task Login()
        {
            try
            {
                Error = false;
                IsBusy = true;
                App app = (Application.Current as App);
                propChangedCallBack();
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCrepjXrkXJ4tWxVLZrH-UaoYFs271eG-8"));

                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
                app.AuthLink = auth;

                if (app.AuthLink != null)
                {
                    userProfile.Email = Email;
                    userProfile.CreatedAt = DateTime.Now.ToString("yyyy-M-dd HH:mm:ss");
                    var firebase = new FirebaseClient("https://touristeye-tech.firebaseio.com/");
                    await firebase.Child("userprofiles").Child(auth.User.LocalId).PutAsync<UserProfile>(userProfile);
                    app.UserProfile = userProfile;

                    var _userProfile = JsonConvert.SerializeObject(app.UserProfile);
                    var _authLink = JsonConvert.SerializeObject(app.AuthLink);

                    IFolder folder = FileSystem.Current.LocalStorage;
                    IFolder log = await folder.CreateFolderAsync("TouristEye", CreationCollisionOption.OpenIfExists);
                    IFile UPFile = await log.CreateFileAsync("UserProfile", CreationCollisionOption.OpenIfExists);
                    IFile ALFile = await log.CreateFileAsync("AuthLink", CreationCollisionOption.OpenIfExists);
                    await UPFile.WriteAllTextAsync(_userProfile);
                    await ALFile.WriteAllTextAsync(_authLink);

                    app.MainPage = new NavigationPage(new Home());
                    IsBusy = false;
                    propChangedCallBack();
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                ErrorMsg = e.Message;
                Error = true;
            }
            finally
            {
                IsBusy = false;
            }

        }

        //properties
        private UserProfile _userProfile = new UserProfile();
        public UserProfile userProfile
        {
            get { return _userProfile; }
            set { _userProfile = value; OnPropertyChanged(); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }
    }

}