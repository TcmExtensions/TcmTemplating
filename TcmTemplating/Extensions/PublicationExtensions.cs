#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: PublicationExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using TcmTemplating.Helpers;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="PublicationExtensions" /> supplies .NET extension functions for <see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" />
    /// </summary>
    public static class PublicationExtensions    
	{
		/// <summary>
		/// Returns the metadata <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> for the
		/// <see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" />
		/// </summary>
		/// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /></returns>
		public static ItemFields MetadataFields(this Publication publication)
		{
			if (publication != null)
				return ItemFieldsFactory.Get(publication, publication.Metadata, publication.MetadataSchema);

			return null;
		}

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component" /> value</returns>
        public static Component ComponentMetaValue(this Publication publication, String fieldName)
        {
			if (publication != null)			
				return publication.MetadataFields().ComponentValue(fieldName);

			return null;
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component" /> values</returns>
        public static IList<Component> ComponentMetaValues(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().ComponentValues(fieldName);

			return new List<Component>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded value
        /// </returns>
        public static Component ComponentMetaValue(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().ComponentValue(fieldName, embeddedFieldName);

			return null;
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded values
        /// </returns>
        public static IList<Component> ComponentMetaValues(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().ComponentValues(fieldName, embeddedFieldName);

			return new List<Component>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> external link value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.String" /> external link value</returns>
        public static String ExternalLinkMetaValue(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().ExternalLinkValue(fieldName);

			return String.Empty;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.String" /> values</returns>
        public static IList<String> ExternalLinkMetaValues(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().ExternalLinkValues(fieldName);

			return new List<String>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> embedded value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> embedded value
        /// </returns>
        public static String ExternalLinkMetaValue(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().ExternalLinkValue(fieldName, embeddedFieldName);

			return String.Empty;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> embedded values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> embedded values
        /// </returns>
        public static IList<String> ExternalLinkMetaValues(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().ExternalLinkValues(fieldName, embeddedFieldName);

			return new List<String>();
        }
                
        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.String" /> value</returns>
        public static String StringMetaValue(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().StringValue(fieldName);

			return String.Empty;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.String" /> values</returns>
        public static IList<String> StringMetaValues(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().StringValues(fieldName);

			return new List<String>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> embedded value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> embedded value
        /// </returns>
        public static String StringMetaValue(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().StringValue(fieldName, embeddedFieldName);

			return String.Empty;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> embedded values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> embedded values
        /// </returns>
        public static IList<String> StringMetaValues(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().StringValues(fieldName, embeddedFieldName);

			return new List<String>();
        }
        
        /// <summary>
        /// Retrieves the <see cref="T:System.DateTime" /> value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.DateTime" /> value</returns>
        public static DateTime DateMetaValue(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().DateValue(fieldName);

			return default(DateTime);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.DateTime" /> values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.DateTime" /> values</returns>
        public static IList<DateTime> DateMetaValues(this Publication publication, String fieldName)
        {
			if (publication != null)				
				return publication.MetadataFields().DateValues(fieldName);

			return new List<DateTime>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.DateTime" /> embedded value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> embedded value
        /// </returns>
        public static DateTime DateMetaValue(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().DateValue(fieldName, embeddedFieldName);

			return default(DateTime);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.DateTime" /> embedded values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.DateTime" /> embedded values
        /// </returns>
        public static IList<DateTime> DateMetaValues(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().DateValues(fieldName, embeddedFieldName);

			return new List<DateTime>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> value</returns>
        public static Keyword KeywordMetaValue(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().KeywordValue(fieldName);

			return null;
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values</returns>
        public static IList<Keyword> KeywordMetaValues(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().KeywordValues(fieldName);

			return new List<Keyword>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> embedded value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> embedded value
        /// </returns>
        public static Keyword KeywordMetaValue(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().KeywordValue(fieldName, embeddedFieldName);

			return null;
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> embedded values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> embedded values
        /// </returns>
        public static IList<Keyword> KeywordMetaValues(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().KeywordValues(fieldName, embeddedFieldName);

			return new List<Keyword>();
        }
        
        /// <summary>
        /// Retrieves the <see cref="T:System.Double" /> value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.String" /> value</returns>
        public static Double NumberMetaValue(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().NumberValue(fieldName);

			return Double.NaN;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.Double" /> values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.Double" /> values</returns>
        public static IList<Double> NumberMetaValues(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().NumberValues(fieldName);

			return new List<Double>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.Double" /> embedded value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.Double" /> embedded value
        /// </returns>
        public static Double NumberMetaValue(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().NumberValue(fieldName, embeddedFieldName);

			return Double.NaN;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.Double" /> embedded values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.Double" /> embedded values
        /// </returns>
        public static IList<Double> NumberMetaValues(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().NumberValues(fieldName, embeddedFieldName);

			return new List<Double>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XHTML value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.String" /> XHTML value</returns>
        public static String XHTMLMetaValue(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().XHTMLValue(fieldName);

			return String.Empty;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XHTML values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.String" /> XHTML values</returns>
        public static IList<String> XHTMLMetaValues(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().XHTMLValues(fieldName);

			return new List<String>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XHTML embedded value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XHTML embedded value
        /// </returns>
        public static String XHTMLMetaValue(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().XHTMLValue(fieldName, embeddedFieldName);

			return String.Empty;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XHTML embedded values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XHTML embedded values
        /// </returns>
        public static IList<String> XHTMLMetaValues(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().XHTMLValues(fieldName, embeddedFieldName);

			return new List<String>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XML value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.String" /> XML value</returns>
        public static String XMLMetaValue(this Publication publication, String fieldName)
		{
			if (publication != null)
				return publication.MetadataFields().XMLValue(fieldName);

			return String.Empty;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XML values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:System.String" /> XML values</returns>
        public static IList<String> XMLMetaValues(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().XMLValues(fieldName);

			return new List<String>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XML embedded value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XML embedded value
        /// </returns>
        public static String XMLMetaValue(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().XMLValue(fieldName, embeddedFieldName);

			return String.Empty;
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XML embedded values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XML embedded values
        /// </returns>
        public static IList<String> XMLMetaValues(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().XMLValues(fieldName, embeddedFieldName);

			return new List<String>();
        }
        
        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> value</returns>
        public static ItemFields EmbeddedMetaValue(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().EmbeddedValue(fieldName);

			return null;
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> values</returns>
        public static IList<ItemFields> EmbeddedMetaValues(this Publication publication, String fieldName)
        {
			if (publication != null)
				return publication.MetadataFields().EmbeddedValues(fieldName);

			return new List<ItemFields>();
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> embedded value
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> embedded value
        /// </returns>
        public static ItemFields EmbeddedMetaValue(this Publication publication, String fieldName, String embeddedFieldName)
        {
			if (publication != null)
				return publication.MetadataFields().EmbeddedValue(fieldName, embeddedFieldName);

			return null;
        }

        /// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> embedded values
        /// </summary>
        /// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="embeddedFieldName">Name of the embedded field.</param>
        /// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> embedded values
        /// </returns>
        public static IList<ItemFields> EmbeddedMetaValues(this Publication publication, String fieldName, String embeddedFieldName)
		{
			if (publication != null)
				return publication.MetadataFields().EmbeddedValues(fieldName, embeddedFieldName);

			return new List<ItemFields>();
        }

		/// <summary>
		/// Returns the metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> this <see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /> is using.
		/// </summary>
		/// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
		/// <returns>Metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name or <c>String.Empty</c></returns>
		public static String UsingMetadataSchema(this Publication publication)
		{
			if (publication != null && publication.MetadataSchema != null)
				return publication.MetadataSchema.RootElementName;

			return String.Empty;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> is based on the given <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name.
		/// </summary>
		/// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
		/// <param name="SchemaElementName">Metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name</param>
		/// <remarks>Metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name comparison is case-insensitive</remarks>
		/// <returns><c>True</c> if the metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name is matching, otherwise <c>false</c></returns>
		public static Boolean IsUsingOnMetadataSchema(this Publication publication, String MetadataSchemaElementName)
		{
			return String.Equals(publication.UsingMetadataSchema(), MetadataSchemaElementName, StringComparison.OrdinalIgnoreCase);
		}
    }
}
