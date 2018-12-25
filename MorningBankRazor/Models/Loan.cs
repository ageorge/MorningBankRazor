using System;
using System.Collections.Generic;

namespace MorningBankRazor.Models
{
    public partial class Loan
    {
        public int LoanId { get; set; }
        public string LoanName { get; set; }
        public decimal? LoanAmt { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
    }
}
