using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Service.DataAccess.Entity
{
    public class BaseEntity
    {
        [Required]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        [Column("updated")]
        public DateTime Updated { get; set; } = DateTime.Now;

        [Required]
        [Column("deleted")]
        public bool Deleted { get; set; } = false;
    }
}
