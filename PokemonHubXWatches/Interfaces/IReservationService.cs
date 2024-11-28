using PokemonHubXWatches.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonHubXWatches.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationDTO>> GetAllReservations();
        Task<ReservationDTO> GetReservationById(int reservationId);
        Task CreateReservation(ReservationDTO reservationDTO);
        Task UpdateReservation(int reservationId, ReservationDTO reservationDTO);
        Task DeleteReservation(int reservationId);
    }
}
