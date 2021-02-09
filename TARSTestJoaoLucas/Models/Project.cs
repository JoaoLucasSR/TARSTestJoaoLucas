using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TARSTestJoaoLucas.Models
{
    class Project
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public double WorkedHours { get; set; }

        [ForeignKey("Worker")]
        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
    }
}