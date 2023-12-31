﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Models
{
    public partial class SizeCategory
    {
        public SizeCategory()
        {
            ProductGroups = new HashSet<ProductGroup>();
        }

        [Key]
        public int SizeId { get; set; }
        [Required]
        [StringLength(50)]
        public string SizeName { get; set; }

        [InverseProperty("fk_Size")]
        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
    }
}