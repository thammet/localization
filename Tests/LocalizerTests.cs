using System.Threading.Tasks;
using Localization;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Tests.Models;

namespace Tests
{
	public class LocalizerTests
	{
		private ILocalizer _localizer;
		
		[SetUp]
		public void Setup()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddSingleton<Resolvers.TestResourceResolver>();
			serviceCollection.AddSingleton<ILocalizer, Localizer>();

			// Simulate asp.net app
			var serviceProvider = serviceCollection.BuildServiceProvider();
			_localizer = serviceProvider.GetRequiredService<ILocalizer>();
		}

		[Test]
		[TestCase("en", "Hello World", "Main Hello World", "Alternative Hello World")] // Default
		[TestCase("es", "Hello World", "Main Hola Mundo", "Hola mundo alternativo")]
		[TestCase("fr", "Hello World", "Main Hello World","Alternative Hello World")] // Not defined, so defaults to en
		public async Task Localizes(string cultureName, string original, string expected, string expectedAlternative)
		{
			var model = new ValidModel();

			var localizedModel = await _localizer.Localize(model, cultureName);
			
			Assert.AreEqual(expected, localizedModel.Name);
			Assert.AreEqual(original, localizedModel.Skip);
			Assert.AreEqual(expectedAlternative, localizedModel.Alternative);
			Assert.AreEqual("Test Resource Resolver", localizedModel.ResourceResolver);
			Assert.IsNull(localizedModel.NullValue);
			
			Validate_Nested_Valid_Model_Localized(localizedModel.NestedValidModel, original, expected, expectedAlternative);

			foreach (var nestedValidModel in localizedModel.NestedValidModelsArray)
			{
				Validate_Nested_Valid_Model_Localized(nestedValidModel, original, expected, expectedAlternative);
			}
			
			foreach (var nestedValidModel in localizedModel.NestedValidModelsList)
			{
				Validate_Nested_Valid_Model_Localized(nestedValidModel, original, expected, expectedAlternative);
			}
			
			foreach (var nestedValidModel in localizedModel.NestedValidModelsHashSet)
			{
				Validate_Nested_Valid_Model_Localized(nestedValidModel, original, expected, expectedAlternative);
			}
			
			foreach (var nestedValidModel in localizedModel.NestedValidModelsHashSet)
			{
				Validate_Nested_Valid_Model_Localized(nestedValidModel, original, expected, expectedAlternative);
			}
			
			foreach (var nestedValidModel in localizedModel.NestedValidModelsArraySkip)
			{
				Validate_Nested_Valid_Model_Not_Localized(nestedValidModel, original);
			}
			
			foreach (var nestedValidModel in localizedModel.NestedValidModelsListSkip)
			{
				Validate_Nested_Valid_Model_Not_Localized(nestedValidModel, original);
			}
			
			foreach (var nestedValidModel in localizedModel.NestedValidModelsHashSetSkip)
			{
				Validate_Nested_Valid_Model_Not_Localized(nestedValidModel, original);
			}
		}

		private void Validate_Nested_Valid_Model_Localized(NestedValidModel nestedValidModel, string original, string expected,
			string expectedAlternative)
		{
			Assert.AreEqual(expected, nestedValidModel.Name);
			Assert.AreEqual(original, nestedValidModel.Skip);
			Assert.AreEqual(expectedAlternative, nestedValidModel.Alternative);
		}
		
		private void Validate_Nested_Valid_Model_Not_Localized(NestedValidModel nestedValidModel, string original)
		{
			Assert.AreEqual(original, nestedValidModel.Name);
			Assert.AreEqual(original, nestedValidModel.Skip);
			Assert.AreEqual(original, nestedValidModel.Alternative);
		}

		[Test]
		[TestCase("en", "Long String")]
		[TestCase("es", "Long String in spanish")]
		public async Task Localizes_Long_String(string cultureName, string expected)
		{
			var model = new LongStringModel();

			var localizedModel = await _localizer.Localize(model, cultureName);
			
			Assert.AreEqual(expected, localizedModel.Value);
		}
	}
}