﻿using System;

namespace ChinookConsole.DataAccess
{
    internal class InvoiceInfo
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string SalesAgent { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingCountry { get; set; }
        public string CustomerName { get; set; }
        public double Total { get; set; }
    }
}