#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: RepositoryLocalObjectExtensions
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
using Tridion.ContentManager.Publishing;
using Tridion.ContentManager.Templating;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="RepositoryLocalObjectExtensions"/> supplies .NET extension functions for <see cref="Tridion.ContentManager.ContentManagement.RepositoryLocalObject" />
    /// </summary>
    public static class RepositoryLocalObjectExtensions
    {
		/// <summary>
		/// Returns the metadata <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> for the
		/// <see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" />
		/// </summary>
		/// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /></returns>
		public static ItemFields MetadataFields(this RepositoryLocalObject repositoryLocalObject)
		{
			if (repositoryLocalObject != null)
				return ItemFieldsFactory.Get(repositoryLocalObject, repositoryLocalObject.Metadata, repositoryLocalObject.MetadataSchema);

			return null;
		}

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
        /// </returns>
        public static Component ComponentMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().ComponentValue(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> values
        /// </returns>
        public static IList<Component> ComponentMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().ComponentValues(fieldName);
        }

        /// <summary>
        /// Retrieves the embedded <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
        /// </returns>
        public static Component ComponentMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().ComponentValue(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded values
        /// </returns>
        public static IList<Component> ComponentMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().ComponentValues(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> external link meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.String" /> external link meta value
        /// </returns>
        public static String ExternalLinkMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().ExternalLinkValue(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> external link meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.String" /> external link meta values
        /// </returns>
        public static IList<String> ExternalLinkMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().ExternalLinkValues(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> external link embedded meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> external link embedded meta value
        /// </returns>
        public static String ExternalLinkMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().ExternalLinkValue(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> external link embedded meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> external link embedded meta values
        /// </returns>
        public static IList<String> ExternalLinkMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().ExternalLinkValues(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.String" /> meta value
        /// </returns>
        public static String StringMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().StringValue(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.String" /> meta values
        /// </returns>
        public static IList<String> StringMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().StringValues(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> embedded meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> embedded meta value
        /// </returns>
        public static String StringMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().StringValue(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> embedded meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> embedded meta values
        /// </returns>
        public static IList<String> StringMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().StringValues(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.DateTime" /> meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.DateTime" /> meta value
        /// </returns>
        public static DateTime DateMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().DateValue(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.DateTime" /> meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.DateTime" /> meta values
        /// </returns>
        public static IList<DateTime> DateMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().DateValues(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.DateTime" /> embedded meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.DateTime" /> embedded meta value
        /// </returns>
        public static DateTime DateMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().DateValue(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.DateTime" /> embedded meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.DateTime" /> embedded meta values
        /// </returns>
        public static IList<DateTime> DateMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().DateValues(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ComponentManagement.Keyword" /> meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ComponentManagement.Keyword" /> meta value
        /// </returns>
        public static Keyword KeywordMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().KeywordValue(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ComponentManagement.Keyword" /> meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ComponentManagement.Keyword" /> meta values
        /// </returns>
        public static IList<Keyword> KeywordMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().KeywordValues(fieldName);
        }


        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ComponentManagement.Keyword" /> embedded meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ComponentManagement.Keyword" /> embedded meta value
        /// </returns>
        public static Keyword KeywordMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().KeywordValue(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ComponentManagement.Keyword" /> embedded meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ComponentManagement.Keyword" /> embedded meta values
        /// </returns>
        public static IList<Keyword> KeywordMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().KeywordValues(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.Double" /> meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.Double" /> meta value
        /// </returns>
        public static Double NumberMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().NumberValue(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.Double" /> meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.Double" /> meta values
        /// </returns>
        public static IList<Double> NumberMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().NumberValues(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.Double" /> embedded meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.Double" /> embedded meta value
        /// </returns>
        public static Double NumberMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().NumberValue(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.Double" /> embedded meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.Double" /> embedded meta values
        /// </returns>
        public static IList<Double> NumberMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().NumberValues(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XHTML meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XHTML meta value
        /// </returns>
        public static String XHTMLMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().XHTMLValue(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XHTML meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XHTML meta values
        /// </returns>
        public static IList<String> XHTMLMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().XHTMLValues(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XHTML embedded meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XHTML embedded meta value
        /// </returns>
        public static String XHTMLMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().XHTMLValue(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XHTML embedded meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XHTML embedded meta values
        /// </returns>
        public static IList<String> XHTMLMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().XHTMLValues(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XML meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XML meta value
        /// </returns>
        public static String XMLMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().XMLValue(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> XML meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:System.String" /> XML meta values
        /// </returns>
        public static IList<String> XMLMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().XMLValues(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> embedded XML meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> embedded XML meta value
        /// </returns>
        public static String XMLMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().XMLValue(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:System.String" /> embedded XML meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <param name="EmbeddedFieldName">Name of the embedded field.</param>
        /// <returns>
        ///   <see cref="T:System.String" /> embedded XML meta values
        /// </returns>
        public static IList<String> XMLMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().XMLValues(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> meta value
        /// </returns>
        public static ItemFields EmbeddedMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().EmbeddedValue(fieldName);
        }

        /// <summary>
        /// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> meta values
        /// </returns>
        public static IList<ItemFields> EmbeddedMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName)
        {
            return repositoryLocalObject.MetadataFields().EmbeddedValues(fieldName);
        }

        /// <summary>
        /// Retrieves the embedded <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> meta value
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> embedded meta value
        /// </returns>
        public static ItemFields EmbeddedMetaValue(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().EmbeddedValue(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Retrieves the embedded <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> meta values
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="FieldName">Field Name</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> embedded meta values
        /// </returns>
        public static IList<ItemFields> EmbeddedMetaValues(this RepositoryLocalObject repositoryLocalObject, String fieldName, String embeddedFieldName)
        {
            return repositoryLocalObject.MetadataFields().EmbeddedValues(fieldName, embeddedFieldName);
        }

        /// <summary>
        /// Returns the last publish date in the current <see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /> and
        /// current <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget"/>
        /// </summary>
        /// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
        /// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine" /></param>
        /// <returns>Returns <see cref="T:System.DateTime"/> or DateTime.MinValue</returns>
        public static DateTime PublishedAt(this RepositoryLocalObject repositoryLocalObject, Engine engine)
        {
            if (engine.RenderMode == RenderMode.Publish)
				return repositoryLocalObject.PublishedAt(repositoryLocalObject.ContextRepository as Publication, engine.PublishingContext.PublicationTarget);

            return default(DateTime);
        }

		/// <summary>
		/// Returns the metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> this <see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /> is using.
		/// </summary>
		/// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
		/// <returns>Metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name or <c>String.Empty</c></returns>
		public static String UsingMetadataSchema(this RepositoryLocalObject repositoryLocalObject)
		{
			if (repositoryLocalObject.MetadataSchema != null)
				return repositoryLocalObject.MetadataSchema.RootElementName;

			return String.Empty;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> is based on the given <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name.
		/// </summary>
		/// <param name="repositoryLocalObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
		/// <param name="schemaElementName">Metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name</param>
		/// <remarks>Metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name comparison is case-insensitive</remarks>
		/// <returns><c>True</c> if the metadata <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name is matching, otherwise <c>false</c></returns>
		public static Boolean IsUsingOnMetadataSchema(this RepositoryLocalObject repositoryLocalObject, String schemaElementName)
		{
			return String.Equals(repositoryLocalObject.UsingMetadataSchema(), schemaElementName, StringComparison.OrdinalIgnoreCase);
		}
    }
}

