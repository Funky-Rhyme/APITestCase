using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using SystemsOfControlAPI.Entities.DTO;
using SystemsOfControlAPI.Entities.Enums;
using SystemsOfControlAPI.Entities.Models;

namespace SystemsOfControlAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly HospitalContext _context;

        public PatientsController(HospitalContext context)
        {
            _context = context;
        }


        [HttpGet("GetPatients/{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpDelete("DeletePatient/{id}")]
        public async Task<StatusCodeResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> ModifyPatient(int id, Patient updatedPatient)
        {
            if (id != updatedPatient.Id)
            {
                return BadRequest("Id's must be equal!");
            }

            _context.Entry(updatedPatient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPost("AddPatient/")]
        public async Task<IActionResult> AddPatient([FromBody] Patient patient)
        {
            if (patient == null)
            {
                return BadRequest("Invalid patient data");
            }

            if (string.IsNullOrWhiteSpace(patient.Name) || string.IsNullOrWhiteSpace(patient.Surname))
            {
                return BadRequest("First name and surname are required");
            }

            try
            {
                await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
                return CreatedAtRoute(nameof(AddPatient), new { patient.Id}, patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while adding the patient");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPatientsList(
            [FromQuery] PaginationParams @params,
            [FromQuery] PatientsSortEnum @sortParams,
            [FromQuery] SortDirectioneEnum @sortingDirection)
        {
            var patients = _context.Patients.AsQueryable();

            switch (sortParams)
            {
                case PatientsSortEnum.Id:
                    {
                        patients = (@sortingDirection == SortDirectioneEnum.Ascending)
                            ? patients.OrderBy(p => p.Id)
                            : patients.OrderByDescending(p => p.Id);
                        break;
                    }
                case PatientsSortEnum.Name:
                    {
                        patients = (sortingDirection == SortDirectioneEnum.Ascending)
                            ? patients.OrderBy(p => p.Name)
                            : patients.OrderByDescending(p => p.Name);
                        break;
                    }
                case PatientsSortEnum.Surname:
                    {
                        patients = (sortingDirection == SortDirectioneEnum.Ascending)
                            ? patients.OrderBy(p => p.Surname)
                            : patients.OrderByDescending(p => p.Surname);
                        break;
                    }
            }

            var patientsList = await patients
                .Skip((@params.Page - 1) * @params.ItemPerPage)
                .Take(@params.ItemPerPage)
                .ToListAsync();

            return Ok(patientsList);
        }



        private bool PatientExists(int id)
        {
            return _context.Patients.Any(p => p.Id == id);
        }




    }
}