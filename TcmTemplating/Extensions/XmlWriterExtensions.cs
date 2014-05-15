#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: XmlWriterExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Xml;
using Tridion.ContentManager;
using Tridion.ContentManager.ContentManagement;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="XmlWriterExtensions" /> supplies .NET extension functions for <see cref="T:System.Xml.XmlWriter" />
    /// </summary>
    public static class XmlWriterExtensions
    {
		private const String XML_DATESTAMP = "yyyy-MM-ddTHH:mm:ss";
		private const String XML_DATE = "yyyy-MM-dd";
		private const String XML_TIME = "HH:mm:ss";

        /// <summary>
        /// Writes a Tridion <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> to XML
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
        /// <param name="name">Node Name.</param>
        /// <param name="value"><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /></param>
        public static void WriteKeywordNode(this XmlWriter writer, String name, Keyword value)
        {
			if (writer != null)
				writer.WriteKeywordNode(name, value, false);
        }

        /// <summary>
        /// Writes a Tridion <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> to XML
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
        /// <param name="name">Node Name.</param>
        /// <param name="value"><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /></param>
        /// <param name="includeLevel">if set to <c>true</c> [include level].</param>
        public static void WriteKeywordNode(this XmlWriter writer, String name, Keyword value, bool includeLevel)
        {
			if (writer != null && !String.IsNullOrEmpty(name) && value != null)
            {
				writer.WriteStartElement(name);
				writer.WriteAttribute("uri", value.Id);

				writer.WriteAttributeOptional("key", value.Key);
				writer.WriteAttributeOptional("description", value.Description);

				writer.WriteAttribute("root", value.IsRoot.ToString().ToLower());

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Writes Tridion <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values to XML
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
        /// <param name="name">Node Name.</param>
        /// <param name="values"><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values</param>
        public static void WriteKeywordNodes(this XmlWriter writer, String name, IList<Keyword> values)
        {
			if (writer != null)
				writer.WriteKeywordNodes(name, values, false);
        }

        /// <summary>
        /// Writes Tridion <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values to XML
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="name">Node Name.</param>
		/// <param name="values"><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> values</param>
		/// <param name="includeLevel">if set to <c>true</c> [include level].</param>
        public static void WriteKeywordNodes(this XmlWriter writer, String name, IList<Keyword> values, bool includeLevel)
        {
			if (writer != null && !String.IsNullOrEmpty(name) && values != null && values.Count > 0)
            {
                foreach (Keyword value in values)
                {
                    writer.WriteKeywordNode(name, value, includeLevel);
                }
            }
        }

        /// <summary>
        /// Writes <see cref="T:System.String" /> values to XML
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
        /// <param name="name">Node Name.</param>
        /// <param name="values"><see cref="T:System.String" /> values</param>        
        public static void WriteTextNodes(this XmlWriter writer, String name, IList<String> values)
        {
			if (writer != null && values != null && values.Count > 0)
            {
                foreach (String value in values)
                {
                    writer.WriteElementString(name, value);
                }
            }
        }

        /// <summary>
        /// Writes a <see cref="T:System.String" /> XML value to XML
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
        /// <param name="Name">Node Name.</param>
        /// <param name="Value"><see cref="T:System.String" /> values</param>
        public static void WriteXmlNode(this XmlWriter writer, String name, String value)
        {
            if (writer != null && !String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(name);
                writer.WriteRaw(value);
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Writes <see cref="T:System.String" /> XML values to XML
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
        /// <param name="name">Node Name.</param>
        /// <param name="values"><see cref="T:System.String" /> values</param>
        public static void WriteXmlNodes(this XmlWriter writer, String name, IList<String> values)
        {
            if (writer != null && values != null && values.Count > 0)
            {
                foreach (String value in values)
                {
                    writer.WriteXmlNode(name, value);
                }
            }
        }

		/// <summary>
		/// Writes a <see cref="T:System.String" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.Int" /> value</param>
		public static void WriteAttribute(this XmlWriter writer, String localName, String value)
		{
			if (writer != null)
				writer.WriteAttributeString(localName, value);
		}

		/// <summary>
		/// Optionally writes a <see cref="T:System.String" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.Int" /> value</param>
		public static void WriteAttributeOptional(this XmlWriter writer, String localName, String value)
		{
			if (writer != null && !String.IsNullOrEmpty(value))
				writer.WriteAttributeString(localName, value);
		}
		
		/// <summary>
		/// Writes a <see cref="T:System.Int32" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.Int" /> value</param>
		public static void WriteAttribute(this XmlWriter writer, String localName, int value)
		{
			if (writer != null)
				writer.WriteAttributeString(localName, value.ToString());
		}

		/// <summary>
		/// Writes a <see cref="T:System.Double" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.Double" /> value</param>
		public static void WriteAttribute(this XmlWriter writer, String localName, double value)
		{
			if (writer != null)
				writer.WriteAttributeString(localName, value.ToString());
		}

		/// <summary>
		/// Writes a <see cref="T:Tridion.ContentManager.TcmUri" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:Tridion.ContentManager.TcmUri" /> value</param>
		/// <remarks>Only the TcmUri.ItemId is written as publishing is already publication specific</remarks>
		public static void WriteAttribute(this XmlWriter writer, String localName, TcmUri value)
		{
			if (writer != null)	
				writer.WriteAttributeString(localName, value != null ? value.ItemId.ToString() : String.Empty);
		}

		/// <summary>
		/// Writes a <see cref="T:Tridion.ContentManager.TcmUri" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:Tridion.ContentManager.TcmUri" /> value</param>
		/// <remarks>Only the TcmUri.ItemId is written as publishing is already publication specific</remarks>
		public static void WriteAttributeOptional(this XmlWriter writer, String localName, TcmUri value)
		{
			if (writer != null && value != null)
				writer.WriteAttributeString(localName, value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:dateTime /&gt;</remarks>
		public static void WriteAttribute(this XmlWriter writer, String localName, DateTime value)
		{			
			if (writer != null)
				writer.WriteAttributeString(localName, value.ToString(XML_DATESTAMP));
		}

		/// <summary>
		/// Optionally writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:dateTime /&gt;</remarks>
		public static void WriteAttributeOptional(this XmlWriter writer, String localName, DateTime value)
		{
			if (writer != null && value != DateTime.MinValue)
				writer.WriteAttribute(localName, value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Name of the local.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <param name="format">Datetime format.</param>
		/// <remarks>
		/// Xml value type is &lt;xs:dateTime /&gt;
		/// </remarks>
		[Obsolete("Use WriteAttribute without format to output valid xsd:dateTime XML")]
		public static void WriteAttribute(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (writer != null)
				writer.WriteAttributeString(localName, value.ToString(format));
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:date /&gt;</remarks>
		public static void WriteAttributeDate(this XmlWriter writer, String localName, DateTime value)
		{
			if (writer != null)
				writer.WriteAttributeString(localName, value.ToString(XML_DATE));
		}

		/// <summary>
		/// Optionally writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:date /&gt;</remarks>
		public static void WriteAttributeDateOptional(this XmlWriter writer, String localName, DateTime value)
		{
			if (value != DateTime.MinValue)
				writer.WriteAttributeDate(localName, value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:date /&gt;</remarks>
		[Obsolete("Use WriteAttribute without format to output valid xsd:date XML")]
		public static void WriteAttributeDate(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (writer != null)
				writer.WriteAttributeString(localName, value.ToString(format));
		}

		/// <summary>
		/// Optionally writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:date /&gt;</remarks>
		[Obsolete("Use WriteAttribute without format to output valid xsd:date XML")]
		public static void WriteAttributeDateOptional(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (value != DateTime.MinValue)
				writer.WriteAttributeDate(localName, value, format);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:time /&gt;</remarks>
		public static void WriteAttributeTime(this XmlWriter writer, String localName, DateTime value)
		{
			if (writer != null)
				writer.WriteAttributeString(localName, value.ToString(XML_TIME));
		}

		/// <summary>
		/// Optionally Writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:time /&gt;</remarks>
		public static void WriteAttributeTimeOptional(this XmlWriter writer, String localName, DateTime value)
		{
			if (value != DateTime.MinValue)
				writer.WriteAttributeTime(localName, value);
		}
		
		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:time /&gt;</remarks>
		[Obsolete("Use WriteAttribute without format to output valid xsd:time XML")]
		public static void WriteAttributeTime(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (writer != null)
				writer.WriteAttributeString(localName, value.ToString(format));
		}

		/// <summary>
		/// Optionally writes a <see cref="T:System.DateTime" /> value to a XML attribute
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:time /&gt;</remarks>
		[Obsolete("Use WriteAttribute without format to output valid xsd:time XML")]
		public static void WriteAttributeTimeOptional(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (value != DateTime.MinValue)
				writer.WriteAttributeTime(localName, value, format);
		}

		/// <summary>
		/// Writes a <see cref="T:System.String" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.Int" /> value</param>
		public static void WriteElement(this XmlWriter writer, String localName, String value)
		{
			if (writer != null)
				writer.WriteElementString(localName, value);
		}

        /// <summary>
        /// Writes a <see cref="T:System.Int32" /> value to a XML element
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
        /// <param name="value"><see cref="T:System.Int" /> value</param>
        public static void WriteElement(this XmlWriter writer, String localName, int value)
        {
			if (writer != null)
				writer.WriteElement(localName, value.ToString());
        }

		/// <summary>
		/// Writes a <see cref="T:System.Double" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.Double" /> value</param>
		public static void WriteElement(this XmlWriter writer, String localName, double value)
		{
			if (writer != null)
				writer.WriteElement(localName, value.ToString());
		}

		/// <summary>
		/// Writes a <see cref="T:System.Boolean" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.Boolean" /> value</param>
		public static void WriteElement(this XmlWriter writer, String localName, bool value)
		{
			if (writer != null)
				writer.WriteElement(localName, value.ToString());
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:dateTime /&gt;</remarks>
		public static void WriteElement(this XmlWriter writer, String localName, DateTime value)
		{
			if (writer != null)
				writer.WriteElement(localName, value.ToString(XML_DATESTAMP));
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <param name="format">Date format.</param>
		/// <remarks>
		/// Xml value type is &lt;xs:dateTime /&gt;
		/// </remarks>
		[Obsolete("Use WriteElement without format to output valid xsd:dateTime XML")]
		public static void WriteElement(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (writer != null)
				writer.WriteElement(localName, value.ToString(format));
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:date /&gt;</remarks>		
		public static void WriteElementDate(this XmlWriter writer, String localName, DateTime value)
		{	
			if (writer != null)
				writer.WriteElement(localName, value.ToString(XML_DATE));
		}

		/// <summary>
		/// Optionally writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:date /&gt;</remarks>		
		public static void WriteElementDateOptional(this XmlWriter writer, String localName, DateTime value)
		{
			if (value != DateTime.MinValue)
				writer.WriteElementDate(localName, value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <param name="format">Date format.</param>
		/// <remarks>
		/// Xml value type is &lt;xs:date /&gt;
		/// </remarks>
		[Obsolete("Use WriteElement without format to output valid xsd:date XML")]
		public static void WriteElementDate(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (writer != null)
				writer.WriteElement(localName, value.ToString(format));
		}

		/// <summary>
		/// Optionally writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <param name="format">Date format.</param>
		/// <remarks>
		/// Xml value type is &lt;xs:date /&gt;
		/// </remarks>
		[Obsolete("Use WriteElement without format to output valid xsd:date XML")]
		public static void WriteElementDateOptional(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (value != DateTime.MinValue)
				writer.WriteElementDate(localName, value, format);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:time /&gt;</remarks>
		public static void WriteElementTime(this XmlWriter writer, String localName, DateTime value)
		{
			writer.WriteElement(localName, value.ToString(XML_TIME));
		}

		/// <summary>
		/// Optionally writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:time /&gt;</remarks>
		public static void WriteElementTimeOptional(this XmlWriter writer, String localName, DateTime value)
		{
			if (value != DateTime.MinValue)
				writer.WriteElementTime(localName, value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:time /&gt;</remarks>
		[Obsolete("Use WriteElement without format to output valid xsd:time XML")]
		public static void WriteElementTime(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (writer != null)
				writer.WriteElement(localName, value.ToString(format));
		}

		/// <summary>
		/// Optionally writes a <see cref="T:System.DateTime" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:System.DateTime" /> value</param>
		/// <remarks>Xml value type is &lt;xs:time /&gt;</remarks>
		[Obsolete("Use WriteElement without format to output valid xsd:time XML")]
		public static void WriteElementTimeOptional(this XmlWriter writer, String localName, DateTime value, String format)
		{
			if (value != DateTime.MinValue)
				writer.WriteElementTime(localName, value, format);
		}

		/// <summary>
		/// Writes a <see cref="T:Tridion.ContentManager.ContentManagement.Component" /> value to a XML element
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="value"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /> value</param>
		public static void WriteElement(this XmlWriter writer, String localName, Component value)
		{
			if (writer != null)
			{
				writer.WriteStartElement(localName);

				if (value != null)
				{
					writer.WriteAttributeString("xlink", "type", global::Tridion.Constants.XlinkNamespace, "simple");
					writer.WriteAttributeString("xlink", "title", global::Tridion.Constants.XlinkNamespace, value.Title);
					writer.WriteAttributeString("xlink", "href", global::Tridion.Constants.XlinkNamespace, value.Id);
				}

				writer.WriteEndElement();
			}			
		}

        /// <summary>
        /// Optionally writes a <see cref="T:System.String" /> value to a XML element
        /// Note it only creates the element when the value is not null or String.Empty
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" /></param>
        /// <param name="localName">Node Name</param>
        /// <param name="Value"><see cref="T:System.String" /> value</param>
        public static void WriteElementOptional(this XmlWriter writer, String localName, String value)
        {
            if (writer != null && !String.IsNullOrEmpty(value))
                writer.WriteElement(localName, value);
        }

		/// <summary>
		/// Writes the given XHTML to the <see cref="T:System.Xml.XmlWriter" />
		/// Ensure the XHTML string is valid XML to write.
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="XHTML">XHTML <see cref="T:System.String"/> value</param>
		public static void WriteXHTML(this XmlWriter writer, String localName, String XHTML)
		{
			if (writer != null)
			{
				writer.WriteStartElement(localName);

				if (!String.IsNullOrEmpty(XHTML))
					writer.WriteRaw(XHTML);

				writer.WriteEndElement();
			}
		}

		/// <summary>
		/// Writes the given XHTML to the <see cref="T:System.Xml.XmlWriter" />
		/// Ensure the XHTML string is valid XML to write.
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="localName">Node Name.</param>
		/// <param name="XHTML">XHTML <see cref="T:System.String"/> value</param>
		public static void WriteXHTMLOptional(this XmlWriter writer, String localName, String XHTML)
		{
			if (writer != null && !String.IsNullOrEmpty(XHTML))
				writer.WriteXHTML(localName, XHTML);
		}

        /// <summary>
        /// Writes the given processed time in seconds as an <see cref="T:System.Xml.XmlComment" /> .
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" />.</param>
        /// <param name="ProcessedTime">Template processing time in milliseconds.</param>
        public static void WriteProcessingTime(this XmlWriter writer, long ProcessedTime)
        {
			if (writer != null)
				writer.WriteComment(String.Format(" Render time {0:0.00000} seconds @ {1:dd/MM/yyyy hh:mm:ss tt} ", ProcessedTime / 1000.0, DateTime.Now));
        }
    }
}
