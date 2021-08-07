using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Example.Models;
using Example.Repositories;
using Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers
{
	public class RetailerController : Controller
	{
		private readonly IRetailerRepository _retailerRepository;
		private readonly ILocalizer _localizer;

		public RetailerController(IRetailerRepository retailerRepository, ILocalizer localizer)
		{
			_retailerRepository = retailerRepository;
			_localizer = localizer;
		}
		
		[HttpGet("retailers")]
		public async Task<List<Retailer>> GetRetailers()
		{
			var stores = await _retailerRepository.GetRetailers();
			return await _localizer.Localize(stores, RequestedCulture);
		}

		private CultureInfo RequestedCulture
		{
			get
			{
				var culture = HttpContext.Request.GetTypedHeaders().AcceptLanguage.First().ToString();
				return CultureInfo.GetCultureInfo(CultureInfo.GetCultureInfo(culture).TwoLetterISOLanguageName);
			}
		}
	}
}