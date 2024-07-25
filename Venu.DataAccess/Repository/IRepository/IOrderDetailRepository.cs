using System;
using Venu.Models.Models;
namespace Venu.DataAccess.Repository.IRepository
{
	public interface IOrderDetailRepository: IRepository<OrderDetail>
	{
		void Update(OrderDetail obj);
		
	}
}

