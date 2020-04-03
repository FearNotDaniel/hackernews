using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace HackerNewsScraper.Tests
{
    public class ResourceLoader
    {
        public static HtmlDocument LoadHtmlDocument(string resourceName)
        {
            // Facilitate loading of Html documents by tests. Docs are embedded as resources to enable
            // tests to run correctly during remote build on GitHub server.
            var doc = new HtmlDocument();
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                doc.Load(stream);

            return doc;
        }
    }
}
