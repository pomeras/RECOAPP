using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RECOApp.Models
{
    public class Page
    {
        public Guid Id { get; set; }
        [Required]
        public Guid DocumentId { get; set; }

        [Display(Name = "Page Number")]
        [Required]
        [Range(1, 100)]
        public int Number { get; set; }
        [Required]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }
}
