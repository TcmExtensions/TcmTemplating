using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TcmTemplating.Extensions;
using TcmTemplating.Interfaces;
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
		private IEnumerable<IFormatTextResolver> mResolvers;

		public readonly String SCHEMA_NONE = "<None>";

		private String PrefixLink(String url)
		{
			foreach (IFormatTextResolver resolver in mResolvers)
			{
				if (resolver.IsSupported(SCHEMA_NONE))
					return resolver.PrefixLink(url);
			}

			return String.Empty;
		}

        /// <summary>
        /// Resolves the specified XHTML.
        /// </summary>
        /// <param name="xhtml">The <see cref="T:System.Xml.Linq.XElement"/>.</param>
        /// <param name="component">The <see cref="T:Tridion.ContentManager.ContentManagement.Component"/>.</param>
		private void Resolve(XElement xhtml, Component component)
		{
			String schema = component.BasedOnSchema();

			foreach (IFormatTextResolver resolver in mResolvers)
			{
                if (resolver.IsSupported(schema))
                {
                    resolver.Resolve(TemplateBase, xhtml, component);
                    break;
                }
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatTextResolver" /> class.
        /// </summary>
        /// <param name="templateBase"><see cref="T:TcmTemplating.TemplateBase" /></param>
		public FormatTextResolver(TemplateBase templateBase): this(templateBase, null)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatTextResolver" /> class.
        /// </summary>
        /// <param name="templateBase"><see cref="T:TcmTemplating.TemplateBase" /></param>
        /// <param name="formatTextResolvers">List (in priority) of format text resolvers to use</param>
        public FormatTextResolver(TemplateBase templateBase, IEnumerable<IFormatTextResolver> formatTextResolvers)
        {
            mTemplateBase = templateBase;

            if (formatTextResolvers == null || formatTextResolvers.Count() == 0)
                formatTextResolvers = new IFormatTextResolver[] { new DefaultFormatTextResolver() };
            else
                formatTextResolvers = formatTextResolvers.Concat(new IFormatTextResolver[] { new DefaultFormatTextResolver() });

            mResolvers = formatTextResolvers;
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
		/// Resolves the specified format text.
		/// </summary>
		/// <param name="formatText">Format Text to resolve</param>
		/// <param name="preservedNamespaces">Namespaces to preserve</param>
		/// <returns>Resolved Format Text</returns>
		public String Resolve(String formatText)
		{
			return Resolve(formatText, null, false);
		}

		/// <summary>
		/// Resolves the specified format text.
		/// </summary>
		/// <param name="formatText">Format Text to resolve</param>
		/// <param name="preservedNamespaces">Namespaces to preserve</param>
		/// <returns>Resolved Format Text</returns>
		public String Resolve(String formatText, IEnumerable<String> preservedNamespaces)
		{
			return Resolve(formatText, preservedNamespaces, false);
		}

		/// <summary>
		/// Resolves the specified format text.
		/// </summary>
		/// <param name="formatText">Format Text to resolve</param>
		/// <param name="preservedNamespaces">Namespaces to preserve</param>
		/// <param name="removeImages">Indicates whether images are removed</param>
		/// <returns>Resolved Format Text</returns>
		public String Resolve(String formatText, IEnumerable<String> preservedNamespaces, Boolean removeImages)
		{
			try
			{
				XElement xDoc = XElement.Parse("<body>" + formatText + "</body>", LoadOptions.None);

				// Remove all images from the format text field if requested
				if (removeImages)
					xDoc.Descendants(XName.Get("img", Constants.XhtmlNamespace)).Remove();
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

						Resolve(xImage, imageComponent);
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
							Resolve(xLink, component);
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
