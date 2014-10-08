using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TcmTemplating.Extensions;
using TcmTemplating.Interfaces;
using Tridion.ContentManager.ContentManagement;

namespace TcmTemplating.Helpers
{
	public class DefaultFormatTextResolver : IFormatTextResolver
	{
		private String GetTitle(XElement xhtml, Component component)
		{
			if (xhtml.Attribute("title") != null && (IdentifiableObjectExtensions.mNavigable.IsMatch(xhtml.Attribute("title").Value)
				|| String.Equals(component.Title, xhtml.Attribute("title").Value, StringComparison.OrdinalIgnoreCase)))
			{
				String title = LinkTitle(component);

				if (!String.IsNullOrEmpty(title))
					xhtml.SetAttributeValue("title", title);
			}

			return String.Empty;
		}

		/// <summary>
		/// Retrieves the link title to be used for the <paramref name="component" />
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <returns>
		/// Link title or null (delete attribute)
		/// </returns>
		public virtual String LinkTitle(Component component)
		{
			return null;
		}

		/// <summary>
		/// Prefixes a link
		/// </summary>
		/// <param name="url">Link URL to prefix</param>
		/// <returns>
		/// Prefixed link
		/// </returns>
		public virtual String PrefixLink(String url)
		{
			return url;
		}

		/// <summary>
		/// Determines whether a <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> link can be resolved by this <see cref="IFormatTextResolver" />.
		/// </summary>
		/// <param name="schemaName">Name of the schema.</param>
		/// <returns>
		///   <c>true</c> if the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> is supported for resolving, otherwise <c>false</c>.
		/// </returns>
		public virtual Boolean IsSupported(String schemaName)
		{
			// Default resolver supports any schema
			return true;
		}

		/// <summary>
		/// Resolves an application specific Tridion image for a given <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
		/// </summary>
		/// <param name="templateBase"><see cref="T:TcmTemplate.TemplateBase" /></param>
		/// <param name="xhtml"><see cref="T:System.Linq.Xml.XElement" /> with XHTML to transform</param>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		public virtual void Resolve(TemplateBase templateBase, XElement xhtml, Component component)
		{
			xhtml.SetAttributeValue("title", LinkTitle(component));

			// Publish multi-media components
			if (component.ComponentType == ComponentType.Multimedia)
			{
				String binaryUrl = templateBase.PublishBinary(component);

				if (String.Equals(xhtml.Name.LocalName, "img", StringComparison.OrdinalIgnoreCase))
					xhtml.SetAttributeValue("src", PrefixLink(binaryUrl));
				else
					xhtml.SetAttributeValue("href", PrefixLink(binaryUrl));
				
				return;
			}

			// Normal component link, render as <a href="tcm:5-22">
			xhtml.SetAttributeValue("href", component.Id);
		}
	}
}
