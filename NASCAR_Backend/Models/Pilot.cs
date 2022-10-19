using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NASCAR_Backend.Models
{
    [Table("PILOT")]
    public class Pilot
    {
        [Key]
        [Required]
        [Column("PilotID")]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        public string Name { get; set; }

        [Required]
        [Column("Surname" ,TypeName = "varchar(20)")]
        public string SurName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string BirthCountry { get; set; }

        [Column(TypeName = "nvarchar(18)")]
        public string ? BirthState { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string BirthCity { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Column(TypeName = "varchar(3)")]
        public string PerformanceStatus { get; set; }

        [Required]
        public bool PlayOffStatus { get; set; }

        [Required]
        [Range(0,99)]
        public int CarsNumber { get; set; }

        [Range(0, Int32.MaxValue)]
        public int? Points { get; set; }

        public int ? TeamID{ get; set; }
        public Team ? Team { get; set; }

    }
}
