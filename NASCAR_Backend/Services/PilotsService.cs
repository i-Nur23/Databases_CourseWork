﻿using NASCAR_Backend.Repositories;
using NASCAR_Backend.Models;
using NASCAR_Backend.Controllers.Jsons;

namespace NASCAR_Backend.Services
{
    public class PilotsService
    {
        private readonly PilotsRepository _pilotsRepository;
        private readonly ChangesRepository _changesRepository;

        public PilotsService(PilotsRepository repository, ChangesRepository changesRepository)
        {
            _pilotsRepository = repository;
            _changesRepository = changesRepository;
        }

        public async Task<IEnumerable<Pilot>> GetParticipatingPilots()
        {
            return await _pilotsRepository.GetParticipatingPilots();
        }
        public async Task<IEnumerable<Pilot>> GetPilotsToAddResult(List<int> IDs)
        {
            var pilots = await _pilotsRepository.GetParticipatingPilots();
            return pilots.Where(x => IDs.Contains(x.Id));
        }

        public async Task<IEnumerable<Pilot>> GetPilotsAsync()
        {
            var pilots = await _pilotsRepository.GetPilotsByOrder();
            return pilots;
        }

        public async Task<IEnumerable<Pilot>> GetTopFivePilotsAsync()
        {
            var pilots = await _pilotsRepository.GetPilotsByOrder();
            return pilots.Take(5);
        }

        public async Task AddPilotAsync(PilotToUpdate pilot)
        {
            var pilotWithSameNum = pilot.Number != 0 ? await _pilotsRepository.GetByNumber(pilot.Number) : null;
            if (pilotWithSameNum != null)
            {
                await _changesRepository.SetCarsNumberAsync(pilotWithSameNum, 0);
            }

            var newPilot = new Pilot()
            {
                Name = pilot.Name,
                SurName = pilot.SurName,
                BirthCountry = pilot.Country,
                BirthState = (pilot.State.Length == 0) ? null : pilot.State,
                BirthCity = pilot.City,
                BirthDate = pilot.birthDate,
                PerformanceStatus = pilot.Status,
                Points = 0,
                Wins = 0,
                PlayOffStatus = false,
                TeamID = pilot.Team
            };

            await Task.Run(async () => {
                await _pilotsRepository.AddPilotAsync(newPilot);
                await _changesRepository.SetCarsNumberAsync(newPilot, pilot.Number);
            });
        }
    }
}
