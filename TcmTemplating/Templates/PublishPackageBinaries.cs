#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: PublishPackageBinariesTemplate
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.IO;
using TcmTemplating.Extensions;
using Tridion.ContentManager;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating.Templates
{
	/// <summary>
	/// <see cref="PublishPackageBinaries" /> publishes all binaries in the current package
	/// </summary>
    [TcmTemplateTitle("Publish Package Binaries")]
    public class PublishPackageBinaries : TcmTemplating.TemplateBase
    {
        private String ConstructFileName(Item item)
        {
            TcmUri uri = item.PropertyAsUri(Item.ItemPropertyTcmUri);
            String filename = item.Property(Item.ItemPropertyFileName);
            String prefix = item.Property(Item.ItemPropertyFileNamePrefix);
            String suffix = item.Property(Item.ItemPropertyFileNameSuffix);
            String extension = item.Property(Item.ItemPropertyFileNameExtension);
            
            if (String.IsNullOrEmpty(filename))
            {
                Logger.Warning("No filename set in property {0}.", "FileName");
                return String.Empty;
            }

            if (String.IsNullOrEmpty(extension))
                extension = filename.Substring(filename.LastIndexOf('.') + 1);

            // Remove any trailing data after the last '.'
            filename = filename.Substring(0, filename.LastIndexOf('.'));

            if (uri != null)
                return String.Format("{0}{1}{2}_tcm{3}-{4}.{5}", prefix, filename, suffix, uri.PublicationId, uri.ItemId, extension);

            return String.Empty;
        }

		/// <summary>
		/// Performs the actual transformation logic of this <see cref="TemplateBase" />.
		/// </summary>
		/// <exception cref="Tridion.ContentManager.Templating.TemplatingException">
		/// msgTemplatingTargetNotStructureGroup
		/// or
		/// msgTemplatingTargetNotStructureGroup
		/// or
		/// msgTemplatingItemStreamUnavailable
		/// </exception>
		/// <remarks>
		/// Transform is the main entry-point for template functionality.
		/// </remarks>
		protected override void Transform()
		{
            TcmUri targetLocation = null;

            String targetSG = Package.ItemAsString("sg_TargetStructureGroup");

            if (!String.IsNullOrEmpty(targetSG))
            {
                targetLocation = Package.ItemAsUri("sg_TargetStructureGroup");

                if (targetLocation == null)
                    throw new TemplatingException("msgTemplatingTargetNotStructureGroup", new Object[] { targetSG });
            }

            foreach (Item item in Package.GetAllByType(new ContentType("*/*")))
            {
                TcmUri componentUri = item.PropertyAsUri(Item.ItemPropertyTcmUri);

                // Verify the item has a tcm uri, filename and the publishing path is not specified
                if (componentUri == null || !item.Properties.ContainsKey(Item.ItemPropertyFileName) || item.Properties.ContainsKey(Item.ItemPropertyPublishedPath))
                    continue;

                Logger.Debug("Publishing item of type {0}.", item.ContentType);

                String fileName = ConstructFileName(item);

                targetSG = item.Property(Item.ItemPropertyTargetStructureGroup);

                if (!String.IsNullOrEmpty(targetSG))
                {
                    TcmUri targetUri = item.PropertyAsUri(Item.ItemPropertyTargetStructureGroup);

                    if (targetUri != null)
                        throw new TemplatingException("msgTemplatingTargetNotStructureGroup", new Object[] { targetUri });

                    targetLocation = targetUri;
                }

                TcmUri templateUri = item.PropertyAsUri(Item.ItemPropertyTemplateUri);

                using (Stream stream = item.GetAsStream())
                {
                    if (stream == null)
                        throw new TemplatingException("msgTemplatingItemStreamUnavailable", new Object[] { item.ContentType.ToString() });

                    byte[] buffer = new byte[stream.Length];

                    stream.Read(buffer, 0, buffer.Length);
                    String url = Engine.AddBinary(componentUri, templateUri, targetLocation, buffer, fileName);
                    item.Properties[Item.ItemPropertyPublishedPath] = url;

                    stream.Close();
                }
            }
        }
    }
}
