﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModel
{
    [Table("Payments")]
    public class Payments
    {
        [Key]
        [Column("paymentId")]
        public Guid? PaymentId { get; set; } = Guid.NewGuid();

        [Column("Type")]
        public string? Type { get; set; }

        [Column("amount")]
        public float? Amount { get; set; }

        [Column("date_time")]
        public DateTime? DateTime { get; set; }

        [Column("bank")]
        public string? Bank { get; set; }

        [Column("image")]
        public string? Image { get; set; }
    }
}
