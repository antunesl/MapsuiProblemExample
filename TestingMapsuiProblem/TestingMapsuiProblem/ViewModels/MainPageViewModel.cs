using Mapsui.UI.Forms;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestingMapsuiProblem.Views;
using Xamarin.Forms;

namespace TestingMapsuiProblem.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        IPageDialogService _dialogService;
        INavigationService _navigationService;
        static Dictionary<string, byte[]> LocalResources = new Dictionary<string, byte[]>();
        private DateTime mapPinpointsUpdateTime;
        private static byte[] GetResourceStream(string fileName)
        {
            byte[] image = null;
            try
            {



                LocalResources.TryGetValue(fileName, out image);
                if (image != null)
                    return image;

                Assembly assembly = typeof(MainPage).Assembly;




                image = assembly.GetManifestResourceStream($"TestingMapsuiProblem.{fileName}").ToBytes() ?? assembly.GetManifestResourceStream($"TestingMapsuiProblem.local.{fileName}").ToBytes();
                LocalResources.Add(fileName, image);
            }
            catch (Exception)
            {


            }
            return image;
        }
        public MapView MapView { get; set; }
        public IEnumerable<object> Products { get; private set; }
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            Title = "Main Page";
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (MapView != null)
                await LoadServices();
        }

        private async void Viewport_ViewportChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "Resolution" && !IsBusy)
            {
                if (Double.IsNaN(MapView.Viewport.Center.X) || Double.IsNaN(MapView.Viewport.Center.Y))
                    return;

                var centerPoint = Mapsui.Projection.SphericalMercator.ToLonLat(MapView.Viewport.Center.X, MapView.Viewport.Center.Y);

                //var lon = centerPoint.X;
                //var lat = centerPoint.Y;

                var _mapSearchLocationPoint = new Mapsui.Geometries.Point(38.7844586, -9.0977246);

                if (!IsBusy && (DateTime.Now - mapPinpointsUpdateTime).TotalSeconds > 1)
                {
                    mapPinpointsUpdateTime = DateTime.Now;
                    await LoadServices();
                }

            }
        }       
        bool IsBusy = false;

        public async void SetMapView(MapView mapView, Mapsui.Geometries.Point userLocationPoint)
        {
            MapView = mapView;



            MapView.MyLocationLayer.UpdateMyLocation(new Mapsui.UI.Forms.Position(38.7844586, -9.0977246));

            MapView.RotationLock = true;


            MapView.PinClicked += MapView_PinClicked;
            MapView.Viewport.ViewportChanged += Viewport_ViewportChanged;
            await LoadServices();
            //MapView.Map.Home = n => n.NavigateTo(_userLocationPoint, mapView.Map.Resolutions[mapView.Map.Resolutions.Count - 3]);

        }
        protected async Task ShowUserMessage(string message, string title = null, string buttonText = null)
        {
            await _dialogService.DisplayAlertAsync(title ?? "Helo", message, buttonText ?? "Close");
        }

        bool IsPinClicked = false;
        private async void MapView_PinClicked(object sender, PinClickedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

                try
                {

                    if (!IsPinClicked && !IsBusy)
                    {
                        IsPinClicked = true;
                        e.Handled = true;

                        if (e.Pin.Tag != null)
                        {
                            var id = (int)e.Pin.Tag;

                            await _navigationService.NavigateAsync("DetailService");

                        }
                    }

                }
                catch (Exception ex)
                {
                    await ShowUserMessage(ex.Message);
                }
                finally
                {
                    IsPinClicked = false;
                }
            });
        }

        private async Task SetPinsInMap()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {


                MapView.Pins.Clear();
                var icon = GetResourceStream("legal.png");
                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7844586, -9.0977246),

                    Type = PinType.Icon,
                    Tag = 1,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7854686, -9.0976246),

                    Type = PinType.Icon,
                    Tag = 2,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7844596, -9.0957146),

                    Type = PinType.Icon,
                    Tag = 3,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7344581, -9.0877244),

                    Type = PinType.Icon,
                    Tag = 4,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7842556, -9.0972226),

                    Type = PinType.Icon,
                    Tag = 5,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7841536, -9.0977286),

                    Type = PinType.Icon,
                    Tag = 6,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7844536, -9.0971146),

                    Type = PinType.Icon,
                    Tag = 7,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7843511, -9.0977211),

                    Type = PinType.Icon,
                    Tag = 8,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7844522, -9.0977022),

                    Type = PinType.Icon,
                    Tag = 9,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7840555, -9.0977255),

                    Type = PinType.Icon,
                    Tag = 10,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7849526, -9.0977246),

                    Type = PinType.Icon,
                    Tag = 11,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7845596, -9.0977246),

                    Type = PinType.Icon,
                    Tag = 12,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7845586, -9.0975247),

                    Type = PinType.Icon,
                    Tag = 13,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7841586, -9.0971248),

                    Type = PinType.Icon,
                    Tag = 14,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7840586, -9.0970249),

                    Type = PinType.Icon,
                    Tag = 15,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7842589, -9.0971246),

                    Type = PinType.Icon,
                    Tag = 16,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7847588, -9.0977246),

                    Type = PinType.Icon,
                    Tag = 17,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7844136, -9.0977146),

                    Type = PinType.Icon,
                    Tag = 18,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7844081, -9.0977146),

                    Type = PinType.Icon,
                    Tag = 19,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7844280, -9.0977440),

                    Type = PinType.Icon,
                    Tag = 20,
                    Scale = 0.5f,
                    Icon = icon

                });

                MapView.Pins.Add(new Pin(MapView)
                {
                    Label = $"",
                    IsCalloutVisible = false,
                    Position = new Mapsui.UI.Forms.Position(38.7844200, -9.0977900),

                    Type = PinType.Icon,
                    Tag = 21,
                    Scale = 0.5f,
                    Icon = icon

                });
                return;
            });
        }
        private async Task LoadServices()
        {
            IsBusy = true;
            //WE CALL A API REST, SIMULATE THE CALL WITH TASK DELAY
            await Task.Delay(1000);
            await SetPinsInMap();
            IsBusy = false;
            return;

        }
    }
}
