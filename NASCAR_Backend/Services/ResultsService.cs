using NASCAR_Backend.Repositories;
using NASCAR_Backend.Models;
using static NASCAR_Backend.Controllers.Jsons.PlaceJson;

namespace NASCAR_Backend.Services
{
    public class ResultsService
    {
        private readonly ResultsRepository _resultsRepository;
        private readonly StagesRepository _stagesRepository;
        private readonly PilotsRepository _pilotsRepository;
        public ResultsService(ResultsRepository resultsRepository, StagesRepository stagesRepository, PilotsRepository pilotsRepository)
        {
            _resultsRepository = resultsRepository;
            _stagesRepository = stagesRepository;
            _pilotsRepository = pilotsRepository;
        }

        public async Task AddResultsAsync(PlaceInfo[] placeInfo)
        {
            await Task.Run(async () =>
            {
                var numOfPilots = placeInfo.Length;

                var numOfPilotsWhomGapChenges = new Random().Next(10, numOfPilots);

                var resultsProtocol = new List<Result>();
                var rand = new Random();
                var naturalCautions = rand.Next(0, 5);
                var avgNumOfPitStops = 5 + naturalCautions;
                var currentNumOfStage = (await _stagesRepository.GetNearestStage()).StageNumber;
                var gap = 0;
                for (int i = 0; i < numOfPilots; i++)
                {
                    var chance = (int)(100 * (double)placeInfo[i].order / numOfPilots);
                    var numOfPits = avgNumOfPitStops;

                    if (rand.Next(25, 100) < chance)
                    {
                        gap += 1;
                        numOfPits -= (gap / 15);
                    }

                    resultsProtocol.Add(new Result()
                    {
                        PilotID = placeInfo[i].id,
                        StageID = currentNumOfStage,
                        Place = placeInfo[i].order,
                        LeaderGap = gap,
                        NumberOfPitStops = numOfPits
                    });
                }

                await _resultsRepository.AddResult(resultsProtocol);
                await _pilotsRepository.SetPilotspoints(resultsProtocol);
                /*foreach (var item in resultsProtocol)
                {
                    Console.WriteLine(item.PilotID + " " + item.StageID + " " + item.Place + " " + item.LeaderGap + " " + item.NumberOfPitStops);
                }*/
            });
        }

        public IEnumerable<Result> GetByStageID (int stageId)
        {
            return _resultsRepository.GetByStageID(stageId);
        }


    }
}
