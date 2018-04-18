using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class Cars
    {
        [Key]
        public string CarID { get; set; }

        public string CarType { get; set; }

        public string CarClass { get; set; }

        public string Available { get; set; }
    }
}