﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Models
{
    public partial class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int fk_CardId { get; set; }
        public int fk_ProductId { get; set; }
        [Required]
        [StringLength(700)]
        public string Description { get; set; }
        public int Qty { get; set; }

        [ForeignKey("fk_CardId")]
        [InverseProperty("CartItems")]
        public virtual ShoppingCart fk_Card { get; set; }
        [ForeignKey("fk_ProductId")]
        [InverseProperty("CartItems")]
        public virtual ProductGroup fk_Product { get; set; }
    }
}