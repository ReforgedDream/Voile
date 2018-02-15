using System;
using System.IO;
using System.Text;

namespace voileapp
{
    class Backup
    {
        /**
         * Saving the specified file in the backup directory
         * One must specify the name by which the file will be stored
         **/
        public static void MakeBackup(String path, String fileName)
        {
            StringBuilder backupFolder = new StringBuilder("C:").Append("\\").Append("TestBackup");
            
            try
            {
                // If there's already a directory, this will simply try to return it
                Directory.CreateDirectory(backupFolder.ToString());

                backupFolder.Append("\\").Append(fileName);

                // Overwriting isn't allowed to prevent backup files corruption
                File.Copy(path, backupFolder.ToString(), false);
            }
            catch (IOException e)
            {
                Console.WriteLine("IO Exception during backup!");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
