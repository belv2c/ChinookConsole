using System;

namespace ChinookConsole.DataAccess
{
    internal class InvoiceInfo
    {
        public string SalesAgent { get; set; }
        public string BillingCountry { get; set; }
        public string CustomerName { get; set; }
        public double Total { get; set; }
    }
}