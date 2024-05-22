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
        /// <summary>
        /// this class is used to implement the Operation result pattern where
        /// each request or function to anywhere outside of the application like
        /// a database or API would return a result of failure or success 
        /// and the data can be changed by refernece.
        /// </summary>

        public ErrorTypes Error { get; set; }
        public string ErrorMessage {  get; set; }
        public bool IsSuccess { get; set; }

        //successful operation
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

        //failed operation with error type and message
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
