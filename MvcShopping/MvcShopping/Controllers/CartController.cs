using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcShopping.Controllers
{
    public class CartController : Controller
    {
        // 加入產品項目到購物車，如果沒有傳入 Amount 參數則預設購買數量為 1
        [HttpPost] // 因為知道要透過 AJAX 呼叫這個 Action，所以可以先標示 [HttpPost] 屬性
        public ActionResult AddToCart(int ProductId, int Amount = 1)
        {
            return View();
        }

        // 顯示目前的購物車項目
        public ActionResult Index()
        {
            return View();
        }

        // 移除購物車項目
        [HttpPost] // 因為知道要透過 AJAX 呼叫這個 Action，所以可以先標示 [HttpPost] 屬性
        public ActionResult Remove(int ProductId)
        {
            return View();
        }

        // 更新購物車中特定項目的購買數量
        [HttpPost] // 因為知道要透過 AJAX 呼叫這個 Action，所以可以先標示 [HttpPost] 屬性
        public ActionResult UpdateAmount(int ProductId, int NewAmount)
        {
            return View();
        }
    }
}
