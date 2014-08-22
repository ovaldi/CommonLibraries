#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.Data.IsolatedStorage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Data.Tests.IsolatedStorage
{
    [TestClass]
    public class DiskIsolatedStorageTests
    {
        string baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IsolatedStorage");
        [TestInitialize]
        public void TestInitialize()
        {
            if (Directory.Exists(baseDirectory))
            {
                Directory.Delete(baseDirectory, true);
            }
            Directory.CreateDirectory(baseDirectory);
        }

        [TestMethod]
        public void Test_Name()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);

            Assert.AreEqual("Test", storage.Name);
        }

        [TestMethod]
        public void Test_Quota()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);

            Assert.IsTrue(storage.Quota == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Test_UsedSize()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            Assert.AreEqual(0, storage.UsedSize);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Test_IncreaseQuotaTo()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);

            Random r = new Random();
            long newQuotaSize = r.Next();

            storage.IncreaseQuotaTo(newQuotaSize);
        }

        [TestMethod]
        public void Test_InitStore()
        {
            var storeName = Guid.NewGuid().ToString();
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storePath, storePath);
            storage.InitStore();

            Assert.IsTrue(Directory.Exists(storePath));
        }

        [TestMethod]
        public void Test_Remove()
        {
            var storeName = Guid.NewGuid().ToString();
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();
            storage.Remove();

            Assert.IsFalse(Directory.Exists(storePath));
        }

        [TestMethod]
        public void Test_CopyfFile_No_OverWrite()
        {
            var storeName = Guid.NewGuid().ToString();
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            string sourceFileName = "a.txt";
            string destinationFileName = "b.txt";

            string sourcePath = Path.Combine(storePath, sourceFileName);
            string destinationPath = Path.Combine(storePath, destinationFileName);

            //创建一个源文件
            if (File.Exists(sourcePath))
            {
                File.Delete(sourcePath);
            }
            using (FileStream fs = new FileStream(sourcePath, FileMode.CreateNew))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write("This is a.text");
                }
            }

            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }
            storage.CopyFile(sourceFileName, destinationFileName);

            Assert.IsTrue(File.Exists(destinationPath));
        }

        [TestMethod]
        public void Test_CopyFile_OverWrite()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            string sourceFileName = "a.txt";
            string destinationFileName = "b.txt";

            string sourcePath = Path.Combine(storePath, sourceFileName);
            string destinationPath = Path.Combine(storePath, destinationFileName);

            //创建一个源文件
            if (File.Exists(sourcePath))
            {
                File.Delete(sourcePath);
            }
            FileStream fs = new FileStream(sourcePath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write("This is a.text");
            sw.Close();
            fs.Close();

            //创建一个已存在的目标文件
            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }
            FileStream fs1 = new FileStream(destinationPath, FileMode.CreateNew);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.Write("This is b.text");
            sw1.Close();
            fs1.Close();


            if (File.Exists(destinationPath))
            {
                storage.CopyFile(sourceFileName, destinationFileName, true);
            }

            //判断目标文件是否被重写
            bool isTrue = false;
            foreach (string line in File.ReadLines(destinationPath))
            {
                if (line.Contains("This is a.text"))
                {
                    isTrue = true;
                }
            }

            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void Test_CreateDirectory()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            string dir = "Test" + Guid.NewGuid().ToString();
            storage.CreateDirectory(dir);

            string dirPath = Path.Combine(storePath, dir);
            Assert.IsTrue(Directory.Exists(dirPath));
        }

        [TestMethod]
        public void Test_CreateFile()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, 18);

            var filePath = Guid.NewGuid().ToString() + ".txt";

            storage.CreateFile(filePath, stream);

            var filePhysicalPath = Path.Combine(storePath, filePath);
            Assert.IsTrue(File.Exists(filePhysicalPath));
        }

        [TestMethod]
        public void Test_DeleteDirectory()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            string dir = "Test" + Guid.NewGuid().ToString();
            storage.CreateDirectory(dir);

            storage.DeleteDirectory(dir);

            string dirPath = Path.Combine(storePath, dir);
            Assert.IsFalse(Directory.Exists(dirPath));
        }

        public void Test_Create_SubDir()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();

            string dir = "Test_Create_SubDir" + Guid.NewGuid().ToString();
            string subDir = Path.Combine(dir, "Sub");

            storage.CreateDirectory(subDir);

            Assert.IsTrue(storage.DirectoryExists(subDir));

            storage.DeleteDirectory(subDir);

            Assert.IsFalse(storage.DirectoryExists(subDir));
        }

        [TestMethod]
        public void Test_DeleteFile()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //创建文件
            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, 18);
            var filePath = Guid.NewGuid().ToString() + ".txt";
            storage.CreateFile(filePath, stream);


            storage.DeleteFile(filePath);
            var filePhysicalPath = Path.Combine(storePath, filePath);
            Assert.IsFalse(File.Exists(filePhysicalPath));
        }

        [TestMethod]
        public void Test_DirectoryExists()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //创建目录
            string dir = "Test" + Guid.NewGuid().ToString();
            storage.CreateDirectory(dir);

            bool result = storage.DirectoryExists(dir);

            //删除目录
            storage.DeleteDirectory(dir);
            bool result1 = storage.DirectoryExists(dir);

            Assert.IsTrue(result);
            Assert.IsFalse(result1);
        }

        [TestMethod]
        public void Test_FileExists()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //创建文件
            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, 18);
            var filePath = Guid.NewGuid().ToString() + ".txt";
            storage.CreateFile(filePath, stream);
            bool result = storage.FileExists(filePath);

            //删除文件
            storage.DeleteFile(filePath);
            bool result1 = storage.FileExists(filePath);

            Assert.IsTrue(result);
            Assert.IsFalse(result1);
        }

        [TestMethod]
        public void Test_GetCreationTimeUtc()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //创建目录
            string dir = "Test" + Guid.NewGuid().ToString();
            storage.CreateDirectory(dir);
            var dirCreatedDate = Directory.GetCreationTimeUtc(Path.Combine(storePath, dir));

            //创建文件
            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, 18);
            var filePath = Guid.NewGuid().ToString() + ".txt";
            storage.CreateFile(filePath, stream);
            var fileCreatedDate = File.GetCreationTimeUtc(Path.Combine(storePath, filePath));

            var dirResult = storage.GetCreationTimeUtc(dir);
            var fileResult = storage.GetCreationTimeUtc(filePath);

            Assert.AreEqual(dirCreatedDate, dirResult);
            Assert.AreEqual(fileCreatedDate, fileResult);
        }

        [TestMethod]
        public void Test_GetDirectoryNames()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //删除Test目录下的所以文件和文件夹
            string[] strDirs = Directory.GetDirectories(storePath);
            string[] strFiles = Directory.GetFiles(storePath);
            foreach (var file in strFiles)
                File.Delete(file);
            foreach (var dir in strDirs)
                Directory.Delete(dir, true);

            //创建多个目录
            for (int i = 0; i < 5; i++)
            {
                storage.CreateDirectory("Dir/Di_" + Guid.NewGuid().ToString());
                string dir = "Dir_" + Guid.NewGuid().ToString();
                storage.CreateDirectory(dir);
            }

            var result1 = storage.GetDirectoryNames("Dir");
            var result2 = storage.GetDirectoryNames(null, "Dir_*");
            var result3 = storage.GetDirectoryNames("Dir", "Di_*");

            Assert.AreEqual(5, result1.Count());
            Assert.AreEqual(5, result2.Count());
            Assert.AreEqual(5, result3.Count());
        }

        [TestMethod]
        public void Test_GetFileNames()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //删除Test目录下的所以文件和文件夹
            string[] strDirs = Directory.GetDirectories(storePath);
            string[] strFiles = Directory.GetFiles(storePath);
            foreach (var file in strFiles)
                File.Delete(file);
            foreach (var dir in strDirs)
                Directory.Delete(dir, true);

            //创建多个文件
            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, 18);
            for (int i = 0; i < 5; i++)
            {
                var fileName = Guid.NewGuid().ToString() + ".txt";
                storage.CreateDirectory("fDir");
                storage.CreateFile("fDir/" + fileName, stream);
                storage.CreateFile(fileName, stream);
            }

            var result1 = storage.GetFileNames("fDir");
            var result2 = storage.GetFileNames(null, "*.txt");
            var result3 = storage.GetFileNames("fDir", "*.txt");

            Assert.AreEqual(5, result1.Count());
            Assert.AreEqual(5, result2.Count());
            Assert.AreEqual(5, result3.Count());
        }

        [TestMethod]
        public void Test_GetLastAccessTimeUtc()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //创建目录
            string dir = "Test" + Guid.NewGuid().ToString();
            storage.CreateDirectory(dir);
            var dirLastAccessTime = Directory.GetLastAccessTimeUtc(Path.Combine(storePath, dir));

            //创建文件
            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, 18);
            var filePath = Guid.NewGuid().ToString() + ".txt";
            storage.CreateFile(filePath, stream);
            FileStream fs = File.Open(Path.Combine(storePath, filePath), FileMode.Open);
            fs.Close();

            var fileLastAccessTime = File.GetLastAccessTimeUtc(Path.Combine(storePath, filePath));


            var dirResult = storage.GetLastAccessTimeUtc(dir);
            var fileResult = storage.GetLastAccessTimeUtc(filePath);

            Assert.AreEqual(dirLastAccessTime, dirResult);
            Assert.AreEqual(fileLastAccessTime, fileResult);
        }

        [TestMethod]
        public void Test_GetLastWriteTimeUtc()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //创建目录
            string dir = "Test" + Guid.NewGuid().ToString();
            storage.CreateDirectory(dir);
            //在目录里创建文件
            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, 18);
            var filePath = dir + "/" + Guid.NewGuid().ToString() + ".txt";
            storage.CreateFile(filePath, stream);
            //修改文件内容
            FileStream fs = File.Open(Path.Combine(storePath, filePath), FileMode.Open);
            Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
            fs.Write(info, 0, info.Length);
            fs.Close();

            var dirLastWriteTime = Directory.GetLastWriteTimeUtc(Path.Combine(storePath, dir));
            var fileLastWriteTime = File.GetLastWriteTimeUtc(Path.Combine(storePath, filePath));


            var dirResult = storage.GetLastWriteTimeUtc(dir);
            var fileResult = storage.GetLastWriteTimeUtc(filePath);

            Assert.AreEqual(dirLastWriteTime, dirResult);
            Assert.AreEqual(fileLastWriteTime, fileResult);
        }

        [TestMethod]
        public void Test_MoveDirectory()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //删除Test目录下的所以文件和文件夹
            string[] strDirs = Directory.GetDirectories(storePath);
            string[] strFiles = Directory.GetFiles(storePath);
            foreach (var file in strFiles)
                File.Delete(file);
            foreach (var dir in strDirs)
                Directory.Delete(dir, true);

            //创建两个目录
            storage.CreateDirectory("dir1");
            storage.CreateDirectory("dir2");

            //把dir2移到dir1中
            storage.MoveDirectory("dir2", "dir1/dir2");

            var resultPath = Path.Combine(storePath, "dir1/dir2");

            Assert.IsTrue(Directory.Exists(resultPath));
        }

        [TestMethod]
        public void Test_MoveFile()
        {
            var storage = new DiskIsolateStorage("Test", baseDirectory);
            storage.InitStore();
            string storePath = Path.Combine(baseDirectory, "Test");

            //删除Test目录下的所以文件和文件夹
            string[] strDirs = Directory.GetDirectories(storePath);
            string[] strFiles = Directory.GetFiles(storePath);
            foreach (var file in strFiles)
                File.Delete(file);
            foreach (var dir in strDirs)
                Directory.Delete(dir, true);

            //创建一个目录和一个文件
            storage.CreateDirectory("dir");
            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, 18);
            storage.CreateFile("file.txt", stream);

            storage.MoveFile("file.txt", "dir/file.txt");

            var resultPath = Path.Combine(storePath, "dir/file.txt");

            Assert.IsTrue(File.Exists(resultPath));
        }

        [TestMethod]
        public void Test_OpenFile_SaveFile()
        {
            string storePath = Path.Combine(baseDirectory, "Test");
            var storage = new DiskIsolateStorage("Test", storePath);
            storage.InitStore();

            //删除Test目录下的所以文件和文件夹
            string[] strDirs = Directory.GetDirectories(storePath);
            string[] strFiles = Directory.GetFiles(storePath);
            foreach (var file in strFiles)
                File.Delete(file);
            foreach (var dir in strDirs)
                Directory.Delete(dir, true);

            var fileName = "file.txt";
            //创建文件
            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = UTF8Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, buffer.Length);
            storage.CreateFile(fileName, stream);

            //获取文件名、文件路径。。
            using (var of = storage.OpenFile(fileName, FileMode.Open))
            {
                var filePath = of.StorageFile.FilePath;
                var storageName = of.StorageFile.StorageName;

                //读取文件内容
                byte[] bytes = new byte[of.Stream.Length];
                UTF8Encoding temp = new UTF8Encoding(true);
                of.Stream.Seek(0, SeekOrigin.Begin);
                of.Stream.Read(bytes, 0, bytes.Length);
                string readStr = temp.GetString(bytes);
                Assert.AreEqual(testString, readStr);
            }
            //写文件并保存
            string writeString = "This is another teststring";
            byte[] buffer1 = UTF8Encoding.Default.GetBytes(writeString);
            using (var memoryStream = new MemoryStream(buffer1))
            {
                storage.UpdateFile(fileName, memoryStream);
            }

            //再读取文件内容
            using (var of1 = storage.OpenFile("file.txt", FileMode.Open))
            {
                byte[] b = new byte[of1.Stream.Length];
                UTF8Encoding temp1 = new UTF8Encoding(true);
                of1.Stream.Seek(0, SeekOrigin.Begin);
                of1.Stream.Read(b, 0, b.Length);
                string readStr1 = temp1.GetString(b);

                Assert.AreEqual(writeString, readStr1);
            }



        }
    }
}
