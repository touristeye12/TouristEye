using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristEye.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TouristEye.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationMapPage : ContentPage
    {
        public Location location { get; set; }
        public LocationMapPage(Location _location)
        {
            InitializeComponent();
            location = _location;
            GetLOcation();
        }
        private async void GetLOcation()
        {
            try
            {
                double _longitude = location.Longitude;
                double _latitude = location.Latitude;
                Map.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        new Position(_latitude, _longitude), Distance.FromMiles(0.1)));
                var Pinposition = new Position(_latitude, _longitude);
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = Pinposition,
                    Label = location.Name,
                    Address = location.Address
                };
                Map.Pins.Add(pin);
            }
            catch (Exception e)
            {
                await DisplayAlert("Alert!", "Turn on your GPS and Internet and restart the page.", "OK");
            }

        }
    }
}