#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: PackageToContextTemplate
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating.Blocks
{
	/// <summary>
	/// <see cref="PackageToContext" /> moves items from the current package into the current context,
	/// this is used in conjunction with <see cref="T:TcmTemplating.BuildingBlocks.Context.Package" />
	/// </summary>
    [TcmTemplateTitle("Package To Context")]
    public class PackageToContext : TemplateBase
    {
		/// <summary>
		/// Performs the actual transformation logic of this <see cref="T:TcmTemplating.TemplateBase" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{	
            Item itemVariables = Package.GetByName("Variables");

            if (itemVariables != null)
            {
                String variables = itemVariables.GetAsString();

                foreach (String variable in variables.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
					Item itemVariable = Package.GetByName(variable);

                    if (itemVariable != null)
                        Engine.PublishingContext.RenderContext.ContextVariables[String.Format("Package.{0}", variable)] = itemVariable;
                }
            }
        }
    }
}
