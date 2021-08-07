using Localization.Attributes;

namespace Example.Models
{
	[LocalizeResource(typeof(Resources.Store))]
	public class Store
	{
		[Localize] public string Name { get; set; }
	}
}