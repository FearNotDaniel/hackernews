![.NET Core Tests](https://github.com/FearNotDaniel/hackernews/workflows/.NET%20Core%20Tests/badge.svg?event=push)

# hackernews
Console app to scrape entries from HN and convert to Json.

# Quick Start

To run in a Docker container and output the first 50 posts:

`> docker build -t hn github.com/FearNotDaniel/hackernews`

`> docker run hn`

# Self Contained Executables

As a .NET Core Console App, the project can be run from the output directory in the usual way:

`> dotnet hackernews.dll --posts 50`

This requires the relevant runtime to be installed on the target machine. As an alternative, self-contained binaries for all platforms can be downloaded from the [Releases page](https://github.com/FearNotDaniel/hackernews/releases).

# Tests

Significant classes are tested with xUnit. GitHub badge above should indicate passing tests; clone the repo and open in Visual Studio to execute the tests locally from Test Explorer.

# Libraries Used

* **.Net Core 2.1**: LTS version of the 2.x series; I can also work with 3.1 but my current home dev setup only has VS2017 and new laptop delivery is delayed by the lockdown :-()
* **HtmlAgilityPack**: a popular web scraping library that I have used on several previous projects. Advantages are that it is very forgiving of poorly-formed HTML, runs on many versions of both .NET Framwework and Core, is well maintained and has good documentation.
* **Fizzler for HtmlAgilityPack**: useful extensions that allow CSS selector syntax instead of XPath notation, for a more intuitive coding experience.
* **McMaster CommandLineUtils**: originally forked from Microsoft's own CommandLineUtils, greatly eases the parsing and validation of program arguments along with an auto-generated --help command.
* **Newtonsoft.Json**: the standard Json library for aeons, only superseded by Microsoft's own native library in Core 3.x
