using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JrkWeather.Constants;
using JrkWeather.Models;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace JrkWeather.Services
{
    public class DeviceLocationService
    {
        public async Task<(Coordinates? location, string? error)> GetCachedLocationAsync()
        {
            try
            {
                var location = await Geolocation.Default.GetLastKnownLocationAsync();
                if (location != null)
                {
                    return (new Coordinates(location.Latitude, location.Longitude), null);
                }
            }
            catch (FeatureNotSupportedException)
            {
                return (null, GeoLocationErrors.NotSupported.Text);
            }
            catch (FeatureNotEnabledException)
            {
                return (null, GeoLocationErrors.NotEnabled.Text);
            }
            catch (PermissionException)
            {
                return (null, GeoLocationErrors.PermissionNotGranted.Text);
            }
            catch (Exception)
            {
                return (null, GeoLocationErrors.UnknownError.Text);
            }
            return (null, GeoLocationErrors.UnknownError.Text);
        }

        public async Task<(Coordinates? location, string? error)> GetCurrentLocationAsync(CancellationToken ct = default)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Lowest, TimeSpan.FromSeconds(10));
                var location = await Geolocation.Default.GetLocationAsync(request, ct);

                if (location != null)
                {
                    return (new Coordinates(location.Latitude, location.Longitude), null);
                }
            }
            catch (FeatureNotSupportedException)
            {
                return (null, GeoLocationErrors.NotSupported.Text);
            }
            catch (FeatureNotEnabledException)
            {
                return (null, GeoLocationErrors.NotEnabled.Text);
            }
            catch (PermissionException)
            {
                return (null, GeoLocationErrors.PermissionNotGranted.Text);
            }
            catch (Exception)
            {
                return (null, GeoLocationErrors.UnknownError.Text);
            }
            return (null, GeoLocationErrors.UnknownError.Text);
        }

        public bool DetectMovingFaraway(Coordinates previousLocation, Coordinates currentLocation)
        {
            var l1 = new Location(previousLocation.Latitude, previousLocation.Longitude);
            var l2 = new Location(currentLocation.Latitude, currentLocation.Longitude);
            var distance = Location.CalculateDistance(l1, l2, DistanceUnits.Kilometers);
            return distance >= DefaultConstants.MovingToNewLocationDistanceKm;
        }
    }

}
