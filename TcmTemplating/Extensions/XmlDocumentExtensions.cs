#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: XmlDocumentExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.IO;
using System.Xml;
using TcmTemplating.Helpers;

namespace TcmTemplating.Extensions
{
	/// <summary>
	/// <see cref="XmlDocumentExtensions"/> supplies .NET extension functions for <see cref="T:System.Xml.XmlDocument" />
	/// </summary>
	public static class XmlDocumentExtensions
	{
		/// <summary>
		/// Strip all namespaces from an existing <see cref="T:System.Xml.XmlDocument"/>
		/// </summary>
		/// <param name="document"><see cref="T:System.Xml.XmlDocument"/></param>
		/// <returns>XML as <see cref="T:System.String"/> with namespaces removed.</returns>
		public static String RemoveNameSpaces(this XmlDocument document)
		{
			if (document != null)
			{
				using (StringWriter sw = new StringWriter())
				{
					using (XmlNoNamespaceWriter xw = new XmlNoNamespaceWriter(sw, new XmlWriterSettings()
					{
						ConformanceLevel = ConformanceLevel.Auto,
						OmitXmlDeclaration = true,
						Indent = false
					}))
					{
						document.Save(xw);
					}

					return sw.ToString();
				}
			}

			return String.Empty;
		}
	}
}
