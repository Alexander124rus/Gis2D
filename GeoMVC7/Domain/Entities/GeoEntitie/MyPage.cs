using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoMVC7.Domain.Entities.GeoEntitie
{
    public class MyPage
    {
        [Required(ErrorMessage = "Введите значение")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? MinDescription { get; set; }
        [Required(ErrorMessage = "Введите значение")]
        public string Layers { get; set; }
        public string? Name_ru { get; set; }
        public string? Image { get; set; }

        [Key, ForeignKey("MyGeometry"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MyGeometryId { get; set; }
        [JsonIgnore]
        public MyGeometry? MyGeometry { get; set; }
    }
}
