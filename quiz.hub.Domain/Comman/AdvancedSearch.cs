using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Comman
{
    public class AdvancedSearch
    {
        [MaxLength(100)]
        [Display(Name = "Search by Name")]
        public string SearchByName { get; set; } = string.Empty;

        public int pageNumber { get; set; } = 0;

        public int resultsPerPage { get; set; } = 100;

        public string SortedBy { get; set; } = string.Empty;

        public bool IsDesc { get; set; } = false;
    }
}
