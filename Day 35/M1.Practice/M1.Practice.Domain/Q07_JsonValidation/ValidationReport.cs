using System.Collections.Generic;

namespace M1.Practice.Domain.Q07_JsonValidation
{
    public class ValidationReport
    {
        public int Total { get; set; }

        public int Valid { get; set; }

        public int Invalid { get; set; }

        public List<ValidationError> Errors { get; set; }
            = new();
    }
}
