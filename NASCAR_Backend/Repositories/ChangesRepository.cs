using NASCAR_Backend.Context;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Repositories
{
    public class ChangesRepository
    {
        private readonly NascarDbContext _context;
        private readonly StagesRepository _stagesRepository;

        public ChangesRepository(NascarDbContext context, StagesRepository stagesRepository)
        {
            _context = context;
            _stagesRepository = stagesRepository;
        }

        public async Task ChangeNumberAsync(Pilot pilot1, Pilot pilot2)
        {
            var number1 = pilot1.CarsNumber;
            var number2 = pilot2.CarsNumber;
            pilot1.CarsNumber = number2;
            pilot2.CarsNumber = number1;

            var nearestStageNumber = (await _stagesRepository.GetNearestStage()).StageNumber;

            var change1 = new Change()
            {
                OldNumber = number1,
                NewNumber = number2,
                PilotID = pilot1.Id,
                StageNumber = nearestStageNumber
            };

            var change2 = new Change()
            {
                OldNumber = number2,
                NewNumber = number1,
                PilotID = pilot2.Id,
                StageNumber = nearestStageNumber
            };

            _context.Changes.AddRange( change1, change2 );
            _context.Pilots.UpdateRange(  pilot1, pilot2 );
            _context.SaveChanges();
        }

        public async Task SetCarsNumberAsync(Pilot pilot, int number)
        {
            var nearestStageNumber = (await _stagesRepository.GetNearestStage()).StageNumber;


            Console.WriteLine(nearestStageNumber);


            var change = new Change()
            {
                OldNumber = pilot.CarsNumber,
                NewNumber = number,
                PilotID = pilot.Id,
                StageNumber = nearestStageNumber
            };

            pilot.CarsNumber = number;

            _context.Changes.Add(change);
            _context.Pilots.Update( pilot );
            _context.SaveChanges();
        }
    }
}
