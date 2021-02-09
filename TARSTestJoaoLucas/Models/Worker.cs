using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TARSTestJoaoLucas.Models
{
     public class Worker
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(60)]
        public string Name { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}