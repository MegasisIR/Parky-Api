using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Repository
{

    public class TrailRepository : ITrailRepository
    {
        private readonly ParkDbContext _context;

        public TrailRepository(ParkDbContext context)
        {
            _context=context;
        }

        public bool CreateTrail(Trail Trail)
        {
            _context.Trails.Add(Trail);
            return Save();
        }

        public bool DeleteTrail(Trail Trail)
        {
            _context.Trails.Remove(Trail);
            return Save();
        }

        public Trail GetTrailById(int TrailId)
        {
            return _context.Trails.FirstOrDefault(i => i.Id.Equals(TrailId));
        }

        public ICollection<Trail> GetTrails()
        {
            return _context.Trails.OrderBy(x => x.Name).ToList();
        }

        public bool TrailExists(int TrailId)
        {
            return _context.Trails.Any(x => x.Id.Equals(TrailId));
        }

        public bool TrailExists(string name)
        {
            return _context.Trails.Any(x => x.Name.ToLower().Equals(name.ToLower()));
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrail(Trail Trail)
        {
            _context.Trails.Update(Trail);
            return Save();
        }

        public Trail GetTrailByNationalParkId(int nationalParkId)
        {
            return _context.Trails.FirstOrDefault(i => i.NationalParkId.Equals(nationalParkId));
        }
    }
}
