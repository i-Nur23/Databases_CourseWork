using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NASCAR_Backend.Models
{
    [Table("STAGE")]
    public class Stage
    {
        [Key]
        [Required]
        [Range(1, Int32.MaxValue)]
        public int StageNumber { get; set; }

        [Required]
        [Column("StagesName", TypeName = "varchar(60)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime EventsDate { get; set; }

        [Required]
        public int TrackID { get; set; }
        public Track Track { get; set; }
    }
}
