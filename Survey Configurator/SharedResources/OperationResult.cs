using System;

namespace SharedResources
{
    public enum ErrorTypes
    {
        SqlError,
        IOError,
        NullValueError,
        UnAuthorizedAccessException,
        UnknownError
    }
    public class OperationResult
    {
        public ErrorTypes Error { get; set; }
        public string ErrorMessage {  get; set; }
        public bool IsSuccess { get; set; }


        public OperationResult()
        {
            IsSuccess = true;
        }
        public OperationResult(ErrorTypes pError, string pErrorMessage)
        {
            IsSuccess = false;
            ErrorMessage = pErrorMessage;
            Error = pError;
        }

    }
}
