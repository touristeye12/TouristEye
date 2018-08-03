using Firebase.Xamarin.Auth;
using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouristEye.Models;
using TouristEye.Views;
using Xamarin.Forms;

namespace TouristEye.ViewModels
{
    public class LoaderViewModel : BaseViewModel
    {
        App app = (Application.Current as App);
        public ICommand navigateCmd { get; }

        public LoaderViewModel(/*INavigation navigation*/)
        {
            navigateCmd = new Command(async () => await navigate());
        }



        async Task navigate()
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            IFolder log = await folder.CreateFolderAsync("TouristEye", CreationCollisionOption.OpenIfExists);
            IFile ALFile = await log.CreateFileAsync("AuthLink", CreationCollisionOption.OpenIfExists);
            var ALFieResult = await ALFile.ReadAllTextAsync();
            if (ALFieResult != "")
            {
                app.AuthLink = JsonConvert.DeserializeObject<FirebaseAuthLink>(ALFieResult);
                IFile UPFile = await log.CreateFileAsync("UserProfile", CreationCollisionOption.OpenIfExists);
                var UPFileResult = await UPFile.ReadAllTextAsync();
                app.UserProfile = JsonConvert.DeserializeObject<UserProfile>(UPFileResult);
                await Task.Delay(3000);
                app.MainPage = new NavigationPage(new Home());
            }
            else
            {
                await Task.Delay(3000);
                app.MainPage = new NavigationPage(new LoginPage());

            }
        }

    }
}
