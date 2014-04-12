﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MvcShopping.Models
{
    [DisplayName("訂單明細")]
    [DisplayColumn("Name")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("訂單主檔")]
        [Required]
        public virtual OrderHeader OrderHeader { get; set; }

        [DisplayName("訂購商品")]
        [Required]
        public Product Product { get; set; }

        [DisplayName("商品售價")]
        [Required(ErrorMessage = "請輸入商品售價")]
        [Range(99, 10000, ErrorMessage = "商品售價必須介於 99 ~ 10,000 之間")]
        [Description("由於商品售價可能會經常異動，因此必須保留購買當下的商品售價")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

    }
}
