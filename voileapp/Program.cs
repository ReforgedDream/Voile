using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voileapp
{
    class Program
    {
        static void Main(string[] args)
        {
            char dirSeparator = Path.DirectorySeparatorChar;
            String sysRoot = Environment.ExpandEnvironmentVariables("%SystemRoot%");
            String sysDir = Environment.SystemDirectory;
            String programFilesDir = Environment.ExpandEnvironmentVariables("%ProgramFiles%");

            StringBuilder backupPath = new StringBuilder("C:").Append(dirSeparator).Append("TestBackup");
            String twain32Name = "twain_32.dll";
            String nslookupName = "nslookup.exe";
            String iexploreName = "iexplore.exe";
            String iexploreDir = "Internet Explorer";
            
            StringBuilder twain32 = new StringBuilder(sysRoot).Append(dirSeparator).Append(twain32Name);
            StringBuilder nslookup = new StringBuilder(sysDir).Append(dirSeparator).Append(nslookupName);
            StringBuilder iexplore = new StringBuilder(programFilesDir).Append(dirSeparator).Append(iexploreDir)
                .Append(dirSeparator).Append(iexploreName);

            Console.WriteLine("Hey");

            Directory.CreateDirectory(backupPath.ToString());
            Console.WriteLine("Backup path created");

            backupPath.Append(dirSeparator);
            File.Copy(twain32.ToString(), backupPath.ToString() + twain32Name, true);
            File.Copy(nslookup.ToString(), backupPath.ToString() + nslookupName, true);
            File.Copy(iexplore.ToString(), backupPath.ToString() + iexploreName, true);
            Console.WriteLine("Backups created");
        }
    }
}
