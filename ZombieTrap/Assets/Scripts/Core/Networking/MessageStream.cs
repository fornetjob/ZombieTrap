using System;
using System.IO;

namespace Assets.Scripts.Core.Networking
{
    public class MessageStream : Stream
    {
        #region Fields

        private long
            _lenght;

        private int
            _position;

        private MessageFragment[]
            _fragments;

        private int
            _fragmentSize;

        #endregion

        #region ctor

        public MessageStream(MessageFragment[] fragments)
        {
            _fragments = fragments;

            _fragmentSize = fragments[0].Data.Length;

            _lenght = 0;

            for (int i = 0; i < fragments.Length; i++)
            {
                _lenght += fragments[i].Data.Length;
            }
        }

        #endregion

        #region Properties

        public override bool CanRead { get { return true; } }

        public override bool CanSeek { get { return true; } }

        public override bool CanWrite { get { return false; } }

        public override long Length
        {
            get
            {
                return _lenght;
            }
        }

        public override long Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = (int)value;
            }
        }

        #endregion

        public override void Flush()
        {
            _position = 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_position == _lenght)
            {
                return 0;
            }

            var fragmentIndex = _position / _fragmentSize;
            var fragmentByteIndex = _position % _fragmentSize;

            var fragment = _fragments[fragmentIndex];
            var readSize = Math.Min(count, fragment.Data.Length - fragmentByteIndex);

            Array.Copy(fragment.Data, fragmentByteIndex, buffer, offset, readSize);

            _position += readSize;

            return readSize;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotSupportedException();
        }

        public override void SetLength(long value)
        {
            _lenght = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new System.NotSupportedException();
        }
    }
}
