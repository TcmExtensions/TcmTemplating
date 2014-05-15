#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: XmlTcmUriResolver
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TcmTemplating.Extensions;
using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.Templating;

namespace TcmTemplating.Helpers
{
	/// <summary>
	/// <see cref="XmlTcmUriResolver" /> allows resolving of Tridion Contentmanager TCM uri's.
	/// </summary>
	public class XmlTcmUriResolver : System.Xml.XmlUrlResolver
	{
		private TemplatingLogger mTemplatingLogger = null;
		private Engine mEngine;

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlTcmUriResolver"/> class.
		/// </summary>
		/// <param name="engine"><see cref="T:Tridion.ContentManager.Templating.Engine" /></param>
		public XmlTcmUriResolver(Engine engine)
		{
			mEngine = engine;
			mTemplatingLogger = TemplatingLogger.GetLogger(this.GetType());
		}

		/// <summary>
		/// This method adds the ability for the resolver to return other types than just <see cref="T:System.IO.Stream" />.
		/// </summary>
		/// <param name="absoluteUri">The URI.</param>
		/// <param name="type">The type to return.</param>
		/// <returns>
		/// Returns true if the <paramref name="type" /> is supported.
		/// </returns>
		public override bool SupportsType(Uri absoluteUri, Type type)
		{
			switch (absoluteUri.Scheme.ToLower())
			{
				case "tcm":
					return true;
				case "res":
					return true;
				default:
					// Fall through to the original XmlUrlresolver handler
					return base.SupportsType(absoluteUri, type);
			}
		}

		/// <summary>
		/// Maps a URI to an object containing the actual resource.
		/// </summary>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" /></param>
		/// <param name="role">The current implementation does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can be mapped to the xlink:role and used as an implementation specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns System.IO.Stream objects.</param>
		/// <returns>
		/// A System.IO.Stream object or null if a type other than stream is specified.
		/// </returns>
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (ofObjectToReturn != null && ofObjectToReturn != typeof(Stream))
				return null;

			String tridionUri = null;

			switch (absoluteUri.Scheme.ToLower())
			{
				case "tcm":
					tridionUri = absoluteUri.ToString();
					break;
				case "webdav":
					tridionUri = absoluteUri.PathAndQuery.ToString();
					break;
				case "res":
					Assembly assembly = AppDomain.CurrentDomain.GetAssemblies()
						.FirstOrDefault(a => !a.IsDynamic && a.GetManifestResourceNames().Any(r => String.Equals(r, absoluteUri.PathAndQuery, StringComparison.OrdinalIgnoreCase)));

					if (assembly != null)
						return assembly.GetManifestResourceStream(absoluteUri.PathAndQuery);

					return null;
				default:
					// Fall through to the original XmlUrlresolver handler
					return base.GetEntity(absoluteUri, role, ofObjectToReturn);
			}

			IdentifiableObject identifiableObject = mEngine.GetObject(tridionUri);

			String xml = identifiableObject is TemplateBuildingBlock ? (identifiableObject as TemplateBuildingBlock).Content : identifiableObject.ToXml().OuterXml;

			return new MemoryStream(Encoding.UTF8.GetBytes(xml))
			{
				Position = 0
			};
		}

		/// <summary>
		/// Resolves the absolute URI from the base and relative URIs.
		/// </summary>
		/// <param name="baseUri">The base URI used to resolve the relative URI.</param>
		/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative. If absolute, this value effectively replaces the <paramref name="baseUri" /> value. If relative, it combines with the <paramref name="baseUri" /> to make an absolute URI.</param>
		/// <returns>
		/// A <see cref="T:System.Uri" /> representing the absolute URI, or null if the relative URI cannot be resolved.
		/// </returns>
		public override Uri ResolveUri(Uri baseUri, String relativeUri)
		{
			mTemplatingLogger.Info("ResolveUri: baseUri '{0}', relativeUri: '{1}'.", baseUri, relativeUri);

			if (relativeUri.StartsWith("/webdav", StringComparison.OrdinalIgnoreCase))
				return new Uri("webdav:" + relativeUri);

			return base.ResolveUri(baseUri, relativeUri);
		}
	}
}
