using System;
using System.IO;

namespace voileapp
{
    class FileCopy
    {
        /**
         * Removes the target file specified in path parameter
         * and replaces it with the instance of this program's executive.
         * It's mostly self-descriptive.
         **/
        public static void CopyExecutive(String path)
        {
            String pathToExe = System.Reflection.Assembly.GetEntryAssembly().Location;

            try
            {
                File.Delete(path);

                // Overwrite enabled
                File.Copy(pathToExe, path, true);
            }
            catch (IOException e)
            {
                Console.WriteLine("IO Exception during replacing with the executive!");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
