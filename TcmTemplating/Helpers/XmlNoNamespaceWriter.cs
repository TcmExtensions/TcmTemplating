#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: XmlNoNamespaceWriter
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace TcmTemplating.Helpers
{
	/// <summary>
	/// <see cref="XmlNoNamespaceWriter"/> extends <see cref="T:System.Xml.XmlWriter" /> to prevent writing namespaces and namespace attributes.
	/// </summary>
	public class XmlNoNamespaceWriter : XmlWriter
	{
		private XmlWriter mXmlWriter;
		private MethodInfo mAddNamespace;
		private Stack<bool> mAttributeStack;
		private bool mStripNextString;

		private XmlNoNamespaceWriter(XmlWriter xmlWriter)
		{
			mAttributeStack = new Stack<bool>();
			mStripNextString = false;

			mXmlWriter = xmlWriter;
			mAddNamespace = mXmlWriter.GetType().GetMethod("AddNamespace", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlNoNamespaceWriter"/> class.
		/// </summary>
		/// <param name="textWriter"><see cref="T:System.IO.TextWriter" /></param>
		public XmlNoNamespaceWriter(TextWriter textWriter): this(XmlWriter.Create(textWriter))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlNoNamespaceWriter" /> class.
		/// </summary>
		/// <param name="textWriter"><see cref="T:System.IO.TextWriter" /></param>
		/// <param name="settings"><see cref="T:System.Xml.XmlWriterSettings" /></param>
		public XmlNoNamespaceWriter(TextWriter textWriter, XmlWriterSettings settings): this(XmlWriter.Create(textWriter, settings))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlNoNamespaceWriter" /> class.
		/// </summary>
		/// <param name="stream"><see cref="T:System.IO.Stream" /></param>
		public XmlNoNamespaceWriter(Stream stream): this(XmlWriter.Create(stream))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlNoNamespaceWriter"/> class.
		/// </summary>
		/// <param name="stream"><see cref="T:System.IO.Stream" /></param>
		/// <param name="settings"><see cref="T:System.Xml.XmlWriterSettings" /></param>
		public XmlNoNamespaceWriter(Stream stream, XmlWriterSettings settings): this(XmlWriter.Create(stream, settings))
		{
		}

		/// <summary>
		/// Gets the <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this <see cref="T:System.Xml.XmlWriter" /> instance.
		/// </summary>
		/// <returns>The <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this writer instance. If this writer was not created using the <see cref="Overload:System.Xml.XmlWriter.Create" /> method, this property returns null.</returns>
		public override XmlWriterSettings Settings
		{
			get
			{
				return base.Settings;
			}
		}

		/// <summary>
		/// When overridden in a derived class, gets the state of the writer.
		/// </summary>
		/// <returns>One of the <see cref="T:System.Xml.WriteState" /> values.</returns>
		public override System.Xml.WriteState WriteState
		{
			get
			{
				return mXmlWriter.WriteState;
			}
		}

		/// <summary>
		/// When overridden in a derived class, closes this stream and the underlying stream.
		/// </summary>
		public override void Close()
		{
			mXmlWriter.Close();
		}

		/// <summary>
		/// When overridden in a derived class, flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
		/// </summary>
		public override void Flush()
		{
			mXmlWriter.Flush();
		}

		/// <summary>
		/// When overridden in a derived class, returns the closest prefix defined in the current namespace scope for the namespace URI.
		/// </summary>
		/// <param name="ns">The namespace URI whose prefix you want to find.</param>
		/// <returns>
		/// The matching prefix or null if no matching namespace URI is found in the current scope.
		/// </returns>
		public override String LookupPrefix(String ns)
		{
			return mXmlWriter.LookupPrefix(ns);
		}

		/// <summary>
		/// When overridden in a derived class, encodes the specified binary bytes as Base64 and writes out the resulting text.
		/// </summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			mXmlWriter.WriteBase64(buffer, index, count);
		}

		/// <summary>
		/// When overridden in a derived class, writes out a &lt;![CDATA[...]]&gt; block containing the specified text.
		/// </summary>
		/// <param name="text">The text to place inside the CDATA block.</param>
		public override void WriteCData(String text)
		{
			mXmlWriter.WriteCData(text);
		}

		/// <summary>
		/// When overridden in a derived class, forces the generation of a character entity for the specified Unicode character value.
		/// </summary>
		/// <param name="ch">The Unicode character for which to generate a character entity.</param>
		public override void WriteCharEntity(char ch)
		{
			mXmlWriter.WriteCharEntity(ch);
		}

		/// <summary>
		/// When overridden in a derived class, writes text one buffer at a time.
		/// </summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position in the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		public override void WriteChars(char[] buffer, int index, int count)
		{
			mXmlWriter.WriteChars(buffer, index, count);
		}

		/// <summary>
		/// When overridden in a derived class, writes out a comment &lt;!--...--&gt; containing the specified text.
		/// </summary>
		/// <param name="text">Text to place inside the comment.</param>
		public override void WriteComment(String text)
		{
			mXmlWriter.WriteComment(text);
		}

		/// <summary>
		/// When overridden in a derived class, writes the DOCTYPE declaration with the specified name and optional attributes.
		/// </summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty.</param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments.</param>
		/// <param name="sysid">If <paramref name="pubid" /> is null and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument.</param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument.</param>
		public override void WriteDocType(String name, String pubid, String sysid, String subset)
		{
			mXmlWriter.WriteDocType(name, pubid, sysid, subset);
		}

		/// <summary>
		/// When overridden in a derived class, closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)" /> call.
		/// </summary>
		public override void WriteEndAttribute()
		{
			if (mAttributeStack.Pop())
			{
				if (mXmlWriter.WriteState == WriteState.Attribute)
					mXmlWriter.WriteEndAttribute();
			}
		}

		/// <summary>
		/// When overridden in a derived class, closes any open elements or attributes and puts the writer back in the Start state.
		/// </summary>
		public override void WriteEndDocument()
		{
			mXmlWriter.WriteEndDocument();
		}

		/// <summary>
		/// When overridden in a derived class, closes one element and pops the corresponding namespace scope.
		/// </summary>
		public override void WriteEndElement()
		{
			mXmlWriter.WriteEndElement();
		}

		/// <summary>
		/// When overridden in a derived class, writes out an entity reference as &amp;name;.
		/// </summary>
		/// <param name="name">The name of the entity reference.</param>
		public override void WriteEntityRef(String name)
		{
			mXmlWriter.WriteEntityRef(name);
		}

		/// <summary>
		/// When overridden in a derived class, closes one element and pops the corresponding namespace scope.
		/// </summary>
		public override void WriteFullEndElement()
		{
			mXmlWriter.WriteFullEndElement();
		}

		/// <summary>
		/// When overridden in a derived class, writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.
		/// </summary>
		/// <param name="name">The name of the processing instruction.</param>
		/// <param name="text">The text to include in the processing instruction.</param>
		public override void WriteProcessingInstruction(String name, String text)
		{
			mXmlWriter.WriteProcessingInstruction(name, text);
		}

		/// <summary>
		/// When overridden in a derived class, writes raw markup manually from a string.
		/// </summary>
		/// <param name="data">String containing the text to write.</param>
		public override void WriteRaw(String data)
		{
			mXmlWriter.WriteRaw(data);
		}

		/// <summary>
		/// When overridden in a derived class, writes raw markup manually from a character buffer.
		/// </summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position within the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			mXmlWriter.WriteRaw(buffer, index, count);
		}

		/// <summary>
		/// When overridden in a derived class, writes the start of an attribute with the specified prefix, local name, and namespace URI.
		/// </summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI for the attribute.</param>
		public override void WriteStartAttribute(String prefix, String localName, String ns)
		{
			bool write = (!prefix.Equals("xmlns", StringComparison.OrdinalIgnoreCase) && !localName.Equals("xmlns", StringComparison.OrdinalIgnoreCase));

			// Ensure the XmlWellFormedWriter thinks the namespace has been written previously
			if (!String.IsNullOrEmpty(ns))
				mAddNamespace.Invoke(mXmlWriter, new Object[] { prefix, ns, 0 });

			if (write)
			{
				mXmlWriter.WriteStartAttribute(prefix, localName, null);
			}
			else
				mStripNextString = true;

			mAttributeStack.Push(write);
		}

		/// <summary>
		/// When overridden in a derived class, writes the XML declaration with the version "1.0".
		/// </summary>
		public override void WriteStartDocument()
		{
			mXmlWriter.WriteStartDocument();
		}

		/// <summary>
		/// When overridden in a derived class, writes the XML declaration with the version "1.0" and the standalone attribute.
		/// </summary>
		/// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no".</param>
		public override void WriteStartDocument(bool standalone)
		{
			mXmlWriter.WriteStartDocument(standalone);
		}

		/// <summary>
		/// When overridden in a derived class, writes the specified start tag and associates it with the given namespace and prefix.
		/// </summary>
		/// <param name="prefix">The namespace prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		public override void WriteStartElement(String prefix, String localName, String ns)
		{
			// Ensure the XmlWellFormedWriter thinks the namespace has been written previously
			if (!String.IsNullOrEmpty(ns))
				mAddNamespace.Invoke(mXmlWriter, new Object[] { prefix, ns, 0 });

			mXmlWriter.WriteStartElement(prefix, localName, ns);
		}

		/// <summary>
		/// When overridden in a derived class, writes the given text content.
		/// </summary>
		/// <param name="text">The text to write.</param>
		public override void WriteString(String text)
		{
			if (!mStripNextString)
				mXmlWriter.WriteString(text);

			mStripNextString = false;
		}

		/// <summary>
		/// When overridden in a derived class, generates and writes the surrogate character entity for the surrogate character pair.
		/// </summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF.</param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF.</param>
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			mXmlWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		/// <summary>
		/// When overridden in a derived class, writes out the given white space.
		/// </summary>
		/// <param name="ws">The string of white space characters.</param>
		public override void WriteWhitespace(String ws)
		{
			mXmlWriter.WriteWhitespace(ws);
		}
	}
}
