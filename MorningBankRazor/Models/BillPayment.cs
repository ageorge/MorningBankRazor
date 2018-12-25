using System;
using System.Collections.Generic;

namespace MorningBankRazor.Models
{
    public partial class BillPayment
    {
        public int BillId { get; set; }
        public string BillName { get; set; }
        public string Username { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
    }
}
