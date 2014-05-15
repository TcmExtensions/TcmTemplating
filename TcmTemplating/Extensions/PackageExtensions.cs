#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: PackageExtensions
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using Tridion.ContentManager;
using Tridion.ContentManager.Templating;

namespace TcmTemplating.Extensions
{
	/// <summary>
	/// <see cref="PackageExtensions" /> supplies .NET extension functions for <see cref="T:Tridion.ContentManager.Templating.Package" />
	/// </summary>
	public static class PackageExtensions
	{
		/// <summary>
		/// Obtains the requested property of the given package item
		/// </summary>
		/// <param name="item">Package item.</param>
		/// <param name="key">Property key.</param>
		/// <returns>Property Value as <see cref="T:System.String" /></returns>
		public static String Property(this Item item, String key)
		{
			if (item != null)
			{
				String value;

				if (item.Properties.TryGetValue(key, out value))
				{
					return value;
				}
			}

			return String.Empty;
		}

		/// <summary>
		/// Obtains the requested property of the given package item
		/// </summary>
		/// <param name="item">Package item.</param>
		/// <param name="key">Property key.</param>
		/// <returns>Property Value as <see cref="T:Tridion.ContentManager.TcmUri" /></returns>
		public static TcmUri PropertyAsUri(this Item item, String key)
		{
			if (item != null)
			{
				String value = item.Property(key);

				if (!String.IsNullOrEmpty(value) && TcmUri.IsValid(value))
				{
					return new TcmUri(value);
				}
			}

			return TcmUri.UriNull;
		}

		/// <summary>
		/// Add a new value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="contentType"><see cref="T:Tridion.ContentManager.Templating.ContentType" />.</param>
		/// <param name="name">Item Name</param>
		/// <param name="value">Item Value</param>
		public static void AddValue(this Package package, ContentType contentType, String name, String value)
		{
			if (package != null)
			{
				Item packageItem = package.CreateStringItem(contentType, value);
				package.PushItem(name, packageItem);
			}
		}

		/// <summary>
		/// Add a new <see cref="T:System.String"/> value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddString(this Package package, String name, String value)
		{
			if (package != null)
				package.AddValue(ContentType.Text, name, value);
		}

		/// <summary>
		/// Add a new <see cref="T:System.Int32"/> value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddNumber(this Package package, String name, int value)
		{
			if (package != null)
				package.AddValue(ContentType.Number, name, value.ToString());
		}

		/// <summary>
		/// Add a new <see cref="T:System.Double"/> value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddNumber(this Package package, String name, double value)
		{
			if (package != null)
				package.AddValue(ContentType.Number, name, value.ToString());
		}

		/// <summary>
		/// Add a new <see cref="T:System.DateTime"/> value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddDateTime(this Package package, String name, DateTime value)
		{
			if (package != null)
				package.AddValue(ContentType.DateTime, name, value.ToString());
		}

		/// <summary>
		/// Add a new <see cref="T:System.String"/> XML value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		/// <remarks>An XML processing instruction is automatically added if required.</remarks>
		public static void AddXml(this Package package, String name, String value)
		{
			if (package != null)
			{
				String packageValue = value;

				// Add an XML prolog to ensure a valid XML snippet
				if (!packageValue.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase))
					packageValue = String.Format("<?xml version=\"1.0\" encoding=\"{0}\"?>\n", Encoding.UTF8.HeaderName) + packageValue;

				package.AddValue(ContentType.Xml, name, packageValue);
			}
		}

		/// <summary>
		/// Add a new <see cref="T:System.String"/> XHTML value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddXhtml(this Package package, String name, String value)
		{
			if (package != null)
				package.AddValue(ContentType.Xhtml, name, value);
		}

		/// <summary>
		/// Add a new <see cref="T:System.String"/> ExternalLink value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddExternalLink(this Package package, String name, String value)
		{
			if (package != null)
				package.AddValue(ContentType.ExternalLink, name, value);
		}

		/// <summary>
		/// Add a new <see cref="T:Tridion.ContentManager.TcmUri"/> Link value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddLink(this Package package, String name, TcmUri value)
		{
			if (package != null)
				package.AddValue(ContentType.ItemLink, name, value != null ? value : TcmUri.UriNull);
		}

		/// <summary>
		/// Add a new <see cref="T:Tridion.ContentManager.IdentifiableOject"/> Link value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddLink(this Package package, String name, IdentifiableObject value)
		{
			if (package != null)
				package.AddValue(ContentType.ItemLink, name, value != null ? value.Id : TcmUri.UriNull);
		}

		/// <summary>
		/// Add a new <see cref="T:System.String"/> HTML value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddHtml(this Package package, String name, String value)
		{
			if (package != null)
			{
				Item packageItem = package.CreateHtmlItem(value);
				package.PushItem(name, packageItem);
			}
		}

		/// <summary>
		/// Add a new <see cref="T:Tridion.ContentManager.TcmUri"/> value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddComponent(this Package package, String name, TcmUri value)
		{
			if (package != null)
			{
				Item packageItem = package.CreateTridionItem(ContentType.Component, value != null ? value : TcmUri.UriNull);
				package.PushItem(name, packageItem);
			}
		}

		/// <summary>
		/// Add a new <see cref="T:T:Tridion.ContentManager.IdentifiableOject"/> value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddComponent(this Package package, String name, IdentifiableObject value)
		{
			if (package != null)
			{
				Item packageItem = package.CreateTridionItem(ContentType.Component, value);
				package.PushItem(name, packageItem);
			}
		}

		/// <summary>
		/// Add a new <see cref="T:Tridion.ContentManager.TcmUri"/> value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddMultimedia(this Package package, String name, TcmUri value)
		{
			if (package != null)
			{
				Item packageItem = package.CreateMultimediaItem(value);
				package.PushItem(name, packageItem);
			}
		}

		/// <summary>
		/// Adds new <see cref="T:Tridion.ContentManager.TcmUri"/> values to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddComponents(this Package package, String name, IList<TcmUri> value)
		{
			if (package != null)
			{
				Item packageItem = package.CreateComponentUriListItem(ContentType.ComponentArray, value);
				package.PushItem(name, packageItem);
			}
		}

		/// <summary>
		/// Add a new <see cref="T:Tridion.ContentManager.Templating.ComponentPresentationList"/> value to the current <see cref="T:Tridion.ContentManager.Templating.Package" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddComponents(this Package package, String name, ComponentPresentationList value)
		{
			if (package != null)
			{
				Item packageItem = package.CreateStringItem(ContentType.ComponentArray, value != null ? value.ToXml() : new ComponentPresentationList().ToXml());
				package.PushItem(name, packageItem);
			}
		}

		/// <summary>
		/// Gets a <see cref="T:Tridion.ContentManager.Templating.Package" /> as <see cref="T:System.String" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <returns>Value as <see cref="T:System.String"/></returns>
		public static String ItemAsString(this Package package, String name)
		{
			if (package != null)
			{
				Item packageItem = package.GetByName(name);

				if (packageItem != null)
					return packageItem.GetAsString();
			}

			return String.Empty;
		}

		/// <summary>
		/// Gets a <see cref="T:Tridion.ContentManager.Templating.Package" /> as <see cref="T:Tridion.ContentManager.TcmUri" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <returns>Value as <see cref="T:Tridion.ContentManager.TcmUri"/></returns>
		public static TcmUri ItemAsUri(this Package package, String name)
		{
			if (package != null)
			{
				String value = package.ItemAsString(name);

				if (!String.IsNullOrEmpty(value) && TcmUri.IsValid(value))
					return new TcmUri(value);
			}

			return TcmUri.UriNull;
		}

		/// <summary>
		/// Gets a <see cref="T:Tridion.ContentManager.Templating.Package" /> as <see cref="T:Tridion.ContentManager.Templating.IComponentPresentationList" />
		/// </summary>
		/// <param name="package"><see cref="T:Tridion.ContentManager.Templating.Package" /></param>
		/// <param name="name">Name.</param>
		/// <returns>Value as <see cref="T:Tridion.ContentManager.Templating.IComponentPresentationList"/></returns>
		public static IComponentPresentationList ItemAsComponentList(this Package package, String name)
		{
			if (package != null)
			{
				String value = package.ItemAsString(name);

				if (!String.IsNullOrEmpty(value))
					return ComponentPresentationList.FromXml(value);
			}

			return new ComponentPresentationList();
		}
	}
}
