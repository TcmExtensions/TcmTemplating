#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: ComponentIndexTemplate
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using TcmTemplating.Extensions;
using Tridion.ContentManager.Templating;
using CommMgmt = Tridion.ContentManager.CommunicationManagement;

namespace TcmTemplating.Blocks
{
	/// <summary>
	/// <see cref="ComponentIndex" /> stores the index of a current component and component template into the package under "Index"
	/// </summary>
    public class ComponentIndex : TcmTemplating.TemplateBase
    {
		/// <summary>
		/// Performs the actual transformation logic of this <see cref="T:TcmTemplating.TemplateBase" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
            int index = 0;

            foreach (CommMgmt.ComponentPresentation componentPresentation in Page.ComponentPresentations)
            {
                // Template do not match, initiate a new block
                if (componentPresentation.ComponentTemplate.Id == ComponentTemplate.Id)
                    index++;

                if (componentPresentation.ComponentTemplate.Id == ComponentTemplate.Id &&
                    componentPresentation.Component.Id == Component.Id)
                {
                    Package.AddString("Index", index.ToString());
                    break;
                }
            }
        }
    }
}
