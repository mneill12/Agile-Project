using System;
using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class ChatMessage : ObjectBase
    {
        private int _MessageId;
        private DateTime _Time;
        private Account _Sender;
        private string _Message;

        public int MessageId
        {
            get { return _MessageId; }
            set
            {
                if (_MessageId != value)
                {
                    _MessageId = value;
                    OnPropertyChanged(() => MessageId);
                }
            }
        }

        public DateTime Time
        {
            get { return _Time; }
            set
            {
                if (_Time != value)
                {
                    _Time = value;
                    OnPropertyChanged(() => Time);
                }
            }
        }

        public Account Sender
        {
            get { return _Sender; }
            set
            {
                if (_Sender != value)
                {
                    _Sender = value;
                    OnPropertyChanged(() => Sender);
                }
            }
        }

        public string Message
        {
            get { return _Message; }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged(() => Message);
                }
            }
        }
    }
}