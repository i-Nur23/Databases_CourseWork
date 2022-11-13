using NASCAR_Backend.Models;

namespace NASCAR_Backend.Services.ModelsVM
{
    public class TeamVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoundationYear { get; set; }
        public string Founder { get; set; }
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public int Points { get; set; } 
    }
}
