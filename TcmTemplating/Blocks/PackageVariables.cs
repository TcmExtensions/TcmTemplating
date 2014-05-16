#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Package Variables
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
using Tridion.ContentManager.ContentManagement.Fields;
using TcmTemplating.Extensions;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager;

namespace TcmTemplating.Blocks
{
	/// <summary>
	/// <see cref="PackageVariables" /> is a building block which recursively extracts metadata
	/// as per the configured parameter schema
	/// </summary>
	public abstract class PackageVariables : TcmTemplating.TemplateBase
	{
		private enum FieldMatch
		{
			NoMatch,
			PartialMatch,
			Match
		}

		private readonly String FIELD_SEPERATOR = ".";

		/// <summary>
		/// Gets the field seperator character for package variables
		/// </summary>
		/// <value>
		/// Field seperator character for package variables
		/// </value>
		protected String FieldSeperator
		{
			get
			{
				return FIELD_SEPERATOR;
			}
		}

		/// <summary>
		/// Returns the prefix used for parsed variables in the <see cref="T:Tridion.ContentManager.Temlating.Package" />
		/// </summary>
		/// <returns>Package variable prefix</returns>
		protected abstract String PackagePrefix
		{
			get;
		}

		private FieldMatch IncludeField(String fieldName, IEnumerable<String> extractFields)
		{
			foreach (String field in extractFields)
			{
				// Direct match, field should be included
				if (String.Equals(field, fieldName, StringComparison.OrdinalIgnoreCase))
					return FieldMatch.Match;

				// Partial match (field , field should be evaluated
				if (field.StartsWith(fieldName + FIELD_SEPERATOR, StringComparison.OrdinalIgnoreCase))
					return FieldMatch.PartialMatch;
			}

			return FieldMatch.NoMatch;
		}

		private void ParseFields(String prefix, IEnumerable<String> extractFields, ItemFields fields)
		{
			foreach (ItemField field in fields)
			{
				String fieldName = String.Concat(prefix, field.Name);
				String packageName = String.Concat(PackagePrefix, ".", prefix, field.Name);

				// Verify if the value is not already present in the package
				if (Package.GetByName(packageName) == null)
				{
					FieldMatch fieldMatch = IncludeField(fieldName, extractFields);

					if (fieldMatch == FieldMatch.Match)
					{
						if (field is SingleLineTextField || field is MultiLineTextField || field is ExternalLinkField)
						{
							String value = field.StringValue();

							if (!String.IsNullOrEmpty(value))
								Package.AddString(packageName, value);

							continue;
						}

						if (field is DateField)
						{
							DateTime value = field.DateValue();

							if (value != DateTime.MinValue)
								Package.AddDateTime(packageName, value);

							continue;
						}

						if (field is NumberField)
						{
							Double value = field.NumberValue();

							if (!Double.IsNaN(value))
								Package.AddNumber(packageName, value);

							continue;
						}

						if (field is ComponentLinkField)
						{
							IList<Component> components = field.ComponentValues();

							if (components != null && components.Count > 0)
								Package.AddComponents(packageName, components.Select(c => c.Id).ToList());

							continue;
						}

						if (field is XhtmlField)
						{
							String value = field.XHTMLValue();

							if (!String.IsNullOrEmpty(value))
								Package.AddXhtml(packageName, value);

							continue;
						}

						if (field is KeywordField)
						{
							Keyword keyword = field.KeywordValue();

							if (keyword != null)
							{
								Package.AddLink(packageName, keyword);
								Package.AddString(String.Concat(packageName, FIELD_SEPERATOR, "Title"), keyword.Title);
								Package.AddString(String.Concat(packageName, FIELD_SEPERATOR, "Description"), keyword.Description);
							}

							continue;
						}
					}

					if (fieldMatch == FieldMatch.PartialMatch)
					{
						if (field is EmbeddedSchemaField)
						{
							ItemFields itemFields = field.EmbeddedValue();

							if (fields != null)
								ParseFields(String.Concat(fieldName, FIELD_SEPERATOR), extractFields, itemFields);

							continue;
						}

						if (field is ComponentLinkField)
						{
							Component component = field.ComponentValue();

							if (component != null)
							{
								ItemFields itemFields = component.Fields();

								if (fields != null)
									ParseFields(String.Concat(fieldName, FIELD_SEPERATOR), extractFields, itemFields);

								continue;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Recursively parses the given 
		/// <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> into
		/// <see cref="T:Tridion.ContentManager.Temlating.Package" /> variables
		/// </summary>
		/// <param name="extractFields"><see cref="I:System.Collections.Generic.IEnumerable{String}" /> of fieldnames to extract</param>
		/// <param name="fields"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /></param>
		/// <remarks>Extract field format is in the form of "FieldName.EmbeddedField"</remarks>
		protected void ParseFields(IEnumerable<String> extractFields, ItemFields fields)
		{
			if (fields != null && extractFields.Any())
				ParseFields(String.Empty, extractFields, fields);
		}
	}
}


