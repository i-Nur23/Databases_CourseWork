namespace NASCAR_Backend.Controllers.Jsons
{
    public record class PilotToUpdate(string Name, string SurName, DateTime birthDate, string Country, string State, string City);
}
