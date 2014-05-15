#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: LastPublished
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Text;
using System.Web.UI;
using TcmTemplating.Extensions;
using TcmTemplating.Helpers;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating.Templates
{
    /// <summary>
    /// The <see cref="LastPublished" /> template renders the current time and user which is publishing the page.
    /// This can be used to verify publishing functionality across multiple servers.
    /// </summary>
	[TcmTemplateTitle("LastPublished")]
    public class LastPublished : TcmTemplating.TemplateBase
    {
		/// <summary>
		/// Performs the actual transformation logic of this <see cref="T:TcmTemplatingTemplateBase" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
            using (StringWriterEncoding sw = new StringWriterEncoding(Encoding.UTF8))
            {
                using (TemplateHtmlWriter hw = new TemplateHtmlWriter(sw))
                {
                    hw.RenderBeginTag(HtmlTextWriterTag.Html);
                    hw.RenderBeginTag(HtmlTextWriterTag.Head);

                    hw.AddAttribute(HtmlTextWriterAttribute.Name, "Robots");
                    hw.AddAttribute(HtmlTextWriterAttribute.Content, "NoIndex, NoFollow");
                    hw.RenderBeginTag(HtmlTextWriterTag.Meta);
                    hw.RenderEndTag();

                    hw.RenderBeginTag(HtmlTextWriterTag.Title);
                    hw.Write("Last Published");
                    hw.RenderEndTag();

                    hw.RenderBeginTag(HtmlTextWriterTag.Body);
                    hw.RenderBeginTag(HtmlTextWriterTag.P);
                    hw.Write("Last Published at {0}", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));

                    if (PublishingUser != null)
                        hw.Write(" by {0}", PublishingUser.Title);

                    hw.RenderEndTag();
                    hw.RenderEndTag();

                    hw.RenderEndTag();
                    hw.RenderEndTag();
                }

                Package.AddString(Package.OutputName, sw.ToString());
            }
        }
    }
}
