using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Zad2
{
    public class CaesarStream : Stream
    {
        private readonly Stream _decoratedStream;
        private int _shift;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decoratedStream">Stream to decorate</param>
        /// <param name="shift">How much to shift every byte?</param>
        public CaesarStream(Stream decoratedStream, int shift)
        {
            _decoratedStream = decoratedStream;
            _shift = shift;
        }

        public override void Flush()
        {
            _decoratedStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _decoratedStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _decoratedStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var read = _decoratedStream.Read(buffer, offset, count);
            for (var i = 0; i < read; i++)
            {
                buffer[i] = EncodeByte(buffer[i]);
            }
            return read;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var encodedBuffer = buffer.Select(EncodeByte).ToArray();
            
            _decoratedStream.Write(encodedBuffer, offset, count);
        }

        private byte EncodeByte(byte b)
        {
            var encoded = (b + _shift) % (byte.MaxValue + 1);
            return (byte)encoded;
        }

        public override bool CanRead
        {
            get { return _decoratedStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _decoratedStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _decoratedStream.CanWrite; }
        }

        public override long Length
        {
            get { return _decoratedStream.Length; }
        }

        public override long Position { get { return _decoratedStream.Position; } set { _decoratedStream.Position = value; } }
    }
}
