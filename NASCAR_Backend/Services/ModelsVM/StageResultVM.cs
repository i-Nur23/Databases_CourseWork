namespace NASCAR_Backend.Services.ModelsVM
{ 

    public class PilotResultVM
    {
        public int Id { get; set; }
        public List<int> PilotResults { get; set; } = (new int[36]).ToList() ;
        public string Name { get; set; }
        public string SurName { get; set; }
        public bool PlayOffStatus { get; set; }
        public int Wins { get; set; }
        public int Points { get; set; }
        public bool HasWonInThisPlayOffRound { get; set; } = false;

    }
    public class TeamResultVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }
    public class ManufacturerResultVM
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public int Points { get; set; }
    }
}
