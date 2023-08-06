using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using System.Diagnostics;
using SystemsOfControlAPI.Entities.DTO;
using SystemsOfControlAPI.Entities.Enums;
using SystemsOfControlAPI.Entities.Models;
using SystemsOfControlAPI.Entities.Services;

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

        [HttpGet("GetDoctor/{id}", Name = "GetDoctor")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);


            if (doctor == null)
            {
                return NotFound();
            }

            var doctorDto = new DoctorDTO()
            {
                FullName = doctor.FullName,
                Cabinet = Convert.ToString(doctor.Cabinet),
                Specialization = Convert.ToString(doctor.Specialization),
                District = Convert.ToString(doctor.District),
            };

            return Ok(doctorDto);
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
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDTO doctorDto)
        {
            if (doctorDto == null)
            {
                return BadRequest("Invalid doctor data");
            }

            if (string.IsNullOrWhiteSpace(doctorDto.FullName))
            {
                return BadRequest("FullName are required");
            }

            try
            {
                var doctor = new Doctor
                {
                    FullName = doctorDto.FullName,
                    Cabinet = Convert.ToInt32(doctorDto.Cabinet),
                    Specialization = Convert.ToInt32(doctorDto.Specialization),
                    District = Convert.ToInt32(doctorDto.District)
                };

                doctor.CabinetNavigation = await _context.Cabinets.FindAsync(doctorDto.Cabinet);
                doctor.SpecializationNavigation = await _context.Specializations.FindAsync(doctorDto.Specialization);
                doctor.DistrictNavigation = await _context.Districts.FindAsync(doctorDto.District);

                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
                return CreatedAtRoute("GetDoctor", new { id = doctor.Id }, doctorDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while adding the doctor");
            }
        }

        /// <param name="sortParams">0 - Sort by ID, 1 - Sort by Full name, 2 - Sort by Cabinet, 
        /// 3 - Sort by Specialization, 4 - Sort by District</param>
        /// <param name="sortingDirection">0 - Sort Ascending, 1 - Descending</param>
        [HttpGet("GetDoctorsList/")]
        public async Task<ActionResult> GetDoctorsList(
            [FromQuery] PaginationParams @params,
            [FromQuery] DoctorsPaginationEnum sortParams,
            [FromQuery] SortDirectioneEnum sortingDirection)
        {
            var doctors = _context.Doctors
                .Include(d => d.CabinetNavigation)
                .Include(d => d.SpecializationNavigation)
                .Include(d => d.DistrictNavigation)
                .AsQueryable();

            switch (sortParams)
            {
                case DoctorsPaginationEnum.Id:
                    {
                        doctors = (sortingDirection == SortDirectioneEnum.Ascending)
                            ? doctors.OrderBy(p => p.Id)
                            : doctors.OrderByDescending(p => p.Id);
                        break;
                    }
                case DoctorsPaginationEnum.FullName:
                    {
                        doctors = (sortingDirection == SortDirectioneEnum.Ascending)
                            ? doctors.OrderBy(p => p.FullName)
                            : doctors.OrderByDescending(p => p.FullName);
                        break;
                    }
                case DoctorsPaginationEnum.Cabinet:
                    {
                        doctors = (sortingDirection == SortDirectioneEnum.Ascending)
                            ? doctors.OrderBy(p => p.Cabinet)
                            : doctors.OrderByDescending(p => p.Cabinet);
                        break;
                    }

                case DoctorsPaginationEnum.Specialization:
                    {
                        doctors = (sortingDirection == SortDirectioneEnum.Ascending)
                            ? doctors.OrderBy(p => p.Specialization)
                            : doctors.OrderByDescending(p => p.Specialization);
                        break;
                    }
                case DoctorsPaginationEnum.District:
                    {
                        doctors = (sortingDirection == SortDirectioneEnum.Ascending)
                            ? doctors.OrderBy(p => p.District)
                            : doctors.OrderByDescending(p => p.District);
                        break;
                    }
            }

            var doctorsList = await doctors
                .Skip((@params.Page - 1) * @params.ItemPerPage)
                .Take(@params.ItemPerPage)
                .ToListAsync();

            List<DoctorDTO> doctorDtoList = new List<DoctorDTO>();

            foreach (var doctor in doctorsList)
                doctorDtoList.Add(
                    new DoctorDTO
                    {
                        Cabinet = Convert.ToString(doctor.CabinetNavigation.Number),
                        FullName = doctor.FullName,
                        Specialization = doctor.SpecializationNavigation.Name,
                        District = Convert.ToString(doctor.DistrictNavigation.Number),
                    });

            return Ok(doctorDtoList);
        }


        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(p => p.Id == id);
        }

    }
}
