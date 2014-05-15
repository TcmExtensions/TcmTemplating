#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: PublishBinaryDCP
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System.Text;
using System.Xml;
using TcmTemplating.Extensions;
using TcmTemplating.Helpers;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating.Templates
{
    /// <summary>
    /// The <see cref="C:PublishBinaryDCP" /> publishes a binary component as a DCP to ensure the binary's availability on the content deployer side.
    /// </summary>
    [TcmTemplateTitle("Publish Binary DCP")]
    public class PublishBinaryDCP : TcmTemplating.TemplateBase
    {
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
				using (XmlWriter xml = XmlTextWriter.Create(sw))
				{
					xml.WriteStartDocument();
					xml.WriteStartElement("binary");
					xml.WriteAttribute("uri", Component.Id);
					xml.WriteAttribute("id", Component.Id.ItemId);
					xml.WriteAttribute("title", Component.Title);
					xml.WriteAttribute("url", PublishBinary(Component));
					xml.WriteEndElement();
					xml.WriteEndDocument();
				}

				Package.AddString(Tridion.ContentManager.Templating.Package.OutputName, sw.ToString());
			}			
		}
    }
}
