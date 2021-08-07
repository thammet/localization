using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Localization
{
	public interface ILocalizer
	{
		Task<T> Localize<T>(T source, string cultureName);
		
		Task<T> Localize<T>(T source, CultureInfo culture);
	}
}