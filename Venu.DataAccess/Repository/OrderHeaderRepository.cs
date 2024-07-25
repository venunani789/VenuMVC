using System;
using Venu.DataAccess.Data;
using Venu.Models.Models;
using Venu.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Venu.DataAccess.Repository
{

    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);

        }
        public void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromdb = _db.OrderHeaders.FirstOrDefault(u => u.id == Id);
            if (orderFromdb != null)
            {
                orderFromdb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromdb.PaymentStatus = paymentStatus;
                }
            }

        }
        

        public void UpdateStripePayment(int Id, string sessionId, string paymentIntentId)
        {
            var orderFromdb = _db.OrderHeaders.FirstOrDefault(u => u.id == Id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromdb.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromdb.PayementIntentId = paymentIntentId;
                orderFromdb.PaymentDate = DateTime.Now;
            }
        }
    }
}