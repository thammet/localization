using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace Localization.Resolvers
{
	public interface IResourceResolver
	{
		Task<string> Resolve(object source, string value, PropertyInfo propertyInfo, CultureInfo cultureInfo);
	}
}