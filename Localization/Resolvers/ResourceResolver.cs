using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace Localization.Resolvers
{
	public class ResourceResolver : IResourceResolver
	{
		private readonly ResourceManager _resourceManager;
		
		public ResourceResolver(IReflect type)
		{
			_resourceManager = (ResourceManager) type
				.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
				.FirstOrDefault(p => p.PropertyType == typeof(ResourceManager))
				?.GetValue(null, null);
		}
		
		public Task<string> Resolve(object source, string value, PropertyInfo propertyInfo, CultureInfo cultureInfo)
		{
			var localizedString =
				_resourceManager.GetString(value ?? string.Empty, cultureInfo);
			
			return Task.FromResult(localizedString ?? value);
		}
	}
}