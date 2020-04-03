![.NET Core Tests](https://github.com/FearNotDaniel/hackernews/workflows/.NET%20Core%20Tests/badge.svg)

# hackernews
Console app to scrape entries from HN and convert to Json.

# Quick Start

To run in a Docker container and output the first 50 posts:

`> docker build -t hn github.com/FearNotDaniel/hackernews`

`> docker run hn`

# Libraries Used

* .Net Core 2.1
* HtmlAgilityPack
* Fizzler for HtmlAgilityPack
* McMaster CommandLineUtils
