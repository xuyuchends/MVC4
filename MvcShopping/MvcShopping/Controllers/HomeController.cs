using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcShopping.Controllers
{
    public class HomeController : Controller
    {
        MvcShoppingContext db = new MvcShoppingContext();
        // 首頁
        public ActionResult Index()
        {
            var data = db.ProductCategories.ToList();
            if (data.Count == 0)
            {
                db.ProductCategories.Add(new ProductCategory() { Id = 1, Name = "文具" });
                db.ProductCategories.Add(new ProductCategory() { Id = 2, Name = "礼品" });
                db.ProductCategories.Add(new ProductCategory() { Id = 3, Name = "数据" });
                db.ProductCategories.Add(new ProductCategory() { Id = 4, Name = "书籍" });
                db.SaveChanges();
                data = db.ProductCategories.ToList();
            };
            return View(data);
        }

        // 商品列表
        public ActionResult ProductList(int id)
        {
            var productCategory = db.ProductCategories.Find(id);
            if (productCategory != null)
            {
                var data = productCategory.products.ToList();
                if (data.Count == 0)
                {
                    productCategory.products.Add(new Product()
                    {
                        Name = productCategory.Name + "类别下的商品1",
                        Color = Color.Red,
                        Description = "N/A",
                        Price = 99,
                        PublishOn = DateTime.Now,
                        ProductCategory = productCategory
                    });
                    db.SaveChanges();
                    data = productCategory.products.ToList();
                }
                  return View(data);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // 商品明細
        public ActionResult ProductDetail(int id)
        {
            var data = db.Products.Find(id);
            return View(data);
        }
    }
}
