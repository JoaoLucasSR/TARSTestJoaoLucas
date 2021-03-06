using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TARSTestJoaoLucas.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public double WorkedHours { get; set; }

        [ForeignKey("Worker")]
        public int WorkerId { get; set; }
        [JsonIgnore]
        public virtual Worker Worker { get; set; }
    }
}