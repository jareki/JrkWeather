using CommunityToolkit.Mvvm.ComponentModel;

namespace JrkWeather.ViewModels
{
    public partial class BaseViewModel: ObservableObject
    {
        #region Fields

        [ObservableProperty]
        private string _title;

        #endregion
        public BaseViewModel() { }
    }
}
