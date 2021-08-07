using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Localization.Attributes;
using Localization.Extensions;

namespace Localization
{
	public class Localizer : ILocalizer
	{
		private readonly IServiceProvider _serviceProvider;

		public Localizer(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		
		public async Task<T> Localize<T>(T source, string cultureName)
		{
			var culture = CultureInfo.GetCultureInfo(cultureName);

			await Localize(source, culture);

			return source;
		}

		public async Task<T> Localize<T>(T source, CultureInfo culture)
		{
			if (source is IEnumerable sourceEnumerable)
			{
				foreach (var item in sourceEnumerable)
				{
					await Localize(item, culture);
				}

				return source;
			}
			
			await LocalizeObject(source, culture);

			return source;
		}
		
		private async Task LocalizeObject(object source, CultureInfo culture)
		{
			var localizeResourceAttribute = source.GetType().GetCustomAttribute<LocalizeResourceAttribute>();
			
			var resourceResolver = _serviceProvider.GetResourceResolver(localizeResourceAttribute?.ResourceType);
			
			var properties = source.GetType().GetProperties();
			
			foreach (var property in properties)
			{
				var localizeAttribute = property.GetCustomAttribute<LocalizeAttribute>();
				
				if (localizeAttribute != null)
				{
					var value = property.GetValue(source);

					switch (value)
					{
						case string s:
							var propertyResourceResolver = _serviceProvider.GetResourceResolver(localizeAttribute.ResourceType) ?? resourceResolver;
							if (propertyResourceResolver != null)
							{							
								value = await propertyResourceResolver.Resolve(source, s, property, culture);
							}
							
							property.SetValue(source, value);
							break;
						case null:
							break;
						default:
							await Localize(value, culture);
							break;
					}
				}
			}
		}
	}
}