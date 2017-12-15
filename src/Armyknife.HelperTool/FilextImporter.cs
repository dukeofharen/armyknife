using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Armyknife.HelperTool
{
    public class FilextImporter
    {
        public static string GetFileExtensionsFromWikipedia()
        {
            var result = new List<FileExtensionInfo>();
            var urls = new[]
            {
                "https://en.wikipedia.org/wiki/List_of_filename_extensions",
                "https://en.wikipedia.org/wiki/List_of_filename_extensions_(A%E2%80%93E)",
                "https://en.wikipedia.org/wiki/List_of_filename_extensions_(F%E2%80%93L)",
                "https://en.wikipedia.org/wiki/List_of_filename_extensions_(M%E2%80%93R)",
                "https://en.wikipedia.org/wiki/List_of_filename_extensions_(S%E2%80%93Z)"
            };

            foreach(var url in urls)
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);

                var tables = doc.DocumentNode
                    .Descendants("body")
                    .Select(d => d.Descendants()
                    .Where(e => e.Attributes["class"]?.Value?.Contains("wikitable") ?? false)
                    .ToArray());
                foreach(var table in tables)
                {
                    var trs = table
                        .SelectMany(e => e.Descendants())
                        .Where(e => e.Name == "tr")
                        .ToArray();
                    foreach(var tr in trs)
                    {
                        var tds = tr
                            .Descendants()
                            .Where(e => e.Name == "td")
                            .ToArray();
                        if (tds.Any())
                        {
                            result.Add(new FileExtensionInfo
                            {
                                Extension = tds[0].InnerText,
                                Description = tds[1].InnerText,
                                UsedBy = tds.Length < 3 ? string.Empty : tds[2].InnerText
                            });
                        }
                    }
                }
            }

            return JsonConvert.SerializeObject(result);
        }

        private class FileExtensionInfo
        {
            public string Extension { get; set; }

            public string Description { get; set; }

            public string UsedBy { get; set; }
        }
    }
}
