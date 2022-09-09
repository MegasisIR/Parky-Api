using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Repository
{

    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ParkDbContext _context;

        public NationalParkRepository(ParkDbContext context)
        {
            _context=context;
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark? GetNationalPark(int nationalParkId)
        {
            return _context.NationalParks.FirstOrDefault(i => i.Id.Equals(nationalParkId));
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _context.NationalParks.OrderBy(x => x.Name).ToList();
        }

        public bool NationalParkExists(int nationalParkId)
        {
            return _context.NationalParks.Any(x => x.Id.Equals(nationalParkId));
        }

        public bool NationalParkExists(string name)
        {
            return _context.NationalParks.Any(x => x.Name.ToLower().Equals(name.ToLower()));
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
