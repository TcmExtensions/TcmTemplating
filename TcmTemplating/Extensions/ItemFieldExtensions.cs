#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: ItemFieldExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Xml;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="ItemFieldExtensions" /> supplies .NET extension functions for <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" />
    /// </summary>
    public static class ItemFieldExtensions
    {
        /// <summary>
        /// Retrieves a <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /> field value.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /></returns>
        public static ItemFields EmbeddedValue(this ItemField itemField)
        {
			if (itemField != null)
			{
				IList<ItemFields> values = itemField.EmbeddedValues();

				if (values.Count > 0)
					return values[0];
			}

            return null;
        }

        /// <summary>
        /// Retrieves a <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> field value.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></returns>
		public static Component ComponentValue(this ItemField itemField)
        {
			if (itemField != null)
			{
				IList<Component> values = itemField.ComponentValues();

				if (values.Count > 0)
					return values[0];
			}

            return null;
        }

        /// <summary>
        /// Retrieves a <see cref="T:System.String" /> external link field value.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.String" /> external link value</returns>
		public static String ExternalLinkValue(this ItemField itemField)
        {
			if (itemField != null)
			{
				IList<String> values = itemField.ExternalLinkValues();

				if (values.Count > 0)
					return values[0];
			}

            return String.Empty;
        }

        /// <summary>
        /// Retrieves a <see cref="T:System.DateTime" /> field value.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.DateTime" /></returns>
		public static DateTime DateValue(this ItemField itemField)
        {
			if (itemField != null)
			{
				IList<DateTime> values = itemField.DateValues();

				if (values.Count > 0)
					return values[0];
			}

			return default(DateTime);
        }

        /// <summary>
        /// Retrieves a <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> field value.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /></returns>
		public static Keyword KeywordValue(this ItemField itemField)
        {
			if (itemField != null)
			{
				IList<Keyword> values = itemField.KeywordValues();

				if (values.Count > 0)
					return values[0];
			}

            return null;
        }

        /// <summary>
        /// Retrieves a <see cref="T:System.Double" /> field value.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.Double" /></returns>
		public static Double NumberValue(this ItemField itemField)
        {
			if (itemField != null)
			{
				IList<Double> values = itemField.NumberValues();

				if (values.Count > 0)
					return values[0];
			}

			return Double.NaN;
        }

        /// <summary>
        /// Retrieves a <see cref="T:System.String" /> field value.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.String" /></returns>
		public static String StringValue(this ItemField itemField)
        {
			if (itemField != null)
			{
				IList<String> values = itemField.StringValues();

				if (values.Count > 0)
					return values[0];
			}

            return String.Empty;
        }

        /// <summary>
        /// Retrieves a <see cref="T:System.String" /> XHTML field value.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.String" /> XHTML value</returns>
		public static String XHTMLValue(this ItemField itemField)
		{
			if (itemField != null)
			{
				IList<String> values = itemField.XHTMLValues();

				if (values.Count > 0)
					return values[0];
			}

            return String.Empty;
        }

        /// <summary>
        /// Retrieves a <see cref="T:System.String" /> XML field value.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.String" /> XML value</returns>
		public static String XMLValue(this ItemField itemField)
        {
			if (itemField != null)
			{

				IList<String> values = itemField.XMLValues();

				if (values.Count > 0)
					return values[0];
			}

            return String.Empty;
        }

        /// <summary>
        /// Retrieves multiple <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> field values.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component" /> values</returns>
		public static IList<Component> ComponentValues(this ItemField itemField)
        {
			if (itemField != null)
			{
				ComponentLinkField field = itemField as ComponentLinkField;

				if (field != null && field.Values.Count > 0)
					return field.Values;
			}

            return new List<Component>();
        }

        /// <summary>
        /// Retrieves multiple <see cref="T:System.String" /> field values.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.String" /> values</returns>
		public static IList<String> ExternalLinkValues(this ItemField itemField)
        {
			if (itemField != null)
			{
				ExternalLinkField field = itemField as ExternalLinkField;

				if (field != null && field.Values.Count > 0)
					return field.Values;
			}

            return new List<String>();
        }

        /// <summary>
        /// Retrieves multiple <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> field values.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> values</returns>
		public static IList<ItemFields> EmbeddedValues(this ItemField itemField)
        {
			if (itemField != null)
			{
				EmbeddedSchemaField field = itemField as EmbeddedSchemaField;

				if (field != null && field.Values.Count > 0)
					return field.Values;
			}

            return new List<ItemFields>();
        }

        /// <summary>
        /// Retrieves multiple <see cref="T:System.DateTime" /> field values.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.DateTime" /> values</returns>
		public static IList<DateTime> DateValues(this ItemField itemField)
        {
			if (itemField != null)
			{
				DateField field = itemField as DateField;

				if (field != null && field.Values.Count > 0)
					return field.Values;
			}

            return new List<DateTime>();
        }

        /// <summary>
        /// Retrieves multiple <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> field values.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values</returns>
		public static IList<Keyword> KeywordValues(this ItemField itemField)
        {
			if (itemField != null)
			{
				KeywordField field = itemField as KeywordField;

				if (field != null && field.Values.Count > 0)
					return field.Values;
			}

            return new List<Keyword>();
        }

        /// <summary>
        /// Retrieves multiple <see cref="T:System.Double" /> field values.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.Double" /> values</returns>
		public static IList<Double> NumberValues(this ItemField itemField)
        {
			if (itemField != null)
			{

				NumberField field = itemField as NumberField;

				if (field != null && field.Values.Count > 0)
					return field.Values;
			}

            return new List<Double>();
        }

        /// <summary>
        /// Retrieves multiple <see cref="T:System.String" /> field values.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.String" /> values</returns>
		public static IList<String> StringValues(this ItemField itemField)
        {
			if (itemField != null)
			{
				TextField field = itemField as TextField;

				if (field != null && field.Values.Count > 0)
					return field.Values;
			}

            return new List<String>();
        }

        /// <summary>
        /// Retrieves multiple <see cref="T:System.String" /> XHTML field values.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.String" /> XHTML values</returns>
		public static IList<String> XHTMLValues(this ItemField itemField)
        {
			if (itemField != null)
			{
				XhtmlField field = itemField as XhtmlField;

				if (field != null && field.Values.Count > 0)
					return field.Values;
			}

            return new List<String>();
        }

        /// <summary>
        /// Retrieves multiple <see cref="T:System.String" /> XML field values.
        /// </summary>
		/// <param name="itemField"><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemField" /></param>
        /// <returns><see cref="T:System.String" /> XML values</returns>
		public static IList<String> XMLValues(this ItemField itemField)
        {
			List<String> results = new List<String>();

			if (itemField != null)
			{				
				TextField field = itemField as TextField;

				if (field != null && field.Values.Count > 0)
				{
					XmlDocument xDoc = new XmlDocument();

					foreach (String value in field.Values)
					{
						xDoc.LoadXml("<x>" + value + "</x>");

						String xml = xDoc.RemoveNameSpaces();
						xml = xml.Substring(3, xml.Length - 7);

						results.Add(xml);
					}
				}
			}

            return results;
        }
    }
}
