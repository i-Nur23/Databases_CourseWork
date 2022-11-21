using Microsoft.EntityFrameworkCore;
using NASCAR_Backend.Context;
using NASCAR_Backend.Controllers.Jsons;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Repositories
{
    public class PilotsRepository
    {
        private readonly NascarDbContext _context;

        public PilotsRepository(NascarDbContext context)
        {
            _context = context;
        }


        public async Task<HashSet<int>> GetPilotsWonInCurrentRound(int currentStage)
        {
            var set = new HashSet<int>();

            var downLimit = 27;
            var upLimit = 29;

            switch (true)
            {
                case true when currentStage >= 30 && currentStage <= 32:
                    downLimit = 30;
                    upLimit = 32;
                    break;
                case true when currentStage >= 33 && currentStage <= 35:
                    downLimit = 33;
                    upLimit = 35;
                    break;
                default:
                    break;
            }

            set = _context.Results
                .Where(x => x.StageID >= downLimit && x.StageID <= upLimit)
                .Where(x => x.Pilot.PlayOffStatus)
                .Select(x => x.PilotID)
                .ToHashSet();

            return set;
        }

        public async Task<Dictionary<int, int>> GetDictinaryOfExtraPointsForTopTen()
        {
            var dict = new Dictionary<int, int>();

            var sortedPilotsList = _context.Results
                .GroupBy(x => new {pilot = x.Pilot, points = _context.Results.Sum(x => x.Pilot.Points) })
                .OrderByDescending(x => x.Key.pilot.Wins)
                .ThenByDescending(x => x.Key.pilot.Points)
                .Take(10)
                .Select(x => x.Key.pilot)
                .ToList();

            dict.Add(sortedPilotsList[0].Id, 15);
            dict.Add(sortedPilotsList[1].Id, 10);
            dict.Add(sortedPilotsList[2].Id, 8);
            dict.Add(sortedPilotsList[3].Id, 7);
            dict.Add(sortedPilotsList[4].Id, 6);
            dict.Add(sortedPilotsList[5].Id, 5);
            dict.Add(sortedPilotsList[6].Id, 4);
            dict.Add(sortedPilotsList[7].Id, 3);
            dict.Add(sortedPilotsList[8].Id, 2);
            dict.Add(sortedPilotsList[9].Id, 1);

            return dict;
        }

        public async Task<IEnumerable<Pilot>> GetParticipatingPilots()
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;
            return await _context.Pilots
                .Where(p => p.CarsNumber != 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pilot>> GetPilotsByOrder()
        {
            return await _context.Pilots
                .OrderByDescending(p => p.Wins)
                .ThenByDescending(p => p.Points)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pilot>> GetPilotsByPoints()
        {
            return await _context.Pilots
                .OrderByDescending(p => p.Points)
                .ToListAsync();
        }

        public async Task SetPilotspoints(List<Result> results)
        {
            await Task.Run(async () =>
            {
                //if (results[0].StageID != 26 || results[0].StageID != 29 || results[0].StageID != 32 || results[0].StageID != 35)
                //{
                    foreach (var item in results)
                    {
                        int points;
                        var pilot = _context.Pilots.FirstOrDefault(x => x.Id == item.PilotID);
                        if (item.Place == 1)
                        {
                            pilot.Wins += 1;
                            points = 40;
                            /*if (pilot.PlayOffStatus)
                            {
                                PilotsWonInPlayOff.Add(pilot.Id);
                            }*/
                        }
                        else
                        {
                            points = 35 - (item.Place - 2);
                            if (points <= 0) { points = 1; }
                        }
                        pilot.Points += points;
                        _context.Pilots.Update(pilot);
                        _context.SaveChanges();
                    }

                var PlayOffPointsForTopTen = await GetDictinaryOfExtraPointsForTopTen();
                //}
                if (results[0].StageID == 26)
                {
                    var pilots = _context.Pilots
                        .OrderByDescending(x => x.Wins)
                        .ThenByDescending(x => x.Points)
                        .ToList();

                    for (int i = 0; i < 16; i++)
                    {
                        pilots[i].PlayOffStatus = true;
                        pilots[i].Points = 2000 + pilots[i].Wins * 5;
                        if (PlayOffPointsForTopTen.ContainsKey(pilots[i].Id)) { pilots[i].Points += PlayOffPointsForTopTen[pilots[i].Id]; }
                    }
                    _context.Pilots.UpdateRange(pilots);
                    _context.SaveChanges();
                }

                else if (results[0].StageID == 29) 
                {
                    var PilotsWonInPlayOff = await GetPilotsWonInCurrentRound(29);

                    var pilots = _context.Pilots
                        .Where(pilot => pilot.PlayOffStatus)
                        .OrderByDescending(x => x.Points)
                        .ToList();

                    pilots.RemoveAll(pilot => PilotsWonInPlayOff.Contains(pilot.Id));

                    foreach (var item in PilotsWonInPlayOff)
                    {
                        pilots.Insert(0, _context.Pilots.FirstOrDefault(p => p.Id == item));
                    }

                    for (int i = 0; i < 12; i++)
                    {
                        pilots[i].Points = 3000 + pilots[i].Wins * 5;
                        if (PlayOffPointsForTopTen.ContainsKey(pilots[i].Id)) { pilots[i].Points += PlayOffPointsForTopTen[pilots[i].Id]; }
                    }
                    for (int i = 12; i < 16; i++)
                    {
                        pilots[i].PlayOffStatus = false;
                    }

                    _context.Pilots.UpdateRange(pilots);
                    _context.SaveChanges();
                }

                else if (results[0].StageID == 32)
                {
                    var PilotsWonInPlayOff = await GetPilotsWonInCurrentRound(32);

                    var pilots = _context.Pilots
                        .Where(pilot => pilot.PlayOffStatus)
                        .OrderByDescending(x => x.Points)
                        .ToList();

                    pilots.RemoveAll(pilot => PilotsWonInPlayOff.Contains(pilot.Id));

                    foreach (var item in PilotsWonInPlayOff)
                    {
                        pilots.Insert(0, _context.Pilots.FirstOrDefault(p => p.Id == item));
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        pilots[i].Points = 4000 + pilots[i].Wins * 5;
                        if (PlayOffPointsForTopTen.ContainsKey(pilots[i].Id)) { pilots[i].Points += PlayOffPointsForTopTen[pilots[i].Id]; }
                    }
                    for (int i = 8; i < 12; i++)
                    {
                        pilots[i].PlayOffStatus = false;
                    }
                    _context.Pilots.UpdateRange(pilots);
                    _context.SaveChanges();
                }

                else if (results[0].StageID == 35)
                {
                    var PilotsWonInPlayOff = await GetPilotsWonInCurrentRound(35);

                    var pilots = _context.Pilots
                        .Where(pilot => pilot.PlayOffStatus)
                        .OrderByDescending(x => x.Points)
                        .ToList();

                    pilots.RemoveAll(pilot => PilotsWonInPlayOff.Contains(pilot.Id));

                    foreach (var item in PilotsWonInPlayOff)
                    {
                        pilots.Insert(0, _context.Pilots.FirstOrDefault(p => p.Id == item));
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        pilots[i].Points = 5000;
                    }

                    for (int i = 4; i < 8; i++)
                    {
                        pilots[i].PlayOffStatus = false;
                    }
                    _context.Pilots.UpdateRange(pilots);
                    _context.SaveChanges();
                } 

            });
        }

        public async Task AddPilotAsync(Pilot pilot)
        {
            await Task.Run(() =>
            {
                _context.Pilots.Add(pilot);
                _context.SaveChanges();
            });
        }

        public async Task<Pilot> GetByNumber(int number)
        {
            return await _context.Pilots.FirstOrDefaultAsync(x => x.CarsNumber == number);
        }

        public async Task<Pilot> GetById(int id)
        {
            return await _context.Pilots.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task PutPilot(PilotToUpdate newInfo, Pilot currentPilot)
        {
            currentPilot.Name = newInfo.Name;
            currentPilot.SurName = newInfo.SurName;
            currentPilot.BirthDate = newInfo.birthDate;
            currentPilot.BirthCountry = newInfo.Country;
            currentPilot.BirthState = newInfo.State;
            currentPilot.BirthCity = newInfo.City;
            currentPilot.PerformanceStatus = newInfo.Status;
            currentPilot.TeamID = newInfo.Team;

            _context.Pilots.Update(currentPilot);
            _context.SaveChanges();
        }

        public async Task<int> GetTeamsPoints(Team team)
        {
            return await _context.Pilots
                .Where(p => p.Team == team)
                .SumAsync(p => p.Points); 
        }

        public async Task<int> GetmanufacturerPoints(Manufacturer manufacturer)
        {
            return await _context.Pilots
                .Where(p => p.Team.Manufacturer == manufacturer)
                .SumAsync(p => p.Points);
        }

        public async Task<int> GetPilotsCount()
        {
            return await _context.Pilots.CountAsync();
        }

    }
}
