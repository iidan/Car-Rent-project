using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class MyOrder
    {
        [Required]
        [RegularExpression("^[a-z,A-Z]*$", ErrorMessage = "* Your Username have to be only words.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "* Your Username have to be between 5 and 10 words")]
        public string Username { get; set; }

        [Key]
        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "* Your OrderID have to be between 5 to 10 numbers")]
        public string Password { get; set; }
    }
}