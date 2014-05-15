#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: ComponentsFromComponentTemplate
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System.Collections.Generic;
using TcmTemplating.Extensions;
using Tridion.ContentManager;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating.Blocks
{
	/// <summary>
	/// <see cref="ComponentsFromComponent" /> extracts components from component link fields and adds them to the current package
	/// </summary>
    [TcmTemplateTitle("ComponentsFromComponent")]
    public class ComponentsFromComponent : TcmTemplating.TemplateBase
    {
		/// <summary>
		/// Performs the actual transformation logic of this <see cref="T:TcmTemplating.TemplateBase" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
            if (Component != null && Component.Content != null)
            {
				ItemFields fields = Component.Fields();

                foreach (ItemField field in fields)
                {
                    if (field is ComponentLinkField)
                    {
                        IList<Component> values = field.ComponentValues();

                        if (values.Count > 0)
                        {
                            IList<TcmUri> uris = new List<TcmUri>();

                            foreach (Component value in values)
                            {
                                uris.Add(value.Id);
                            }

                            Package.AddComponents("Component." + field.Name, uris);
                        }
                    }
                }
            }
        }
    }
}
