using CommunityToolkit.Mvvm.ComponentModel;
using JrkWeather.Models;

namespace JrkWeather.ViewModels
{
    public partial class MainViewModel: BaseViewModel
    {
        [ObservableProperty]
        private Forecast forecast;
    }
}
