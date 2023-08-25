﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Models
{
    [Keyless]
    [Table("ProductComment")]
    public partial class ProductComment
    {
        public int Id { get; set; }
        public int fk_MemberId { get; set; }
        public int fk_ProductGroupId { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        public int Score { get; set; }
        public bool Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }

        [ForeignKey("fk_MemberId")]
        public virtual Member fk_Member { get; set; }
        [ForeignKey("fk_ProductGroupId")]
        public virtual ProductGroup fk_ProductGroup { get; set; }
    }
}