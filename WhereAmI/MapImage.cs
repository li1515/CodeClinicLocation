using System;
using System.Device.Location;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace WhereAmI
{
    class MapImage
    {
        public static void Show(GeoCoordinate location)
        {
            string filename = $"{location.Latitude:##.000},{location.Longitude:##.000},{location.HorizontalAccuracy:####}m.jpg";

            DownloadMapImage(BuildURI(location), filename);

            OpenWithDefaultApp(filename);
        }

        private static void DownloadMapImage(Uri target, string filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(target, filename);
            }
        }

        /// <summary>
        /// Map Image REST API by HERE Location Services
        /// </summary>
        /// <remarks>
        /// https://developer.here.com/
        /// </remarks>
        private static Uri BuildURI(GeoCoordinate location)
        {
            #region HERE App ID & App Code
            string HereApi_ApiKey = "ZSkW1GjdLS-WGct1ILlVqs6T9-gzO3TqQ-1rSIAd6Yg";
            #endregion

            var HereApi_DNS = "image.maps.ls.hereapi.com";
            var HereApi_URL = $"https://{HereApi_DNS}/mia/1.6/mapview";
            var HereApi_Secrets = $"?apiKey={HereApi_ApiKey}";

            var latlon = $"&lat={location.Latitude.ToString("#.000", CultureInfo.InvariantCulture)}&lon={location.Longitude.ToString("#.000", CultureInfo.InvariantCulture)}&vt=0&z=14";

            return new Uri(HereApi_URL + HereApi_Secrets + latlon);
        }

        private static void OpenWithDefaultApp(string filename)
        {
            var si = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/C start {filename}",
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(si);
        }
    }
}
