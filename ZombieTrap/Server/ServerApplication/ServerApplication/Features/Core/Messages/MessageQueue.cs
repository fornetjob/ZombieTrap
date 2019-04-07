using Assets.Scripts.Core.Networking;
using System.Collections.Generic;

namespace ServerApplication.Features.Core.Messages
{
    public class MessageQueue
    {
        #region Fields

        private object _lockObj = new object();

        private List<MessageContract>
            _messages = new List<MessageContract>();

        #endregion

        #region Properties

        public int Count
        {
            get { return _messages.Count; }
        }

        #endregion

        #region Public methods

        public void Enqueue(MessageContract msg)
        {
            lock (_lockObj)
            {
                if (msg.Type.IsStrongMessage() == false)
                {
                    Remove(msg.Type);
                }

                _messages.Add(msg);
            }
        }

        public MessageContract Peek()
        {
            lock (_lockObj)
            {
                if (Count == 0)
                {
                    return null;
                }

                return _messages[0];
            }
        }

        public MessageContract Dequeue()
        {
            lock (_lockObj)
            {
                var msg = _messages[0];

                _messages.RemoveAt(0);

                return msg;
            }
        }

        #endregion

        #region Private methods

        private void Remove(MessageType type)
        {
            for (int i = 0; i < _messages.Count; i++)
            {
                var msg = _messages[i];

                if (msg.Type == type)
                {
                    _messages.RemoveAt(i);

                    i--;
                }
            }
        }

        #endregion
    }
}
