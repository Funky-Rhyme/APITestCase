using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using SystemsOfControlAPI.Entities.DTO;
using SystemsOfControlAPI.Entities.Enums;
using SystemsOfControlAPI.Entities.Models;
using SystemsOfControlAPI.Entities.Services;

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


        [HttpGet("GetPatients/{id}", Name = "GetPatient")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            var patientDto = new PatientDTO()
            {
                Surname = patient.Surname,
                Name = patient.Name,
                MiddleName = patient.MiddleName,
                Address = patient.Address,
                DateOfBirth = patient.DateOfBirth,
                Sex = patient.Sex,
                District = patient.District,
            };

            return Ok(patientDto);
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

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientDTO patientDto)
        {
            if (patientDto == null)
            {
                return BadRequest("Invalid patient data");
            }

            if (string.IsNullOrWhiteSpace(patientDto.Name) || string.IsNullOrWhiteSpace(patientDto.Surname))
            {
                return BadRequest("First name and surname are required");
            }

            try
            {
                var patient = new Patient 
                { 
                    Surname = patientDto.Surname,
                    Name = patientDto.Name,
                    MiddleName = patientDto.MiddleName,
                    Address = patientDto.Address,
                    DateOfBirth = patientDto.DateOfBirth,
                    Sex = patientDto.Sex,
                    District = patientDto.District,
                };

                patient.DistrictNavigation = await _context.Districts.FindAsync(patientDto.District);
                
                await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
                return CreatedAtRoute("GetPatient", new { id = patient.Id }, patientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while adding the patient");
            }
        }


        /// <param name="sortParams">0 - Sort by ID, 1 - Sort by name, 2 - Sort by Surname</param>
        /// <param name="sortingDirection">0 - Sort Ascending, 1 - Descending</param>
        [HttpGet("GetPatientsList/")]
        public async Task<ActionResult> GetPatientsList(
            [FromQuery] PaginationParams @params,
            [FromQuery] PatientsSortEnum sortParams,
            [FromQuery] SortDirectioneEnum sortingDirection)
        {
            var patients = _context.Patients.AsQueryable();

            switch (sortParams)
            {
                case PatientsSortEnum.Id:
                    {
                        patients = (sortingDirection == SortDirectioneEnum.Ascending)
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