using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcShopping.Controllers
{
    [Authorize] // 必須登入會員才能使用訂單結帳功能
    public class OrderController : Controller
    {
        MvcShoppingContext db = new MvcShoppingContext();
        List<Cart> Carts
        {
            get
            {
                if (Session["Carts"] == null)
                {
                    Session["Carts"] = new List<Cart>();
                }
                return (Session["Carts"] as List<Cart>);
            }
            set
            {
                Session["Carts"] = value;
            }

        }

        // 顯示完成訂單的表單頁面
        public ActionResult Complete(OrderHeader form)
        {
            var member = db.Members.Where(p => p.Email == User.Identity.Name).FirstOrDefault();
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (this.Carts.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }
            OrderHeader oh = new OrderHeader()
            {
                Member = member,
                ContactName = form.ContactName,
                ContactAddress = form.ContactName,
                ContactPhoneNo = form.ContactPhoneNo,
                BuyOn = form.BuyOn,
                Memo = form.Memo,
                OrderDetailItems = new List<OrderDetail>()
            };
            int totalPrice = 0;
            foreach (var item in this.Carts)
            {
                var product = db.Products.Find(item.Product.Id);
                if (product == null)
                {
                    return RedirectToAction("Index", "Cart");
                }
                totalPrice += item.Product.Price * item.Amount;
                oh.OrderDetailItems.Add(new OrderDetail()
                {
                    Product = product,
                    Price = product.Price,
                    Amount = item.Amount
                });
            }
            oh.TotalPrice = totalPrice;
            db.Orders.Add(oh);
            db.SaveChanges();
            this.Carts.Clear();

            return RedirectToAction("Index", "Home");
        }

        // 將訂單資料與購物車資料寫入資料庫
        public ActionResult Complete(FormCollection form)
        {
            // TODO: 將訂單資料與購物車資料寫入資料庫

            // TODO: 訂單完成後必須清空現有購物車資料

            // 訂單完成後回到網站首頁
            return RedirectToAction("Index", "Home");
        }
    }
}
