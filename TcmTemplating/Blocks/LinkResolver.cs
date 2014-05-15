#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: LinkResolverTemplate
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using TcmTemplating.Extensions;
using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating.Blocks
{
	/// <summary>
	/// <see cref="LinkResolver" /> is a single-pass multi-replacement link resolver
	/// </summary>
	[TcmTemplateTitle("Link Resolver")]
	public class LinkResolver : TcmTemplating.TemplateBase
	{
		private class Replacement
		{
			public int Offset
			{
				get;
				set;
			}

			public int OriginalSize
			{
				get;
				set;
			}

			public String Value
			{
				get;
				set;
			}

			/// <summary>
			/// Returns a <see cref="System.String" /> that represents this instance.
			/// </summary>
			/// <returns>
			/// A <see cref="System.String" /> that represents this instance.
			/// </returns>
			public override String ToString()
			{
				return String.Format("Offset: {0}, Size: {1}, Value: {2}", Offset, OriginalSize, Value);
			}
		}

		private const String COMPONENT_TAG = "Tridion.Web:ComponentLink";
		private const String PAGE_TAG = "Tridion.Web:PageLink";
		private const String BINARY_TAG = "Tridion.Web:BinaryLink";
		private const String IMAGE_TAG = "Tridion.Web:Image";

		private static readonly Regex mTags = new Regex(@"(?<content><(?<tag>(a|img))[^>]+(href|src)[^>]+?>)", RegexOptions.Compiled | RegexOptions.Multiline);
		private static readonly Regex mAttributes = new Regex(@"(\S+)=[""']?((?:.(?![""']?\s+(?:\S+)=|[>""']))+.)[""']?", RegexOptions.Compiled | RegexOptions.Multiline);
		private static readonly Regex mLinkTitle = new Regex(@"^[\d]{1,3}\.{0,1}\s", RegexOptions.Compiled);

		private static readonly String[] mIgnoredAttributes = new String[] { "tridion:href", "xmlns:tridion", "tridion:type", "tridion:targetattribute", "href", "src" };
		private static readonly String[] mSupportedAttributes = new String[] { "href", "src", "tridion:href" };

		/// <summary>
		/// Performs the actual transformation logic of this <see cref="TemplateBase" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
			Item packageItem = Package.GetByName(Package.OutputName);

			if (packageItem != null && packageItem.ContentType.Matches(new ContentType("text/*")))
			{
				String output = packageItem.GetAsString();
				List<Replacement> replacements = new List<Replacement>();

				foreach (Match match in mTags.Matches(output))
					if (match.Success)
						replacements.AddRange(GetReplacements(match, output));

				int replacementOffset = 0;

				// Sort all replacements according first offset then to size
				foreach (Replacement replacement in replacements.OrderBy(r => r.Offset).ThenBy(r => r.OriginalSize))
				{
					if (replacement.OriginalSize > 0)
						output = output.Remove(replacement.Offset + replacementOffset, replacement.OriginalSize);

					output = output.Insert(replacement.Offset + replacementOffset, replacement.Value);

					replacementOffset += (replacement.Value.Length - replacement.OriginalSize);
				}

				// Re-save the updated content into the package
				packageItem.SetAsString(output);
			}
		}

		private IEnumerable<Replacement> GetReplacements(Match match, String output)
		{
			String tagContent = match.Groups["content"].Value;
			String tag = match.Groups["tag"].Value;

			if (tagContent.IndexOf(">") == -1)
			{
				Logger.Warning("LinkResolver: Invalid tag {0}.", tagContent);
				yield break;
			}

			Dictionary<String, String> attributes = ExtractAttributes(tagContent);
			String attributeValue;

			// Does any of the supported attributes contain an tcm uri?
			foreach (String supportedAttribute in mSupportedAttributes)
				if (attributes.TryGetValue(supportedAttribute, out attributeValue) &&
					(attributeValue.StartsWith("tcm:", StringComparison.OrdinalIgnoreCase)))
				{
					String beginTag;
					String endTag;

					if (GetReplacement(tag, attributeValue, attributes, out beginTag, out endTag))
					{
						// Self-closing tag, no additional content available
						if (tagContent.LastIndexOf("/>") == (tagContent.Length - 2))
						{
							// Replacement for the tag
							yield return new Replacement()
							{
								Offset = match.Index,
								OriginalSize = match.Length,
								Value = beginTag.Insert(beginTag.Length - 1, " /")
							};
						}
						else
						{
							// Extract inner html content
							int contentLength = GetContentLength(match, output, tag, tagContent);

							// Replacement for the begin tag
							yield return new Replacement()
							{
								Offset = match.Index,
								OriginalSize = match.Length,
								Value = beginTag
							};

							// Replacement for the end tag
							yield return new Replacement()
							{
								Offset = match.Index + match.Length + contentLength,
								OriginalSize = tag.Length + 3,
								Value = endTag
							};
						}

						// Ensure we do not process any other supported attributes for the same tag
						break;
					}
				}
		}

		private bool GetReplacement(String tag, String uri, Dictionary<String, String> attributes, out String beginTag, out String endTag)
		{
			String tagToRender = String.Empty;

			beginTag = String.Empty;
			endTag = String.Empty;

			IdentifiableObject identifiableObject = GetObject<IdentifiableObject>(uri);

			if (identifiableObject != null)
			{
				// Update link title as necessary
				ProcessLinkTitle(identifiableObject, ref attributes);

				Component component = identifiableObject as Component;

				// Publish multimedia components directly into the supported attributes
				if (component != null)
				{
					if (component.ComponentType == ComponentType.Multimedia)
					{
						String binaryUrl = PublishBinary(component);

						if (String.Equals(tag, "img", StringComparison.OrdinalIgnoreCase))
						{
							attributes["ImageUrl"] = binaryUrl;
							tagToRender = IMAGE_TAG;
						}
						else
						{
							attributes["ItemUri"] = identifiableObject.Id;
							tagToRender = BINARY_TAG;
						}
					}
					else
					{
						attributes.Add("ItemUri", identifiableObject.Id);
						tagToRender = COMPONENT_TAG;
					}
				} 
				else if (identifiableObject is Page)
				{
					attributes.Add("ItemUri", identifiableObject.Id);
					tagToRender = PAGE_TAG;
				}

				beginTag = String.Format("<{0} runat=\"server\"{1}>", tagToRender, RenderAttributes(attributes));
				endTag = String.Format("</{0}>", tagToRender);

				return true;
			}

			return false;
		}

		private String RenderAttributes(Dictionary<String, String> attributes)
		{
			StringBuilder output = new StringBuilder();

			foreach (KeyValuePair<String, String> value in attributes)
				if (!mIgnoredAttributes.Contains(value.Key, StringComparer.OrdinalIgnoreCase))
					output.AppendFormat(" {0}=\"{1}\"", value.Key, value.Value);

			return output.ToString();
		}

		private int GetContentLength(Match match, String output, String tag, String tagContent)
		{
			int index = match.Index + match.Length;

			// Retrieve the remainder of HTML in the document
			String remainder = output.Substring(index);

			int length = remainder.IndexOf("</" + tag + ">", StringComparison.OrdinalIgnoreCase);

			if (length != -1)
				return length;

			return 0;
		}

		/// <summary>
		/// Extracts all HTML attributes into a Dictionary&gt;String, String&lt;
		/// </summary>
		/// <param name="Link">HTML Link</param>
		/// <remarks>Note that if duplicate attribute names occur only the last attribute value is returned.</remarks>
		/// <returns>Dictionary&gt;String, String&lt; containing all attributes</returns>
		private Dictionary<String, String> ExtractAttributes(String tag)
		{
			Dictionary<String, String> attributes = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);

			foreach (Match match in mAttributes.Matches(tag))
			{
				if (match.Success)
				{
					if (match.Groups.Count == 3)
					{
						String key = match.Groups[1].Value;
						String value = match.Groups[2].Value;

						if (!String.IsNullOrEmpty(key) && !String.IsNullOrEmpty(value))
							attributes[key.ToLower()] = value;
					}
				}
			}

			return attributes;
		}

		private void ProcessLinkTitle(IdentifiableObject identifiableObject, ref Dictionary<String, String> attributes)
		{
			String title = String.Empty;

			if (attributes.TryGetValue("title", out title))
			{
				title = title.Replace("&lt;br/&gt;", " ");

				// Retrieve the title of a navigable item
				Match match = mLinkTitle.Match(title);

				// Tridion numbered links
				if (match.Success)
					title = title.Substring(match.Length);

				attributes["Tooltip"] = title;
				attributes.Remove("title");
			}

			// If no title was specified, or its a tridion richtext link field
			if (String.IsNullOrEmpty(title))
			{
				Component component = identifiableObject as Component;

				if (component != null && component.Content != null)
				{
					XmlNode xTitle = component.Content.SelectSingleNode("//node()[local-name() = 'Title']");

					if (xTitle != null && !String.IsNullOrEmpty(xTitle.InnerText))
					{
						title = xTitle.InnerText.Replace("<br/>", " ");

						attributes["Tooltip"] = HttpUtility.HtmlEncode(title);
						attributes.Remove("title");
					}
				}
			}
		}
	}
}
