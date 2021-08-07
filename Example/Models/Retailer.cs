using System;
using System.Collections.Generic;
using Localization.Attributes;

namespace Example.Models
{
	[LocalizeResource(typeof(Resources.Retailer))]
	public class Retailer
	{
		[Localize] public string Name { get; set; }
		
		[Localize] public List<Store> Stores { get; set; }
		
		public DateTime DateCreated { get; set; }
	}
}