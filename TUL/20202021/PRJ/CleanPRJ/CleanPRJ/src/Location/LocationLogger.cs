using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
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
        private static CultureInfo usCulture = CultureInfo.GetCultureInfo("en-US");

        public static void StartLogingLocation(DateTime start)
        {
            if (token.CanBeCanceled)
            {
                Debug.WriteLine("Logger allready running");
                StopLogingLocation();
            }
            currentFileName = $"{start:yyyy_MM_dd_HH_mm_ss}_data_geo.csv";
            var fileAccess = DependencyService.Get<IAccessFileService>();
            logging = LogLocation(fileAccess);
        }

        public static void StopLogingLocation()
        {
            if (!logging.IsCanceled)
            {
                source.Cancel();
            }
        }

        private static async Task LogLocation(IAccessFileService fileService)
        {
            token = source.Token;
            fileService.CreateFile(currentFileName);
            fileService.WriteNewLineToFile(currentFileName, $"Date,Accuracy,Latitude,Longitude, Altitude");
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            Xamarin.Essentials.Location location;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    location = await Geolocation.GetLocationAsync(request);
                    if (location != null)
                    {
                        Debug.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                        fileService.WriteNewLineToFile(currentFileName, $"{DateTime.Now:O},{location.Accuracy?.ToString(usCulture)},{location.Latitude.ToString(usCulture)},{location.Longitude.ToString(usCulture)}, {location.Altitude?.ToString(usCulture)}");
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
                await Task.Delay((int)(GrabberSettingsModel.WaitInBetweenGPS * 1000), token);
            }
        }
    }
}
