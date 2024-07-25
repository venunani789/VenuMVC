using System;
using Venu.Models.Models;
namespace Venu.DataAccess.Repository.IRepository
{
	public interface IShopingCartRepository : IRepository<ShopingCart>
	{
		void Update(ShopingCart obj);
		
	}
}

