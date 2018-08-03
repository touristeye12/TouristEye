using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TouristEye.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsViewPage : ContentPage
    {
        public MapsViewPage()
        {
            InitializeComponent();
            GetLOcation();

        }
        private async void GetLOcation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(100000));
                double _longitude = position.Longitude;
                double _latitude = position.Latitude;
                Map.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        new Position(_latitude, _longitude), Distance.FromMiles(0.1)));
                var Pinposition = new Position(_latitude, _longitude);
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = Pinposition,
                    Label = "Hello",
                    Address = "I am here"
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