#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: OrganizationalItemExtensions
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
using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="OrganizationalItemExtensions" /> supplies .NET extension functions for <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
    /// </summary>
    public static class OrganizationalItemExtensions
    {
		/// <summary>
		/// Retrieves a list of <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> from a
		/// <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
		/// </summary>
		/// <param name="organizationalItem"><see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" /></param>
		/// <param name="schema">Schema <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> for filtering.</param>
		/// <returns>List of <see cref="T:Tridion.ContentManager.ContentManagement.Component" /></returns>
		public static List<Component> Components(this OrganizationalItem organizationalItem, Schema schema)
		{
			if (organizationalItem != null)
			{
				OrganizationalItemItemsFilter filter = new OrganizationalItemItemsFilter(organizationalItem.Session)
				{
					ItemTypes = new ItemType[] { ItemType.Component },
					BasedOnSchemas = schema != null ? new Schema[] { schema } : null
				};

				// Return a list of all matching component types
				return organizationalItem.GetItems(filter).OfType<Component>().ToList();
			}

			return new List<Component>();
		}

		/// <summary>
		/// Retrieves a list of <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> from a
		/// <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
		/// </summary>
		/// <param name="organizationalItem"><see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" /></param>
		/// <param name="recursive">if set to <c>true</c> [recursive].</param>
		/// <returns>
		/// List of <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
		/// </returns>
		public static List<Component> Components(this OrganizationalItem organizationalItem, bool recursive)
		{
			if (organizationalItem != null)
			{
				OrganizationalItemItemsFilter filter = new OrganizationalItemItemsFilter(organizationalItem.Session)
				{
					ItemTypes = new ItemType[] { ItemType.Component },
					Recursive = recursive
				};

				// Return a list of all matching component types
				return organizationalItem.GetItems(filter).OfType<Component>().ToList();
			}

			return new List<Component>();
		}


        /// <summary>
        /// Retrieves a list of <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> from a
        /// <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
        /// </summary>
		/// <param name="organizationalItem"><see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" /></param>
        /// <param name="Schema">Schema <see cref="T:Tridion.ContentManager.TcmUri" /> for filtering.</param>
        /// <returns>List of <see cref="T:Tridion.ContentManager.ContentManagement.Component" /></returns>
		public static List<Component> Components(this OrganizationalItem organizationalItem, TcmUri schema)
        {
			if (schema != null && TcmUri.IsValid(schema))
				return organizationalItem.Components(new Schema(schema, organizationalItem.Session));
			else
				return organizationalItem.Components(null as Schema);
        }

		/// <summary>
		/// Retrieves a list of <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> from a
		/// <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
		/// </summary>
		/// <param name="organizationalItem"><see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" /></param>
		/// <param name="schema">Schema TCM uri for filtering.</param>
		/// <returns>List of <see cref="T:Tridion.ContentManager.ContentManagement.Component" /></returns>
		public static List<Component> Components(this OrganizationalItem organizationalItem, String schema)
		{
			if (!String.IsNullOrEmpty(schema) && TcmUri.IsValid(schema))
				return organizationalItem.Components(new TcmUri(schema));
			else
				return organizationalItem.Components(null as Schema);
		}

        /// <summary>
        /// Retrieves a list of <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> from a
        /// <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
        /// </summary>
		/// <param name="organizationalItem"><see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" /></param>
        /// <returns>
        /// List of <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
        /// </returns>
		public static List<Component> Components(this OrganizationalItem organizationalItem)
        {
			return organizationalItem.Components(null as Schema);
        }

		/// <summary>
		/// Retrieves a list of <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" /> from a
		/// <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
		/// </summary>
		/// <param name="organizationalItem"><see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" /></param>
		/// <param name="recursive">if set to <c>true</c> [recursive].</param>
		/// <returns>
		/// List of <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />
		/// </returns>
		public static List<Page> Pages(this OrganizationalItem organizationalItem, bool recursive)
		{
			if (organizationalItem != null)
			{
				OrganizationalItemItemsFilter filter = new OrganizationalItemItemsFilter(organizationalItem.Session)
				{
					ItemTypes = new ItemType[] { ItemType.Page },
					Recursive = recursive
				};

				// Return a list of all matching component types
				return organizationalItem.GetItems(filter).OfType<Page>().ToList();
			}

			return new List<Page>();
		}
		
		/// <summary>
		/// Retrieves a list of <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" /> from a
		/// <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
		/// </summary>
		/// <param name="organizationalItem"><see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" /></param>
		/// <returns>
		/// List of <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />
		/// </returns>
		public static List<Page> Pages(this OrganizationalItem organizationalItem)
		{
			return organizationalItem.Pages(false);
		}

		/// <summary>
		/// Retrieves a list of <see cref="T:Tridion.ContentManager.CommunicationManagement.StructureGroup" /> from a
		/// <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
		/// </summary>
		/// <param name="organizationalItem"><see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" /></param>
		/// <param name="recursive">if set to <c>true</c> [recursive].</param>
		/// <returns>
		/// List of <see cref="T:Tridion.ContentManager.CommunicationManagement.StructureGroup" />
		/// </returns>
		public static List<StructureGroup> StructureGroups(this OrganizationalItem organizationalItem, bool recursive)
		{
			if (organizationalItem != null)
			{
				OrganizationalItemItemsFilter filter = new OrganizationalItemItemsFilter(organizationalItem.Session)
				{
					ItemTypes = new ItemType[] { ItemType.StructureGroup },
					Recursive = recursive
				};

				// Return a list of all matching component types
				return organizationalItem.GetItems(filter).OfType<StructureGroup>().ToList();
			}

			return new List<StructureGroup>();
		}

		/// <summary>
		/// Retrieves a list of <see cref="T:Tridion.ContentManager.CommunicationManagement.StructureGroup" /> from a
		/// <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
		/// </summary>
		/// <param name="organizationalItem"><see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" /></param>
		/// <returns>
		/// List of <see cref="T:Tridion.ContentManager.CommunicationManagement.StructureGroup" />
		/// </returns>
		public static List<StructureGroup> StructureGroups(this OrganizationalItem organizationalItem)
		{
			return organizationalItem.StructureGroups(false);
		}

		/// <summary>
		/// Retrieves a list of tridion items specified by <see cref="T:Tridion.ContentManager.ItemType" /> from a <see cref="T:Tridion.ContentManager.ContentManagement.OrganizationalItem" />
		/// </summary>
		/// <param name="itemTypes">Array of <see cref="T:Tridion.ContentManager.ItemType" /></param>
		/// <param name="Recursive">if set to <c>true</c> [recursive].</param>
		/// <returns>List of Tridion KeyValuePair&lt;TcmUri, String&gt;</returns>
		public static List<KeyValuePair<TcmUri, String>> GetItems(this OrganizationalItem organizationalItem, ItemType[] itemTypes, bool recursive)
		{
			if (organizationalItem != null)
			{
				OrganizationalItemItemsFilter filter = new OrganizationalItemItemsFilter(organizationalItem.Session)
				{
					ItemTypes = itemTypes,
					Recursive = recursive
				};

				return (from XmlNode item in organizationalItem.GetListItems(filter).SelectNodes("/*/*")
						select new KeyValuePair<TcmUri, String>(new TcmUri(item.Attributes["ID"].Value), item.Attributes["Title"].Value)).ToList();
			}

			return new List<KeyValuePair<TcmUri, String>>();
		}
    }
}
