using System.ComponentModel.DataAnnotations;

namespace GeoMVC7.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
