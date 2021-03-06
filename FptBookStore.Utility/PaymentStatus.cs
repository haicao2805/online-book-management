using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FptBookStore.Utility
{
    public class PaymentStatus
    {
        public const string Pending = "Pending";
        public const string Processing = "Processing";
        public const string Approved = "Approved";
        public const string DelayedPayment = "ApprovedForDelayedPayment";
        public const string Rejected = "Rejected";
        public const string Refunded = "Refunded";
        public const string Canceled = "Canceled";
        public const string Shipped = "Shipped";
    }
}
