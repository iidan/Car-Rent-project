using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class Login
    {
        [Key]
        [Required]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Your ID have to be 5 numbers.")]
        public string ID { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "* Your Payment have to be between 5 to 10 numbers")]
        public string Visa { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "* Your OrderID have to be between 5 to 10 numbers")]
        public string OrderID { get; set; }

        public string StartDate { get; set;}
        public string EndDate { get; set; }
        public string CarSelect { get; set;}
    }
}