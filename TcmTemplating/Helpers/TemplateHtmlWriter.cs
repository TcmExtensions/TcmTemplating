#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: TemplateHtmlWriter
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
using System.IO;

namespace TcmTemplating.Helpers
{
	/// <summary>
	/// <see cref="TemplatHtmlWriter" /> supplies a <see cref="T:System.Web.UI.HtmlTextWriter" /> implementing formatted HTML output for templates.
	/// </summary>
    public class TemplateHtmlWriter : System.Web.UI.HtmlTextWriter
    {
        public KeyValuePair<String, String> Attribute(String Key, String Value)
        {
            return new KeyValuePair<String, String>(Key, Value);
        }

		/// <summary>
		/// Renders an ASP.net user control tag and its associated attributes
		/// </summary>
		/// <param name="Tag">Usercontrol Tag</param>
		/// <param name="Attributes">Associated Attributes</param>
        public void RenderControl(String Tag, params KeyValuePair<String, String>[] Attributes)
        {
            WriteBeginTag(Tag);

            WriteAttribute("runat", "server");

            foreach (KeyValuePair<String, String> attribute in Attributes)
            {
                if (!String.IsNullOrEmpty(attribute.Value))
                    WriteAttribute(attribute.Key, attribute.Value);
            }

            Write(TemplateHtmlWriter.SelfClosingTagEnd);
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="TemplateHtmlWriter" /> class.
		/// </summary>
		/// <param name="Writer"><see cref="T:System.IO.TextWriter" /></param>
        public TemplateHtmlWriter(TextWriter Writer): base(Writer, String.Empty)
        {
            this.NewLine = String.Empty;
            this.Indent = 0;            
        }
    }
}
