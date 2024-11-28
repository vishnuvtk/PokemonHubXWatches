using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using System.Threading.Tasks;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsAPIController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsAPIController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/ReservationsAPI
        [HttpGet]
        public async Task<ActionResult> GetReservations()
        {
            var reservations = await _reservationService.GetAllReservations();
            return Ok(reservations);
        }

        // GET: api/ReservationsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetReservation(int id)
        {
            var reservation = await _reservationService.GetReservationById(id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        // POST: api/ReservationsAPI
        [HttpPost]
        public async Task<ActionResult> PostReservation(ReservationDTO reservationDTO)
        {
            await _reservationService.CreateReservation(reservationDTO);
            return CreatedAtAction(nameof(GetReservation), new { id = reservationDTO.ReservationID }, reservationDTO);
        }

        // PUT: api/ReservationsAPI/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutReservation(int id, ReservationDTO reservationDTO)
        {
            if (id != reservationDTO.ReservationID) return BadRequest();
            await _reservationService.UpdateReservation(id, reservationDTO);
            return NoContent();
        }

        // DELETE: api/ReservationsAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            await _reservationService.DeleteReservation(id);
            return NoContent();
        }
    }
}
