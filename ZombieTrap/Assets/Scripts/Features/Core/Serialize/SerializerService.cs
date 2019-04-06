using Assets.Scripts.Core.Networking;
using System;
using System.IO;

public class SerializerService : IService
{
    private const int FragmentSize = 1200 - MessageFragment.FragmentHeaderSize;

    private MemoryStream
        _stream = new MemoryStream();

    public T Deserialize<T>(byte[] data)
    {
        _stream.Position = 0;
        _stream.SetLength(0);
        _stream.Write(data, 0, data.Length);
        _stream.Position = 0;

        return ProtoBuf.Serializer.Deserialize<T>(_stream);
    }

    public byte[] Serialize<T>(T item)
    {
        _stream.Position = 0;
        _stream.SetLength(0);

        ProtoBuf.Serializer.Serialize(_stream, item);

        return _stream.ToArray();
    }

    public MessageContract Defragment(params MessageFragment[] fragments)
    {
        using (var stream = new MessageStream(fragments))
        {
            return ProtoBuf.Serializer.Deserialize<MessageContract>(stream);
        }
    }

    public MessageFragment[] Fragment(MessageContract contract)
    {
        var bytes = Serialize(contract);

        ushort fragmentCount = (ushort)((bytes.Length + (FragmentSize - 1)) / FragmentSize);

        var fragments = new MessageFragment[fragmentCount];

        for (ushort i = 0; i < fragmentCount; i++)
        {
            var offset = i * FragmentSize;

            var lenght = Math.Min(bytes.Length - offset, FragmentSize);

            var data = new byte[lenght];

            Array.Copy(bytes, offset, data, 0, lenght);

            fragments[i] = new MessageFragment
            {
                Index = i,
                Count = fragmentCount,
                Data = data
            };
        }

        return fragments;
    }
}