using System.ComponentModel.DataAnnotations;

namespace GeoMVC7.Domain.Entities
{
    public interface IBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
