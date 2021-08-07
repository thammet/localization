using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Models;

namespace Example.Repositories
{
	public interface IRetailerRepository
	{
		Task<List<Retailer>> GetRetailers();
	}

	public class RetailerRepository : IRetailerRepository
	{
		public async Task<List<Retailer>> GetRetailers()
		{
			return new List<Retailer>
			{
				new Retailer
				{
					Name = "Walmart Store",
					Stores = new List<Store>
					{
						new Store
						{
							Name = "Shelly's Store"
						}
					},
					DateCreated = DateTime.Now
				}
			};
		}
	}
}