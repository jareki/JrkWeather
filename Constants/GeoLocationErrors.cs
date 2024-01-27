using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace JrkWeather.Constants
{
    public class GeoLocationErrors(string text)
    {
        public string Text { get; set; } = text;

        public static GeoLocationErrors NotSupported = new GeoLocationErrors("Geolocation not supported on this device");

        public static GeoLocationErrors NotEnabled = new GeoLocationErrors("Geolocation not enabled on this device");

        public static GeoLocationErrors PermissionNotGranted = new GeoLocationErrors("Permissions not granted for using device location");

        public static GeoLocationErrors UnknownError = new GeoLocationErrors("Unknown error while getting device location");
    }
}
