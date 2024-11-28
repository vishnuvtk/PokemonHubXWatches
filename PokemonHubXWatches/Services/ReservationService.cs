using PokemonHubXWatches.Models;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;

namespace PokemonHubXWatches.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new reservation
        public async Task<ReservationDTO> CreateReservation(ReservationDTO reservationDTO)
        {
            var reservation = new Reservation
            {
                ReservationDate = reservationDTO.ReservationDate,
                UserId = reservationDTO.UserID,
                WatchID = reservationDTO.WatchID
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            reservationDTO.ReservationID = reservation.ReservationID;
            return reservationDTO;
        }

        // Get a reservation by ID
        public async Task<ReservationDTO> GetReservation(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Watch)
                .Where(r => r.ReservationID == id)
                .Select(r => new ReservationDTO
                {
                    ReservationID = r.ReservationID,
                    ReservationDate = r.ReservationDate,
                    WatchID = r.WatchID,
                    UserID = r.UserId,
                    UserFullName = r.User.UserName,
                    WatchName = r.Watch.Name
                })
                .FirstOrDefaultAsync();

            return reservation;
        }

        // Get all reservations
        public async Task<IEnumerable<ReservationDTO>> GetAllReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Watch)
                .Select(r => new ReservationDTO
                {
                    ReservationID = r.ReservationID,
                    ReservationDate = r.ReservationDate,
                    WatchID = r.WatchID,
                    UserID = r.UserId,
                    UserFullName = r.User.UserName,
                    WatchName = r.Watch.Name
                })
                .ToListAsync();

            return reservations;
        }

        // Delete a reservation by ID
        public async Task<bool> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
