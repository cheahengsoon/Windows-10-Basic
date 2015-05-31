using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WorkShop10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GPS : Page
    {
        public GPS()
        {
            this.InitializeComponent();
        }

        private async void getCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            //Remind add Device Capabiltiy in Package.App...

            Geolocator geolocator = new Geolocator();

            // Define the accuracy for the location service in meters
            // There is no point to set the accuracy to any value below 5;
            // Also can define the location service accuracy in DesiredAccuracy with the options of Default or High
            geolocator.DesiredAccuracyInMeters = 10;

            Geoposition currentposition = await geolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromMinutes(5),    // to specify the aging of the cache 
                timeout: TimeSpan.FromSeconds(10));     // to specify the timeout for the getting the location information

         
            latTextBlock.Text = currentposition.Coordinate.Point.Position.Latitude.ToString("0.000000");
            lonTextBlock.Text = currentposition.Coordinate.Point.Position.Longitude.ToString("0.000000");
           // altTextBlock.Text = currentposition.Coordinate.Point.Position.Altitude.ToString("0.000000");

           // headingTextBlock.Text = currentposition.Coordinate.Heading.ToString();
           // speedTextBlock.Text = currentposition.Coordinate.Speed.ToString();

            sourceTextBlock.Text = currentposition.Coordinate.PositionSource.ToString();
        }
    }
}
