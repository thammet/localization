using System;

namespace Localization.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class LocalizeAttribute : Attribute
	{
		// Override the parent resource manager
		public Type ResourceType { get; set; }
	}
}