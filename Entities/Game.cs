using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(81)]
        public string StaticNumbers { get; set; }

        [Required]
        [MaxLength(81)]
        public string SolvedNumbers { get; set; }

        [Required]
        public int TimeSpent { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int Mistakes { get; set; }

        public bool Finished { get; set; }

        [Required]
        public Guid ObjectId { get; set; }
    }
}
