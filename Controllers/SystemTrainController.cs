using AutoMapper;
using Booking_TrainTickets.Core.DTO;
using Booking_TrainTickets.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainTicket_Booking.Core.Context;

namespace Booking_TrainTickets.Controllers
{
    [Route("TrainAPI/[controller]")]
    [ApiController]
    public class SystemTrainController : ControllerBase
    {
        // we need database so we inject it using constrictor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SystemTrainController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // CRUD
        [HttpPost]
        public async Task<ActionResult<Train>> CreateTrain(CreateTrainDto createTrainDto)

        {
            // Set IDENTITY_INSERT to ON for the Trains table
            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Trains ON");

            var train = _mapper.Map<Train>(createTrainDto);
            _context.Trains.Add(train);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrain), new { id = train.Id }, train);
        }

        //Read All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTrainDto>>> GetTrain(string? q)
        {
            //Get Tickets from Context
            //var tickets = await _context.Tickets.ToListAsync();

            ///check if  we have search parameter or not
            IQueryable<Train> query = _context.Trains;
            if (q is not null)
            {
                query = query.Where(t => t.TrainName.Contains(q));

            }
            var Trains = await query.ToListAsync();

            var convertedTrain = _mapper.Map<IEnumerable<GetTrainDto>>(Trains);

            return Ok(convertedTrain);
        }
        [HttpGet]
        [Route("{TrainName}")]
        public async Task<ActionResult<GetTrainDto>> GetTrainByName([FromRoute] string TrainName)
        {
            // Get train from context and check if it exists
            var train = await _context.Trains.FirstOrDefaultAsync(t => t.TrainName == TrainName);
            if (train == null)
            {
                return NotFound("Train Not Found");
            }

            // Map the train object to the GetTrainDto object
            var convertedTrain = _mapper.Map<GetTrainDto>(train);

            // Return the converted train object as an OK response
            return Ok(convertedTrain);
        }
        //Update
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditTrain([FromRoute] long id, [FromBody] UpdateTrainDto updateTrainDto)

        {
            //get ticket from context and check  if it extits
            var train = await _context.Trains.FirstOrDefaultAsync(t => t.Id == id);
            if (train == null)
            {
                return NotFound("Train Not Found");

            }
            _mapper.Map(updateTrainDto, train);
            train.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok("Train Updated sucessfully!");
        }
        //Delete
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] long id)
        {
            //get ticket from context and check  if it extits
            var train = await _context.Trains.FirstOrDefaultAsync(t => t.Id == id);
            if (train == null)
            {
                return NotFound("Train Not Found");

            }
            _context.Trains.Remove(train);
            await _context.SaveChangesAsync();

            return Ok("Train deleted Succcesully!");

        }
    }
}
