using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.ViewModel
{
    public class CarsInStock
    {
        public Cars car { get; set; }

        public List<Cars> cars { get; set; }
    }
}