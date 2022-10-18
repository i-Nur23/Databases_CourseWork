using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NASCAR_Backend.Models
{
    [Table("TRACK")]
    public class Track
    {
        [Key]
        [Required]
        [Column("TrackID")]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Column("TracksName", TypeName = "varchar(60)")]
        public string Name { get; set; }

        [Required]
        [Column("TracksType", TypeName = "char(2)")]
        public string Type { get; set; }

        [Required]
        [Column("TracksState", TypeName = "nvarchar(18)")]
        public string State { get; set; }

        [Required]
        [Column("TracksCity", TypeName = "nvarchar(18)")]
        public string City { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double Length { get; set; }

    }
}
