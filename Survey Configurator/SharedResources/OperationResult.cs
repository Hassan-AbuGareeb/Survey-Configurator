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
            try
            {
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
            }
        }
        public OperationResult(ErrorTypes pError, string pErrorMessage)
        {
            try
            {
                IsSuccess = false;
                ErrorMessage = pErrorMessage;
                Error = pError;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
            }
        }

    }
}
