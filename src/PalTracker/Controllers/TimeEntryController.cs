using Microsoft.AspNetCore.Mvc;
using PalTracker.Entities;
using PalTracker.Repositories;

namespace PalTracker.Controllers
{
    [Route("/time-entries")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private ITimeEntryRepository _timeEntryRepository;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            return _timeEntryRepository.Contains(id) ? (IActionResult) Ok(_timeEntryRepository.Find(id)) : NotFound();

        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_timeEntryRepository.List());
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            var createdTimeEntry = _timeEntryRepository.Create(timeEntry);

            return CreatedAtRoute("GetTimeEntry", new {id = createdTimeEntry.Id}, createdTimeEntry);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            return _timeEntryRepository.Contains(id) ? (IActionResult) Ok(_timeEntryRepository.Update(id, timeEntry)) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!_timeEntryRepository.Contains(id)) // Bertrand: personnally I would send a NoContent
            {
                return NotFound();
            }

            _timeEntryRepository.Delete(id);
            return NoContent();
        }
    }
}
