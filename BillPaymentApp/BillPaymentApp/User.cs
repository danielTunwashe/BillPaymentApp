using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPaymentApp
{
    internal class User
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int SecretPin { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal Wallet { get; set; }
        public bool IsLocked { get; set; }
        public int TotalLogin { get; set; }
    }
}
