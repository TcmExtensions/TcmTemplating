using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Tridion.ContentManager.ContentManagement;

namespace TcmTemplating.Interfaces
{
	/// <summary>
	/// <see cref="IFormatTextResolver" />
	/// </summary>
	public interface IFormatTextResolver
	{
		/// <summary>
		/// Retrieves the link title to be used for the <paramref name="component" />
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <returns>Link title or null (delete attribute)</returns>
		String LinkTitle(Component component);

		/// <summary>
		/// Prefixes a link
		/// </summary>
		/// <param name="url">Link URL to prefix</param>
		/// <returns>Prefixed link</returns>
		String PrefixLink(String url);

		/// <summary>
		/// Determines whether a <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> link can be resolved by this <see cref="IFormatTextResolver" />.
		/// </summary>
		/// <param name="schemaName">Name of the schema.</param>
		/// <returns><c>true</c> if the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> is supported for resolving, otherwise <c>false</c>.</returns>
		bool IsSupported(String schemaName);

		/// <summary>
		/// Resolves an application specific Tridion image for a given <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
		/// </summary>
		/// <param name="templateBase"><see cref="T:TcmTemplate.TemplateBase" /></param>
		/// <param name="xhtml"><see cref="T:System.Linq.Xml.XElement" /> with XHTML to transform</param>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		void Resolve(TemplateBase templateBase, XElement xhtml, Component component);
	}
}
