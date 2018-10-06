<a name="html-to-plain-text"></a>
# How to transform HTML to plain text

Although the HTML tags could be removed using regular expressions, the best solution is parsing the HTML code with a specific library, such as [HTMLAgilityPack](http://html-agility-pack.net/). 

The following code shows how to parse an input string with HTML code and remove all tags:

```csharp
using HtmlAgilityPack;

namespace Example {

    internal class Example
    {
		/// <summary>
		/// Convert the HTML content to plain text
		/// </summary>
		/// <param name="html">The html content which is going to be converted</param>
		/// <returns>A string</returns>
		public static string HtmlToPlainText(string html)
		{
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			return document.DocumentNode == null ? string.Empty : document.DocumentNode.InnerText;
		}	
	}
}

```
