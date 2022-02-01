using System.Collections.Generic;

namespace api.Dtos
{
    public class IsPossibleRequestDto
    {
        public List<int> Numbers { get; set; }
        public int Index { get; set; }
        public int Value { get; set; }
    }
}
