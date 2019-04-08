using Game.Core.Networking;
using Assets.Scripts.Features.Networking;
using NUnit.Framework;

public class ConcurrentReceiveStackTest
{
    [Test]
    public void Stack()
    {
        var stack = new ConcurrentReceiveStack();

        stack.Push(new MessageContract
        {
            Id = 1
        });

        Assert.AreEqual(stack.Count, 1);

        stack.Push(new MessageContract
        {
            Id = 2
        });

        Assert.AreEqual(stack.Count, 2);

        stack.Push(new MessageContract
        {
            Id = 3
        });

        Assert.AreEqual(stack.Count, 3);

        MessageContract message;

        Assert.IsFalse(stack.TryPopMessage(out message));

        stack.Push(new MessageContract
        {
            Id = 0
        });

        Assert.AreEqual(stack.Count, 4);

        Assert.IsTrue(stack.TryPopMessage(out message));
        Assert.IsTrue(stack.TryPopMessage(out message));
        Assert.IsTrue(stack.TryPopMessage(out message));
        Assert.IsTrue(stack.TryPopMessage(out message));

        Assert.AreEqual(stack.Count, 0);

        Assert.IsFalse(stack.TryPopMessage(out message));

        stack.Push(new MessageContract
        {
            Id = 1
        });

        Assert.AreEqual(stack.Count, 0);

        stack.Push(new MessageContract
        {
            Id = 3
        });

        Assert.AreEqual(stack.Count, 0);

        stack.Push(new MessageContract
        {
            Id = 4
        });

        Assert.AreEqual(stack.Count, 1);

        Assert.IsTrue(stack.TryPopMessage(out message));

        Assert.AreEqual(stack.Count, 0);
    }
}