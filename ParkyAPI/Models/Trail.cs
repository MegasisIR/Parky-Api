using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParkyAPI.Enums;

namespace ParkyAPI.Models
{
    public class Trail
    {
        public Trail()
        {
        }

        public Trail(int id, string name, double distance, DifficultyType difficulty,NationalPark nationalPark)
        {
            Id=id;
            Name=name;
            Distance=distance;
            Difficulty=difficulty;
            NationalPark=nationalPark;
            NationalParkId = nationalPark.Id;
            CreatedDateTime = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        [Required]
        public int NationalParkId { get; set; }
        public DifficultyType Difficulty { get; set; }

        [ForeignKey(nameof(NationalParkId))]
        public NationalPark NationalPark { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public void UpdateInfo(string name,double distance,DifficultyType difficulty)
        {
            Name = name;
            Distance = distance;
            Difficulty = difficulty;
        }
    }
}
