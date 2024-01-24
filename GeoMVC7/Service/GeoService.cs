using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using GeoMVC7.Domain.Repos.Interfaces;
using Newtonsoft.Json;

namespace GeoMVC7.Service
{
    //Служба преобразования координат точки из бд в json файл
    public class GeoService
    {
        public readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMyGeometryRepo _myGeometryRepo;
        public GeoService(IMyGeometryRepo myGeometryRepo, IWebHostEnvironment webHostEnvironment)
        {
            _myGeometryRepo = myGeometryRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Geo()
        {
            var listPoint = _myGeometryRepo.GetAll();    
            var featureList = new List<Feature>();
            foreach (var item in listPoint)
            {
                featureList.Add(new Feature(
                    new Point(new Position(item.CoordinatesLat, item.CoordinatesLng)),
                    new Dictionary<string, object> {
                        { "description", $"<div style=\"width: 300px; float: left;\"><h3>{item.MyPage.Title}</h3><p>{item.MyPage.MinDescription}</p><a href=\"Home/Mark/{item.Id}\" target=\"_blank\">Подробнее</a></div>" },
                        { "image", item.MyPage.Image },
                        { "layers", item.MyPage.Layers },
                        { "name_ru", item.MyPage.Name_ru }
                    }
                ));
            }
            var featureCollection = new FeatureCollection(featureList);
            var json = JsonConvert.SerializeObject(featureCollection);

            File.WriteAllText(_webHostEnvironment.WebRootPath+ "/js/geomarkers.json", json);
        }
    }
}
