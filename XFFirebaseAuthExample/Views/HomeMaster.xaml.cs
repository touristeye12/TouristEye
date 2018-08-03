using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouristEye.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeMaster : ContentPage
    {
        App app = Application.Current as App;
        public ListView ListView;
        public HomeMaster()
        {
            InitializeComponent();
            BindingContext = new HomeMasterViewModel();
            Name.Text = app.UserProfile.Name;
            Email.Text = app.UserProfile.Email;
            ListView = MenuItemsListView;
        }

        class HomeMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<HomeMenuItem> MenuItems { get; set; }
            
            public HomeMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomeMenuItem>(new[]
                {
                    new HomeMenuItem { Id = 0, Title = "Accomodation", Icon="accomodation.png", TargetType=typeof(AccomodationList) },
                    new HomeMenuItem { Id = 1, Title = "Entertainment", Icon="entertainment.png", TargetType=typeof(EntertainmentList) },
                    new HomeMenuItem { Id = 2, Title = "Events", Icon="event.png", TargetType=typeof(EventList) },
                    new HomeMenuItem { Id = 3, Title = "Tours", Icon="tour.png", TargetType=typeof(TourList) },
                    new HomeMenuItem { Id = 4, Title = "Travels", Icon="travel.png", TargetType=typeof(TravelList) },
                    new HomeMenuItem { Id = 4, Title = "Stores", Icon="store.png", TargetType=typeof(StoreList) },
                    new HomeMenuItem { Id = 4, Title = "Find Me!", Icon="me.png", TargetType=typeof(MapsViewPage) },
                });
            }

            

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
        private async Task logout_Tapped(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Logout!", "Are you sure, you want to Logout?", "Yes", "Cancel");
            if (!result) return;
            IFolder folder = FileSystem.Current.LocalStorage;
            IFolder log = await folder.CreateFolderAsync("TouristEye", CreationCollisionOption.OpenIfExists);
            IFile ALFile = await log.CreateFileAsync("AuthLink", CreationCollisionOption.OpenIfExists);
            await ALFile.WriteAllTextAsync("");
            this.app.MainPage = new NavigationPage(new LoginPage());
        }
    }
}