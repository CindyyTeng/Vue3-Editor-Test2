﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Models
{
    public partial class logistics_company
    {
        public logistics_company()
        {
            orders = new HashSet<order>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string name { get; set; }
        [StringLength(12)]
        [Unicode(false)]
        public string tel { get; set; }
        [StringLength(50)]
        public string logistics_description { get; set; }

        [InverseProperty("logistics_company")]
        public virtual ICollection<order> orders { get; set; }
    }
}