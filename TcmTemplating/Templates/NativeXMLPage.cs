#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: NativeXMLPage
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Text;
using System.Xml;
using TcmTemplating.Extensions;
using TcmTemplating.Helpers;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating.Templates
{
	/// <summary>
	/// <see cref="NativePage" /> is a C# native only page template to enable XML rendering from templates using the
	/// <see cref="T:TcmTemplating.XmlTemplateBase" />
	/// </summary>
	[TcmTemplateTitle("NativeXMLPage")]
	public class NativeXMLPage : TemplateBase
	{
		public const String NATIVE_XML_WRITER = "NativeXMLWriter";

		/// <summary>
		/// Performs the actual transformation logic of this <see cref="T:TcmTemplating.TemplateBase" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
			using (StringWriterEncoding sw = new StringWriterEncoding(Encoding.UTF8))
			{
				using (XmlWriter xml = XmlTextWriter.Create(sw, TemplateXmlWriterSettings))
				{
					// Ensure the XmlWriter is available in the current rendering context
					Engine.PublishingContext.RenderContext.ContextVariables.Add(NATIVE_XML_WRITER, xml);
				
					foreach (global::Tridion.ContentManager.CommunicationManagement.ComponentPresentation componentPresentation in Page.ComponentPresentations)
					{
						xml.WriteRaw(Engine.RenderComponentPresentation(componentPresentation.Component.Id, componentPresentation.ComponentTemplate.Id));
					}

					xml.WriteProcessingTime(ProcessedTime);
										
					xml.Close();

					// Remove the XmlWriter from the current rendering context
					Engine.PublishingContext.RenderContext.ContextVariables.Remove(NATIVE_XML_WRITER);
				}

				Package.AddXml(global::Tridion.ContentManager.Templating.Package.OutputName, sw.ToString());
			}			
		}
	}
}
