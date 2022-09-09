using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ParkyAPI.Enums;

namespace ParkyAPI.Models.Dtos
{
    public class TrailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public int NationalParkId { get; set; }
        public DifficultyType Difficulty { get; set; }

        public NationalParkDto NationalPark { get; set; }
    }
}
