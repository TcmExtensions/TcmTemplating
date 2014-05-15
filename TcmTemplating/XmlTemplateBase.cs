#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: XmlTemplateBase
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.IO;
using System.Text;
using System.Xml;
using TcmTemplating.Extensions;
using TcmTemplating.Helpers;
using TcmTemplating.Templates;

namespace TcmTemplating
{
	/// <summary>
	/// <see cref="XmlTemplateBase" /> extends <see cref="TemplateBase" /> in order to add functions for rendering in the context of
	/// a <see cref="T:TcmTemplating.Templates.NativeXmlPage" />
	/// </summary>
	public abstract class XmlTemplateBase : TemplateBase
	{
		private StringWriter mStringWriter;
		private XmlWriter mXmlWriter;
		private String mRootElementName = null;
		private Boolean mIsXmlFragment = false;

		/// <summary>
		/// Allow internal templates to provide a PreTransform hook
		/// </summary>
		internal override void PreTransform()
		{
			// We are rendering in a ComponentTemplate context without a Page present
			if (Page == null)
			{
				mStringWriter = new StringWriterEncoding(Encoding.UTF8);
				mXmlWriter = XmlTextWriter.Create(mStringWriter, TemplateXmlWriterSettings);
			}
		}

		/// <summary>
		/// Allow internal templates to provide a PostTransform hook
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		internal override void PostTransform()
		{
			if (Page == null)
			{
				mXmlWriter.WriteProcessingTime(ProcessedTime);
				mXmlWriter.Close();

				// No output since we are rendering inside XmlTemplateBase
				Package.AddXml(Tridion.ContentManager.Templating.Package.OutputName, mStringWriter.ToString());

				mStringWriter.Close();
				mStringWriter.Dispose();
			}
			else
			{
				// No output since we are rendering inside XmlTemplateBase on a NativeXmlPage
				Package.AddString(Tridion.ContentManager.Templating.Package.OutputName, String.Empty);
			}
		}

		/// <summary>
		/// Gets the <see cref="T:System.Xml.XmlWriter" /> for this <see cref="T:TcmTemplating.Templates.NativeXmlPage" />
		/// </summary>
		/// <value>
		/// <see cref="T:System.Xml.XmlWriter" />
		/// </value>
		protected XmlWriter Xml
		{
			get
			{
				if (mXmlWriter == null)
				{
					mXmlWriter = Engine.PublishingContext.RenderContext.ContextVariables[NativeXMLPage.NATIVE_XML_WRITER] as XmlWriter;

					if (mXmlWriter == null)
						throw new ArgumentNullException("XmlWriter is null, ensure that this ComponentPresentation is rendering in a NativeXMLPage context.");
				}

				return mXmlWriter;
			}
		}

		/// <summary>
		/// Writes the start root element of the current <see cref="XmlTemplateBase" />
		/// </summary>
		/// <param name="elementName">Name of the element.</param>
		protected void WriteStartRootElement(String elementName)
		{
			mRootElementName = elementName;

			if (Component.IsFirstComponent(Page))
				Xml.WriteStartElement(elementName);
			else
				// The document output is an XmlFragment
				// We assume the start root tag has already been written
				mIsXmlFragment = true;
		}

		/// <summary>
		/// Write the root element closing tag of the current <see cref="XmlTemplateBase" />
		/// </summary>
		protected void WriteEndRootElement()
		{
			if (mIsXmlFragment)
			{
				// Since we are a fragment, only close the page when we are the last component presentation
				// on this page.
				if (Component.IsLastComponent(Page))
					Xml.WriteRaw(String.Format("</{0}>", mRootElementName));
			}
			else
				Xml.WriteEndElement();
		}		
	}
}
