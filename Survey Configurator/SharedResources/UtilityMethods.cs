using SharedResources.models;
using System;

namespace SharedResources
{
     public class UtilityMethods
    {

        /// <summary>
        /// Utility methods used across the application layers
        /// </summary>

        //constants
        private const string cErrorLogFileName = "\\errorlogs.txt";

        /// <summary>
        /// Logs any exception in a ErrorLog text file in the application folder
        /// </summary>
        /// <param name="pExceptionData">data of the recieved Exception</param>
        public static void LogError(Exception pExceptionData)
        {
            try
            {
                //collect the error info to log to the file
                string[] tExceptionDetails = [
                    $"{DateTime.Now.ToUniversalTime()} UTC",
                    $"Exception: {pExceptionData.GetType().Name}",
                    $"Exception message: {pExceptionData.Message}",
                    $"Stack trace:\n{pExceptionData.StackTrace}"];
                //check that file exists
                string tDirectoryPath = Directory.GetCurrentDirectory() + cErrorLogFileName;
                if (!Directory.Exists(tDirectoryPath))
                {
                    Directory.CreateDirectory(tDirectoryPath);
                }

                //create the file if it doesn't exist
                string tFilePath = tDirectoryPath + cErrorLogFileName;
                if (!File.Exists(tFilePath))
                {
                    FileStream fs = File.Create(tFilePath);
                    fs.Close();
                }

                //add the default values to the file
                StreamWriter tWriter = File.AppendText(tFilePath);
                tWriter.WriteLine(string.Join(",\n", tExceptionDetails) + "\n\n--------\n");
                tWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(SharedData.ErrorMessages[ErrorTypes.LoggingError] + $": {ex.Message}");
            }
        }
    }
}
