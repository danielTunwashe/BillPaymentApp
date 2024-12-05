using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPaymentApp
{
    internal class Transaction
    {
        public string Username { get; set; }

        public decimal Amount { get; set; }

        public string TransactionStatus { get; set; }

        public string Remark { get; set; }

        public DateTime DateSent { get; set; }
    }
}
