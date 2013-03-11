using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCGuestbook.Models
{
    public class Gueskbook
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string context { get; set; }
    }
}