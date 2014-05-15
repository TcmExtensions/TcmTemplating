#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: ItemFieldsExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
// 	Change History
//	Date Modified       : May 14, 2014
//	Changed By          : Venkata Siva Charan Sandra
//	Change Description  : Fixed the recursion error in EmbeddedValues() logic.
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="ItemFieldsExtensions" /> supplies .NET extension functions for <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" />
    /// </summary>
    public static class ItemFieldsExtensions
    {
        /// <summary>
        /// Retrieves a <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> field value.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></returns>
        public static Component ComponentValue(this ItemFields itemFields, String fieldName)
        {
            if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].ComponentValue();

            return null;
        }

        /// <summary>
        /// Retrieves <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> field values.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component" /> values</returns>
        public static IList<Component> ComponentValues(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].ComponentValues();

            return new List<Component>();
        }

		/// <summary>
		/// Retrieves an embedded <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> field value.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
		/// </returns>
		public static Component ComponentValue(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).ComponentValue(embeddedFieldName);

			return null;
		}

		/// <summary>
		/// Retrieves embedded <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> field values.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component" /> values</returns>
		/// </returns>
		public static IList<Component> ComponentValues(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).ComponentValues(embeddedFieldName);

			return new List<Component>();
		}

        /// <summary>
        /// Retrieves a <see cref="T:System.String" /> field value.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.String" /></returns>
        public static String ExternalLinkValue(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].ExternalLinkValue();

			return String.Empty;
        }

        /// <summary>
        /// Retrieves <see cref="T:System.String" /> field values.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.String" /> values</returns>
        public static IList<String> ExternalLinkValues(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].ExternalLinkValues();

            return new List<String>();
        }

		/// <summary>
		/// Retrieves an embedded <see cref="T:System.String" /> field value.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		///   <see cref="T:System.String" />
		/// </returns>
		public static String ExternalLinkValue(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).ExternalLinkValue(embeddedFieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves embedded <see cref="T:System.String" /> field values.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">embedded field to retrieve</param>
		/// <returns>
		/// <returns><see cref="T:System.String" /> values</returns>
		/// </returns>
		public static IList<String> ExternalLinkValues(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).ExternalLinkValues(embeddedFieldName);

			return new List<String>();
		}

        /// <summary>
        /// Retrieves a <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> field value.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /></returns>
        public static ItemFields EmbeddedValue(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].EmbeddedValue();

            return null;
        }

        /// <summary>
        /// Retrieves <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> field values.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> values</returns>
        public static IList<ItemFields> EmbeddedValues(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].EmbeddedValues();

            return new List<ItemFields>();
        }

		/// <summary>
		/// Retrieves an embedded <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> field value.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" />
		/// </returns>
		public static ItemFields EmbeddedValue(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).EmbeddedValue(embeddedFieldName);

			return null;
		}

		/// <summary>
		/// Retrieves embedded <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> field values.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">embedded field to retrieve</param>
		/// <returns>
		/// <returns><see cref="T:System.String" /> values</returns>
		/// </returns>
		public static IList<ItemFields> EmbeddedValues(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).EmbeddedValues(embeddedFieldName);

			return new List<ItemFields>();
		}

        /// <summary>
        /// Retrieves a <see cref="T:System.DateTime" /> field value.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.DateTime" /></returns>
        public static DateTime DateValue(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].DateValue();

			return default(DateTime);
        }

        /// <summary>
        /// Retrieves <see cref="T:System.DateTime" /> field values.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.DateTime" /> values</returns>
        public static IList<DateTime> DateValues(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].DateValues();

            return new List<DateTime>();
        }

		/// <summary>
		/// Retrieves an embedded <see cref="T:System.DateTime" /> field value.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		///   <see cref="T:System.DateTime" />
		/// </returns>
		public static DateTime DateValue(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).DateValue(embeddedFieldName);

			return default(DateTime);
		}

		/// <summary>
		/// Retrieves embedded <see cref="T:System.DateTime" /> field values.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">embedded field to retrieve</param>
		/// <returns>
		/// <returns><see cref="T:System.String" /> values</returns>
		/// </returns>
		public static IList<DateTime> DateValues(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).DateValues(embeddedFieldName);

			return new List<DateTime>();
		}

        /// <summary>
        /// Retrieves a <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> field value.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /></returns>
        public static Keyword KeywordValue(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].KeywordValue();

            return null;
        }

        /// <summary>
        /// Retrieves <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> field values.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values</returns>
        public static IList<Keyword> KeywordValues(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].KeywordValues();

            return new List<Keyword>();
        }

		/// <summary>
		/// Retrieves an embedded <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> field value.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" />
		/// </returns>
		public static Keyword KeywordValue(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).KeywordValue(embeddedFieldName);

			return null;
		}

		/// <summary>
		/// Retrieves embedded <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> field values.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">embedded field to retrieve</param>
		/// <returns>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values</returns>
		/// </returns>
		public static IList<Keyword> KeywordValues(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).KeywordValues(embeddedFieldName);

			return new List<Keyword>();
		}

        /// <summary>
        /// Retrieves a <see cref="T:System.Double" /> field value.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.Double" /></returns>
        public static Double NumberValue(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].NumberValue();

			return Double.NaN;
        }

        /// <summary>
        /// Retrieves <see cref="T:System.Double" /> field values.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.Double" /> values</returns>
        public static IList<Double> NumberValues(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].NumberValues();

            return new List<Double>();
        }

		/// <summary>
		/// Retrieves an embedded <see cref="T:System.Double" /> field value.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		///   <see cref="T:System.Double" />
		/// </returns>
		public static Double NumberValue(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).NumberValue(embeddedFieldName);

			return Double.NaN;
		}

		/// <summary>
		/// Retrieves embedded <see cref="T:System.Double" /> field values.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">embedded field to retrieve</param>
		/// <returns>
		/// <returns><see cref="T:System.Double" /> values</returns>
		/// </returns>
		public static IList<Double> NumberValues(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).NumberValues(embeddedFieldName);

			return new List<Double>();
		}

        /// <summary>
        /// Retrieves a <see cref="T:System.String" /> field value.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.String" /></returns>
        public static String StringValue(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].StringValue();

            return String.Empty;
        }

        /// <summary>
        /// Retrieves <see cref="T:System.String" /> field values.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.String" /> values</returns>
        public static IList<String> StringValues(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].StringValues();

            return new List<String>();
        }

		/// <summary>
		/// Retrieves an embedded <see cref="T:System.String" /> field value.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		///   <see cref="T:System.String" />
		/// </returns>
		public static String StringValue(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).StringValue(embeddedFieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves embedded <see cref="T:System.String" /> field values.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">embedded field to retrieve</param>
		/// <returns>
		/// <returns><see cref="T:System.String" /> values</returns>
		/// </returns>
		public static IList<String> StringValues(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).StringValues(embeddedFieldName);

			return new List<String>();
		}

        /// <summary>
        /// Retrieves a <see cref="T:System.String" /> XHTML field value.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.String" /> XHTML</returns>
        public static String XHTMLValue(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].XHTMLValue();

            return String.Empty;
        }

        /// <summary>
        /// Retrieves <see cref="T:System.String" /> XHTML field values.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.String" /> XHTML values</returns>
        public static IList<String> XHTMLValues(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].XHTMLValues();

            return new List<String>();
        }

		/// <summary>
		/// Retrieves an embedded <see cref="T:System.String" /> XHTML field value.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		///   <see cref="T:System.String" />
		/// </returns>
		public static String XHTMLValue(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).XHTMLValue(embeddedFieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves embedded <see cref="T:System.String" /> XHTML field values.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">embedded field to retrieve</param>
		/// <returns>
		/// <returns><see cref="T:System.String" /> values</returns>
		/// </returns>
		public static IList<String> XHTMLValues(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).XHTMLValues(embeddedFieldName);

			return new List<String>();
		}

        /// <summary>
        /// Retrieves a <see cref="T:System.String" /> XML field value.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.String" /> XML</returns>
        public static String XMLValue(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].XMLValue();

            return String.Empty;
        }

        /// <summary>
        /// Retrieves <see cref="T:System.String" /> XML field values.
        /// </summary>
        /// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
        /// <param name="fieldName">Fieldname to retrieve</param>
        /// <returns><see cref="T:System.String" /> XML values</returns>
        public static IList<String> XMLValues(this ItemFields itemFields, String fieldName)
        {
			if (itemFields != null && itemFields.Contains(fieldName))
                return itemFields[fieldName].XMLValues();

            return new List<String>();
        }

		/// <summary>
		/// Retrieves an embedded <see cref="T:System.String" /> XML field value.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">Embedded field to retrieve</param>
		/// <returns>
		///   <see cref="T:System.String" />
		/// </returns>
		public static String XMLValue(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{	
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).XMLValue(embeddedFieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves embedded <see cref="T:System.String" /> XML field values.
		/// </summary>
		/// <param name="itemFields">The <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> collection.</param>
		/// <param name="fieldName">Fieldname to retrieve</param>
		/// <param name="embeddedFieldName">embedded field to retrieve</param>
		/// <returns>
		/// <returns><see cref="T:System.String" /> values</returns>
		/// </returns>
		public static IList<String> XMLValues(this ItemFields itemFields, String fieldName, String embeddedFieldName)
		{
			if (itemFields != null && itemFields.Contains(fieldName))
				return itemFields.EmbeddedValue(fieldName).XMLValues(embeddedFieldName);

			return new List<String>();
		}
    }
}
