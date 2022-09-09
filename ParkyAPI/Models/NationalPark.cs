using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkyAPI.Models
{
    public class NationalPark
    {
        public NationalPark(int id, string name, string state)
        {
            Id=id;
            Name=name;
            State=state;
            Created = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string State { get; private set; }
        public DateTime Created { get; private set; }
        public byte[]? Picture { get; set; }
        public DateTime? Established { get; set; }
        public void UpdateInfo(string name, string state)
        {
            Name = name;
            State = state;
        }
    }
}
