using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
    //    [Remote("CheckUserName","Account")]
        public string Username { get; set; }
        [Required]
        [StringLength(10,MinimumLength=2)]
        public string FirstName  { get; set; }
        [MaxWords(5)]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Range(10,30)]
        public int Age { get; set; }
        public decimal Total { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}