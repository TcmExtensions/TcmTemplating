#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: TemplateBase
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using TcmTemplating.Extensions;
using TcmTemplating.Helpers;
using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.Publishing;
using Tridion.ContentManager.Publishing.Rendering;
using Tridion.ContentManager.Security;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TcmTemplating
{
	/// <summary>
	/// <see cref="TemplateBase" /> is the abstract base class for all Tridion template transformations.
	/// </summary>
	public abstract class TemplateBase : ITemplate
	{
		#region Private Members
		 
		private long mTicksStart;

		private Engine  mEngine;
		private Package mPackage;

		private TemplatingLogger mLogger;
		private static XmlNamespaceManager mNSM;

        // Cache local Tridion objects if requested
        private Publication mPublication = null;
        private StructureGroup mRootStructureGroup = null;
        private Page mPage = null;
        private PageTemplate mPageTemplate = null;
        private Component mComponent = null;
        private ComponentTemplate mComponentTemplate = null;
        private PublishTransaction mPublishTransaction = null;
        private User mPublishingUser = null;
        private PublicationTarget mPublicationTarget = null;

        private Lazy<Dictionary<String, String>> mPublishedBinaries = new Lazy<Dictionary<String, String>>(() => new Dictionary<String, String>());

		#region Constants				
		private const String THUMBNAIL_SUFFIX = "thumb";
		private const String THUMBNAIL_VARIANT = "thumbnail";

		// Output Format constants
		public const String OF_ASPJSCRIPT = "ASP JScript";
		public const String OF_ASPVBSCRIPT = "ASP VBScript";
		public const String OF_ASCX = "ASCX WebControl";
		public const String OF_JSP = "JSP Scripting";

		// Target Language constants
		public const String TL_ASPJSCRIPT = "ASP/JavaScript";
		public const String TL_ASPVBSCRIPT = "ASP/VBScript";
		public const String TL_ASPDOTNET = "ASP.NET";
		public const String TL_JSP = "JSP";
		#endregion

		#endregion

		#region Properties

		/// <summary>
		/// An XmlNameSpaceManager already initialized with several XML namespaces such as: tcm, xlink and xhtml
		/// </summary>
		public static XmlNamespaceManager NSManager
		{
			get
			{
                if (mNSM == null)
				{
					mNSM = new XmlNamespaceManager(new NameTable());
					
					mNSM.AddNamespace(Tridion.Constants.TcmPrefix, Tridion.Constants.TcmNamespace);
                    mNSM.AddNamespace(Tridion.Constants.XlinkPrefix, Tridion.Constants.XlinkNamespace);
                    mNSM.AddNamespace(Tridion.Constants.XhtmlPrefix, Tridion.Constants.XhtmlNamespace);
				}

                return mNSM;
			}
		}

		/// <summary>
		/// Gets default <see cref="T:System.Xml.XmlWriterSettings" /> to use for writing Xml templates.
		/// </summary>
		/// <value>
		/// Template <see cref="T:System.Xml.XmlWriterSettings" /> 
		/// </value>
		public static XmlWriterSettings TemplateXmlWriterSettings
		{
			get
			{
				return new XmlWriterSettings()
				{
					 CheckCharacters = true,
					 ConformanceLevel = ConformanceLevel.Fragment,
					 Encoding = Encoding.UTF8,
					 Indent = false,
					 NamespaceHandling = NamespaceHandling.OmitDuplicates,
					 NewLineHandling = NewLineHandling.None
				};
			}
		}

		/// <summary>
		/// Get the tridion <see cref="T:Tridion.ContentManager.Templating.TemplatingLogger" />
		/// </summary>
		/// <value>
		/// <see cref="T:Tridion.ContentManager.Templating.TemplatingLogger" />
		/// </value>
		public TemplatingLogger Logger
		{
			get
			{
				if (mLogger == null)
                    mLogger = TemplatingLogger.GetLogger(this.GetType());

                return mLogger;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is in preview mode.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is in preview mode; otherwise, <c>false</c>.
		/// </value>
		public bool IsPreview
		{
			get
			{
				return mEngine.RenderMode == RenderMode.PreviewDynamic || mEngine.RenderMode == RenderMode.PreviewStatic;
			}
		}

		/// <summary>
		/// Returns true if the current render mode is Publish
		/// </summary>
		public bool IsPublishing
		{
			get
			{
				return (mEngine.RenderMode == RenderMode.Publish);
			}
		}

        /// <summary>
        /// Checks whether there is an item in the package of type tridion/page
        /// </summary>
        /// <returns>True if there is a page item in the package</returns>
		public bool IsPage
        {
            get
            {                
                return (Page != null);
            }
        }

        /// <summary>
        /// Checks whether there is an item in the package of type tridion/component
        /// </summary>
        /// <returns>True if there is a component item in the package</returns>
		public bool IsComponent
        {
            get
            {
                return (Component != null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this publishing instance is publishing to live/production.
        /// </summary>
        /// <value>
        /// <c>true</c> if publishing to production; otherwise, <c>false</c>.
        /// </value>
		public bool IsProduction
        {
            get
            {
                // For publication targets which are named 'Live' or 'Production' we indicate the Production environment
                return (PublicationTarget != null && (PublicationTarget.Title.Contains("live", StringComparison.OrdinalIgnoreCase) || PublicationTarget.Title.Contains("production", StringComparison.OrdinalIgnoreCase)));
            }
        }

		/// <summary>
		/// Obtains the <see cref="T:TcmTemplating.Helpers.FormatTextResolver" /> used to expand Format Text Fields (RTF).
		/// </summary>
		/// <value>
		/// <see cref="T:TcmTemplating.Helpers.FormatTextResolver" />
		/// </value>
		protected virtual FormatTextResolver FormatTextResolver
		{
			get
			{
				return new FormatTextResolver(this);
			}
		}
		#endregion

		/// <summary>
		/// Performs the actual transformation logic of this <see cref="TemplateBase"/>.
		/// </summary>
		/// <remarks>Transform is the main entry-point for template functionality.</remarks>
		protected abstract void Transform();

		/// <summary>
		/// Allow templates to provide a PreTransform hook
		/// </summary>
		protected virtual void PreTransform() { }

		/// <summary>
		/// Allow templates to provide a PostTransform hook
		/// </summary>
		protected virtual void PostTransform() { }

        /// <summary>
        /// Execute the transformation for the specified template
        /// </summary>
        /// <param name="Engine"><see cref="T:Tridion.ContentManager.Templating.Engine"/>.</param>
        /// <param name="Package"><see cref="T:Tridion.ContentManager.Templating.Package"/></param>
        public void Transform(Engine Engine, Package Package)
        {
			try
			{				
				mTicksStart = Environment.TickCount;

				mEngine = Engine;
				mPackage = Package;

				// Actual template transformation
				PreTransform();
				Transform();
				PostTransform();
			}
			catch (Exception ex)
			{
				String exceptionStack = LoggerExtensions.TraceException(ex);
		
				Logger.Error("TemplateBase.Transform Exception\n" + exceptionStack);

				StringBuilder sb = new StringBuilder();
				sb.AppendLine(ex.Message);
				sb.AppendFormat("Publisher: {0}\n", Environment.MachineName);
				sb.Append(exceptionStack);

				// Re-throw to ensure Tridion knows what happened
				throw new Exception(sb.ToString(), ex);
			}
			// Ensure we always clean up, no matter what happens during the template transformation
			finally
			{
				Logger.Info("{0}: Render time {1:0.00000} seconds @ {2:dd/MM/yyyy hh:mm:ss tt} ", this.GetType().FullName, ProcessedTime / 1000.0, DateTime.Now);

				// Do not cache objects across template transformations
				ItemFieldsFactory.ClearCache();

				// Clear published binaries list if it was used
				if (mPublishedBinaries.IsValueCreated)
					mPublishedBinaries.Value.Clear();
								
				mPackage = null;
				mEngine = null;
				mLogger = null;

				mComponent = null;
				mComponentTemplate = null;
				mPage = null;
				mPageTemplate = null;
				mRootStructureGroup = null;
				mPublication = null;
				mPublishTransaction = null;
				mPublishingUser = null;
				mPublicationTarget = null;
			}
		}		

		#region Base Functionality

		/// <summary>
		/// Gets the amount of time in milliseconds this current template has been processing.
		/// </summary>
		/// <value>
		/// Processing time in milliseconds
		/// </value>
		public long ProcessedTime
		{
			get
			{
				return (Environment.TickCount - mTicksStart);
			}
		}

		/// <summary>
		/// Returns the current <see cref="T:Tridion.ContentManager.Templating.Engine"/>
		/// </summary>
		/// <value>
		/// <see cref="T:Tridion.ContentManager.Templating.Engine"/>
		/// </value>
		public Engine Engine
        {
            get
            {
                return mEngine;
            }
        }

		/// <summary>
		/// Returns the current <see cref="T:Tridion.ContentManager.Templating.Package"/>
		/// </summary>
		/// <value>
		/// <see cref="T:Tridion.ContentManager.Templating.Package"/>
		/// </value>
		public Package Package
        {
            get
            {
                return mPackage;
            }
        }

		
		/// <summary>
		/// Returns the component object that is defined in the package for this template.
		/// </summary>
		/// <remarks>
		/// This method should only be called when there is an actual Component item in the package. 
		/// It does not currently handle the situation where no such item is available.
		/// </remarks>
		/// <returns>the component object that is defined in the package for this template.</returns>
		public Component Component
		{
            get
            {
                if (mComponent == null)
                {
                    Item component = mPackage.GetByType(ContentType.Component);

                    if (component != null)
                        mComponent = GetComponent(component.GetAsSource().GetValue("ID"));
                }

                return mComponent;
            }
		}

		/// <summary>
		/// Returns the Template from the resolved item if it's a Component Template
		/// </summary>
		/// <returns>A Component Template or null</returns>
		public ComponentTemplate ComponentTemplate
		{
            get
            {
                if (mComponentTemplate == null)
                {
                    Template template = mEngine.PublishingContext.ResolvedItem.Template;

                    if (template is ComponentTemplate)
                    {
                        mComponentTemplate = (ComponentTemplate)template;
                    }
                }

                return mComponentTemplate;
            }
		}

		/// <summary>
		/// Returns the Template from the resolved item if it's a Page Template
		/// </summary>
		/// <returns>A Page Template or null</returns>
		public PageTemplate PageTemplate
		{
            get
            {
                if (mPageTemplate == null)
                {
					Template template = mEngine.PublishingContext.ResolvedItem.Template;

                    if (template is PageTemplate)
                    {
                        mPageTemplate = (PageTemplate)template;
                    }                    
                }

                return mPageTemplate;
            }
		}

		/// <summary>
		/// Returns the page object that is defined in the package for this template.
		/// </summary>
		/// <remarks>
		/// This method should only be called when there is an actual Page item in the package. 
		/// It does not currently handle the situation where no such item is available.
		/// </remarks>
		/// <returns>the page object that is defined in the package for this template.</returns>
		public Page Page
		{
            get
            {
                if (mPage == null)
                {
                    Item pageItem = mPackage.GetByType(ContentType.Page);
                    
                    if (pageItem != null)
                        mPage = mEngine.GetObject(pageItem.GetAsSource().GetValue("ID")) as Page;

                    if (Engine.PublishingContext.RenderContext.ContextItem is Page)
                        mPage = Engine.PublishingContext.RenderContext.ContextItem as Page;
                }

                return mPage;
            }
		}

        /// <summary>
        /// Returns the page object that is defined in the package for this template.
        /// </summary>
        /// <remarks>
        /// This method should only be called when there is an actual Page item in the package. 
        /// It does not currently handle the situation where no such item is available.
        /// </remarks>
        /// <returns>the page object that is defined in the package for this template.</returns>
		public StructureGroup RootStructureGroup
        {
            get
            {
                if (mRootStructureGroup == null)
                {
                    Publication publication = Publication;

                    if (publication != null)
                    {
                        mRootStructureGroup = publication.RootStructureGroup;
                    }
                }

                return mRootStructureGroup;
            }

        }

		/// <summary>
		/// Returns the publication object that can be determined from the package for this template.
		/// </summary>
		/// <remarks>
		/// This method currently depends on a Page item being available in the package, meaning that
		/// it will only work when invoked from a Page Template.
		/// 
		/// Updated by Kah Tan (kah.tang@indivirtual.com)
		/// </remarks>
		/// <returns>the Publication object that can be determined from the package for this template.</returns>
		public Publication Publication
		{
            get
            {
                if (mPublication == null)
                {
                    RepositoryLocalObject pubItem = null;
                    Repository repository = null;

                    pubItem = Page;

                    if (pubItem == null)
                        pubItem = Component;

                    if (pubItem != null)
                    {
                        repository = pubItem.ContextRepository;
                        mPublication = repository as Publication;
                    }
                }

                return mPublication;
            }
		}

        /// <summary>
        /// Returns the currently executing PublishTransaction.
        /// Note that Tridion does not easily expose this data, so use this with care.
        /// </summary>
        /// <value>
        /// Active <see cref="T:Tridion.ContentManager.Publishing.PublishTransaction" /> or null
        /// </value>
		public PublishTransaction PublishTransaction            
        {
            get
            {
                if (mPublishTransaction == null)
                {
                    String binaryPath = mEngine.PublishingContext.PublishInstruction.RenderInstruction.BinaryStoragePath;
                    Regex tcmRegex = new Regex(@"tcm_\d+-\d+-66560");
                    Match match = tcmRegex.Match(binaryPath);

                    if (match.Success)
                    {
                        String transactionId = match.Value.Replace('_', ':');
                        TcmUri transactionUri = new TcmUri(transactionId);

                        mPublishTransaction = new PublishTransaction(transactionUri, mEngine.GetSession());
                    }
                }

                return mPublishTransaction;
            }
        }

        /// <summary>
        /// Gets the current publishing user (user that initiated the publishing action).
        /// </summary>
        /// <value>
        /// Active <see cref="T:Tridion.ContentManager.Security.User"/> or Null
        /// </value>
		public User PublishingUser
        {
            get
            {
                if (mPublishingUser == null)
                {
                    PublishTransaction transaction = PublishTransaction;

                    if (transaction != null)
                    {
                        mPublishingUser = transaction.Creator;
                    }
                }

                return mPublishingUser;
            }
        }

        /// <summary>
        /// Gets the current <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget" />
        /// </summary>
        /// <value>
        /// <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget" />
        /// </value>
		public PublicationTarget PublicationTarget
        {
            get
            {
                if (mPublicationTarget == null)
                {
                    mPublicationTarget = Engine.PublishingContext.PublicationTarget;
                }

                return mPublicationTarget;
            }
        }

        /// <summary>
        /// Gets the <see cref="T:Tridion.ContentManager.ContentManagement.IdentifiableObject"/> object specified by the ID
        /// </summary>
        /// <param name="ID">IdentifiableObject ID</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.IdentifiableObject"/></returns>
		public T GetObject<T>(String ID) where T : IdentifiableObject
        {
            return mEngine.GetObject(ID) as T;
        }

        /// <summary>
        /// Gets the <see cref="T:Tridion.ContentManager.ContentManagement.IdentifiableObject"/> object specified by the ID
        /// </summary>
        /// <param name="ID">IdentifiableObject ID</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.IdentifiableObject"/></returns>
		public T GetObject<T>(TcmUri ID) where T : IdentifiableObject
        {
            return mEngine.GetObject(ID) as T;
        }

        /// <summary>
        /// Gets the <see cref="T:Tridion.ContentManager.ContentManagement.Component"/> component specified by the ID
        /// </summary>
        /// <param name="ID"><see cref="T:Tridion.ContentManager.TcmUri"/></param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></returns>
		public Component GetComponent(TcmUri ID)
        {
            return GetObject<Component>(ID);
        }

        /// <summary>
        /// Gets the <see cref="T:Tridion.ContentManager.ContentManagement.Component"/> component specified by the ID
        /// </summary>
        /// <param name="ID">Component ID</param>
        /// <returns><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></returns>
		public Component GetComponent(String ID)
        {
            return GetObject<Component>(ID);
        }

        /// <summary>
        /// Gets the <see cref="T:Tridion.ContentManager.CommunicationManagement.StructureGroup"/> sepecified by the ID
        /// </summary>
        /// <param name="ID">Structure Group ID.</param>
        /// <returns><see cref="T:Tridion.ContentManager.CommunicationManagement.StructureGroup"/></returns>
		public StructureGroup GetStructureGroup(String ID)
        {
            return GetObject<StructureGroup>(ID);            
        }

        /// <summary>
        /// Gets the <see cref="T:Tridion.ContentManager.CommunicationManagement.StructureGroup"/> specified by the ID
        /// </summary>
        /// <param name="ID"><see cref="T:Tridion.ContentManager.TcmUri"/></param>
        /// <returns><see cref="T:Tridion.ContentManager.CommunicationManagement.StructureGroup"/></returns>
		public StructureGroup GetStructureGroup(TcmUri ID)
        {
            return GetObject<StructureGroup>(ID);
        }

        /// <summary>
        /// Gets the <see cref="T:Tridion.ContentManager.CommunicationManagement.Page"/> specified by the ID
        /// </summary>
        /// <param name="ID">Page ID.</param>
        /// <returns><see cref="T:Tridion.ContentManager.CommunicationManagement.Page"/></returns>
		public Page GetPage(String ID)
        {
            return GetObject<Page>(ID);
        }

        /// <summary>
        /// Gets the <see cref="T:Tridion.ContentManager.CommunicationManagement.Page"/> specified by the ID
        /// </summary>
        /// <param name="ID"><see cref="T:Tridion.ContentManager.TcmUri"/></param>
        /// <returns><see cref="T:Tridion.ContentManager.CommunicationManagement.Page"/></returns>
		public Page GetPage(TcmUri ID)
        {
            return GetObject<Page>(ID);
		}

		/// <summary>
		/// Gets the <see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentTemplate"/> specified by the ID
		/// </summary>
		/// <param name="ID">Component Template ID.</param>
		/// <returns><see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentTemplate"/></returns>
		public ComponentTemplate GetComponentTemplate(String ID)
		{
			return GetObject<ComponentTemplate>(ID);
		}

		/// <summary>
		/// Gets the <see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentTemplate"/> specified by the ID
		/// </summary>
		/// <param name="ID"><see cref="T:Tridion.ContentManager.TcmUri"/></param>
		/// <returns><see cref="T:Tridion.ContentManager.CommunicationManagement.ComponentTemplate"/></returns>
		public ComponentTemplate GetComponentTemplate(TcmUri ID)
		{
			return GetObject<ComponentTemplate>(ID);
		}

		/// <summary>
		/// Checks whether a Target Type URI is associated with the current publication target being published to
		/// </summary>
		public bool IsTTInPublicationContext(String ttURI)
		{
			if (IsPublishing && mEngine.PublishingContext.PublicationTarget != null) // not null only during publishing
			{
				foreach (TargetType tt in mEngine.PublishingContext.PublicationTarget.TargetTypes)
				{
					if (tt.Id == ttURI) 
                        return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Checks whether at least one of a list of Target Type URIs is associated with the current publication target being published to
		/// </summary>
		public bool IsTTInPublicationContext(IEnumerable<String> ttURIs)
		{
			if (mEngine.PublishingContext.PublicationTarget != null)// not null only during publishing
			{
				foreach (String uri in ttURIs)
				{
					foreach (TargetType tt in mEngine.PublishingContext.PublicationTarget.TargetTypes)
					{
						if (tt.Id == uri) return true;
					}
				}
			}

			return false;
		}
		#endregion

		#region Utilities
        /// <summary>
        /// Obtains the Tridion standard name for a published binary
        /// </summary>
        /// <param name="Binary">The binary.</param>
        /// <returns></returns>
		public String BinaryFileName(Component Binary)
        {
            if (Binary.BinaryContent != null)
            {
                return String.Format("{0}_tcm{1}-{2}{3}",
                                     Uri.EscapeDataString(Path.GetFileNameWithoutExtension(Binary.BinaryContent.Filename)),
                                     Binary.Id.PublicationId,
                                     Binary.Id.ItemId,
                                     Path.GetExtension(Binary.BinaryContent.Filename));
            }

            return String.Empty;
        }

        /// <summary>
        /// Publishes the multi-media component binary
        /// </summary>
        /// <param name="Component"><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></param>
        /// <returns>Published binary path</returns>
		public string PublishBinary(Component Component)
        {
            return PublishBinary(Component, String.Empty);
        }

        /// <summary>
        /// Publishes the multi-media component binary
        /// </summary>
        /// <param name="Component"><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></param>
        /// <param name="variantId">Binary Variant Id.</param>
        /// <returns>Published binary path</returns>
		public string PublishBinary(Component Component, String variantId)
        {
            if (Component != null && Component.ComponentType == ComponentType.Multimedia && Component.BinaryContent != null)
            {
                Binary pubBinary = null;

                if (!String.IsNullOrEmpty(variantId))
                {
                    String key = Component.Id.ToString() + "-" + variantId;
                    String value;
										
					if (mPublishedBinaries.Value.TryGetValue(key, out value))
                    {
                        // Return the previous published binary path
                        return value;
                    }
                    else
                    {
                        pubBinary = mEngine.PublishingContext.RenderedItem.AddBinary(Component, variantId);
						mPublishedBinaries.Value.Add(key, pubBinary.Url);

                        return pubBinary.Url;
                    }
                }
                else
                {
                    String key = Component.Id.ToString();
                    String value;

					if (mPublishedBinaries.Value.TryGetValue(key, out value))
                    {
                        // Return the previous published binary path
                        return value;
                    }
                    else
                    {
                        pubBinary = mEngine.PublishingContext.RenderedItem.AddBinary(Component);
						mPublishedBinaries.Value.Add(key, pubBinary.Url);

                        return pubBinary.Url;
                    }
                }            
            }
            else
            {
                Logger.Warning("PublishBinary: {0} is not a multimedia component.", Component != null ? Component.Id : "(null)");
            }

            return String.Empty;
        }

        /// <summary>
        /// Publishes the given binary component, while associating it with the relatedComponent.
        /// </summary>
        /// <param name="Component"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
        /// <param name="relatedComponent"><see cref="T:Tridion.ContentManager.ContentManagement.Component" /></param>
        /// <returns>
        /// Binary publish path
        /// </returns>
		public String PublishBinary(Component Component, Component relatedComponent)
        {
            return PublishBinary(Component, relatedComponent, String.Empty);
        }

        /// <summary>
        /// Publishes the given binary component, while associating it with the relatedComponent.
        /// </summary>
        /// <param name="Component"><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></param>
        /// <param name="relatedComponent"><see cref="T:Tridion.ContentManager.ContentManagement.Component"/></param>
        /// <param name="variantId">variant id.</param>
        /// <returns>Binary publish path</returns>
		public String PublishBinary(Component Component, Component relatedComponent, String variantId)
        {
            if (Component != null && Component.ComponentType == ComponentType.Multimedia && Component.BinaryContent != null && relatedComponent != null)
            {
                using (MemoryStream binaryStream = new MemoryStream())
                {
                    Component.BinaryContent.WriteToStream(binaryStream);
                    binaryStream.Seek(0, SeekOrigin.Begin);
                    
                    String fileName = BinaryFileName(Component);

                    mEngine.PublishingContext.RenderedItem.AddBinary(binaryStream, fileName, variantId, relatedComponent, Component.BinaryContent.MultimediaType.MimeType);

                    return VirtualPathUtility.AppendTrailingSlash(Publication.MultimediaUrl) + fileName;
                }
            }

            return String.Empty;
        }
		

        /// <summary>
        /// Publishes a <see cref="T:Tridion.ContentManager.IdentifiableObject"/> to a given <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget"/> and
        /// with <see cref="T:Tridion.ContentManager.Publishing.PublishPriority"/>
        /// </summary>
        /// <param name="PublishUser"><see cref="T:Tridion.ContentManager.Security.User"/></param>
        /// <param name="Item"><see cref="T:Tridion.ContentManager.IdentifiableObject"/></param>
        /// <param name="Target"><see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget"/></param>
        /// <param name="Priority"><see cref="T:Tridion.ContentManager.Publishing.PublishPriority"/></param>
		public void PublishItem(User PublishUser, IdentifiableObject Item, PublicationTarget Target, PublishPriority Priority)
		{
			PublishItem(PublishingUser, Item, Engine.PublishingContext.PublicationTarget, PublishTransaction.Priority, DateTime.Now);
		}

        /// <summary>
        /// Publishes a <see cref="T:Tridion.ContentManager.IdentifiableObject"/> to a given <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget"/> and
        /// with <see cref="T:Tridion.ContentManager.Publishing.PublishPriority"/>
        /// </summary>
        /// <param name="PublishUser"><see cref="T:Tridion.ContentManager.Security.User"/></param>
        /// <param name="Item"><see cref="T:Tridion.ContentManager.IdentifiableObject"/></param>
        /// <param name="Target"><see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget"/></param>
        /// <param name="Priority"><see cref="T:Tridion.ContentManager.Publishing.PublishPriority"/></param>
		/// <param name="startDate"><see cref="T:System.DateTime"/></param>
		public void PublishItem(User PublishUser, IdentifiableObject Item, PublicationTarget Target, PublishPriority Priority, DateTime startDate)
        {
            if (Engine.RenderMode == RenderMode.Publish)
            {
				if (startDate == null)
					startDate = DateTime.Now;

                using (Session session = new Session(PublishUser.Title))
                {
					PublishInstruction publishInstruction = new PublishInstruction(session)
					{
						StartAt = startDate,
						DeployAt = startDate
					};
                    RenderInstruction renderInstruction = new RenderInstruction(session);

                    renderInstruction.RenderMode = RenderMode.Publish;
                    publishInstruction.RenderInstruction = renderInstruction;

                    PublishEngine.Publish(new IdentifiableObject[] { session.GetObject(Item.Id) }, publishInstruction, new PublicationTarget[] { Target }, Priority);
                }
            }
        }

        /// <summary>
        /// Publishes a <see cref="T:Tridion.ContentManager.IdentifiableObject" /> to the current <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget" /> and
        /// with <see cref="T:Tridion.ContentManager.Publishing.PublishPriority" />
        /// </summary>
        /// <param name="PublishUser"><see cref="T:Tridion.ContentManager.Security.User" /></param>
        /// <param name="Item"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
        /// <param name="Priority"><see cref="T:Tridion.ContentManager.Publishing.PublishPriority" /></param>
		public void PublishItem(User PublishUser, IdentifiableObject Item, PublishPriority Priority)
        {
            PublishItem(PublishUser, Item, Engine.PublishingContext.PublicationTarget, Priority);
        }

        /// <summary>
        /// Publishes a <see cref="T:Tridion.ContentManager.IdentifiableObject" /> to the current <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget" /> and
        /// with <see cref="T:Tridion.ContentManager.Publishing.PublishPriority" /> using the current <see cref="P:PublishingUser"/>
        /// </summary>
        /// <param name="Item"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
        /// <param name="Priority"><see cref="T:Tridion.ContentManager.Publishing.PublishPriority" /></param>
		public void PublishItem(IdentifiableObject Item, PublishPriority Priority)
        {
            PublishItem(PublishingUser, Item, Priority);
        }

		/// <summary>
		/// Publishes a <see cref="T:Tridion.ContentManager.IdentifiableObject" /> to the current <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget" /> and
		/// with the publish transactions <see cref="T:Tridion.ContentManager.Publishing.PublishPriority" /> using the current <see cref="P:PublishingUser"/>
		/// </summary>
		/// <param name="Item"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
		/// <param name="startDate"><see cref="T:System.DateTime"/></param>
		public void PublishItem(IdentifiableObject Item, DateTime startDate)
		{
			PublishItem(PublishingUser, Item, Engine.PublishingContext.PublicationTarget, PublishTransaction.Priority, startDate);
		}

        /// <summary>
        /// Publishes a <see cref="T:Tridion.ContentManager.IdentifiableObject" /> to the current <see cref="T:Tridion.ContentManager.CommunicationManagement.PublicationTarget" /> and
        /// with the publish transactions <see cref="T:Tridion.ContentManager.Publishing.PublishPriority" /> using the current <see cref="P:PublishingUser"/>
        /// </summary>
        /// <param name="Item"><see cref="T:Tridion.ContentManager.IdentifiableObject" /></param>
		public void PublishItem(IdentifiableObject Item)
        {
            PublishItem(PublishingUser, Item, Engine.PublishingContext.PublicationTarget, PublishTransaction.Priority);
        }

		/// <summary>
		/// Generates a scaled image (thumbnail) from the given binary image.
		/// </summary>
		/// <param name="Binary">Binary Multimedia Component</param>
		/// <param name="ThumbnailWidth">Result image Width</param>
		/// <param name="ThumbnailHeight">Result image Height</param>
		/// <returns></returns>
		public String GenerateThumbnail(Component Binary, int ThumbnailWidth, int ThumbnailHeight)
        {
            if (Binary != null && Binary.BinaryContent != null)
            {
                try
                {
                    // Output the existing image to a memory stream
                    using (MemoryStream stream = new MemoryStream())
                    {
                        Binary.BinaryContent.WriteToStream(stream);

                        using (Image image = Image.FromStream(stream))
                        {
							double aspectRatio = (double)image.Width / (double)image.Height;
							double boxRatio = (double)ThumbnailWidth / (double)ThumbnailHeight;
							double scaleFactor = 0;

							if (boxRatio > aspectRatio) // Use height, since that is the most restrictive dimension of box. 
								scaleFactor = (double)ThumbnailHeight / (double)image.Height;
							else
								scaleFactor = (double)ThumbnailWidth / (double)image.Width;

							double newWidth = (double)image.Width * scaleFactor;
							double newHeight = (double)image.Height * scaleFactor;

							using (Bitmap bitmap = new Bitmap((int)newWidth, (int)newHeight))
                            {
                                using (Graphics graphic = Graphics.FromImage(bitmap))
                                {
                                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
									graphic.CompositingQuality = CompositingQuality.HighQuality;
									graphic.CompositingMode = CompositingMode.SourceOver;

                                    graphic.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
                                }

                                using (MemoryStream streamOut = new MemoryStream())
                                {
									String extension = String.Empty;
									String mimeType = String.Empty;
									
									// Determine if the input image has transparency
									if ((image.Flags & (int)ImageFlags.HasAlpha) == (int)ImageFlags.HasAlpha)
									{
										// Save as transparent PNG (Portable Network Graphics)
										bitmap.Save(streamOut, ImageFormat.Png);

										extension = "png";
										mimeType = "image/png";
									}
									else
									{
										// Save as optimized non-transparent JPEG (Joint Photo Experts Group)
										ImageCodecInfo imageCodecInfo = ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => String.Equals(e.MimeType, "image/jpeg", StringComparison.OrdinalIgnoreCase));

										if (imageCodecInfo != null)
										{
											System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
											EncoderParameters parameters = new EncoderParameters(1);
											parameters.Param[0] = new EncoderParameter(encoder, 90L);
																						
											bitmap.Save(streamOut, imageCodecInfo, parameters);

											extension = "jpg";
											mimeType = "image/jpeg";
										}
									}

									if (!String.IsNullOrEmpty(extension) && !String.IsNullOrEmpty(mimeType))
									{
										String fileName = String.Format("{0}-{1}_tcm{2}_{3}.{4}", 
											Path.GetFileNameWithoutExtension(Binary.BinaryContent.Filename),
											THUMBNAIL_SUFFIX,
											Binary.Id.PublicationId,
											Binary.Id.ItemId, 
											extension);

										Binary binary = Engine.PublishingContext.RenderedItem.AddBinary(streamOut, fileName, THUMBNAIL_VARIANT, Binary, mimeType);
										return binary.Url;
									}
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("GenerateThumbnail", ex);
                }
            }

            return String.Empty;
        }

		/// <summary>
		/// Publishes an <see cref="T:Tridion.ContentManager.IdentifiableObject" /> intelligently if and only if, <see cref="T:Tridion.ContentManager.IdentifiableObject" /> is not in 
		/// "Waiting For Publish" state or "Scheduled For Publish" state within the scheduleDateFilter <see cref="T:System.DateTime" />
		/// </summary>
		/// <param name="identifiableObject">The <see cref="T:Tridion.ContentManager.IdentifiableObject" />.</param>
		/// <param name="startDateFilter">The start <see cref="T:System.DateTime"/> filter.</param>
		/// <param name="scheduleDateFilter">The schedule <see cref="T:System.DateTime"/> filter.</param>
		/// <param name="publishStartDate">The publish start <see cref="T:System.DateTime"/>.</param>
		public void PublishIntelligently(IdentifiableObject identifiableObject, DateTime startDateFilter, DateTime scheduleDateFilter, DateTime publishStartDate)
		{
			PublishTransactionsFilter filter = new PublishTransactionsFilter(Engine.GetSession())
			{
				StartDate = startDateFilter,
				PublicationTarget = PublicationTarget,
				ForRepository = GetObject<Repository>(Publication.Id)
			};

			PublishTransaction publishTransaction = PublishEngine.GetPublishTransactions(filter)
				.FirstOrDefault(t => t.Items.Count > 0 &&
								t.Items.First().Id == identifiableObject.Id &&
								(t.State == PublishTransactionState.WaitingForPublish ||
								(t.State == PublishTransactionState.ScheduledForPublish && scheduleDateFilter >= t.Instruction.StartAt)));

			if (publishTransaction == null)
				PublishItem(identifiableObject, publishStartDate);
		}
		#endregion
	}
}
