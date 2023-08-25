﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Models
{
    [Index("fk_MemberId", Name = "UQ__Alternat__3B54230C0458ABB6", IsUnique = true)]
    public partial class AlternateAddress
    {
        [Key]
        public int AddressId { get; set; }
        [StringLength(300)]
        public string AlternateAddress1 { get; set; }
        [StringLength(300)]
        public string AlternateAddress2 { get; set; }
        public int? fk_MemberId { get; set; }

        [ForeignKey("fk_MemberId")]
        [InverseProperty("AlternateAddress")]
        public virtual Member fk_Member { get; set; }
    }
}