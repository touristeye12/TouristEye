using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Newtonsoft.Json;
using PCLStorage;
using Firebase.Xamarin.Database.Query;
using TouristEye.Models;
using TouristEye.Views;

namespace TouristEye.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        App app = (Application.Current as App);
        public INavigation _navigation { get; set; }
        public ICommand LoginCmd { get; }

        //readonly IFirebaseAuthenticator firebaseAuthenticator;
        //readonly NavigationService navigationService;

        Action propChangedCallBack => (LoginCmd as Command).ChangeCanExecute;

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            LoginCmd = new Command(async () => await Login());
            Error = false;
            //Email = new ValidatableObject<string>(propChangedCallBack, new EmailValidator());
            //Password = new ValidatableObject<string>(propChangedCallBack, new PasswordValidator());
        }

        async Task Login()
        {
            try
            {
                Error = false;
                IsBusy = true;
                propChangedCallBack();
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCrepjXrkXJ4tWxVLZrH-UaoYFs271eG-8"));
                
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                app.AuthLink = auth;

                if(auth != null)
                {
                    var firebase = new FirebaseClient("https://touristeye-tech.firebaseio.com/");
                    app.UserProfile = await firebase.Child("userprofiles").Child(app.AuthLink.User.LocalId).OnceSingleAsync<UserProfile>();

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
                //auth.User.FirstName = "demo";
                //auth.User.LastName = "demo";
                ////auth.User.
                //await authProvider.LinkAccountsAsync(auth, "email1@gmail1234.com", "qwerty1234");
                //var againAuth = await authProvider.SignInWithEmailAndPasswordAsync("email1@gmail.com", "qwerty1234");

                
            }
            catch(Exception e)
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
        //public string ErrorMsg
        //{
        //    get => errorMsg;
        //    set
        //    {
        //        errorMsg = value;
        //    }
        //}

    }
}
