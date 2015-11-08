using System;

namespace Core.Common
{
    public class ErrorMessageEventArgs : EventArgs
    {
        public ErrorMessageEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}