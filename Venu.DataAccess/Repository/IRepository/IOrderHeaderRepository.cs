using System;
using Venu.Models.Models;
namespace Venu.DataAccess.Repository.IRepository
{
	public interface IOrderHeaderRepository: IRepository<OrderHeader>
	{
		void Update(OrderHeader obj);
		//based on id only update orderstatus and payment status
		void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null);
		void UpdateStripePayment(int Id, string sessionId, string paymentIntentId);
		//Now implement above two in OrderHeaderRepository
		
	}
}

