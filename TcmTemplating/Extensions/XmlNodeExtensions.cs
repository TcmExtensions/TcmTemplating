#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: XmlNodeExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
// 	Change History
//	Date Modified       : 
//	Changed By          : 
//	Change Description  : 
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Security;
using System.Xml;
using Tridion.ContentManager.ContentManagement;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="XmlNodeExtensions"/> supplies .NET extension functions for <see cref="T:System.Xml.XmlNode" />
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Directly get the value of a <see cref="T:System.Xml.XmlNode" /> attribute
        /// </summary>
        /// <param name="node"><see cref="T:System.Xml.XmlNode" /></param>
        /// <param name="name">Attribute name</param>
        /// <returns>Attribute value or String.Empty</returns>
        public static String GetAttribute(this XmlNode node, String name)
        {
            if (node != null && !String.IsNullOrEmpty(name))
            {
                XmlAttribute attribute = node.Attributes[name];

                if (attribute != null)
                    return attribute.Value;
            }

            return String.Empty;
        }

        /// <summary>
        /// Adds a text node to a <see cref="T:System.Xml.XmlNode" />
        /// </summary>
        /// <param name="node"><see cref="T:System.Xml.XmlNode" /></param>
        /// <param name="name">Text Node Name</param>
        /// <param name="value">Text Node Value.</param>
        /// <returns><see cref="T:System.Xml.XmlElement"/> text node</returns>
        public static XmlElement AddTextNode(this XmlNode node, String name, String value)
        {
            if (node != null && !String.IsNullOrEmpty(name) && node.OwnerDocument != null && !String.IsNullOrEmpty(value))
            {
                XmlElement xElement = node.OwnerDocument.CreateElement(name);
                xElement.InnerText = value;
                node.AppendChild(xElement);

                return xElement;
            }

            return null;
        }

        /// <summary>
        /// Adds a new XML node to a <see cref="T:System.Xml.XmlNode" />
        /// </summary>
        /// <param name="node"><see cref="T:System.Xml.XmlNode" /></param>
        /// <param name="name">xml Node Name</param>
        /// <param name="value">xml Node Value.</param>
        /// <returns><see cref="T:System.Xml.XmlElement"/> xml node</returns>
        public static XmlElement AddXmlNode(this XmlNode node, String name, String value)
        {
            if (node != null && !String.IsNullOrEmpty(name) && node.OwnerDocument != null)
            {
                XmlElement xElement = node.OwnerDocument.CreateElement(name);
                xElement.InnerXml = value;
                node.AppendChild(xElement);

                return xElement;
            }

            return null;
        }

        /// <summary>
        /// Adds a new keyord xml node to a <see cref="T:System.Xml.XmlNode" />
        /// </summary>
        /// <param name="node"><see cref="T:System.Xml.XmlNode" /></param>
        /// <param name="name">xml Node Name</param>
        /// <param name="value"><see cref="T:Tridion.ContentManager.ContentManagement.Keyword"/></param>
        /// <returns><see cref="T:System.Xml.XmlElement"/> keyword node</returns>
        public static XmlElement AddKeywordNode(this XmlNode node, String name, Keyword value)
        {
			return node.AddKeywordNode(name, value, false);
        }

        /// <summary>
        /// Adds a new keyword xml node to a <see cref="T:System.Xml.XmlNode" />
        /// </summary>
        /// <param name="node"><see cref="T:System.Xml.XmlNode" /></param>
        /// <param name="name">xml Node Name</param>
        /// <param name="value"><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /></param>
        /// <param name="includeLevel">if set to <c>true</c> [include level].</param>
        /// <returns>
        ///   <see cref="T:System.Xml.XmlElement" /> keyword node
        /// </returns>
        public static XmlElement AddKeywordNode(this XmlNode node, String name, Keyword value, bool includeLevel)
        {
            if (node != null && !String.IsNullOrEmpty(name) && node.OwnerDocument != null && value != null)
            {
                XmlElement xElement = node.OwnerDocument.CreateElement(name);
                xElement.SetAttribute("uri", value.Id);

                if (!String.IsNullOrEmpty(value.Key))
                    xElement.SetAttribute("key", value.Key);

                if (!String.IsNullOrEmpty(value.Description))
                    xElement.SetAttribute("description", value.Description);

                xElement.SetAttribute("root", value.IsRoot.ToString().ToLower());

                if (includeLevel)
                    xElement.SetAttribute("level", value.Level().ToString());

                xElement.InnerXml = SecurityElement.Escape(value.Title);

                node.AppendChild(xElement);
                return xElement;
            }

            return null;
        }

        /// <summary>
        /// Adds text nodes to a <see cref="T:System.Xml.XmlNode" />
        /// </summary>
        /// <param name=nNode"><see cref="T:System.Xml.XmlNode" /></param>
        /// <param name="name">Text Node Name</param>
        /// <param name="value">Text Node Values</param>
        public static void AddTextNodes(this XmlNode node, String name, IList<String> values)
        {
            if (node != null && !String.IsNullOrEmpty(name) && node.OwnerDocument != null && values != null && values.Count > 0)
            {
                foreach (String value in values)
                {
                    node.AddTextNode(name, value);
                }
            }
        }

        /// <summary>
        /// Adds xml nodes to a <see cref="T:System.Xml.XmlNode" />
        /// </summary>
        /// <param name="node"><see cref="T:System.Xml.XmlNode" /></param>
        /// <param name="name">Xml Node Name</param>
        /// <param name="value">Xml Node Values</param>
        public static void AddXmlNodes(this XmlNode node, String name, IList<String> values)
        {
            if (node != null && !String.IsNullOrEmpty(name) && node.OwnerDocument != null && values != null && values.Count > 0)
            {
                foreach (String value in values)
                {
                    node.AddXmlNode(name, value);
                }
            }
        }

        /// <summary>
        /// Adds keyword nodes to a <see cref="T:System.Xml.XmlNode" />
        /// </summary>
        /// <param name="node"><see cref="T:System.Xml.XmlNode" /></param>
        /// <param name="name">Keyword Node Name</param>
        /// <param name="value">Keyword Node Values</param>
        public static void AddKeywordNodes(this XmlNode node, String name, IList<Keyword> values)
        {
            node.AddKeywordNodes(name, values, false);
        }

        /// <summary>
        /// Adds keyword nodes to a <see cref="T:System.Xml.XmlNode" />
        /// </summary>
        /// <param name="node"><see cref="T:System.Xml.XmlNode" /></param>
        /// <param name="name">Keyword Node Name</param>
        /// <param name="values">Keyword Node Values.</param>
        /// <param name="includeLevel">if set to <c>true</c> [include level].</param>
        public static void AddKeywordNodes(this XmlNode node, String name, IList<Keyword> values, bool includeLevel)
        {
            if (node != null && !String.IsNullOrEmpty(name) && node.OwnerDocument != null && values != null && values.Count > 0)
            {
                foreach (Keyword value in values)
                {
                    node.AddKeywordNode(name, value, includeLevel);
                }
            }
        }
    }
}
