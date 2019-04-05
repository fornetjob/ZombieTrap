using System;
using System.IO;

namespace Assets.Scripts.Core.Networking
{
    public class MessageFragmenter
    {
        public const int FragmentSizeWithHeader = 1200;
        private const int FragmentSize = FragmentSizeWithHeader - MessageFragment.HeaderSize;

        private MemoryStream
            _stream = new MemoryStream();

        public MessageContract Defragment(params MessageFragment[] fragments)
        {
            using (var stream = new MessageStream(fragments))
            {
                return ProtoBuf.Serializer.Deserialize<MessageContract>(stream);
            }
        }

        public MessageFragment[] Fragment(MessageContract contract)
        {
            _stream.Position = 0;
            _stream.SetLength(0);

            ProtoBuf.Serializer.Serialize(_stream, contract);

            var bytes = _stream.ToArray();

            int fragmentCount = (int)((bytes.Length + (FragmentSize - 1)) / FragmentSize);

            var fragments = new MessageFragment[fragmentCount];

            for (int i = 0; i < fragmentCount; i++)
            {
                var data = new byte[FragmentSizeWithHeader];

                data[0] = (byte)i;
                data[1] = (byte)(i >> 8);
                data[2] = (byte)fragmentCount;
                data[3] = (byte)(fragmentCount >> 8);

                var offset = i * FragmentSize;

                var lenght = Math.Min(bytes.Length - offset, FragmentSize);

                Array.Copy(bytes, offset, data, MessageFragment.HeaderSize, lenght);

                fragments[i] = new MessageFragment(data);
            }

            return fragments;
        }
    }
}
