using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.ViewModel
{
    public class LoginUsers
    {
        public Login user { get; set; }

        public List<Login> users { get; set; }
    }
}