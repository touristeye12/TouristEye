using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouristEye.Constant;
using TouristEye.Models;
using TouristEye.Views;
using Xamarin.Forms;

namespace TouristEye.ViewModels
{
    public class StoreListViewModel : BaseViewModel
    {
        App app = (Application.Current as App);
        private ObservableCollection<Location> _Items = new ObservableCollection<Location>();
        public ObservableCollection<Location> Items { get { return _Items; } set { _Items = value; OnPropertyChanged(); } }
        public ICommand LoadListCmd { get; }

        public StoreListViewModel(/*INavigation navigation*/)
        {
            Items = new ObservableCollection<Location>();
            LoadListCmd = new Command(async () => await LoadList());
        }



        async Task LoadList()
        {
            try
            {
                refresh = true;
                var firebase = new FirebaseClient("https://touristeye-tech.firebaseio.com/");
                ObservableCollection<Location> list = await firebase.Child("locations").OnceSingleAsync<ObservableCollection<Location>>();
                Items.Clear();
                foreach (var item in list)
                {
                    if(item.Type == LocationType.Store.ToString())
                    {
                        Items.Add(item);
                    }
                }
            }
            catch(Exception e)
            {

            }
            finally
            {
                refresh = false;
            }

        }

    }
}
