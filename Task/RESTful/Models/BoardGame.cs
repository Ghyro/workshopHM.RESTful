using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTful.Models
{
    public class BoardGame
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field must be set.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field must be set.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field must be set.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
