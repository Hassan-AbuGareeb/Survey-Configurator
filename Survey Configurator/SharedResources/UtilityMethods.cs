using SharedResources.models;
using System;

namespace SharedResources
{
     public class UtilityMethods
    {
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
                string tDirectoryPath = Directory.GetCurrentDirectory() + "\\errorlogs";
                if (!Directory.Exists(tDirectoryPath))
                {
                    Directory.CreateDirectory(tDirectoryPath);
                }

                string tFilePath = tDirectoryPath + "\\errorlog.txt";
                if (!File.Exists(tFilePath))
                {
                    //create the file if it doesn't exist
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
                Console.WriteLine($"Error occurred while logging: {ex.Message}");
            }
        }
    }
}
