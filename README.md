## Tessler

UI Testing Framework, provides an easy-to-use layer over Selenium.  It takes away some of the boilerplate code required to get a test suite up and running.

## Features

- Automatically deploys the preferred browser driver
- JQuery selectors for querying the web page
- Built-in screenshot handling
- Awaits pending ajax request before continueing

## Project Info

- Documentation: [github.com/infosupport/Tessler/wiki](https://www.github.com/infosupport/Tessler/wiki)
- Bugs/changes: [github.com/infosupport/Tessler/issues](https://www.github.com/infosupport/Tessler/issues)

## Build Status

**Nightly**
![alt text](http://mbuild.cloudapp.net/app/rest/builds/buildType:Tessler_Nightly/statusIcon "Nightly Build Status")

## Example

### Standard

```csharp
JQuery
  .By("div.ui-dialog h3:contains('Header') + ul li > span:contains('Label') ~ span")
  .Element()
;
```

Or

```csharp
JQuery
  .By("div.ui-dialog")
  .Children("h3:contains('Header')")
  .Next("ul li")
  .Filter("span:contains('Label')")
  .Parent()
  .Children("span")
  .Element()
;
```

### Using Page Objects

```csharp
var email = "marco@flyingpie.nl";
var name = "Marco vd Oever";

HomePage
  .Navigation()
  .ChooseUsersPage()

  .EnterEmailAddress(email)
  .EnterName(name)
  .ClickSubmit()

  .WithSimpleFormTable(email)
    .WithName(a => a.AssertEqual(name))
```

## License
[Apache 2](https://github.com/infosupport/Tessler/blob/master/LICENSE)
