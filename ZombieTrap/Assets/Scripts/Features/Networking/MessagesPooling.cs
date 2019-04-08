using Game.Core.Networking;

using System.Collections.Generic;

namespace Assets.Scripts.Features.Networking
{
    public class MessagesPooling:IDependency
    {
        #region Fields

        private object
            _lockObj = new object();

        private Queue<MessageContract>
            _msgQueue = new Queue<MessageContract>();

        private ulong
            _minMessageId = 0;

        #endregion

        #region Properties

        public int Count
        {
            get
            {
                return _msgQueue.Count;
            }
        }

        #endregion

        #region Public methods

        public void Clear()
        {
            lock (_lockObj)
            {
                _msgQueue.Clear();
            }
        }

        public void Push(MessageContract msg)
        {
            lock (_lockObj)
            {
                if (msg.Type.IsStrongMessage() == false)
                {
                    // Пришло старое сообщение, пропустим
                    if (msg.Id < _minMessageId)
                    {
                        return;
                    }
                }

                _minMessageId = msg.Id;

                _msgQueue.Enqueue(msg);
            }
        }

        public bool TryPopMessage(out MessageContract message)
        {
            lock (_lockObj)
            {
                if (_msgQueue.Count > 0)
                {
                    message = _msgQueue.Dequeue();
                }

                message = null;

                return false;
            }
        }

        #endregion
    }
}