using System;
using System.Collections.Generic;

namespace SharedResources
{
    public enum QuestionType
    {
        Stars=0,
        Smiley=1,
        Slider=2
    }

    public enum ErrorTypes
    {
        SqlError,
        NullValueError,
        LoggingError,
        OperationError,
        DataFetchingError,
        UnAuthorizedAccessError,
        UnknownError
    }


    public class SharedData
    {
        /// <summary>
        /// this class contains resources used across the application layers
        /// such as enums or constants
        /// </summary>


        public static Dictionary<ErrorTypes, string> ErrorMessages = new Dictionary<ErrorTypes, string>
        {
            {ErrorTypes.SqlError, "Database related error, refer to your system admin"},
            {ErrorTypes.UnknownError, "An Unknown error occured"},
            {ErrorTypes.UnAuthorizedAccessError, "You have restrictions on file operations please refer to your system admin"},
            {ErrorTypes.NullValueError, "couldn't get the requested data"},
            {ErrorTypes.OperationError, "Operation Failed, please try again"},
            {ErrorTypes.DataFetchingError, "An error occured while loading data please try again.\nif you keep getting this error restart the app"},
            {ErrorTypes.LoggingError, "An error occured while logging the error"}
        };
    }
}
