using System.Collections.Generic;

namespace UploaderService.Results
{
    public class ValidationResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();

    }
}
