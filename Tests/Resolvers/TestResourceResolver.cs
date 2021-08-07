using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Localization.Resolvers;

namespace Tests.Resolvers
{
	public class TestResourceResolver : IResourceResolver
	{
		public Task<string> Resolve(object source, string value, PropertyInfo propertyInfo, CultureInfo cultureInfo)
		{
			return Task.FromResult("Test Resource Resolver");
		}
	}
}