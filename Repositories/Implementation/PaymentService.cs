﻿using System.Collections.Generic;
using RoadReady.Data;
using RoadReady.Models;

namespace RoadReady.Repositories
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public Payment GetPaymentById(int id)
        {
            return _context.Payments.Find(id);
        }

        public int AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment.PaymentId;
        }

        public string UpdatePayment(Payment payment)
        {
            var existingPayment = _context.Payments.Find(payment.PaymentId);
            if (existingPayment == null) return "Payment not found";

            existingPayment.ReservationId = payment.ReservationId;
            existingPayment.PaymentDate = payment.PaymentDate;
            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentMethod = payment.PaymentMethod;
            existingPayment.Status = payment.Status;

            _context.SaveChanges();
            return "Payment updated successfully";
        }

        public string DeletePayment(int id)
        {
            var existingPayment = _context.Payments.Find(id);
            if (existingPayment == null) return "Payment not found";

            _context.Payments.Remove(existingPayment);
            _context.SaveChanges();
            return "Payment deleted successfully";
        }
    }
}
