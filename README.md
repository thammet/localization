# Localizing applications is hard, so I made it easy

A full example implementation is included [here](https://github.com/thammet/localization/tree/main/Example), but if you are lazy like me, here is the TL;DR

# Setup
Add [thammett.Localization](https://www.nuget.org/packages/thammett.Localization/) to your project. 

# Decorating Objects 

thammett.Localization focuses on Decorating objects with attributes over writing a bunch of localization code.

Localizer requires objects to be decorated with 2 attributes 

* **LocalizeResource**
* **Localize**

# LocalizeResource Attribute

LocalizeResource is used to decorate a class like so:

```c#
[LocalizeResource(typeof(Resources.Retailer))]
public class Retailer
{
    public string Name { get; set; }
}
```

LocalizeResource requires a reference to a Type used to resolve localizations. 

These types can be a reference to a generated [Resource file](https://docs.microsoft.com/en-us/dotnet/framework/resources/creating-resource-files-for-desktop-apps) or a type that implements IResourceResolver.

> In other words, LocalizeResource will tell ILocalizer which resource to use for localization.

## Using Resource Files

Localizer supports the usage of generated resource files. 

To make Localizer use a generated resource file, simply pass a reference to the designer class. See example above.

> You can actually use any type that has a static ResourceManager property defined.  

## Using Custom Resolvers

If resource files are not an option, you may also define your own ResourceResolver like so:

``` c#
public class MyResourceResolver : IResourceResolver
{
    // .. Constructor, dependencies

    public Task<string> Resolve(object source, string value, PropertyInfo propertyInfo, CultureInfo cultureInfo)
    {
        // .. do something
    }
}
```

Afterwards, update the LocalizeResource attribute on your object like so:

```c#
[LocalizeResource(typeof(MyResourceResolver))]
public class Retailer
{
    public string Name { get; set; }
}
```

# Localize Attribute

Localize is used to tell Localizer which properties to localize. 

```c#
[LocalizeResource(typeof(MyResourceResolver))]
public class Store
{
    [Localize] public string Name { get; set; }
}
```

## Override Object ResourceResolver 

You can use a different Resource Resolver for a property like so:

```c#
[LocalizeResource(typeof(MyResourceResolver))]
public class Retailer
{
    [Localize(ResourceType = typeof(MyOtherResourceResolver))] public string Name { get; set; }
}
```

This will cause MyOtherResourceResolver to be used for the Name property, but MyResourceResolver will be used for all other properties. 


## Localize Gotchas

You must add a Localize attribute to every property you want to localize, even collections and nested objects.

```c#
[LocalizeResource(typeof(MyResourceResolver))]
public class Retailer
{
    // Must add localize to object types
    [Localize] public Store Store { get; set; }

    // Must add localize to collections
    [Localize] public List<Store> Stores { get; set; }
}
```

# Add ILocalizer Dependency ([asp.net](https://dotnet.microsoft.com/apps/aspnet) example)


Add ILocalizer to your dependency provider.

```c#
services.AddSingleton<ILocalizer, Localizer>();

// Add your custom resource resolvers
services.AddTransient<MyResourceResolver>();
services.AddTransient<MyOtherResourceResolver>();
```

> The sample above is from an application.

## Inject Localizer

After adding ILocalizer to your dependency provider, it can be injected as such:

```c#
private readonly ILocalizer _localizer;

public RetailerController(ILocalizer localizer)
{
    _localizer = localizer;
}
```

## Using Localizer

```c#
[HttpGet("retailers")]
public async Task<List<Retailer>> GetRetailers()
{
    var stores = await _retailerRepository.GetRetailers();
    return await _localizer.Localize(stores, RequestedCulture);
}

private CultureInfo RequestedCulture
{
    get
    {
        var culture = HttpContext.Request.GetTypedHeaders().AcceptLanguage.First().ToString();
        return CultureInfo.GetCultureInfo(CultureInfo.GetCultureInfo(culture).TwoLetterISOLanguageName);
    }
}
```

# Issues

Please create Pull Requests for issues.