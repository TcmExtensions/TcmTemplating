#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Linq Extensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TcmTemplating.Extensions
{
	public static class LinqExtensions
	{
		/// <summary>
		/// Returns the index of a given condition match
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">List.</param>
		/// <param name="condition">Condition Predicate</param>
		/// <returns>Item index or -1</returns>
		public static int IndexOf<T>(this IEnumerable<T> list, Predicate<T> condition)
		{
			if (list != null && condition != null)
			{
				int index = -1;

				return list.Any(item =>
				{
					index++;
					return condition(item);
				}) ? index : -1;
			}

			return -1;
		}

		/// <summary>
		/// Returns the InnerXml of a <see cref="T:System.Xml.Linq.XNode" />
		/// </summary>
		/// <param name="node"><see cref="T:System.Xml.Linq.XNode" /></param>
		/// <returns>InnerXml</returns>
		public static String InnerXml(this XNode node)
		{
			if (node != null)
			{
				using (XmlReader reader = node.CreateReader())
				{
					reader.MoveToContent();
					return reader.ReadInnerXml();
				}
			}

			return String.Empty;
		}

		/// <summary>
		/// Returns a copy of the <see cref="T:System.Xml.Linq.XElement" /> with all namespaces removed.
		/// </summary>
		/// <param name="element"><see cref="T:System.Xml.Linq.XElement" /></param>
		/// <param name="preservedNamespaces">List of namespaces to preserve</param>
		/// <returns>
		///   <see cref="T:System.Xml.Linq.XElement" />
		/// </returns>
		public static XElement RemoveNamespaces(this XElement element, IEnumerable<String> preservedNamespaces)
		{
			if (element != null && preservedNamespaces != null)
			{
				XElement result = new XElement(element.Name.LocalName);

				foreach (XAttribute attribute in element.Attributes())
				{
					// Copy any namespace attributes which have to be preserved or attributes which are in a namespace to be preserved
					if ((attribute.IsNamespaceDeclaration && preservedNamespaces.Contains(attribute.Value)) || preservedNamespaces.Contains(attribute.Name.NamespaceName))
					{
						result.Add(attribute);
						continue;
					}

					if (!attribute.IsNamespaceDeclaration && String.IsNullOrEmpty(attribute.Name.NamespaceName))
					{
						result.Add(new XAttribute(attribute.Name.LocalName, attribute.Value));
					}
				}

				result.Add(from n in element.Nodes()
						   select ((n is XElement) ? (n as XElement).RemoveNamespaces(preservedNamespaces) : n));

				return result;
			}

			return null;
		}

		/// <summary>
		/// Returns a copy of the <see cref="T:System.Xml.Linq.XElement" /> with all namespaces removed.
		/// </summary>
		/// <param name="element"><see cref="T:System.Xml.Linq.XElement" /></param>
		/// <returns><see cref="T:System.Xml.Linq.XElement" /></returns>
		public static XElement RemoveNamespaces(this XElement element)
		{
			return element.RemoveNamespaces(new String[] { });
		}
	}
}
