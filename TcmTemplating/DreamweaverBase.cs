#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: DreamweaverBase
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion

using Tridion.ContentManager.Templating;

namespace TcmTemplating
{
    /// <summary>
	/// <see cref="DreamweaverBase"/> inherits from <see cref="T:TcmTemplating.TemplateBase"/> in order
	/// to allow the <see cref="T:TcmTemplating.Functions.Dreamweaver"/> function source to re-use existing templating functionality.
    /// </summary>
    internal class DreamweaverBase : TemplateBase
    {
		/// <summary>
		/// Performs the actual transformation logic of this <see cref="TemplateBase" />.
		/// </summary>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
			// No direct templating action
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DreamweaverBase" /> class.
        /// </summary>
        /// <param name="Engine"><see cref="T:Tridion.ContentManager.Templating.Engine"/></param>
        /// <param name="Package"><see cref="T:Tridion.ContentManager.Templating.Package"/></param>
        public DreamweaverBase(Engine Engine, Package Package)
        {
            base.Initialize(Engine, Package);
        }
    }
}
