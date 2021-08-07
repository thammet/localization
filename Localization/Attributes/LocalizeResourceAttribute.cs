using System;

namespace Localization.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class LocalizeResourceAttribute : Attribute
	{
		public LocalizeResourceAttribute(Type resourceType)
		{
			ResourceType = resourceType;
		}
		
		public Type ResourceType { get; set; }
	}
}