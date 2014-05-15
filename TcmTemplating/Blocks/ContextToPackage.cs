#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: ContextToPackageTemplate
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System.Collections;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating.Blocks
{
	/// <summary>
	/// <see cref="ContextToPackage" /> moves items in the current rendering context starting with "Package." into the current package,
	/// this is used in conjunction with <see cref="T:TcmTemplating.BuildingBlocks.PackageToContext" />
	/// </summary>
    [TcmTemplateTitle("Context To Package")]
    public class ContextToPackage : TemplateBase
    {
		/// <summary>
		/// Performs the actual transformation logic of this <see cref="T:TcmTemplating.TemplateBase" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
            foreach (DictionaryEntry entry in Engine.PublishingContext.RenderContext.ContextVariables)
            {   
                if (entry.Key.ToString().StartsWith("Package.") && entry.Value is Item)
                    Package.PushItem(entry.Key.ToString().Substring(8), entry.Value as Item);
            }
        }
    }
}
