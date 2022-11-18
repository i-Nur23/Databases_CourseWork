using NASCAR_Backend.Models;

namespace NASCAR_Backend.Services.ModelsVM
{
    public class ResultWithActualNumbersVM
    {
        public int PilotID { get; set; }
        public virtual Pilot Pilot { get; set; }
        public int StageID { get; set; }
        public virtual Stage Stage { get; set; }
        public int Place { get; set; }
        public int LeaderGap { get; set; }
        public int NumberOfPitStops { get; set; }
        public int CarsNumber { get; set; }
    }
}
