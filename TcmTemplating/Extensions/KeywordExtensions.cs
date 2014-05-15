#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: KeywordExtensions
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
using Tridion.ContentManager.ContentManagement;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="KeywordExtensions"/> supplies .NET extension functions for <see cref="Tridion.ContentManager.ContentManagement.Keyword" />
    /// </summary>
    public static class KeywordExtensions
    {
        /// <summary>
        /// Determine the hierachical level of a keyword
        /// </summary>
		/// <param name="keyword"><see cref="T:Tridion.ContentManager.ContentManagement.Keyword" /></param>
        /// <returns>Hierachical Level as <see cref="T:System.Int" /></returns>
        public static int Level(this Keyword keyword)
        {
			if (keyword != null)
			{
				int level = 1;
				Keyword item = keyword;

				if (item != null)
				{
					while (!item.IsRoot && item.ParentKeywords != null && item.ParentKeywords.Count > 0)
					{
						item = item.ParentKeywords[0];
						level++;
					}
				}

				return level;
			}

			return -1;
        }
    }
}
