using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NASCAR_Backend.Models
{
    [Table("RESULT")]
    public class Result
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int PilotID { get; set; }
        public Pilot Pilot { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int StageID  { get; set; }
        [ForeignKey("StageID")]
        public Stage Stage { get; set; } 

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Place { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int LeaderGap { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int NumberOfPitStops { get; set; }

    }
}
