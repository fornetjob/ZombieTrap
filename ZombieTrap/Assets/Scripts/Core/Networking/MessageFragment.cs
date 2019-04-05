using System;

namespace Assets.Scripts.Core.Networking
{
    public class MessageFragment
    {
        public const int HeaderSize = 4;

        public MessageFragment(byte[] data)
        {
            Data = data;
        }

        public ushort Index { get { return BitConverter.ToUInt16(Data, 0); } }
        public ushort Count { get { return BitConverter.ToUInt16(Data, 0 + 2); } }

        public byte[] Data;
    }
}
