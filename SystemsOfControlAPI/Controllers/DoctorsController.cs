using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemsOfControlAPI.Entities.Models;

namespace SystemsOfControlAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly HospitalContext _context;

        public DoctorsController(HospitalContext context)
        {
            _context = context;
        }

        [HttpGet("GetDoctor/{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<StatusCodeResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> ModifyDoctor(int id, Doctor updatedDoctor)
        {
            if (id != updatedDoctor.Id)
            {
                return BadRequest("Id's must be equal!");
            }

            _context.Entry(updatedDoctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound();
                }
                else
                    throw;
            }

            return Ok();
        }

        [HttpPost("AddDoctor/")]
        public async Task<IActionResult> AddDoctor([FromBody] Doctor doctor)
        {
            if (doctor == null)
            {
                return BadRequest("Invalid doctor data");
            }

            if (string.IsNullOrWhiteSpace(doctor.FullName))
            {
                return BadRequest("FullName are required");
            }

            try
            {
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
                return CreatedAtRoute(nameof(AddDoctor), new { doctor.Id }, doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while adding the doctor");
            }
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(p => p.Id == id);
        }

    }
}
