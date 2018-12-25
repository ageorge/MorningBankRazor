using System;
using System.Collections.Generic;

namespace MorningBankRazor.Models
{
    public partial class SavingAccounts
    {
        public string Username { get; set; }
        public long SavingAccountNumber { get; set; }
        public decimal? Balance { get; set; }
    }
}
