using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.ViewModel
{
    public class OrderIHave
    {
        public MyOrder order { get; set; }

        public List<MyOrder> orders { get; set; }
    }
}