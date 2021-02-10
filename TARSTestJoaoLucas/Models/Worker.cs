using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TARSTestJoaoLucas.Models
{
     public class Worker
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}