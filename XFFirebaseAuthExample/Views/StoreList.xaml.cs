using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TouristEye.Models;
using TouristEye.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouristEye.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreList : ContentPage
    {
        StoreListViewModel viewModel;
        public StoreList()
        {
            InitializeComponent();
            BindingContext = viewModel = new StoreListViewModel();

        }
        protected override void OnAppearing()
        {
            viewModel.LoadListCmd.Execute(null);
        }

        public void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            var location = e.SelectedItem as Location;
            if (e.SelectedItem == null)
                return;
            Navigation.PushAsync(new LocationMapPage(location));
            //((ListView)sender).SelectedItem = null;
        }
    }
}
