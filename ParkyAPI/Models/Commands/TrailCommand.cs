using ParkyAPI.Enums;
using ParkyAPI.Models.Dtos;

namespace ParkyAPI.Models.Commands
{
    public class TrailCommand
    {
        public string Name { get; set; }
        public double Distance { get; set; }
        public int NationalParkId { get; set; }
        public DifficultyType Difficulty { get; set; }
    }
}
