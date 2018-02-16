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

        [TestMethod]
        public void FileCopy_MakesBackup()
        {
            
            // Path to file used for testing
            String pathToTestFile = Environment.ExpandEnvironmentVariables("%userprofile%");

            pathToTestFile = pathToTestFile + "\\testfile.txt";

            String testContent = "The file content is all correct";
            String actualContent = null;

            // Create the test file
            using (FileStream fs = File.Create(pathToTestFile))
            {
                // Add some information to the file
                Byte[] info = new UTF8Encoding(true).GetBytes(testContent);
                fs.Write(info, 0, info.Length);
            }

            //Create new FileCopy object with the test file's path
            FileCopy fcObj = new FileCopy(pathToTestFile);
            fcObj.MakeBackup();

            // Open the stream and read backed up file
            using (StreamReader sr = File.OpenText(pathToTestFile))
            {
                actualContent = sr.ReadLine();
            }

            // Compare the content of the file with original test
            Assert.AreEqual(actualContent, testContent, "Backup invalid");

            // Delete test file
            if (File.Exists(pathToTestFile))
            {
                File.Delete(pathToTestFile);
            }
        }
    }
}
