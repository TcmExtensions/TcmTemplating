#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: TransformXml
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Venkata Siva Charan Sandra
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.Templating;

namespace TcmTemplating.Helpers
{
	/// <summary>
	/// <see cref="TransformXml" /> allows easy transformation of Tridion content using Xslt stylesheets
	/// </summary>
	public class TransformXml
	{
		private Engine mEngine;
		private XslCompiledTransform mXslCompiledTransform;
		private XsltArgumentList mXsltArgumentList;

		/// <summary>
		/// Gets the <see cref="T:System.Xml.Xsl.XsltSettings" />
		/// </summary>
		/// <value>
		/// <see cref="T:System.Xml.Xsl.XsltSettings" />
		/// </value>
		protected virtual XsltSettings XsltSettings
		{
			get
			{
				return XsltSettings.TrustedXslt;
			}
		}

		/// <summary>
		/// Gets the <see cref="T:System.Xml.Xsl.XsltArgumentList" />
		/// </summary>
		/// <value>
		/// <see cref="T:System.Xml.Xsl.XsltArgumentList" />
		/// </value>
		public XsltArgumentList XsltArgumentList
		{
			get
			{
				return mXsltArgumentList;
			}
		}

		/// <summary>
		/// Constructs the <see cref="T:TcmTemplating.Helper.XsltExtensions" /> for this transformation
		/// </summary>
		/// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine" /></param>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="templateBase"><see cref="T:TcmTemplating.TemplateBase" /></param>
		/// <returns>Initialized <see cref="T:TcmTemplating.Helpers.XsltExtensions" /></returns>
		protected virtual object XsltExtensions(Engine engine, Package package, TemplateBase templateBase)
		{
			return new XsltExtensions(engine, package, templateBase);
		}

		/// <summary>
		/// Adds a XSLT parameter to the transformation using <see cref="T:Tridion.ContentManager.Templating.Item" />
		/// </summary>
		/// <param name="parameter">Parameter name</param>
		/// <param name="packageItem"><see cref="T:Tridion.ContentManager.Templating.Item" /></param>
		/// <exception cref="System.Exception">Unsupported Itemtype</exception>
		public void AddXsltParameter(String parameter, Item packageItem)
		{
			if (!String.IsNullOrEmpty(parameter) && packageItem != null)
			{
				switch (packageItem.ContentType.ToString())
				{
					case "tridion/component":
					case "tridion/page":
					case "tridion/publication":
					case "tridion/schema":
					case "tridion/template":
						mXsltArgumentList.RemoveParam(parameter, String.Empty);
						mXsltArgumentList.AddParam(parameter, String.Empty, packageItem.GetAsXmlDocument());
						break;
					case "text/date":
					case "tridion/externallink":
					case "text/html":
					case "text/number":
					case "text/plain":
						AddXsltParameter(parameter, packageItem.GetAsString());
						break;
					case "text/xhtml":
					case "text/xml":
						mXsltArgumentList.RemoveParam(parameter, String.Empty);
						mXsltArgumentList.AddParam(parameter, String.Empty, packageItem.GetAsXmlDocument());
						break;
					default:
						throw new Exception(String.Format("Unsupported itemtype {0}", packageItem.ContentType.ToString()));
				}
			}
		}

		/// <summary>
		/// Adds a XSLT parameter to the transformation.
		/// </summary>
		/// <param name="parameter">Parameter name</param>
		/// <param name="value">Parameter value</param>
		public void AddXsltParameter(String parameter, Object value)
		{
			// Do not add null values
			if (String.IsNullOrEmpty(parameter) || value == null)
				return;

			// Always overwrite existing parameters
			mXsltArgumentList.RemoveParam(parameter, String.Empty);

			bool bAdd = false;

			switch (Type.GetTypeCode(value.GetType()))
			{
				case TypeCode.Empty:
					bAdd = false;
					break;
				case TypeCode.String:
					String stringValue = value as String;

					if (!String.IsNullOrEmpty(stringValue))
						mXsltArgumentList.AddParam(parameter, String.Empty, stringValue.Replace("'", "&apos;"));
					break;
				case TypeCode.Int16:
					bAdd = ((Int16)value) != 0;
					break;
				case TypeCode.Int32:
					bAdd = ((Int32)value) != 0;
					break;
				case TypeCode.Int64:
					bAdd = ((Int64)value) != 0;
					break;
				case TypeCode.Double:
					bAdd = ((Double)value) != 0;
					break;
				case TypeCode.Single:
					bAdd = ((Single)value) != 0;
					break;
				case TypeCode.UInt16:
					bAdd = ((UInt16)value) != 0;
					break;
				case TypeCode.UInt32:
					bAdd = ((UInt32)value) != 0;
					break;
				case TypeCode.UInt64:
					bAdd = ((UInt64)value) != 0;
					break;
				case TypeCode.Decimal:
					bAdd = ((Decimal)value) != 0;
					break;
				case TypeCode.Byte:
					bAdd = ((Byte)value) != 0;
					break;
				case TypeCode.SByte:
					bAdd = ((SByte)value) != 0;
					break;
				default:
					bAdd = true;
					break;
			}

			if (bAdd)
				mXsltArgumentList.AddParam(parameter, String.Empty, value);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TransformXml"/> class.
		/// </summary>
		/// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine" /></param>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="templateBase"><see cref="T:TcmTemplating.TemplateBase" /></param>
		private TransformXml(Engine engine, Package package, TemplateBase templateBase)
		{
			mEngine = engine;
			mXslCompiledTransform = new XslCompiledTransform();

			mXsltArgumentList = new XsltArgumentList();
			mXsltArgumentList.AddExtensionObject("urn:XSLTExtensions", XsltExtensions(engine, package, templateBase));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TransformXml"/> class.
		/// </summary>
		/// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine" /></param>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="templateBase"><see cref="T:TcmTemplating.TemplateBase" /></param>
		/// <param name="styleSheet">styleSheet resource full namespace</param>
		public TransformXml(Engine engine, Package package, TemplateBase templateBase, String styleSheet)
			: this(engine, package, templateBase)
		{
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(styleSheet))
			{
				using (XmlReader reader = XmlReader.Create(stream))
				{
					mXslCompiledTransform.Load(reader, XsltSettings, new XmlTcmUriResolver(engine));
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TransformXml"/> class.
		/// </summary>
		/// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine" /></param>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="templateBase"><see cref="T:TcmTemplating.TemplateBase" /></param>
		/// <param name="templateBuildingBlock">Tridion template building block to use as source</param>
		public TransformXml(Engine engine, Package package, TemplateBase templateBase, TcmUri templateBuildingBlockUri): this(engine, package, templateBase)
		{
			if (templateBuildingBlockUri.ItemType != ItemType.TemplateBuildingBlock)
				throw new ArgumentException("templateBuildingBlock is not ItemType.TemplateBuildingBlock");

			TemplateBuildingBlock templateBuildingBlock = engine.GetObject(templateBuildingBlockUri) as TemplateBuildingBlock;

			String content = templateBuildingBlock.Content.Replace("tcm:include", "xsl:include");

			using (StringReader sr = new StringReader(content))
			{
				using (XmlReader reader = XmlReader.Create(sr))
				{
					mXslCompiledTransform.Load(reader, XsltSettings, new XmlTcmUriResolver(engine));
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TransformXml"/> class.
		/// </summary>
		/// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine" /></param>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="templateBase"><see cref="T:TcmTemplating.TemplateBase" /></param>
		/// <param name="templateBuildingBlock">Tridion template building block to use as source</param>
		public TransformXml(Engine engine, Package package, TemplateBase templateBase, Uri templateBuildingBlockUri): this(engine, package, templateBase)
		{
			TemplateBuildingBlock templateBuildingBlock = engine.GetObject(templateBuildingBlockUri.ToString()) as TemplateBuildingBlock;

			if (templateBuildingBlock == null)
				throw new ArgumentException("templateBuildingBlock is not ItemType.TemplateBuildingBlock");

			String content = templateBuildingBlock.Content.Replace("tcm:include", "xsl:include");

			using (StringReader sr = new StringReader(content))
			{
				using (XmlReader reader = XmlReader.Create(sr))
				{
					mXslCompiledTransform.Load(reader, XsltSettings, new XmlTcmUriResolver(engine));
				}
			}
		}

		/// <summary>
		/// Executes a XSLT transformation for the <see cref="T:Tridion.ContentManager.Templating.Item" />
		/// </summary>
		/// <param name="xpathNavigable"><see cref="I:System.Xml.XPath.IXPathNavigable" /></param>
		/// <param name="removeNameSpaces">if set to <c>true</c> [remove name spaces].</param>
		/// <returns>
		/// Transformed XSLT result
		/// </returns>
		public String Execute(IXPathNavigable xpathNavigable, bool removeNameSpaces = true)
		{
			using (StringWriterEncoding sw = new StringWriterEncoding())
			{
				using (XmlWriter xw = removeNameSpaces ? 
					new XmlNoNamespaceWriter(sw, new XmlWriterSettings()
					{
						Indent = false,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Fragment
					}) :
					XmlWriter.Create(sw, new XmlWriterSettings()
					{
						Indent = false,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Fragment
					})
					)
				{
					mXslCompiledTransform.Transform(xpathNavigable, XsltArgumentList, xw, new XmlTcmUriResolver(mEngine));
				}

				return sw.ToString();
			}
		}

		/// <summary>
		/// Executes a XSLT transformation for the <see cref="T:Tridion.ContentManager.Templating.Item" />
		/// </summary>
		/// <param name="packageItem"><see cref="T:Tridion.ContentManager.Templating.Item" /></param>
		/// <param name="removeNameSpaces">if set to <c>true</c> [remove name spaces].</param>
		/// <returns>Transformed XSLT result</returns>
		public String Execute(Item packageItem, bool removeNameSpaces = true)
		{
			return Execute(packageItem.GetAsXmlDocument());
		}

		/// <summary>
		/// Executes a XSLT transformation for the given xml
		/// </summary>
		/// <param name="xml">XML to transform</param>
		/// <param name="removeNameSpaces">if set to <c>true</c> [remove name spaces].</param>
		/// <returns>Transformed XSLT result</returns>
		public String Execute(String xml, bool removeNameSpaces = true)
		{
			using (StringWriterEncoding sw = new StringWriterEncoding())
			{
				using (XmlWriter xw = removeNameSpaces ?
					new XmlNoNamespaceWriter(sw, new XmlWriterSettings()
					{
						Indent = false,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Fragment
					}) :
					XmlWriter.Create(sw, new XmlWriterSettings()
					{
						Indent = false,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Fragment
					})
					)
				{
					using (StringReader sr = new StringReader(xml))
					{
						using (XmlReader xr = XmlReader.Create(sr))
						{
							mXslCompiledTransform.Transform(xr, XsltArgumentList, xw, new XmlTcmUriResolver(mEngine));
						}
					}
				}

				return sw.ToString();
			}
		}

        /// <summary>
        /// Executes a XSLT transformation for the given xml
        /// </summary>
        /// <param name="xml">XML to transform</param>
        /// <param name="xmlWriter"><see cref="T:System.Xml.XmlWriter" /> to output to.</param>
        public void Execute(String xml, XmlWriter xmlWriter)
        {
            using (StringReader sr = new StringReader(xml))
            {
                using (XmlReader xr = XmlReader.Create(sr))
                {
                    mXslCompiledTransform.Transform(xr, XsltArgumentList, xmlWriter, new XmlTcmUriResolver(mEngine));
                }
            }
        }

	}
}
