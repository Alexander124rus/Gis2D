using GeoMVC7.Domain.Entities.GeoEntitie;

namespace GeoMVC7.Service
{
    public class PointLayersService
    {
        public MyGeometry MyGeometry(MyGeometry myGeometry)
        {
            switch (myGeometry.MyPage.Layers)
            {
                case "people":
                    myGeometry.MyPage.Name_ru = "Люди";
                    myGeometry.MyPage.Image = "mapbox-marker-icon-green";
                    break;
                case "places":
                    myGeometry.MyPage.Name_ru = "Места";
                    myGeometry.MyPage.Image = "mapbox-marker-icon-red";
                    break;
            }
            return myGeometry;
        }
    }
}
