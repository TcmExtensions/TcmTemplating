#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: String Extensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using TcmTemplating.Helpers;

namespace TcmTemplating.Extensions
{
	/// <summary>
	/// <see cref="StringExtensions" /> provides extensions for <see cref="T:System.String" />
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Determines whether a <see cref="T:System.String" /> contains <paramref name="containsValue" /> using a <see cref="T:System.StringComparison" />
		/// </summary>
		/// <param name="source"><see cref="T:System.String" /> value</param>
		/// <param name="value"><see cref="T:System.String" > value to look for</param>
		/// <param name="comparer"><see cref="T:System.StringComparison" /></param>
		/// <returns><c>true</c> if the value is found; otherwise <c>false</c></returns>
		public static Boolean Contains(this String source, String value, StringComparison comparison)
		{
			return source.IndexOf(value, comparison) >= 0;
		}

		/// <summary>
		/// Convert a <see cref="T:System.String" /> to a number (ignoring any non-numeric characters).
		/// </summary>
		/// <param name="value"><see cref="T:System.String"/> </param>
		/// <returns><see cref="T:System.Double" /> or NaN</returns>
		public static Double ToNumber(this String value)
		{
			Regex regex = new Regex(@"[^0-9\-\.]");

			if (!String.IsNullOrEmpty(value))
			{
				// Remove any non-accepted characters
				value = regex.Replace(value, String.Empty);
				double result;

				if (double.TryParse(value, out result))
					return result;
			}

			return Double.NaN;
		}

		/// <summary>
		/// Converts a <see cref="T:System.String" /> to propercase / titlecase.
		/// </summary>
		/// <param name="value"><see cref="T:System.String" /></param>
		/// <returns><see cref="T:System.String" /> in proper case.</returns>
		public static String ToProperCase(this String value)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value);
		}

		/// <summary>
		/// Removes any XML namespaces from a <see cref="T:System.String" /> representing an XML fragment
		/// </summary>
		/// <param name="value"><see cref="T:System.String" /> representing an XML fragment</param>
		/// <returns><see cref="T:System.String" /> with namespaces removed</returns>
		public static String RemoveNameSpaces(this String value)
		{
			if (!String.IsNullOrEmpty(value))
			{
				using (StringWriter sw = new StringWriter())
				{
					using (XmlNoNamespaceWriter xw = new XmlNoNamespaceWriter(sw, new XmlWriterSettings()
					{
						ConformanceLevel = ConformanceLevel.Fragment,
						OmitXmlDeclaration = true,
						Indent = false
					}))
					{
						using (StringReader sr = new StringReader(value))
						{
							using (XmlReader xr = XmlReader.Create(sr, new XmlReaderSettings()
							{
								ConformanceLevel = ConformanceLevel.Fragment,
								ValidationType = ValidationType.None
							}))
							{
								while (xr.Read())
								{
									while (xr.NodeType == XmlNodeType.Element ||
										xr.NodeType == XmlNodeType.Comment ||
										xr.NodeType == XmlNodeType.Whitespace ||
										xr.NodeType == XmlNodeType.SignificantWhitespace ||
										xr.NodeType == XmlNodeType.ProcessingInstruction ||
										xr.NodeType == XmlNodeType.DocumentType)
										xw.WriteNode(xr, true);
								}
							}
						}
					}

					return sw.ToString();
				}
			}

			return String.Empty;
		}
	}
}
