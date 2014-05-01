using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcMusicStore.Models
{
   public  class OrderDetail
    {
       public virtual int Id { get; set; }
       public virtual string Name { get; set; }
       public virtual Order Order { get; set; }
    }
}
