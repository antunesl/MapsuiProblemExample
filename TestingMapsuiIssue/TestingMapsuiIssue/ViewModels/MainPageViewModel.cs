using Mapsui.UI.Forms;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestingMapsuiIssue.Views;

namespace TestingMapsuiIssue.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        public MapView MapView { get; private set; }
        public DelegateCommand TestingNavigate { get; set; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            TestingNavigate = new DelegateCommand(Navigate);
            Title = "Main Page";
        }

        
        public async void SetMapView(MapView mapView)
        {
            MapView = mapView;


            MapView.PinClicked += MapView_PinClicked;

            var position = await Xamarin.Essentials.Geolocation.GetLocationAsync();
            var x = position.Longitude;
            var y = position.Latitude;

            MapView.Pins.Add(new Pin(MapView) {
                Label = "Testing",
                Icon = GetResourceStream("construction_architect_pinpoint.png"),
                Type = PinType.Icon,
                Position = new Position(y, x)
            });
        }
        static Dictionary<string, byte[]> LocalResources = new Dictionary<string, byte[]>();
        private static byte[] GetResourceStream(string fileName)
        {
            byte[] image = null;
            try
            {



                LocalResources.TryGetValue(fileName, out image);
                if (image != null)
                    return image;

                Assembly assembly = typeof(MainPage).Assembly;




                image = assembly.GetManifestResourceStream($"TestingMapsuiIssue.{fileName}").ToBytes() ?? assembly.GetManifestResourceStream($"TestingMapsuiIssue.local.{fileName}").ToBytes();
                LocalResources.Add(fileName, image);
            }
            catch (Exception)
            {


            }
            return image;
        }

        private async void MapView_PinClicked(object sender, PinClickedEventArgs e)
        {
            e.Handled = true;
            await NavigateToDetail();
        }
        private async void Navigate()
        {
            await NavigateToDetail();
        }

        private async Task<INavigationResult> NavigateToDetail()
        {
            return await _navigationService.NavigateAsync("TestingNavigation", useModalNavigation: false);
        }
    }
}
