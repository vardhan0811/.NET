using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightSearchEngine.Models
{
    public class SearchViewModel
    {
        [Required]
        public string Source { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        [Range(1, 10)]
        public int NumberOfPersons { get; set; }

        // IMPORTANT FIX
        [ValidateNever]
        public SelectList SourceList { get; set; }

        [ValidateNever]
        public SelectList DestinationList { get; set; }
    }
}