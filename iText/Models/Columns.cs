using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iText.Models
{
    public class Columns
    {
        [Required]
        [Range(1,50, ErrorMessage = "The length of the column should be in the range from 1 to 50")]
        public int ColumnWidth_1 { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "The length of the column should be in the range from 1 to 50")]
        public int ColumnWidth_2 { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "The length of the column should be in the range from 1 to 50")]
        public int ColumnWidth_3 { get; set; }

        [Required]
        public string ColumnText_1 { get; set; }

        [Required]
        public string ColumnText_2 { get; set; }

        [Required]
        public string ColumnText_3 { get; set; }
    }
}