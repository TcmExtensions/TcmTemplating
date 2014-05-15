#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: IdentifiableObjectExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: April 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.Publishing;

namespace TcmTemplating.Extensions
{
    /// <summary>
    /// <see cref="IdentifiableObjectExtensions"/> supplies .NET extension functions for <see cref="T:Tridion.ContentManager.IdentifiableObject" />
    /// </summary>
    public static class IdentifiableObjectExtensions
    {
		internal static Regex mNavigable = new Regex(@"^(?<number>[\d]{1,5})\.{0,1}\s+", RegexOptions.Compiled);
		
		/// <summary> 
		/// Retrieve the <see cref="T:Tridion.ContentManager.IdentifiableObject" /> TcmId 
		/// </summary> 
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param> 
		/// <returns></returns> 
		/// <remarks> 
		/// TcmId allows for a null reference to be passed in. 
		/// </remarks> 
		public static String TcmId(this IdentifiableObject identifiableObject)
		{
			if (identifiableObject != null)
				return identifiableObject.Id.ToString();

			return String.Empty;
		} 
		
        /// <summary>
        /// Retrieve the <see cref="T:Tridion.ContentManager.IdentifiableObject" /> published information
        /// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
        /// <returns><see cref="T:Tridion.ContentManager.Publishing.PublishInfo" /></returns>
		public static IEnumerable<PublishInfo> PublishInfo(this IdentifiableObject identifiableObject)
        {
			if (identifiableObject != null)
				return PublishEngine.GetPublishInfo(identifiableObject);

			return new PublishInfo[] { };
        }

        /// <summary>
        /// Retrieve the <see cref="T:Tridion.ContentManager.IdentifiableObject" /> published information
        /// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
		/// <param name="publication">Publication <see cref="T:Tridion.ContentManager.Publication"/> to filter on.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.Publishing.PublishInfo" />
        /// </returns>
		public static IEnumerable<PublishInfo> PublishInfo(this IdentifiableObject identifiableObject, Publication publication)
        {
			if (identifiableObject != null && publication != null)
			{
				ICollection<PublishInfo> publishInfos = PublishEngine.GetPublishInfo(identifiableObject);

				if (publishInfos != null && publishInfos.Count > 0)
					return publishInfos.Where(x => x.Publication.Id == publication.Id);						
			}

			return new PublishInfo[] { };
        }

        /// <summary>
        /// Retrieve the <see cref="T:Tridion.ContentManager.IdentifiableObject" /> published information
        /// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
		/// <param name="publicationTarget">Publication target <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget"/> to filter on.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.Publishing.PublishInfo" />
        /// </returns>
		public static IEnumerable<PublishInfo> PublishInfo(this IdentifiableObject identifiableObject, PublicationTarget publicationTarget)
        {
			if (identifiableObject != null && publicationTarget != null)
			{
				ICollection<PublishInfo> publishInfos = PublishEngine.GetPublishInfo(identifiableObject);

				if (publishInfos != null && publishInfos.Count > 0)
					return publishInfos.Where(x => x.PublicationTarget.Id == publicationTarget.Id);
			}

			return new PublishInfo[] { };
        }

        /// <summary>
        /// Retrieve the <see cref="T:Tridion.ContentManager.IdentifiableObject" /> published information
        /// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
		/// <param name="publication">Publication <see cref="T:Tridion.ContentManager.Publication"/> to filter on.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.Publishing.PublishInfo" />
        /// </returns>
		public static IEnumerable<PublishInfo> PublishInfo(this IdentifiableObject identifiableObject, TcmUri publication, TcmUri publicationTarget)
        {
			if (identifiableObject != null && publication != null && publicationTarget != null)
			{
				ICollection<PublishInfo> publishInfos = PublishEngine.GetPublishInfo(identifiableObject);

				if (publishInfos != null && publishInfos.Count > 0)
					return publishInfos.Where(x => x.Publication.Id == publication && x.PublicationTarget.Id == publicationTarget);
			}

			return new PublishInfo[] { };
        }

        /// <summary>
        /// Retrieve the <see cref="T:Tridion.ContentManager.IdentifiableObject" /> published information
        /// </summary>
        /// <param name="Object"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
        /// <param name="PublicationTarget">Publication target <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget"/> to filter on.</param>
        /// <returns>
        ///   <see cref="T:Tridion.ContentManager.Publishing.PublishInfo" />
        /// </returns>
		public static IEnumerable<PublishInfo> PublishInfo(this IdentifiableObject identifiableObject, Publication publication, PublicationTarget publicationTarget)
        {
			if (identifiableObject != null && publication != null && publicationTarget != null)
			{
				ICollection<PublishInfo> publishInfos = PublishEngine.GetPublishInfo(identifiableObject);

				if (publishInfos != null && publishInfos.Count > 0)
					return publishInfos.Where(x => x.Publication.Id == publication.Id && x.PublicationTarget.Id == publicationTarget.Id);
			}

            return null;
        }

        /// <summary>
        /// Returns the last publish date in the given <see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /> and
        /// given <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget" />
        /// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.ContentManagement.RepositoryLocalObject" /></param>
		/// <param name="publication"><see cref="T:Tridion.ContentManager.CommunicationManagement.Publication" /></param>
		/// <param name="publicationTarget"><see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget" /></param>
        /// <returns>
        /// Returns <see cref="T:System.DateTime" /> or DateTime.MinValue
        /// </returns>
		public static DateTime PublishedAt(this IdentifiableObject identifiableObject, Publication publication, PublicationTarget publicationTarget)
        {
			if (identifiableObject != null && publication != null && publicationTarget != null)
			{
				PublishInfo info = identifiableObject.PublishInfo(publication, publicationTarget).FirstOrDefault();

				if (info != null)
					return info.PublishedAt;
			}

            return default(DateTime);
        }

		/// <summary>
		/// Determines whether item is published in the current publication context.
		/// </summary>
		/// <param name="identifiableObject">The item <see cref="Tridion.ContentManager.IdentifiableObject"/>.</param>
		/// <param name="publicationTarget">The publication target <see cref="Tridion.ContentManager.CommunicationManagement.PublicationTarget"/>.</param>
		/// <returns>
		///   <c>true</c> if item is published in current publication context, otherwise <c>false</c>.
		/// </returns>
		public static bool IsPublished(this IdentifiableObject identifiableObject, PublicationTarget publicationTarget)
		{
			if (identifiableObject != null && publicationTarget != null)
				return PublishEngine.IsPublished(identifiableObject, publicationTarget, true);

			return false;
		}

		/// <summary>
		/// Determines whether the <see cref="T:Tridion.ContentManager.IdentifiableObject"/> is a "navigable" item.
		/// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
		/// <returns><c>True</c> if the item is a "navigable" item.</returns>
		/// <remarks>Navigable items have to match the following rules:
		/// 1. Start with a 1 to 5 digit number
		/// 2. Optionally have a dot "."
		/// 3. Optionally have a space " "
		/// </remarks>
		public static Boolean IsNavigableItem(this IdentifiableObject identifiableObject)
		{
			if (identifiableObject != null)
				return mNavigable.IsMatch(identifiableObject.Title);

			return false;
		}

		/// <summary>
		/// Extracts the number from a navigable item (if available).
		/// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
		/// <returns>Navigable number if available, otherwise -1.</returns>
		/// <remarks>Navigable items have to match the following rules:
		/// 1. Start with a 1 to 5 digit number
		/// 2. Optionally have a dot "."
		/// 3. Optionally have a space " "
		/// </remarks>
		public static int NavigableNumber(this IdentifiableObject identifiableObject)
		{
			if (identifiableObject != null)
			{
				Match match = mNavigable.Match(identifiableObject.Title);

				if (match.Success && match.Groups.Count > 0)
				{
					int value;

					if (int.TryParse(match.Groups["number"].Value, out value))
					{
						return value;
					}
				}
			}

			return -1;
		}

		/// <summary>
		/// Extracts the title from a navigable item
		/// </summary>
		/// <param name="identifiableObject"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
		/// <returns>Navigable title if available, otherwise standard title.</returns>
		/// <remarks>Navigable items have to match the following rules:
		/// 1. Start with a 1 to 5 digit number
		/// 2. Optionally have a dot "."
		/// 3. Optionally have a space " "
		/// </remarks>	
		public static String NavigableTitle(this IdentifiableObject identifiableObject)
		{
			if (identifiableObject != null)
			{
				Match match = mNavigable.Match(identifiableObject.Title);

				if (match.Success)
				{
					return identifiableObject.Title.Substring(match.Length);
				}

				return identifiableObject.Title;
			}

			return String.Empty;
		}
    }
}
