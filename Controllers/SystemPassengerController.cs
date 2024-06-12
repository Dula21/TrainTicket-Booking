using AutoMapper;
using Booking_TrainTickets.Core.DTO;
using Booking_TrainTickets.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainTicket_Booking.Core.Context;

namespace Booking_TrainTickets.Controllers
{
    
        [Route("PassengerAPI/[controller]")]
        [ApiController]
        public class SystemPassengerController : ControllerBase
        {
            // we need database so we inject it using constrictor
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public SystemPassengerController(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

        // CRUD
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePassenger([FromBody] CreatePassengerDto createPassengerDto)
        {
            var newPassenger = new Passenger { Id = 0 };

            _mapper.Map(createPassengerDto, newPassenger);

            await _context.Passengers.AddAsync(newPassenger);
            await _context.SaveChangesAsync();

            return Ok("Passenger Saved Successfully!");
        }

        //Read All
        [HttpGet]
            public async Task<ActionResult<IEnumerable<GetPassengerDto>>> GetPassenger(string? q)
            {
                //Get Tickets from Context
                //var tickets = await _context.Tickets.ToListAsync();

                ///check if  we have search parameter or not
                IQueryable<Passenger> query = _context.Passengers;
                if (q is not null)
                {
                    query = query.Where(t => t.PassengerName.Contains(q));

                }
                var Passengers = await query.ToListAsync();

                var convertedPassenger = _mapper.Map<IEnumerable<GetPassengerDto>>(Passengers);

                return Ok(convertedPassenger);
            }
            //Read one by ID
            [HttpGet]
            [Route("{id}")]
            public async Task<ActionResult<GetPassengerDto>> GetPassengerById([FromRoute] long id)
            {
                //Get ticket from context and check if it exits
                var passenger = await _context.Passengers.FirstOrDefaultAsync(t => t.Id == id);
                if (passenger == null)
                {
                    return NotFound("Passenger Not Found");

                }
                var convertedPassenger = _mapper.Map<GetPassengerDto>(passenger);
                return Ok(convertedPassenger);
            }
            //Update
            [HttpPut]
            [Route("edit/{id}")]
            public async Task<IActionResult> EditPassenger([FromRoute] long id, [FromBody] UpdatePassengerDto updatePassengerDto)

            {
                //get ticket from context and check  if it extits
                var passenger = await _context.Passengers.FirstOrDefaultAsync(t => t.Id == id);
                if (passenger == null)
                {
                    return NotFound("Passeneger Not Found");

                }
                _mapper.Map(updatePassengerDto, passenger);
                passenger.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok("Passenger Updated sucessfully!");
            }
            //Delete
            [HttpDelete]
            [Route("delete/{id}")]
            public async Task<IActionResult> DeleteTask([FromRoute] long id)
            {
                //get ticket from context and check  if it extits
                var Passenger = await _context.Passengers.FirstOrDefaultAsync(t => t.Id == id);
                if (Passenger == null)
                {
                    return NotFound("Passenger Not Found");

                }
                _context.Passengers.Remove(Passenger);
                await _context.SaveChangesAsync();

                return Ok("Passenger deleted Succcesully!");

            }




















        }
    
}
