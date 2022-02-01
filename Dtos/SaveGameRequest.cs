using System;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class SaveGameRequest
    {
        public int[] StaticNumbers { get; set; }
        public int[] SolvedNumbers { get; set; }
        public int TimeSpent { get; set; }
        public int Points { get; set; }
        public int Mistakes { get; set; }
        public bool Finished { get; set; }
    }
}
