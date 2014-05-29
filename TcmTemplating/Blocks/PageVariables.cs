#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Page Variables
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
using Tridion.ContentManager.Templating.Assembly;
using TcmTemplating.Extensions;
using Tridion.ContentManager.ContentManagement;

namespace TcmTemplating.Blocks
{
	/// <summary>
	/// <see cref="PageVariables" /> extracts metadata from a <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />
	/// and inserts it into the <see cref="T:Tridion.ContentManager.Templating.Package" />
	/// </summary>
	[TcmTemplateTitle("Page Variables")]
	public class PageVariables : PackageVariables
	{
		/// <summary>
		/// Returns the prefix used for parsed variables in the <see cref="T:Tridion.ContentManager.Temlating.Package" />
		/// </summary>
		/// <returns>Package variable prefix</returns>
		protected override String PackagePrefix
		{
			get
			{				
				return "PageVariable";
			}
		}

		/// <summary>
		/// Performs the actual transformation logic of this <see cref="PageVariables" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
			Package.AddString(PackagePrefix + ".Uri", Page.Id);
			
			IEnumerable<String> metadataFields = new String[] { };
			IEnumerable<String> inheritedFields = new String[] { };

			String value = Package.ItemAsString("MetadataFields");

			if (!String.IsNullOrWhiteSpace(value))
				metadataFields = value.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			value = Package.ItemAsString("InheritedFields");

			if (!String.IsNullOrWhiteSpace(value))
				inheritedFields = value.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			// Extract page metadata fields
			ParseFields(metadataFields.Concat(inheritedFields), Page.MetadataFields());

			// If the page is a navigable item, register it
			if (Page.NavigableNumber() > 0)
				Package.AddString(PackagePrefix + ".NavigationParent", Page.Id);

			RepositoryLocalObject item = Page.OrganizationalItem;

			while (item != null && item.Id != Publication.Id)
			{
				// Found navigable item and no navparent value is set yet.
				if (item.NavigableNumber() > 0 && Package.GetByName(PackagePrefix + ".NavigationParent") == null)
					Package.AddString(PackagePrefix + ".NavigationParent", item.Id);

				// Extract inherited fields
				ParseFields(inheritedFields, item.MetadataFields());

				item = item.OrganizationalItem;
			}			
		}
	}
}
