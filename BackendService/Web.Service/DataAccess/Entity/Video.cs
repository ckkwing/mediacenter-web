using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Service.DataAccess.Entity
{
    [Table("video")]
    [Comment("All videos")]
    public class Video : BaseEntity
    {
       

        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("suffix")]
        public string Suffix { get; set; } = string.Empty;
        [Column("size")]
        [Comment("In bytes(unit)")]
        public long Size { get; set; }

    }
}
