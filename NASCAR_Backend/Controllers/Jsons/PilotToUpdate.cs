namespace NASCAR_Backend.Controllers.Jsons
{
    public class PilotToUpdate {
        public string Name { get; set; } 
        public string SurName { get; set; } 
        public DateTime birthDate { get; set; }
        public string Country { get; set; }
        public string ? State { get; set; }
        public string City { get; set; }
        public int Number { get; set; }

        public string Status { get; set; }

    };
}
