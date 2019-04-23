using Mapsui.Projection;
using Mapsui.UI;
using Mapsui.UI.Forms;
using Mapsui.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMapsuiProblem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingMapsuiProblem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private bool isMapInitialized;

        public MainPage()
        {
            InitializeComponent();



            mapView.RotationLock = false;
            mapView.MyLocationLayer.Enabled = false;
            mapView.IsMyLocationButtonVisible = true;
            mapView.IsZoomButtonVisible = false;
            mapView.IsNorthingButtonVisible = false;
            mapView.UnSnapRotationDegrees = 30;
            mapView.ReSnapRotationDegrees = 5;

            mapView.MyLocationLayer.UpdateMyLocation(new Mapsui.UI.Forms.Position());
            Setup(mapView);

            
        }

        protected override async void OnAppearing()
        {
            if (!isMapInitialized)
            {
                Mapsui.Geometries.Point userLocationPoint = await StartGPS();
                SetMapNavigateTo(userLocationPoint, mapView);
                isMapInitialized = true;
            }
        }
        public void SetMapNavigateTo(Mapsui.Geometries.Point userLocationPoint, MapView MapView)
        {
            var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(userLocationPoint.X, userLocationPoint.Y);
            MapView.Map.Home = n => n.NavigateTo(sphericalMercatorCoordinate, mapView.Map.Resolutions[mapView.Map.Resolutions.Count - 3]);

            ((MainPageViewModel)BindingContext).SetMapView(mapView, userLocationPoint);
        }
        public void Setup(IMapControl mapControl)
        {
            mapControl.Map = CreateMap(mapView);

            ((MapView)mapControl).UseDoubleTap = true;
        }
        private static Mapsui.Map CreateMap(MapView mapView)
        {
            var map = new Mapsui.Map
            {
                CRS = "EPSG:3857",
                Transformation = new MinimalTransformation()
            };
            map.Layers.Add(OpenStreetMap.CreateTileLayer());
            map.RotationLock = true;
            return map;
        }
        private async Task<Mapsui.Geometries.Point> StartGPS()
        {
            
            var position = new Xamarin.Essentials.Location(38.784348, -9.0979471);

            
            var userLocationPoint = new Mapsui.Geometries.Point(position.Latitude, position.Longitude);
            mapView.MyLocationLayer.UpdateMyLocation(new Mapsui.UI.Forms.Position(position.Latitude, position.Longitude));
            mapView.Map.Home = n => n.NavigateTo(userLocationPoint, mapView.Map.Resolutions[mapView.Map.Resolutions.Count - 3]);
            mapView.Navigator.NavigateTo(SphericalMercator.FromLonLat(position.Longitude, position.Latitude), mapView.Map.Resolutions[mapView.Map.Resolutions.Count - 3]);

            



            return userLocationPoint;
        }
    }
}