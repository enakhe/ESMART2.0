﻿#nullable disable

using ESMART.Domain.Entities.FrontDesk;
using ESMART.Domain.Entities.Transaction;

namespace ESMART.Domain.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        public string TransactionId { get; set; }
        public string Guest { get; set; }
        public string GuestPhoneNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalVAT { get; set; }
        public decimal TotalServiceCharge { get; set; }
        public decimal TotalReceivables { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalRefund { get; set; }
        public string InvoiceNumber { get; set; }
        public string Description { get; set; }
        public string IssuedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public ICollection<TransactionItem> TransationItem { get; set; }
        public Booking Booking { get; set; }
    }
}
