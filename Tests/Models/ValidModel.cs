using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Localization.Attributes;

namespace Tests.Models
{
	[LocalizeResource(typeof(Resources.Main))]
	public class ValidModel
	{
		[Localize]
		public string Name { get; set; } = "Hello World";

		public string Skip { get; set; } = "Hello World";

		[Localize(ResourceType = typeof(Resources.Alternative))] public string Alternative { get; set; } = "Hello World";

		[Localize(ResourceType = typeof(Resolvers.TestResourceResolver))]
		public string ResourceResolver { get; set; } = "Hello World";

		[Localize] public string NullValue { get; set; } = null;
		
		[Localize] public NestedValidModel NestedValidModel { get; set; } = new NestedValidModel();
		
		[Localize] public NestedValidModel[] NestedValidModelsArray { get; set; } = new[] { new NestedValidModel(), new NestedValidModel() };
		
		[Localize] public List<NestedValidModel> NestedValidModelsList { get; set; } = Enumerable.Repeat(new NestedValidModel(), 10).ToList();
		
		[Localize] public HashSet<NestedValidModel> NestedValidModelsHashSet { get; set; } = new HashSet<NestedValidModel> {new NestedValidModel(), new NestedValidModel()};
		
		public NestedValidModel[] NestedValidModelsArraySkip { get; set; } = new[] { new NestedValidModel(), new NestedValidModel() };
		
		public List<NestedValidModel> NestedValidModelsListSkip { get; set; } = Enumerable.Repeat(new NestedValidModel(), 10).ToList();
		
		public HashSet<NestedValidModel> NestedValidModelsHashSetSkip { get; set; } = new HashSet<NestedValidModel> {new NestedValidModel(), new NestedValidModel()};
		
		public DateTime Date { get; set; } = DateTime.Now;
	}

	[LocalizeResource(typeof(Resources.Main))]
	public class NestedValidModel
	{
		[Localize]
		public string Name { get; set; } = "Hello World";

		public string Skip { get; set; } = "Hello World";
		
		[Localize(ResourceType = typeof(Resources.Alternative))]
		public string Alternative { get; set; } = "Hello World";
	}
}