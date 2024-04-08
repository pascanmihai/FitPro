using FitProDB.Models;
using FitProDB.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private WorkerRepository _workerRepository;
        public WorkerController(WorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }


        [HttpGet("GetWorker")]
        public async Task<IActionResult>Get(long id)
        {
            var entity = await _workerRepository.GetWorker(id);
            if(entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost("AddWorker")]
        public async Task<IActionResult>AddWorker(Worker worker)
        {
            try
            {
                if (worker == null)
                    return BadRequest();
                var add = await _workerRepository.AddWorker(worker);
                if (add == null)
                    return Problem();
                return Ok(add);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message,statusCode: 500);
            }
        }


        [HttpPut("UpdateWorker")]
        public async Task<IActionResult>UpdateWorker(long id,Worker worker)
        {
            if(id != worker.Id)
                return BadRequest();
            var entity = await _workerRepository.UpdateWorker(worker);
            if (entity == null)
                return BadRequest();
            return Ok(entity);
        }


        public async Task<IActionResult>DeleteWorker(int id)
        {
            try
            {
                await _workerRepository.DeleteWorker(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error deleting");
            }
        }
    }
}
