using ParkyAPI.Models;

namespace ParkyAPI.Repository.IRepository
{
    public interface ITrailRepository

    {
        ICollection<Trail> GetTrails();
        Trail GetTrailById(int trailId);
        Trail GetTrailByNationalParkId(int nationalParkId);
        bool TrailExists(int trailId);
        bool TrailExists(string name);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
    }
}
