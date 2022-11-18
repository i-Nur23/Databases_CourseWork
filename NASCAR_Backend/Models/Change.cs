using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NASCAR_Backend.Models
{
    [Table("CHANGE")]
    public class Change
    {
        [Key]
        [Required]
        [Column("ChangeID")]
        [Range(0, Int32.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Range(1,36)]
        public int StageNumber { get; set; }

        [ForeignKey("StageNumber")]
        public virtual Stage Stage { get; set; }

        [Range(0, 99)]
        public int OldNumber { get; set; }
        [Range(0, 99)]
        public int NewNumber { get; set; }

        [Required]
        public int PilotID { get; set; }
        public virtual Pilot Pilot { get; set; }
    }
}
