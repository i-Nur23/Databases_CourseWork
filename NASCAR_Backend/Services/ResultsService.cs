using NASCAR_Backend.Repositories;
using NASCAR_Backend.Models;
using NASCAR_Backend.Services.ModelsVM;
using static NASCAR_Backend.Controllers.Jsons.PlaceJson;
using AutoMapper;

namespace NASCAR_Backend.Services
{
    public class ResultsService
    {

        private readonly ResultsRepository _resultsRepository;
        private readonly StagesRepository _stagesRepository;
        private readonly PilotsRepository _pilotsRepository;
        private readonly ChangesRepository _changesRepository;
        private readonly TeamsRepository _teamsRepository;
        private readonly ManufacturersRepository _manufacturerRepository;
        private readonly IMapper _mapper;
        
        public ResultsService(
                                ResultsRepository resultsRepository, 
                                StagesRepository stagesRepository, 
                                PilotsRepository pilotsRepository,  
                                ChangesRepository changesRepository,
                                TeamsRepository teamsRepository,
                                ManufacturersRepository manufacturersRepository,
                                IMapper mapper)
        {
            _resultsRepository = resultsRepository;
            _stagesRepository = stagesRepository;
            _pilotsRepository = pilotsRepository;
            _changesRepository = changesRepository;
            _teamsRepository = teamsRepository;
            _manufacturerRepository = manufacturersRepository;
            _mapper = mapper;
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

                _resultsRepository.AddResult(resultsProtocol);
                _pilotsRepository.SetPilotspoints(resultsProtocol);
            });
        }

        public async Task<IEnumerable<Result>> GetByStageID (int stageId)
        {
            return await _resultsRepository.GetByStageID(stageId);
        }

        public async  Task<IEnumerable<ResultWithActualNumbersVM>> GetByStageIDWithActualNumbers(int stageId)
        {
            var finalProtocole = new List<ResultWithActualNumbersVM>();
            var resultWithoutNums = await _resultsRepository.GetByStageID(stageId);
            foreach (var pilotResult in resultWithoutNums)
            {
                var resultWithNum = _mapper.Map<ResultWithActualNumbersVM>(pilotResult);
                var pilotsNumber = await _changesRepository.GetCurrentNum(pilotResult.PilotID, pilotResult.StageID);
                if (pilotsNumber != -1)
                {
                    resultWithNum.CarsNumber = pilotsNumber;
                }
                else
                {
                    resultWithNum.CarsNumber = (await _pilotsRepository.GetById(resultWithNum.PilotID)).CarsNumber;
                }
                finalProtocole.Add(resultWithNum);
            }

            return finalProtocole;
            
        }

        public async Task<IEnumerable<int>> GetSortedListOfPilotsId(int stageNum)
        {
            if (stageNum <= 26)
            {
                var listOfPilots = await _pilotsRepository.GetPilotsByOrder();
                return listOfPilots.Select(x => x.Id);
            }
            else
            {
                var listOfPilots = await _pilotsRepository.GetPilotsByPoints();
                var resultList = new List<int>();

                var winnersAtCurrentRound = await _pilotsRepository.GetPilotsWonInCurrentRound(stageNum);
                foreach (var pilot in winnersAtCurrentRound)
                {
                    resultList.Add(pilot);
                }

                resultList = resultList.Concat(listOfPilots
                                                        .Where(x => !winnersAtCurrentRound.Contains(x.Id))
                                                        .Select(x => x.Id))
                                                        .ToList();

                resultList.OrderByDescending(x =>  _pilotsRepository.GetById(x).Result.Points);                 

                return resultList;
            }
        }

        public async Task<IEnumerable<PilotResultVM>> GetPilotsTable()
        {
            var pilotsCount = await _pilotsRepository.GetPilotsCount();
            var allPilots = await _pilotsRepository.GetPilotsByOrder();
            var resultsList = new List<PilotResultVM>();

            var currentSituation = await GetSortedListOfPilotsId(await _resultsRepository.GetNumberOfCurrentStageAsync());
            foreach (var p_id in currentSituation)
            {
                var pilot = allPilots.FirstOrDefault(x => x.Id == p_id);
                var pilotStages = await _resultsRepository.GetPilotsResults(p_id);
                var pilotRes = _mapper.Map<PilotResultVM>(pilot);
                var winnersAtCurrentRound = await _pilotsRepository.GetPilotsWonInCurrentRound(await _resultsRepository.GetNumberOfCurrentStageAsync());

                if (winnersAtCurrentRound.Contains(p_id) || (await CurrentRound()) == 0 && pilot.Wins > 0){
                    pilotRes.HasWonInThisPlayOffRound = true;
                }

                foreach (var pilotStage in pilotStages)
                {
                    pilotRes.PilotResults[pilotStage.StageID - 1] = pilotStage.Place;
                }

                resultsList.Add(pilotRes);
            }

            return resultsList;
        }

        public async Task<IEnumerable<TeamResultVM>> GetTeamsTable()
        {
            var teams = await _teamsRepository.GetAllAsync();
            var teamsTable = new List<TeamResultVM>();
            foreach (var team in teams)
            {
                var teamVM = _mapper.Map<TeamResultVM>(team);
                teamVM.Points = await _pilotsRepository.GetTeamsPoints(team);
                teamsTable.Add(teamVM);
            }
            return teamsTable.OrderByDescending(x => x.Points);
        }

        public async Task<IEnumerable<ManufacturerResultVM>> GetManufacturersTable()
        {
            var manufs = await _manufacturerRepository.GetAll();
            var manufsTable = new List<ManufacturerResultVM>();
            foreach (var manuf in manufs)
            {
                var manufVM = _mapper.Map<ManufacturerResultVM>(manuf);
                manufVM.Points = await _pilotsRepository.GetmanufacturerPoints(manuf);
                manufsTable.Add(manufVM);
            }
            return manufsTable.OrderByDescending(x => x.Points);
        }

        public async Task<int> CurrentRound()
        {
            return await _resultsRepository.GetCurrentRoundsCount();
        }

        public void DeleteAll(IEnumerable<int> pilotIds)
        {
            _resultsRepository.DeleteAll();

            if (pilotIds.Count() != 0)
            {
                _pilotsRepository.DeletePilots(pilotIds);    
            }
            _pilotsRepository.Reset();
        }

    }
}
