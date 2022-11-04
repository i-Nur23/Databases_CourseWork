using Microsoft.EntityFrameworkCore;
using NASCAR_Backend.Context;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Repositories
{
    public class PilotsRepository
    {
        private readonly NascarDbContext _context;
        private readonly Dictionary<int, int> PlayOffPointsForTopTen = new Dictionary<int, int>();
        private readonly HashSet<int> PilotsWonInPlayOff = new HashSet<int>();


        public PilotsRepository(NascarDbContext context)
        {
            _context = context;
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

        public async Task SetPilotspoints(List<Result> results)
        {
            await Task.Run(() =>
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
                            if (pilot.PlayOffStatus)
                            {
                                PilotsWonInPlayOff.Add(pilot.Id);
                            }
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
                //}
                if (results[0].StageID == 26)
                {
                    var pilots = _context.Pilots
                        .OrderByDescending(x => x.Wins)
                        .ThenByDescending(x => x.Points)
                        .ToList();


                    PlayOffPointsForTopTen.Add(pilots[0].Id, 15);
                    PlayOffPointsForTopTen.Add(pilots[1].Id, 10);
                    PlayOffPointsForTopTen.Add(pilots[2].Id, 8);
                    PlayOffPointsForTopTen.Add(pilots[3].Id, 7);
                    PlayOffPointsForTopTen.Add(pilots[4].Id, 6);
                    PlayOffPointsForTopTen.Add(pilots[5].Id, 5);
                    PlayOffPointsForTopTen.Add(pilots[6].Id, 4);
                    PlayOffPointsForTopTen.Add(pilots[7].Id, 3);
                    PlayOffPointsForTopTen.Add(pilots[8].Id, 2);
                    PlayOffPointsForTopTen.Add(pilots[9].Id, 1);

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
                    var pilots = _context.Pilots
                        .Where(pilot => pilot.PlayOffStatus)
                        .OrderByDescending(x => x.Wins)
                        .ThenByDescending(x => x.Points)
                        .ToList();

                    pilots.RemoveAll(pilot => PilotsWonInPlayOff.Contains(pilot.Id));
                    foreach (var item in PilotsWonInPlayOff)
                    {
                        pilots.Insert(0, _context.Pilots.FirstOrDefault(p => p.Id == item));
                    }

                    PilotsWonInPlayOff.Clear();

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
                    var pilots = _context.Pilots
                        .Where(pilot => pilot.PlayOffStatus)
                        .OrderByDescending(x => x.Wins)
                        .ThenByDescending(x => x.Points)
                        .ToList();

                    pilots.RemoveAll(pilot => PilotsWonInPlayOff.Contains(pilot.Id));
                    foreach (var item in PilotsWonInPlayOff)
                    {
                        pilots.Insert(0, _context.Pilots.FirstOrDefault(p => p.Id == item));
                    }

                    PilotsWonInPlayOff.Clear();

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
                    var pilots = _context.Pilots
                        .Where(pilot => pilot.PlayOffStatus)
                        .OrderByDescending(x => x.Wins)
                        .ThenByDescending(x => x.Points)
                        .ToList();

                    pilots.RemoveAll(pilot => PilotsWonInPlayOff.Contains(pilot.Id));
                    foreach (var item in PilotsWonInPlayOff)
                    {
                        pilots.Insert(0, _context.Pilots.FirstOrDefault(p => p.Id == item));
                    }

                    PilotsWonInPlayOff.Clear();

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
    }
}
