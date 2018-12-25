using System;
using System.Collections.Generic;

namespace MorningBankRazor.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            TransactionHistories = new HashSet<TransactionHistories>();
        }

        public long TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }

        public ICollection<TransactionHistories> TransactionHistories { get; set; }
    }
}
