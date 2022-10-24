namespace NASCAR_Backend.Controllers.Jsons
{
        public class HomePageJson
        {
            public List<PilotInfo> Pilots { get; set; }

            public NearestStageInfo NearestStage { get; set; }
        }

        public record class PilotInfo(int Id, string Name, string Surname, int Number, int Points, string? Team);
        public record class NearestStageInfo(string Name, DateTime eventsDate, string TracksName);
   
}
