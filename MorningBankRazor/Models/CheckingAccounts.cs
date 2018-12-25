using System;
using System.Collections.Generic;

namespace MorningBankRazor.Models
{
    public partial class CheckingAccounts
    {
        public string Username { get; set; }
        public long? CheckingAccountNumber { get; set; }
        public decimal? Balance { get; set; }
    }
}
