#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: XsltExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TcmTemplating.Extensions;
using Tridion.ContentManager;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.Templating;

namespace TcmTemplating.Helpers
{
	/// <summary>
	/// <see cref="XsltExtensions" /> provides common Tridion functions for XSLT transformations
	/// </summary>
	public class XsltExtensions
	{
		private Engine mEngine;
		private Package mPackage;
		private TemplateBase mTemplateBase;
		private TemplatingLogger mTemplatingLogger;

		/// <summary>
		/// Gets the <see cref="Tridion.ContentManager.Templating.Engine" />.
		/// </summary>
		/// <value>
		/// <see cref="Tridion.ContentManager.Templating.Engine" />
		/// </value>
		protected Engine Engine
		{
			get
			{
				return mEngine;
			}
		}

		/// <summary>
		/// Gets the <see cref="Tridion.ContentManager.Templating.Package" />.
		/// </summary>
		/// <value>
		/// <see cref="Tridion.ContentManager.Templating.Package" />
		/// </value>
		protected Package Package
		{
			get
			{
				return mPackage;
			}
		}

		/// <summary>
		/// Gets the <see cref="T:TcmTemplating.TemplateBase" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmTemplating.TemplateBase" />
		/// </value>
		protected TemplateBase TemplateBase
		{
			get
			{
				return mTemplateBase;
			}
		}

		/// <summary>
		/// Gets the <see cref="Tridion.ContentManager.Templating.TemplatingLogger" />.
		/// </summary>
		/// <value>
		/// <see cref="Tridion.ContentManager.Templating.TemplatingLogger" />
		/// </value>
		protected TemplatingLogger Logger
		{
			get
			{
				return mTemplatingLogger;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XsltExtensions"/> class.
		/// </summary>
		/// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine" /></param>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="templateBase"><see cref="T:TcmTemplating.TemplateBase" /></param>
		public XsltExtensions(Engine engine, Package package, TemplateBase templateBase)
		{
			mTemplatingLogger = TemplatingLogger.GetLogger(this.GetType());
			mEngine = engine;
			mPackage = package;
			mTemplateBase = templateBase;
		}

		#region Logging
		/// <summary>
		/// Logs a debug message
		/// </summary>
		/// <param name="message">Message</param>
		public void Debug(String message)
		{
			mTemplatingLogger.Debug(message);
		}

		/// <summary>
		/// Logs a debug message
		/// </summary>
		/// <param name="message">Message</param>
		/// /// <param name="param1">Parameter 1</param>
		public void Debug(String message, Object param1)
		{
			mTemplatingLogger.Debug(message, param1);
		}

		/// <summary>
		/// Logs a debug message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		public void Debug(String message, Object param1, Object param2)
		{
			mTemplatingLogger.Debug(message, param1, param2);
		}

		/// <summary>
		/// Logs a debug message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		/// <param name="param3">Parameter 3</param>
		public void Debug(String message, Object param1, Object param2, Object param3)
		{
			mTemplatingLogger.Debug(message, param1, param2, param3);
		}

		/// <summary>
		/// Logs a debug message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		/// <param name="param3">Parameter 3</param>
		/// <param name="param4">Parameter 4</param>
		public void Debug(String message, Object param1, Object param2, Object param3, Object param4)
		{
			mTemplatingLogger.Debug(message, param1, param2, param3, param4);
		}

		/// <summary>
		/// Logs a Info message
		/// </summary>
		/// <param name="message">Message</param>
		public void Info(String message)
		{
			mTemplatingLogger.Info(message);
		}

		/// <summary>
		/// Logs a Info message
		/// </summary>
		/// <param name="message">Message</param>
		/// /// <param name="param1">Parameter 1</param>
		public void Info(String message, Object param1)
		{
			mTemplatingLogger.Info(message, param1);
		}

		/// <summary>
		/// Logs a Info message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		public void Info(String message, Object param1, Object param2)
		{
			mTemplatingLogger.Info(message, param1, param2);
		}

		/// <summary>
		/// Logs a Info message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		/// <param name="param3">Parameter 3</param>
		public void Info(String message, Object param1, Object param2, Object param3)
		{
			mTemplatingLogger.Info(message, param1, param2, param3);
		}

		/// <summary>
		/// Logs a Info message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		/// <param name="param3">Parameter 3</param>
		/// <param name="param4">Parameter 4</param>
		public void Info(String message, Object param1, Object param2, Object param3, Object param4)
		{
			mTemplatingLogger.Info(message, param1, param2, param3, param4);
		}

		/// <summary>
		/// Logs a Warning message
		/// </summary>
		/// <param name="message">Message</param>
		public void Warning(String message)
		{
			mTemplatingLogger.Warning(message);
		}

		/// <summary>
		/// Logs a Warning message
		/// </summary>
		/// <param name="message">Message</param>
		/// /// <param name="param1">Parameter 1</param>
		public void Warning(String message, Object param1)
		{
			mTemplatingLogger.Warning(message, param1);
		}

		/// <summary>
		/// Logs a Warning message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		public void Warning(String message, Object param1, Object param2)
		{
			mTemplatingLogger.Warning(message, param1, param2);
		}

		/// <summary>
		/// Logs a Warning message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		/// <param name="param3">Parameter 3</param>
		public void Warning(String message, Object param1, Object param2, Object param3)
		{
			mTemplatingLogger.Warning(message, param1, param2, param3);
		}

		/// <summary>
		/// Logs a Warning message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		/// <param name="param3">Parameter 3</param>
		/// <param name="param4">Parameter 4</param>
		public void Warning(String message, Object param1, Object param2, Object param3, Object param4)
		{
			mTemplatingLogger.Warning(message, param1, param2, param3, param4);
		}

		/// <summary>
		/// Logs a Error message
		/// </summary>
		/// <param name="message">Message</param>
		public void Error(String message)
		{
			mTemplatingLogger.Error(message);
		}

		/// <summary>
		/// Logs a Error message
		/// </summary>
		/// <param name="message">Message</param>
		/// /// <param name="param1">Parameter 1</param>
		public void Error(String message, Object param1)
		{
			mTemplatingLogger.Error(message, param1);
		}

		/// <summary>
		/// Logs a Error message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		public void Error(String message, Object param1, Object param2)
		{
			mTemplatingLogger.Error(message, param1, param2);
		}

		/// <summary>
		/// Logs a Error message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		/// <param name="param3">Parameter 3</param>
		public void Error(String message, Object param1, Object param2, Object param3)
		{
			mTemplatingLogger.Error(message, param1, param2, param3);
		}

		/// <summary>
		/// Logs a Error message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		/// <param name="param3">Parameter 3</param>
		/// <param name="param4">Parameter 4</param>
		public void Error(String message, Object param1, Object param2, Object param3, Object param4)
		{
			mTemplatingLogger.Error(message, param1, param2, param3, param4);
		}
		#endregion

		/// <summary>
		/// Determines whether the <see cref="T:Tridion.ContentManager.IdentifiableObject"/> is a "navigable" item.
		/// </summary>
		/// <param name="title"><see cref="T:Tridion.ContentManager.IdentifiableObject" /> Title</param>
		/// <returns><c>True</c> if the item is a "navigable" item.</returns>
		/// <remarks>Navigable items have to match the following rules:
		/// 1. Start with a 1 to 5 digit number
		/// 2. Optionally have a dot "."
		/// 3. Optionally have a space " "
		/// </remarks>
		public Boolean IsNavigableItem(String title)
		{
			if (!String.IsNullOrEmpty(title))
				return IdentifiableObjectExtensions.mNavigable.IsMatch(title);

			return false;
		}

		/// <summary>
		/// Extracts the number from a navigable item (if available).
		/// </summary>
		/// <param name="title"><see cref="T:Tridion.ContentManager.IdentifiableObject" /> Title</param>
		/// <returns>Navigable number if available, otherwise -1.</returns>
		/// <remarks>Navigable items have to match the following rules:
		/// 1. Start with a 1 to 5 digit number
		/// 2. Optionally have a dot "."
		/// 3. Optionally have a space " "
		/// </remarks>
		public int NavigableNumber(String title)
		{
			if (!String.IsNullOrEmpty(title))
			{
				Match match = IdentifiableObjectExtensions.mNavigable.Match(title);

				if (match.Success && match.Groups.Count > 0)
				{
					int value;

					if (int.TryParse(match.Groups["number"].Value, out value))
						return value;
				}
			}

			return -1;
		}

		/// <summary>
		/// Extracts the title from a navigable item
		/// </summary>
		/// <param name="title"><see cref="T:Tridion.ContentManager.IdentifiableObject" /> Title</param>
		/// <returns>Navigable title if available, otherwise standard title.</returns>
		/// <remarks>Navigable items have to match the following rules:
		/// 1. Start with a 1 to 5 digit number
		/// 2. Optionally have a dot "."
		/// 3. Optionally have a space " "
		/// </remarks>	
		public String NavigableTitle(String title)
		{
			if (!String.IsNullOrEmpty(title))
			{
				Match match = IdentifiableObjectExtensions.mNavigable.Match(title);

				if (match.Success)
					return title.Substring(match.Length);

				return title;
			}

			return String.Empty;
		}

		/// <summary>
		/// Resolves the XHTML.
		/// </summary>
		/// <param name="formatTextResolver"><see cref="T:TcmTemplating.Helpers.FormatTextResolver" /></param>
		/// <param name="xhtml">Tridion XHTML.</param>
		/// <returns><see cref="T:System.Xml.XPath.XPathNodeIterator" /></returns>
		protected XPathNodeIterator ResolveXHTML(FormatTextResolver formatTextResolver, XPathNodeIterator xhtml)
		{
			using (StringWriterEncoding sw = new StringWriterEncoding())
			{
				using (XmlWriter xw = XmlWriter.Create(sw, TemplateBase.TemplateXmlWriterSettings))
				{
					xw.WriteStartElement("root");

					foreach (XPathNavigator node in xhtml)
						xw.WriteRaw(node.OuterXml);

					xw.WriteEndElement();
				}

				String xml = formatTextResolver.Resolve(sw.ToString());

				XElement parsedXml = XElement.Parse(xml);

				return parsedXml.CreateNavigator().Select("node()");
			}
		}

		/// <summary>
		/// Resolves the XHTML.
		/// </summary>		
		/// <param name="xhtml">Tridion XHTML.</param>
		/// <returns><see cref="T:System.Xml.XPath.XPathNodeIterator" /></returns>
		public virtual XPathNodeIterator ResolveXHTML(XPathNodeIterator xhtml)
		{
			return ResolveXHTML(new FormatTextResolver(mTemplateBase), xhtml);
		}

		/// <summary>
		/// Resolve link
		/// </summary>
		/// <param name="mComponent">The component <see cref="T:Tridion.ContentManager.TcmUri" /></param>
		/// <returns>Resolved URL otherwise <c>String.Empty</c></returns>
		public virtual String ResolveLink(String ComponentUri)
		{
			return String.Empty;
		}

		/// <summary>
		/// Publishes the specified tcm uri as a binary component
		/// </summary>
		/// <param name="uri">TCM URI.</param>
		/// <returns>Published Url or String.Empty</returns>
		public String PublishBinary(String uri)
		{
			IdentifiableObject identifiableObject = mEngine.GetObject(uri);

			if (identifiableObject is Component)
				return mTemplateBase.PublishBinary(identifiableObject as Component);

			return String.Empty;
		}

		/// <summary>
		/// Generates a thumbnail for the given tcm uri binary component
		/// </summary>
		/// <param name="uri">TCM URI</param>
		/// <param name="width">Thumbnail width</param>
		/// <param name="height">Thumbnail height</param>
		/// <returns></returns>
		public String GenerateThumbnail(String uri, int width, int height)
		{
			IdentifiableObject identifiableObject = mEngine.GetObject(uri);

			if (identifiableObject is Component)
				return mTemplateBase.GenerateThumbnail(identifiableObject as Component, width, height);

			return String.Empty;
		}
	}
}
