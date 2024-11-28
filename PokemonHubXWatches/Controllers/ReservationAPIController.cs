using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using System.Threading.Tasks;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// This endpoint returns all Reservations in the system.
        /// </summary>
        /// <returns>[{Reservation},{Reservation},{Reservation}]</returns>
        /// <example>
        /// GET api/ReservationsAPI/List -> 
        /// [
        ///   {
        ///     "reservationID": 1,
        ///     "reservationDate": "2024-10-18T10:00:00",
        ///     "userID": 1,
        ///     "watchID": 1
        ///   },
        ///   {
        ///     "reservationID": 2,
        ///     "reservationDate": "2024-10-19T11:00:00",
        ///     "userID": 2,
        ///     "watchID": 2
        ///   }
        /// ]
        /// </example>
        // GET: api/ReservationsAPI/List
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }


        /// <summary>
        /// This endpoint returns a Reservation specified by its {id}.
        /// </summary>
        /// <param name="id">The Reservation ID</param>
        /// <returns>{Reservation}</returns>
        /// <example>
        /// GET api/ReservationsAPI/Find/1 -> 
        /// {
        ///   "reservationID": 1,
        ///   "reservationDate": "2024-10-18T10:00:00",
        ///   "userID": 1,
        ///   "watchID": 1
        /// }
        /// </example>
        // GET: api/ReservationsAPI/Find/{id}
        [HttpGet("Find/{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }


        /// <summary>
        /// This endpoint updates a Reservation specified by its {id}.
        /// </summary>
        /// <param name="id">The Reservation ID</param>
        /// <param name="reservation">The updated Reservation object</param>
        /// <returns>204 No Content or 404 Not Found</returns>
        /// <example>
        /// PUT api/ReservationsAPI/Update/1
        /// {
        ///   "reservationDate": "2024-11-01T15:30:00",
        ///   "userID": 1,
        ///   "watchID": 2
        /// }
        /// </example>
        // PUT: api/ReservationsAPI/Update/{id}
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.ReservationID)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        /// <summary>
        /// This endpoint adds a new Reservation.
        /// </summary>
        /// <param name="reservation">The Reservation object to add</param>
        /// <returns>{Reservation}</returns>
        /// <example>
        /// POST api/ReservationsAPI/Add
        /// {
        ///   "reservationDate": "2024-11-01T15:30:00",
        ///   "userID": 3,
        ///   "watchID": 2
        /// }
        /// </example>
        // POST: api/ReservationsAPI/Add
        [HttpPost("Add")]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.ReservationID }, reservation);
        }


        /// <summary>
        /// This endpoint deletes a Reservation specified by its {id}.
        /// </summary>
        /// <param name="id">The Reservation ID</param>
        /// <returns>204 No Content or 404 Not Found</returns>
        /// <example>
        /// DELETE api/ReservationsAPI/Delete/1
        /// </example>
        // DELETE: api/ReservationsAPI/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if a reservation exists by its {id}.
        /// </summary>
        /// <param name="id">The Reservation ID</param>
        /// <returns>True if the reservation exists, otherwise false</returns>
        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationID == id);
        }
    }
}
