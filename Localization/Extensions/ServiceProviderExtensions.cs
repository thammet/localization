using System;
using Localization.Resolvers;

namespace Localization.Extensions
{
	public static class ServiceProviderExtensions
	{
		public static IResourceResolver GetResourceResolver(this IServiceProvider serviceProvider, Type type)
		{
			if (type == null)
			{
				return null;
			}

			if (type.IsAssignableTo(typeof(IResourceResolver)))
			{
				return (IResourceResolver) serviceProvider.GetService(type);
			}

			return new ResourceResolver(type);
		}
	}
}