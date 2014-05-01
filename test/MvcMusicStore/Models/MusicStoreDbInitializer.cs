using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Models
{
    public class MusicStoreDbInitializer : DropCreateDatabaseAlways<MusicStoreDB>
    {
        protected override void Seed(MusicStoreDB context)
        {
            context.Artists.Add(new Artist { Name = "Ai Di menla" });
            context.Genres.Add(new Genre { Name = "Jazz" });
            context.Albums.Add(new Album
            {
                Artist = new Artist { Name = "Push" },
                Genre = new Genre { Name = "Rock" },
                Price = 9.99m,
                Title = "Caravan"
            });
            Order order1 = new Order();
            order1.OrderDate = DateTime.Now;
            order1.Username = "Username";
            order1.FirstName = "FirstName";
            order1.LastName = "LastName";
            order1.Address = "Address";
            order1.City = "City";
            order1.State = "State";
            order1.Age = 12;
            order1.PostalCode = "PostalCode";
            order1.Country = "Country";
            order1.Phone = "Phone";
            order1.Email = "Email";
            order1.Total = 99;
            List<OrderDetail> OrderDetails = new List<OrderDetail>();
            OrderDetails.Add(new OrderDetail { Name = "trail1" });
            order1.OrderDetails = OrderDetails;
            context.Orders.Add(order1);
            base.Seed(context);
        }
    }
}