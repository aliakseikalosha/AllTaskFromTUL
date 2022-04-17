using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DataGrabber.DataProvider;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DataGrabber.src.Location
{
    public static class LocationLogger
    {
        private static string currentFileName = "";
        private static Task logging;
        private static CancellationTokenSource source = new CancellationTokenSource();
        private static CancellationToken token;

        public static void StartLogingLocation(DateTime start)
        {
            if(token.CanBeCanceled)
            {
                Debug.WriteLine("Logger allready running");
                StopLogingLocation();
            }
            currentFileName = $"{start:yyyy_MM_dd_HH_mm_ss}_data_geo.csv";
            var fileAccess = DependencyService.Get<IAccessFileService>();
            fileAccess.CreateFile(currentFileName);
            logging = LogLocation(fileAccess);
        }

        public static void StopLogingLocation()
        {
            if(token.CanBeCanceled)
            {
                source.Cancel();
            }
        }

        private static async Task LogLocation(IAccessFileService fileService)
        {
            token = source.Token;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        Debug.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                        fileService.WriteNewLineToFile(currentFileName,  $"{DateTime.Now:O}|{location.Accuracy}| {location.Latitude}|{location.Longitude}| {location.Altitude}".Replace(",",".").Replace("|",","));
                    }
                    
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }
                await Task.Delay(5000, token);
            }
        }
    }
}
