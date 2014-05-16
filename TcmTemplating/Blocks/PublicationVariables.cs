#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Publication Variables
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
using Tridion.ContentManager.ContentManagement.Fields;

namespace TcmTemplating.Blocks
{
	/// <summary>
	/// <see cref="PublicationVariables" /> extracts metadata from a <see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" />
	/// and inserts it into the <see cref="T:Tridion.ContentManager.Templating.Package" />
	/// </summary>
	[TcmTemplateTitle("Publication Variables")]
	public class PublicationVariables : PackageVariables
	{
		/// <summary>
		/// Returns the prefix used for parsed variables in the <see cref="T:Tridion.ContentManager.Temlating.Package" />
		/// </summary>
		/// <returns>Package variable prefix</returns>
		protected override String PackagePrefix
		{
			get
			{
				return "PublicationVariable";
			}
		}

		/// <summary>
		/// Performs the actual transformation logic of this <see cref="PublicationVariables" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
			Package.AddString(PackagePrefix + ".Uri", Publication.Id);
			Package.AddString(PackagePrefix + ".Url", Publication.PublicationUrl);
			Package.AddString(PackagePrefix + ".Path", Publication.PublicationPath);
			
			String value = Package.ItemAsString("MetadataFields");

			if (!String.IsNullOrWhiteSpace(value))
			{
				IEnumerable<String> metadataFields = value.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				ParseFields(metadataFields, Publication.MetadataFields());
			}
		}
	}
}
