#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: ItemFieldsFactory
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//  Description		: Factory class to created ItemFields objects using object caching
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Threading;
using System.Xml;
using Tridion.ContentManager;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace TcmTemplating.Helpers
{
	/// <summary>
	/// <see cref="ItemFieldsFactory" /> handles cached creation of <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" />
	/// </summary>
	public static class ItemFieldsFactory
	{
		private const int CACHE_SIZE = 50;

		private static ThreadLocal<ObjectCache<String, ItemFields>> mCache =
			new ThreadLocal<ObjectCache<String, ItemFields>>(() => new ObjectCache<String, ItemFields>(CACHE_SIZE));
	
		/// <summary>
		/// Determines the cache key for the specified component uri and <see cref="T:Tridion.ContentManager.ContentManagement.Schema"/>
		/// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject"/></param>
		/// <param name="rootElementName">Schema root element name</param>
		/// <param name="schema"><see cref="T:Tridion.ContentManager.ContentManagement.Schema"/></param>
		/// <returns>
		/// Cache key
		/// </returns>
		private static String Key(IdentifiableObject identifiableObject, String rootElementName, Schema schema)
		{
			return identifiableObject.Id.ToString() + rootElementName + schema.Id.ToString();
		}

		/// <summary>
		/// Clear the <see cref="ItemFieldsFactory" /> internal cache
		/// </summary>
		public static void ClearCache()
		{
			mCache.Value.Clear();
		}

		/// <summary>
		/// Gets the (cached) <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields"/> for a given
		/// <see cref="T:Tridion.ContentManager.IdentifiableObject"/> and <see cref="T:Tridion.ContentManager.ContentManagement.Schema" />
		/// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject"/></param>
		/// <param name="rootElement">Content <see cref="T:System.Xml.XmlElement" /></param>
		/// <param name="schema"><see cref="T:Tridion.ContentManager.ContentManagement.Schema" /></param>
		/// <returns>(Cached) <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields"/></returns>
		public static ItemFields Get(IdentifiableObject identifiableObject, XmlElement rootElement, Schema schema)
		{
			if (identifiableObject != null && rootElement != null && schema != null)
			{
				String key = Key(identifiableObject, rootElement.LocalName, schema);

				ItemFields result = mCache.Value.Get(key);

				if (result == null)
				{
					result = new ItemFields(rootElement, schema);
					mCache.Value.Add(key, result);
				}

				return result;
			}

			return null;
		}
	}
}
