using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristEye.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouristEye.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loader : ContentPage
    {
        LoaderViewModel viewModel;
        public Loader()
        {
            InitializeComponent();
            viewModel = new LoaderViewModel();
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            viewModel.navigateCmd.Execute(null);
        }
    }
}