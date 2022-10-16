using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NASCAR_Backend.Models
{
    [Table("MANUFACTURER")]
    public class Manufacturer
    {
        [Key]
        [Required]
        [Column("ManufacturerID", TypeName = "int")]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        public string Brand { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Model { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string BrandsCountry { get; set; }
    }
}
