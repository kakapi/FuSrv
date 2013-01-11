using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace FuLib
{
   public class IOHelper
    {
       public static  bool IsFileLocked(FileInfo file)
       {
           FileStream stream = null;

           try
           {
               stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
           }
           catch (IOException)
           {
               //the file is unavailable because it is:
               //still being written to
               //or being processed by another thread
               //or does not exist (has already been processed)
               return true;
           }
           finally
           {
               if (stream != null)
                   stream.Close();
           }

           //file is not locked
           return false;
       }
       public static string RealFile(string filePath)
       {
           string result  = File.ReadAllText(filePath);
           
           return result;
       }
       public static void WriteFile(string filePath, string content)
       {
           
               File.WriteAllText(filePath, content);
              
       }
    }
}
