#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: ComponentExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 15, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using TcmTemplating.Helpers;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace TcmTemplating.Extensions
{
	/// <summary>
	/// <see cref="ComponentExtensions"/> supplies .NET extension functions for <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
	/// </summary>
	public static class ComponentExtensions
	{
		/// <summary>
		/// Returns the <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> for the
		/// <see cref="T:Tridion.ContentManager.ContentManagement.Component" />
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /></returns>
		public static ItemFields Fields(this Component component)
		{
			if (component != null)
				return ItemFieldsFactory.Get(component, component.Content, component.Schema);

			return null;
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component" /> value</returns>
		public static Component ComponentValue(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().ComponentValue(fieldName);
			
			return null;
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component" /> values</returns>
		public static IList<Component> ComponentValues(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().ComponentValues(fieldName);

			return new List<Component>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded value
		/// </returns>
		public static Component ComponentValue(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().ComponentValue(fieldName, embeddedFieldName);

			return null;
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> embedded values
		/// </returns>
		public static IList<Component> ComponentValues(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().ComponentValues(fieldName, embeddedFieldName);

			return new List<Component>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> external link value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.String" /> external link value</returns>
		public static String ExternalLinkValue(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().ExternalLinkValue(fieldName);


			return String.Empty;
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> external link values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.String" /> external link values</returns>
		public static IList<String> ExternalLinkValues(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().ExternalLinkValues(fieldName);

			return new List<String>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> embedded external link value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.String" /> embedded external link value
		/// </returns>
		public static String ExternalLinkValue(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().ExternalLinkValue(fieldName, embeddedFieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> embedded external link values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.String" /> embedded external link values
		/// </returns>
		public static IList<String> ExternalLinkValues(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().ExternalLinkValues(fieldName, embeddedFieldName);


			return new List<String>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.String" /> value</returns>
		public static String StringValue(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().StringValue(fieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.String" /> values</returns>
		public static IList<String> StringValues(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().StringValues(fieldName);

			return new List<String>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> embedded value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.String" /> embedded value
		/// </returns>
		public static String StringValue(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().StringValue(fieldName, embeddedFieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> embedded values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.String" /> embedded values
		/// </returns>
		public static IList<String> StringValues(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().StringValues(fieldName, embeddedFieldName);
			
			return new List<String>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.DateTime" /> value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.DateTime" /> value</returns>
		public static DateTime DateValue(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().DateValue(fieldName);

			return default(DateTime);
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.DateTime" /> values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.DateTime" /> values</returns>
		public static IList<DateTime> DateValues(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().DateValues(fieldName);
			
			return new List<DateTime>();
		}

		/// <summary>
		/// Retrieves the embedded <see cref="T:System.DateTime" /> value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.DateTime" /> embedded value
		/// </returns>
		public static DateTime DateValue(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().DateValue(fieldName, embeddedFieldName);

			return default(DateTime);
		}

		/// <summary>
		/// Retrieves the embedded <see cref="T:System.DateTime" /> values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.DateTime" /> embedded values
		/// </returns>
		public static IList<DateTime> DateValues(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().DateValues(fieldName, embeddedFieldName);

			return new List<DateTime>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> value</returns>
		public static Keyword KeywordValue(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().KeywordValue(fieldName);
			
			return null;
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values</returns>
		public static IList<Keyword> KeywordValues(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().KeywordValues(fieldName);

			return new List<Keyword>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> embedded value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> embedded value
		/// </returns>
		public static Keyword KeywordValue(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().KeywordValue(fieldName, embeddedFieldName);

			return null;
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> embedded values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> embedded values
		/// </returns>
		public static IList<Keyword> KeywordValues(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().KeywordValues(fieldName, embeddedFieldName);

			return new List<Keyword>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.Double" /> value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.Double" /> value</returns>
		public static Double NumberValue(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().NumberValue(fieldName);

			return Double.NaN;
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.Double" /> values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.Double" /> values</returns>
		public static IList<Double> NumberValues(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().NumberValues(fieldName);

			return new List<Double>();
		}

		/// <summary>
		/// Retrieves the embedded <see cref="T:System.Double" /> value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.Double" /> embedded value</returns>
		public static Double NumberValue(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().NumberValue(fieldName, embeddedFieldName);

			return Double.NaN;
		}

		/// <summary>
		/// Retrieves the embedded <see cref="T:System.Double" /> values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.Double" /> embedded values</returns>
		public static IList<Double> NumberValues(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().NumberValues(fieldName, embeddedFieldName);

			return new List<Double>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> XHTML value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.String" /> XHTML value</returns>
		public static String XHTMLValue(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().XHTMLValue(fieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> XHTML values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.String" /> XHTML values</returns>
		public static IList<String> XHTMLValues(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().XHTMLValues(fieldName);

			return new List<String>();
		}

		/// <summary>
		/// Retrieves the embedded <see cref="T:System.String" /> XHTML value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.String" /> embedded XHTML value
		/// </returns>
		public static String XHTMLValue(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().XHTMLValue(fieldName, embeddedFieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves the embedded <see cref="T:System.String" /> XHTML values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.String" /> embedded XHTML values
		/// </returns>
		public static IList<String> XHTMLValues(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().XHTMLValues(fieldName, embeddedFieldName);

			return new List<String>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> XML value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.String" /> XML value</returns>
		public static String XMLValue(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().XMLValue(fieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> XML values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:System.String" /> XML values</returns>
		public static IList<String> XMLValues(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().XMLValues(fieldName);

			return new List<String>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> embedded XML value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.String" /> embedded XML value
		/// </returns>
		public static String XMLValue(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().XMLValue(fieldName, embeddedFieldName);

			return String.Empty;
		}

		/// <summary>
		/// Retrieves the <see cref="T:System.String" /> embedded XML values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:System.String" /> embedded XML values
		/// </returns>
		public static IList<String> XMLValues(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().XMLValues(fieldName, embeddedFieldName);

			return new List<String>();
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> value</returns>
		public static ItemFields EmbeddedValue(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().EmbeddedValue(fieldName);
			
			return null;
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> values</returns>
		public static IList<ItemFields> EmbeddedValues(this Component component, String fieldName)
		{
			if (component != null)
				return component.Fields().EmbeddedValues(fieldName);

			return new List<ItemFields>();
		}

		/// <summary>
		/// Retrieves the embedded <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> value
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> embedded value
		/// </returns>
		public static ItemFields EmbeddedValue(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().EmbeddedValue(fieldName).EmbeddedValue(embeddedFieldName);

			return null;
		}

		/// <summary>
		/// Retrieves the embedded <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> values
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="fieldName">Field Name</param>
		/// <param name="embeddedFieldName">Name of the embedded field.</param>
		/// <returns>
		///   <see cref="T:Tridion.ContentManager.ContentManagement.Fields.ItemFields" /> embedded values
		/// </returns>
		public static IList<ItemFields> EmbeddedValues(this Component component, String fieldName, String embeddedFieldName)
		{
			if (component != null)
				return component.Fields().EmbeddedValue(fieldName).EmbeddedValues(embeddedFieldName);

			return new List<ItemFields>();
		}

		/// <summary>
		/// Returns the <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> this <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> is based on.
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name or <c>String.Empty</c></returns>
		public static String BasedOnSchema(this Component component)
		{			
			if (component != null && component.Schema != null)
				return component.Schema.RootElementName;

			return String.Empty;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> is based  [the specified component].
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="schemaElementName"><see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name</param>
		/// <remarks><see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name comparison is case-insensitive</remarks>
		/// <returns><c>True</c> if the <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name is matching, otherwise <c>false</c></returns>
		public static Boolean IsBasedOnSchema(this Component component, String schemaElementName)
		{
			if (component != null)
				return String.Equals(component.BasedOnSchema(), schemaElementName, StringComparison.OrdinalIgnoreCase);
			
			return false;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> is based  [the specified component].
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
		/// <param name="schemaElementName"><see cref="T:System.Enum"/> for the <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name</param>
		/// <remarks><see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name comparison is case-insensitive</remarks>
		/// <returns><c>True</c> if the <see cref="T:Tridion.ContentManager.ContentManagement.Schema" /> root element name is matching, otherwise <c>false</c></returns>
		public static Boolean IsBasedOnSchema(this Component component, Enum schemaElementName)
		{
			if (component != null)
				return component.IsBasedOnSchema(schemaElementName.ToString());
			
			return false;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:Tridion.ContentManager.ContentManagement.Component"/>
		/// is used in the first <see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentPresentation" />
		/// on the <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></param>
		/// <param name="page"><see cref="T:Tridion.ContentManager.CommunicationManagement.Page" /></param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="T:Tridion.ContentManager.ContentManagement.Component"/> is used in the first <see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentPresentation" />
		///   on the <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsFirstComponent(this Component component, Page page)
		{
			if (component != null)
			{
				if (page != null)
				{
					ComponentPresentation first = page.ComponentPresentations.First();

					if (first != null)
						return first.Component.Id == component.Id;
				}

				// No page present, this component is the only component
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:Tridion.ContentManager.ContentManagement.Component"/>
		/// is used in the last <see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentPresentation" />
		/// on the <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></param>
		/// <param name="page"><see cref="T:Tridion.ContentManager.CommunicationManagement.Page" /></param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="T:Tridion.ContentManager.ContentManagement.Component"/> is used in the last <see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentPresentation" />
		///   on the <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLastComponent(this Component component, Page page)
		{
			if (component != null)
			{
				if (page != null)
				{
					ComponentPresentation last = page.ComponentPresentations.Last();

					if (last != null)
						return last.Component.Id == component.Id;
				}

				// No page present, this component is the only component
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns grouped components from the current <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />
		/// list of <see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentPresentation" />
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></param>
		/// <param name="template"><see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentTemplate"/></param>
		/// <param name="page"><see cref="T:Tridion.ContentManager.CommunicationManagement.Page"/></param>
		/// <returns>List of <see cref="T:Tridion.ContentManager.ContentManagement.Component"/></returns>
		public static IList<Component> ComponentGroup(this Component component, ComponentTemplate template, Page page)
		{
			bool isLocated = false;
			List<Component> result = new List<Component>();
			ComponentPresentation previous = null;

			if (component != null && page != null)
			{

				foreach (ComponentPresentation cp in page.ComponentPresentations)
				{
					// Locate the first component presentation matching our current component
					// Also ensure its the first matching component in a "block"
					if (!isLocated &&
						cp.Component.Id.ItemId == component.Id.ItemId &&
						cp.ComponentTemplate.Id.ItemId == template.Id.ItemId
						&& (previous == null ||
						(previous.Component.Schema.Id.ItemId != component.Schema.Id.ItemId &&
						previous.ComponentTemplate.Id.ItemId != template.Id.ItemId)))
					{
						isLocated = true;
						result.Add(cp.Component);
						continue;
					}

					// Now find any following components in the same "block" based on the same schema and using the same component template
					if (isLocated)
					{
						if (cp.Component.Schema.Id.ItemId == component.Schema.Id.ItemId && cp.ComponentTemplate.Id.ItemId == template.Id.ItemId)
						{
							result.Add(cp.Component);
						}
						else
						{
							break;
						}
					}

					previous = cp;
				}
			}

			return result;
		}

		/// <summary>
		/// Returns the index of the specified <see cref="T:Tridion.ContentManager.ContentManagement.Component"/>
		/// on the <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />
		/// </summary>
		/// <param name="component"><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></param>
		/// <param name="page"><see cref="T:Tridion.ContentManager.CommunicationManagement.Page" /></param>
		/// <returns>
		///   Specified <see cref="T:Tridion.ContentManager.ContentManagement.Component"/> index on the <see cref="T:Tridion.ContentManager.CommunicationManagement.Page" />; otherwise, <c>-1</c>.
		/// </returns>
		public static int ComponentIndex(this Component component, Page page)
		{
			if (component != null && page != null)
			{
				return page.ComponentPresentations.IndexOf(cp => cp.Component.Id == component.Id);
			}

			return -1;
		}
	}
}
