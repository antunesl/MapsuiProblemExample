using Mapsui;
using Mapsui.Projection;
using Mapsui.UI;
using Mapsui.UI.Forms;
using Mapsui.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMapsuiIssue.ViewModels;
using Xamarin.Forms;

namespace TestingMapsuiIssue.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


            mapView.RotationLock = false;
            mapView.UnSnapRotationDegrees = 30;
            mapView.ReSnapRotationDegrees = 5;

            Setup(mapView);

            ((MainPageViewModel)BindingContext).SetMapView(mapView);
        }

        public void Setup(IMapControl mapControl)
        {
            mapControl.Map = CreateMap();

            ((MapView)mapControl).UseDoubleTap = true;
        }

        public static Map CreateMap()
        {
            var map = new Map
            {
                CRS = "EPSG:3857",
                Transformation = new MinimalTransformation()
            };
            map.Layers.Add(OpenStreetMap.CreateTileLayer());
            //map.Widgets.Add(new ScaleBarWidget(map) { TextAlignment = Mapsui.Widgets.Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
            //map.Widgets.Add(new Mapsui.Widgets.Zoom.ZoomInOutWidget { MarginX = 20, MarginY = 40 });
            return map;
        }

    }
}