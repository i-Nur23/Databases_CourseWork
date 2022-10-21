using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NASCAR_Backend.Models
{
    [Table("TEAM")]
    public class Team
    {
        [Key]
        [Required]
        [Column("TeamID")]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Column("TeamsName", TypeName = "varchar(60)")]
        public string Name { get; set; }

        [Required]
        [Range(1900, 2021)]
        public int FoundationYear { get; set; }

        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Founder { get; set; }

        [Required]
        public int ManufacturerId{ get; set; }
        public virtual Manufacturer Manufacturer { get; set; } 
    }
}
