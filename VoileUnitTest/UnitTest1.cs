using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using voileapp;

namespace VoileUnitTest
{
    [TestClass]
    public class UnitTest1
    {

        // Path to file used for testing
        private static String testFileName = "\\testfile.txt";
        private static String pathToTestFile = Environment.ExpandEnvironmentVariables("%userprofile%") + testFileName;
        private static String testContent = "The file content is all correct";

        [TestMethod]
        public void FileCopy_MakesBackup()
        {
            
            // Create the test file
            CreateTestFile(pathToTestFile, testContent);

            //Create new FileCopy object with the test file's path
            FileCopy fcObj = new FileCopy(pathToTestFile);
            fcObj.MakeBackup();

            try
            {
                // Compare the content of the file with original test
                Assert.IsTrue(Utils.FileCompare(pathToTestFile, FileCopy.BACKUP_FOLDER + testFileName), "Backup invalid");
            }
            finally
            {
                // Delete test file
                if (File.Exists(pathToTestFile))
                {
                    File.Delete(pathToTestFile);
                }
            }
            
        }

        /**
         * WORK IN PROGRESS 
         **/
        /*
        [TestMethod]
        public void FileCopy_CopyExecutive()
        {
            // Create the test file
            CreateTestFile(pathToTestFile, testContent);

            //Create new FileCopy object with the test file's path
            FileCopy fcObj = new FileCopy(pathToTestFile);
            fcObj.CopyExecutive();
            
        }
        */

        private void CreateTestFile(String path, String content)
        {
            // Create the test file
            using (FileStream fs = File.Create(path))
            {
                // Add some information to the file
                Byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
