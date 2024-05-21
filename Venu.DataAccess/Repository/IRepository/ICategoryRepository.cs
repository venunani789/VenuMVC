using System;
using Venu.Models.Models;
namespace Venu.DataAccess.Repository.IRepository
{
	public interface ICategoryRepository: IRepository<Catogory>
	{
		void Update(Catogory obj);
		
	}
}

