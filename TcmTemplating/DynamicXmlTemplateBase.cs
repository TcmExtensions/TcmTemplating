#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: DynamicXmlTemplateBase
// ---------------------------------------------------------------------------------
//	Date Created	: May 15, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using TcmTemplating.Helpers;
using Tridion.ContentManager.ContentManagement;

namespace TcmTemplating
{
	/// <summary>
	/// <see cref="DynamicXmlTemplateBase" /> implements a <see cref="T:TcmTemplating.XmlTemplateBase" /> for dynamic component presentations
	/// </summary>
	public abstract class DynamicXmlTemplateBase : XmlTemplateBase
	{
		private const String CUSTOM_META_INSTRUCTION = "my_metadata";

		private Dictionary<String, Object> mMetadata = null;
		private HashSet<Keyword> mKeywords = null;

		/// <summary>
		/// Allow internal templates to provide a PreTransform hook
		/// </summary>
		protected override void PreTransform()
		{
			IsXmlFragment = true;

			base.PreTransform();
		}

		/// <summary>
		/// Gets default <see cref="T:System.Xml.XmlWriterSettings" /> to use for writing Xml templates.
		/// </summary>
		/// <value>
		/// Template <see cref="T:System.Xml.XmlWriterSettings" />
		/// </value>
		public override XmlWriterSettings TemplateXmlWriterSettings
		{
			get
			{
				XmlWriterSettings settings = base.TemplateXmlWriterSettings;

				// Dynamic Component Presentations should not contain a Xml prolog
				settings.OmitXmlDeclaration = true;
				return settings;
			}
		}

		/// <summary>
		/// Gets the custom meta data.
		/// </summary>
		/// <value>
		/// The custom meta data.
		/// </value>
		protected Dictionary<String, Object> CustomMetaData
		{
			get
			{
				if (mMetadata == null)
					mMetadata = new Dictionary<String, Object>();

				return mMetadata;
			}
		}

		/// <summary>
		/// Gets the custom keywords.
		/// </summary>
		/// <value>
		/// The custom keywords.
		/// </value>
		protected HashSet<Keyword> CustomKeywords
		{
			get
			{
				if (mKeywords == null)
					mKeywords = new HashSet<Keyword>();

				return mKeywords;
			}
		}

		/// <summary>
		/// Write out any custom metadata for the Tridion deployer extension
		/// </summary>
		/// <remarks>
		/// Sample custom metadata XML:
		/// <!--<my_metadata>
		///			<keywords>
		///				<Category Name="my_publication_path">
		///					<Keyword>\publication\english</Keyword>
		///				</Category>
		///			</keywords>
		///			<custom>
		///				<ValidityStart>2010-10-31T15:42:00</ValidityStart>
		///				<ValidityEnd type="dateTime">2020-01-01T23:59:00</ValidityEnd>
		///				<Subject>Dubai</Subject>
		///				<Location />
		///				<ContentType>General</ContentType>
		///				</custom>
		///	</my_metadata>--> 
		/// </remarks>
		protected void WriteCustomMetadata()
		{
			if (CustomMetaData.Count > 0 || CustomKeywords.Count > 0)
			{
				using (StringWriter sw = new StringWriterEncoding(Encoding.UTF8))
				{
					using (XmlTextWriter xw = new XmlTextWriter(sw))
					{
						xw.WriteStartElement(CUSTOM_META_INSTRUCTION);

						if (CustomKeywords.Count > 0)
						{
							xw.WriteStartElement("keywords");

							foreach (IGrouping<String, Keyword> category in CustomKeywords.GroupBy(k => k.OrganizationalItem.Title, k => k))
							{
								xw.WriteStartElement("Category");
								xw.WriteAttributeString("Name", category.Key);

								foreach (Keyword keyword in category)
								{
									xw.WriteStartElement("Keyword");
									xw.WriteValue(keyword.Title);
									xw.WriteEndElement();
								}

								xw.WriteEndElement();
							}
							
							xw.WriteEndElement(); // </keywords>
						}

						if (CustomMetaData.Count > 0)
						{
							xw.WriteStartElement("custom");

							foreach (KeyValuePair<String, Object> entry in CustomMetaData)
							{
								switch (Type.GetTypeCode(entry.Value.GetType()))
								{
									case TypeCode.String:
										xw.WriteStartElement(entry.Key);
										xw.WriteValue(entry.Value as String);
										xw.WriteEndElement();
										break;
									case TypeCode.DateTime:
										xw.WriteStartElement(entry.Key);

										xw.WriteAttributeString("type", "dateTime");
										xw.WriteValue(((DateTime)entry.Value).ToString("yyyy-MM-ddTHH:mm:ss"));

										xw.WriteEndElement();
										break;
									case TypeCode.Byte:
									case TypeCode.Decimal:
									case TypeCode.Double:
									case TypeCode.Int16:
									case TypeCode.Int32:
									case TypeCode.Int64:
									case TypeCode.SByte:
									case TypeCode.Single:
									case TypeCode.UInt16:
									case TypeCode.UInt32:
									case TypeCode.UInt64:
										xw.WriteStartElement(entry.Key);

										xw.WriteAttributeString("type", "float");
										xw.WriteValue(entry.Value.ToString());

										xw.WriteEndElement();
										break;
								}
							}

							xw.WriteEndElement(); // </custom>
						}

						xw.WriteEndElement(); // </my_metadata>
					}	
			
					sw.Flush();

					Xml.WriteComment(sw.ToString());
				}
			}
		}
	}
}
