using System;
using System.IO;
using System.Text;

namespace voileapp
{
    public class FileCopy
    {
        
        private String pathName;

        public const String BACKUP_FOLDER = "C:\\TestBackup\\";

        // Constructor
        public FileCopy(String pathName)
        {
            if (File.Exists(pathName))
            {
                this.pathName = pathName;
            }
            else
            {
                throw new IOException("Specified file does not exist!");
            }
            
        }

        /**
         * Removes the target file specified in pathName field
         * and replaces it with the file, specified in parameter
         * It's mostly self-descriptive.
         **/
        public void CopyExecutive(String path)
        {
            
            try
            {
                File.Delete(pathName);

                // Overwrite enabled
                File.Copy(path, pathName, true);
            }
            catch (IOException e)
            {
                Console.WriteLine("IO Exception during replacing with the executive!");
                Console.WriteLine(e.StackTrace);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Unauthorized access!");
                Console.WriteLine(e.StackTrace);
            }

        }

        /**
         * Saving the specified file in the backup directory
         **/
        public void MakeBackup()
        {

            try
            {
                // If there's already a directory, this will simply try to return it
                Directory.CreateDirectory(BACKUP_FOLDER);
        
                // Overwriting isn't allowed to prevent backup files corruption
                File.Copy(pathName, BACKUP_FOLDER + Path.GetFileName(pathName), false);
            }
            catch (IOException e)
            {
                Console.WriteLine("IO Exception during backup!");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
