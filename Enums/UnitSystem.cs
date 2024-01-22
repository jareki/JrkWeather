namespace JrkWeather.Enums
{
    public enum UnitSystem
    {
        Metric, Imperial
    }

    public static class UnitSystemExtensions
    {
        public static string ToOwmString(this UnitSystem unit)
        {
            switch (unit)
            {
                case UnitSystem.Metric:
                    return "metric";
                case UnitSystem.Imperial:
                    return "imperial";
                default:
                    return string.Empty;
            }
        }
    }
}
