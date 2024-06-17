using System;
using Venu.Models.Models;
namespace Venu.DataAccess.Repository.IRepository
{
	public interface ICompanyRepository: IRepository<Company>
	{
		void Update(Company obj);
		
	}
}

