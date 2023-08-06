using System.Diagnostics;
using SystemsOfControlAPI.Entities.Models;

namespace SystemsOfControlAPI.Controllers
{
    public class TempDataAdding
    {
        private readonly HospitalContext _context;

        public TempDataAdding(HospitalContext context)
        {
            _context = context;
        }

        public void AddData()
        {
            var tempDists = new List<District>
            {
                new District { Number = 1 },
                new District { Number = 2 },
            };

            _context.Districts.AddRange(tempDists);
            _context.SaveChanges();
        }
    }
}
