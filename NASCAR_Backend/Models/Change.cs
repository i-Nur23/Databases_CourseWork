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
        [Column(TypeName = "date")]
        public DateTime ChangesDate { get; set; }

        [Range(1, 99)]
        public int ? OldNumber { get; set; }
        [Range(1, 99)]
        public int ? NewNumber { get; set; }

        [Required]
        public int PilotID { get; set; }
        public Pilot Pilot { get; set; }
    }
}
