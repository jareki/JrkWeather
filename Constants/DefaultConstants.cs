﻿using JrkWeather.Enums;

namespace JrkWeather.Constants
{
    public static class DefaultConstants
    {
        public const int UpdateIntervalMinutes = 60;
        public const UnitSystem UnitSystem = Enums.UnitSystem.Metric;
        public const int LocationsCount = 5;
        public const string DbName = "data.db";
        public const int MovingToNewLocationDistanceKm = 5;
    }
}
