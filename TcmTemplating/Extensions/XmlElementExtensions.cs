#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: XmlElementExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System.Collections.Generic;
using System.Xml;
using Tridion.ContentManager;
using Tridion.ContentManager.Templating;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="XmlElementExtensions"/> supplies .NET extension functions for <see cref="T:System.Xml.XmlElement" />
    /// </summary>
    public static class XmlElementExtensions
    {
        /// <summary>
        /// Return a list of objects of the requested type from the XML.
        /// </summary>
        /// <remarks>
        /// This method goes back to the database to retrieve more information. So it is NOT just a fast and convenient way to get a type safe list from the XML.
        /// </remarks>
        /// <typeparam name="T">The type of object to return, like Publication, User, Group, OrganizationalItem</typeparam>
        /// <param name="listElement">The XML from which to construct the list of objects</param>
		/// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine"/></param>
        /// <returns>a list of objects of the requested type from the XML</returns>
        public static IList<T> TridionObjects<T>(this XmlElement listElement, Engine engine) where T : IdentifiableObject
        {
			if (listElement != null && engine != null)
			{
				XmlNodeList itemElements = listElement.SelectNodes("*");
				List<T> result = new List<T>(itemElements.Count);

				foreach (XmlElement itemElement in itemElements)
				{
					result.Add(itemElement.TridionObject<T>(engine));
				}

				result.Sort(delegate(T item1, T item2)
				{
					return item1.Title.CompareTo(item2.Title);
				});

				return result;
			}

			return new List<T>();
        }

        /// <summary>
        /// Return an object of the requested type from the XML.
        /// </summary>
        /// <remarks>
        /// This method goes back to the database to retrieve more information. So it is NOT just a fast and convenient way to get a type safe list from the XML.
        /// </remarks>
        /// <typeparam name="T">The type of object to return, like Publication, User, Group, OrganizationalItem</typeparam>
        /// <param name="listElement">The XML from which to construct the list of objects</param>
		/// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine"/></param>
        /// <returns>a list of objects of the requested type from the XML</returns>
        public static T TridionObject<T>(this XmlElement itemElement, Engine engine) where T : IdentifiableObject
        {
			if (itemElement != null && engine != null)			
				return engine.GetObject(itemElement.GetAttribute("ID")) as T;

			return null;
        }
    }
}
