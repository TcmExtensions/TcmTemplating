using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TcmTemplating.Extensions;
using Tridion;
using Tridion.ContentManager.ContentManagement;

namespace TcmTemplating.Helpers
{
	/// <summary>
	/// <see cref="FormatTextResolver" /> is a helper class to resolve FormatText fields published from Tridion.
	/// </summary>
	public class FormatTextResolver
	{
		private TemplateBase mTemplateBase;

		/// <summary>
		/// Initializes a new instance of the <see cref="FormatTextResolver"/> class.
		/// </summary>
		/// <param name="templateBase"><see cref="T:TcmTemplating.TemplateBase"/></param>
		public FormatTextResolver(TemplateBase templateBase)
		{
			mTemplateBase = templateBase;
		}

		/// <summary>
		/// Gets the <see cref="T:TcmTemplating.TemplateBase"/>
		/// </summary>
		/// <value>
		/// <see cref="T:TcmTemplating.TemplateBase"/>
		/// </value>
		protected TemplateBase TemplateBase
		{
			get
			{
				return mTemplateBase;
			}
		}

		/// <summary>
		/// Retrieves the title to use for a <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> link
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <returns>Link Title or String.Empty</returns>
		protected virtual String LinkTitle(Component component)
		{
			return String.Empty;
		}

		/// <summary>
		/// Resolves an application specific Tridion link for a given <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <returns>Link or String.Empty</returns>
		protected virtual Dictionary<String, String> ResolveLink(Component component)
		{
			Dictionary<String, String> result = new Dictionary<String, String>();
			result.Add("href", String.Empty);

			return result;
		}

		/// <summary>
		/// Allows a derived class to prefix the resolved Url before rendering it.
		/// </summary>
		/// <param name="url">Resolved Url</param>
		/// <returns>Prefixed Url</returns>
		protected virtual String PrefixLink(String url)
		{
			return url;
		}

		/// <summary>
		/// Resolves the specified format text.
		/// </summary>
		/// <param name="formatText">Format Text to resolve</param>
		/// <param name="preservedNamespaces">Namespaces to preserve</param>
		/// <returns>Resolved Format Text</returns>
		public virtual String Resolve(String formatText)
		{
			return ResolveInternal(formatText, null, false);
		}

		/// <summary>
		/// Resolves the specified format text.
		/// </summary>
		/// <param name="formatText">Format Text to resolve</param>
		/// <param name="preservedNamespaces">Namespaces to preserve</param>
		/// <returns>Resolved Format Text</returns>
		public virtual String Resolve(String formatText, IEnumerable<String> preservedNamespaces)
		{
			return ResolveInternal(formatText, preservedNamespaces, false);
		}

		/// <summary>
		/// Resolves the specified format text.
		/// </summary>
		/// <param name="formatText">Format Text to resolve</param>
		/// <param name="preservedNamespaces">Namespaces to preserve</param>
		/// <returns>Resolved Format Text</returns>
		protected String ResolveInternal(String formatText, IEnumerable<String> preservedNamespaces, Boolean removeImages)
		{
			return ProcessXHTML(formatText, preservedNamespaces, removeImages);
		}

		/// <summary>
		/// Processes the specified format text.
		/// </summary>
		/// <param name="formatText">Format Text to resolve</param>
		/// <param name="preservedNamespaces">Namespaces to preserve in the output</param>
		/// <param name="removeImages">Indicate wether remove images</param>
		/// <returns>Resolved Format Text</returns>
		private String ProcessXHTML(String formatText, IEnumerable<String> preservedNamespaces, Boolean removeImages)
		{
			try
			{
				XElement xDoc = XElement.Parse("<body>" + formatText + "</body>", LoadOptions.None);

				// Remove all images from the format text field if requested
				if (removeImages)
				{					
					xDoc.Descendants(XName.Get("img", Constants.XhtmlNamespace)).Remove();
				}
				else
				{
					foreach (XElement xImage in xDoc.Descendants(XName.Get("img", Constants.XhtmlNamespace))
						.Where(n => n.Attributes(XName.Get("href", Constants.XlinkNamespace))
							.Any(y => y.Value.StartsWith("tcm:", StringComparison.OrdinalIgnoreCase))))
					{
						String href = xImage.Attribute(XName.Get("href", Constants.XlinkNamespace)).Value;

						if (preservedNamespaces != null)
						{
							xImage.SetAttributeValue(XName.Get("href", Constants.XlinkNamespace), null);
							xImage.SetAttributeValue(XName.Get("title", Constants.XlinkNamespace), null);
						}

						Component imageComponent = mTemplateBase.GetComponent(href);

						xImage.SetAttributeValue("src", PrefixLink(mTemplateBase.PublishBinary(imageComponent)));
					}
				}

				foreach (XElement xLink in xDoc.Descendants(XName.Get("a", Constants.XhtmlNamespace)))
				{
					// Verify if the anchor tag contains xlink:href="tcm:..."
					if (xLink.Attributes().Where(n => n.Name == XName.Get("href", Constants.XlinkNamespace))
						.Any(y => y.Value.StartsWith("tcm:", StringComparison.OrdinalIgnoreCase)))
					{
						String href = xLink.Attribute(XName.Get("href", Constants.XlinkNamespace)).Value;

						if (preservedNamespaces != null)
						{
							xLink.SetAttributeValue(XName.Get("href", Constants.XlinkNamespace), null);
							xLink.SetAttributeValue(XName.Get("title", Constants.XlinkNamespace), null);
						}

						Component component = mTemplateBase.GetComponent(href);

						// Update the link title from the components content if a "Title" field is available
						if (component != null)
						{
							if (xLink.Attribute("title") != null 
								&& 
									(IdentifiableObjectExtensions.mNavigable.IsMatch(xLink.Attribute("title").Value)
									|| String.Equals(component.Title, xLink.Attribute("title").Value, StringComparison.OrdinalIgnoreCase)))
							{
								String title = LinkTitle(component);

								if (!String.IsNullOrEmpty(title))
									xLink.SetAttributeValue("title", title);								
							}
							
							// Publish multi-media components
							if (component.ComponentType == ComponentType.Multimedia)
							{	
								String binaryUrl = mTemplateBase.PublishBinary(component);

								if (preservedNamespaces != null)
									xLink.SetAttributeValue(XName.Get("href", Constants.XlinkNamespace), null);

								xLink.SetAttributeValue("href", PrefixLink(binaryUrl));
							}
							else
							{
								xLink.SetAttributeValue(XName.Get("type", "urn:TcmTemplating"), "component");

								Dictionary<String, String> attributes = ResolveLink(component);

								foreach (KeyValuePair<String, String> attribute in attributes)
								{
									if (String.Equals(attribute.Key, "href", StringComparison.OrdinalIgnoreCase))
										xLink.SetAttributeValue("href", PrefixLink(attribute.Value));
									else
										xLink.SetAttributeValue(attribute.Key, attribute.Value);
								}
							}
						}
					}
					else
					{
						XAttribute xHref = xLink.Attribute("href");

						if (xHref != null)
						{
							String url = xLink.Attribute("href").Value;

							if (!String.IsNullOrEmpty(url))
								xLink.SetAttributeValue("href", PrefixLink(url));
						}
					}
				}

				return xDoc.RemoveNamespaces(preservedNamespaces).InnerXml();
			}
			catch (Exception ex)
			{
				TemplateBase.Logger.Error("FormatTextResolver Exception\n" + LoggerExtensions.TraceException(ex));
			}

			return String.Empty;
		}
	}
}
