using Assets.Scripts.Core.Networking;

using System;
using System.Collections.Generic;

namespace Assets.Scripts.Features.Client.Networking
{
    public class ConcurrentReceiveStack
    {
        #region Fields

        private object
            _lockObj = new object();

        private Dictionary<ulong, MessageContract>
            _messages = new Dictionary<ulong, MessageContract>();

        private ulong
            _minMessageId = ulong.MaxValue;

        private ulong
            _currentMessageId;

        #endregion

        #region Properties

        public int Count
        {
            get
            {
                return _messages.Count;
            }
        }

        #endregion

        #region Public methods

        public void Push(MessageContract message)
        {
            lock (_lockObj)
            {
                // Пришло старое сообщение
                if (message.Id < _currentMessageId || _messages.ContainsKey(message.Id))
                {
                    return;
                }

                _minMessageId = Math.Min(_minMessageId, message.Id);

                _messages.Add(message.Id, message);
            }
        }

        public bool TryPopMessage(out MessageContract message)
        {
            lock (_lockObj)
            {
                if (_currentMessageId == _minMessageId)
                {
                    message = Pop();

                    _currentMessageId++;

                    return true;
                }

                message = null;

                return false;
            }
        }

        #endregion

        #region Private methods

        private MessageContract Pop()
        {
            var currentMessage = _messages[_currentMessageId];

            _messages.Remove(currentMessage.Id);

            _minMessageId = uint.MaxValue;

            foreach (var message in _messages.Values)
            {
                _minMessageId = Math.Min(_minMessageId, message.Id);
            }

            return currentMessage;
        }

        #endregion
    }
}