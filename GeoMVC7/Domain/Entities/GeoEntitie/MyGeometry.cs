using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GeoMVC7.Domain.Entities.GeoEntitie
{
    public class MyGeometry
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
        //Долгота
        [Required(ErrorMessage = "Введите значение")]
        [RegularExpression(@"^[\-\+]?(0(\.\d{1,10})?|([1-9](\d)?)(\.\d{1,10})?|1[0-7]\d{1}(\.\d{1,10})?|180\.0{1,10})$", ErrorMessage = "Координаты не соответствуют формату")]
        public double CoordinatesLng { get; set; }

        //Широта
        [Required(ErrorMessage = "Введите значение")]
        [RegularExpression(@"^[\-\+]?((0|([1-8]\d?))(\.\d{1,10})?|90(\.0{1,10})?)$", ErrorMessage = "Координаты не соответствуют формату")]
        public double CoordinatesLat { get; set; }

        public MyPage? MyPage { get; set; }
    }
}
