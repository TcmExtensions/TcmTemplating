#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: NativePage
// ---------------------------------------------------------------------------------
//	Date Created	: April 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System.Text;
using TcmTemplating.Extensions;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating.Templates
{
    /// <summary>
    /// <see cref="NativePage" /> is a C# native only page template to quickly render out all component presentations on the page.
    /// </summary>
    [TcmTemplateTitle("NativePage")]
    public class NativePage : TcmTemplating.TemplateBase
    {
		/// <summary>
		/// Performs the actual transformation logic of this <see cref="T:TcmTemplating.TemplateBase" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
            StringBuilder output = new StringBuilder();

            foreach (Tridion.ContentManager.CommunicationManagement.ComponentPresentation componentPresentation in Page.ComponentPresentations)
            {
                output.Append(Engine.RenderComponentPresentation(componentPresentation.Component.Id, componentPresentation.ComponentTemplate.Id));
            }

            Package.AddString(Tridion.ContentManager.Templating.Package.OutputName, output.ToString());
        }
    }
}
