using System;
using System.IO;

namespace Assets.Scripts.Core.Networking
{
    public class MessageStream : Stream
    {
        #region Fields

        private long
            _realLenghth;

        private int
            _realPosition;

        private MessageFragment[]
            _fragments;

        private int
            _fragmentSize;

        private int
            _realFragmentSize;

        #endregion

        #region ctor

        public MessageStream(MessageFragment[] fragments)
        {
            _fragments = fragments;

            _fragmentSize = fragments[0].Data.Length;
            _realFragmentSize = _fragmentSize - MessageFragment.HeaderSize;

            _realLenghth = 0;

            for (int i = 0; i < fragments.Length; i++)
            {
                _realLenghth += fragments[i].Data.Length - MessageFragment.HeaderSize;
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
                return _realLenghth;
            }
        }

        public override long Position
        {
            get
            {
                return _realPosition;
            }
            set
            {
                _realPosition = (int)value;
            }
        }

        #endregion

        public override void Flush()
        {
            _realPosition = 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var fragmentIndex = _realPosition / _realFragmentSize;
            var fragmentByteIndex = (_realPosition + fragmentIndex * MessageFragment.HeaderSize) % _fragmentSize;

            var fragment = _fragments[fragmentIndex];
            var readSize = Math.Min(count, fragment.Data.Length - fragmentByteIndex);

            Array.Copy(fragment.Data, fragmentByteIndex, buffer, offset, readSize);

            _realPosition += readSize;

            return readSize;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotSupportedException();
        }

        public override void SetLength(long value)
        {
            _realLenghth = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new System.NotSupportedException();
        }
    }
}
