#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Dreamweaver
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using TcmTemplating.Extensions;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Expression;

namespace TcmTemplating.Functions
{
    /// <summary>
    /// <see cref="DreamWeaver" /> implements the <see cref="I:Tridion.ContentManager.Templating.Expression.IFunctionSource" /> in 
    /// order to expose custom C# functions to Dreamweaver templating.
    /// </summary>
    public class Dreamweaver : IFunctionSource
    {
        private DreamweaverBase mTemplateBase;

        /// <summary>
        /// Initializes the <see cref="I:Tridion.ContentManager.Templating.Expression.IFunctionSource" />
        /// </summary>
        /// <param name="Engine"><see cref="T:Tridion.ContentManager.Templating.Engine" /></param>
        /// <param name="Package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
        public void Initialize(Engine Engine, Package Package)
        {
            mTemplateBase = new DreamweaverBase(Engine, Package);
        }

        /// <summary>
        /// Generates a thumbnail image of the given width and/or height
        /// </summary>
        /// <param name="ID">TcmUri as <see cref="T:System.String" />.</param>
        /// <param name="ThumbnailWidth">Width of the thumbnail</param>
        /// <param name="ThumbnailHeight">Height of the thumbnail.</param>
        /// <remarks>If either width or height is set but not both, the ratio of the image is maintained.</remarks>
        /// <returns>Thumbnail URL</returns>
		[TemplateCallable]
        public String GenerateThumbnail(String ID, int ThumbnailWidth, int ThumbnailHeight)
        {
            if (!String.IsNullOrEmpty(ID))
            {
                return mTemplateBase.GenerateThumbnail(mTemplateBase.GetComponent(ID), ThumbnailWidth, ThumbnailHeight);
            }

            return String.Empty;
        }

		/// <summary>
		/// Retrieves the description field of a <see cref="T:Tridion.ContentManager.ContentManagement.Keyword" />
		/// </summary>
		/// <param name="itemID"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /> identifier.</param>
		/// <param name="field"><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> field name</param>
		/// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /> description</returns>
		[TemplateCallable]
		public String KeywordDescription(String itemID, String field)
		{
			Component mComponent = mTemplateBase.GetComponent(itemID);

			if (mComponent != null)
			{
				Keyword k = mComponent.KeywordValue(field);

				if (k != null)
					return k.Description;
			}

			return String.Empty;
		}

		/// <summary>
		/// Sets a variable in the current <see cref="T:Tridion.ContentManager.Publishing.Rendering.RenderContext" />
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Variable value</param>		
		[TemplateCallable]
		public void SetVariable(String variableName, Object value)
		{
			mTemplateBase.Engine.PublishingContext.RenderContext.ContextVariables.Remove(variableName);
			mTemplateBase.Engine.PublishingContext.RenderContext.ContextVariables.Add(variableName, value);
		}

		/// <summary>
		/// Gets a variable from the current <see cref="T:Tridion.ContentManager.Publishing.Rendering.RenderContext" />
		/// </summary>
		/// <param name="variableName">Variable name</param>
		[TemplateCallable]
		public Object GetVariable(String variableName)
		{
			return mTemplateBase.Engine.PublishingContext.RenderContext.ContextVariables[variableName];
		}
    }
}
