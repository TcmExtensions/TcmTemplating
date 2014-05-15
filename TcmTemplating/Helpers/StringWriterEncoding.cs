#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: StringWriterWithEncoding
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.IO;
using System.Text;

namespace TcmTemplating.Helpers
{
	/// <summary>
	/// <see cref="T:StringWriterEncoding" /> allows creation of <see cref="T:System.IO.StringWriter" /> while specifying
	/// <see cref="T:System.Text.Encoding" />
	/// </summary>
	/// <remarks>The default encoding is UTF8</remarks>
    public class StringWriterEncoding : StringWriter
    {
        private readonly Encoding encoding;
        public StringWriterEncoding() : base() { encoding = Encoding.UTF8; }
        public StringWriterEncoding(IFormatProvider formatProvider) : base(formatProvider) { }
        public StringWriterEncoding(StringBuilder sb) : base(sb) { }
        public StringWriterEncoding(StringBuilder sb, IFormatProvider formatProvider) : base(sb, formatProvider) { }
        public StringWriterEncoding(Encoding newEncoding) : base() { encoding = newEncoding; }
        public StringWriterEncoding(IFormatProvider formatProvider, Encoding newEncoding) : base(formatProvider) { encoding = newEncoding; }
        public StringWriterEncoding(StringBuilder sb, IFormatProvider formatProvider, Encoding newEncoding) : base(sb, formatProvider) { encoding = newEncoding; }
        public StringWriterEncoding(StringBuilder sb, Encoding newEncoding) : base(sb) { encoding = newEncoding; }
        public override Encoding Encoding { get { return encoding ?? base.Encoding; } }
    }
}
