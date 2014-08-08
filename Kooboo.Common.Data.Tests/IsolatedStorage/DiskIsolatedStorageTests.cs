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
        static string baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IsolatedStorage");
        [TestInitialize]
        public void TestInitialize()
        {
            if (Directory.Exists(baseDirectory))
            {
                Directory.Delete(baseDirectory, true);
            }
            Directory.CreateDirectory(baseDirectory);
        }

        //生成测试用的文件和目录
        public static void CreateFileAndDir()
        {
            var storePath = Path.Combine(baseDirectory, "Test");


            //创建一个源文件a.txt
            using (FileStream fs = new FileStream(Path.Combine(storePath, "a.txt"), FileMode.CreateNew))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write("This is a.text");
                }
            }

            //创建一个已存在的目标文件不.txt
            using (FileStream fs = new FileStream(Path.Combine(storePath, "b.txt"), FileMode.CreateNew))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write("This is b.text");
                }
            }

            //创建目录
            Directory.CreateDirectory(Path.Combine(storePath, "testDir"));

            //创建多个目录和多个文件
            for (int i = 0; i < 5; i++)
            {
                //目录
                Directory.CreateDirectory(Path.Combine(storePath, "Dir/Di_" + Guid.NewGuid().ToString()));
                string dir = "Dir_" + Guid.NewGuid().ToString();
                Directory.CreateDirectory(Path.Combine(storePath, dir));

                //文件
                var fileName = Guid.NewGuid().ToString() + ".txt";
                Directory.CreateDirectory(Path.Combine(storePath, "fDir"));
                using (FileStream fs = new FileStream(Path.Combine(storePath, "fDir/" + fileName), FileMode.CreateNew))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write("This is test string");
                    }
                }
                using (FileStream fs = new FileStream(Path.Combine(storePath, fileName), FileMode.CreateNew))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write("This is test string");
                    }
                }
            }
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
            var storage = new DiskIsolateStorage(storeName, storePath);
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
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            //文件名
            string sourceFileName = "a.txt";
            string destinationFileName = "b.txt";

            //文件路径
            string sourcePath = Path.Combine(storePath, sourceFileName);
            string destinationPath = Path.Combine(storePath, destinationFileName);

            //创建用于测试的文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();


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
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            //文件名
            string sourceFileName = "a.txt";
            string destinationFileName = "b.txt";

            //文件路径
            string sourcePath = Path.Combine(storePath, sourceFileName);
            string destinationPath = Path.Combine(storePath, destinationFileName);

            //创建用于测试的文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();

            //拷贝到已存在的文件，并覆盖其内容
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
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            string dirName = "Test" + Guid.NewGuid().ToString();
            storage.CreateDirectory(dirName);

            string dirPath = Path.Combine(storePath, dirName);
            Assert.IsTrue(Directory.Exists(dirPath));
        }

        [TestMethod]
        public void Test_CreateFile()
        {
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            //创建流存储字符串
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
        public void Test_DeleteDirectory_And_DirectoryExists()
        {
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            //创建测试所需文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();
            bool result = storage.DirectoryExists("testDir");

            storage.DeleteDirectory("testDir");

            string dirPath = Path.Combine(storePath, "testDir");
            Assert.IsTrue(result);
            Assert.IsFalse(Directory.Exists(dirPath));
        }

        [TestMethod]
        public void Test_DeleteFile_And_FileExists()
        {
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            //创建测试所需文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();

            bool result = storage.FileExists("a.txt");

            storage.DeleteFile("a.txt");
            var filePhysicalPath = Path.Combine(storePath, "a.txt");

            Assert.IsTrue(result);
            Assert.IsFalse(File.Exists(filePhysicalPath));
        }


        [TestMethod]
        public void Test_GetCreationTimeUtc()
        {
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            //创建测试所需文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();

            var dirCreatedDate = Directory.GetCreationTimeUtc(Path.Combine(storePath, "testDir"));
            var fileCreatedDate = File.GetCreationTimeUtc(Path.Combine(storePath, "a.txt"));

            var dirResult = storage.GetCreationTimeUtc("testDir");
            var fileResult = storage.GetCreationTimeUtc("a.txt");

            Assert.AreEqual(dirCreatedDate, dirResult);
            Assert.AreEqual(fileCreatedDate, fileResult);
        }

        [TestMethod]
        public void Test_GetDirectoryNames()
        {
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            //创建测试所需文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();
            
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
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();

            //创建测试所需文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();

            var result1 = storage.GetFileNames("fDir");
            var result2 = storage.GetFileNames(null, "*.txt");
            var result3 = storage.GetFileNames("fDir", "*.txt");

            Assert.AreEqual(5, result1.Count());
            Assert.AreEqual(7, result2.Count());
            Assert.AreEqual(5, result3.Count());
        }

        [TestMethod]
        public void Test_GetLastAccessTimeUtc()
        {
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();


            //创建测试所需文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();
            
            var dirLastAccessTime = Directory.GetLastAccessTimeUtc(Path.Combine(storePath, "testDir"));
            var fileLastAccessTime = File.GetLastAccessTimeUtc(Path.Combine(storePath, "a.txt"));


            var dirResult = storage.GetLastAccessTimeUtc("testDir");
            var fileResult = storage.GetLastAccessTimeUtc("a.txt");

            Assert.AreEqual(dirLastAccessTime, dirResult);
            Assert.AreEqual(fileLastAccessTime, fileResult);
        }

        [TestMethod]
        public void Test_GetLastWriteTimeUtc()
        {
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();


            //创建测试所需文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();

            //在目录里创建文件
            string testString = "This is teststring";
            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.Default.GetBytes(testString);
            stream.Write(buffer, 0, 18);
            var filePath = "testDir/" + Guid.NewGuid().ToString() + ".txt";
            storage.CreateFile(filePath, stream);

            //修改文件内容
            FileStream fs = File.Open(Path.Combine(storePath, filePath), FileMode.Open);
            Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
            fs.Write(info, 0, info.Length);
            fs.Close();

            var dirLastWriteTime = Directory.GetLastWriteTimeUtc(Path.Combine(storePath, "testDir"));
            var fileLastWriteTime = File.GetLastWriteTimeUtc(Path.Combine(storePath, filePath));


            var dirResult = storage.GetLastWriteTimeUtc("testDir");
            var fileResult = storage.GetLastWriteTimeUtc(filePath);

            Assert.AreEqual(dirLastWriteTime, dirResult);
            Assert.AreEqual(fileLastWriteTime, fileResult);
        }

        [TestMethod]
        public void Test_MoveDirectory()
        {
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();


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
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();


            //创建测试所需文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();

            //把 a.txt 移动到 testDir目录下，改名为file.txt
            storage.MoveFile("a.txt", "testDir/file.txt");

            var resultPath = Path.Combine(storePath, "testDir/file.txt");

            Assert.IsTrue(File.Exists(resultPath));
        }

        [TestMethod]
        public void Test_OpenFile_SaveFile()
        {
            var storeName = "Test";
            var storePath = Path.Combine(baseDirectory, storeName);
            var storage = new DiskIsolateStorage(storeName, storePath);

            storage.InitStore();


            //创建测试所需文件和目录
            DiskIsolatedStorageTests.CreateFileAndDir();
            

            //获取文件名、文件路径。。
            var of = storage.OpenFile("a.txt", FileMode.Open);
            var fileName = of.StorageFile.FileName;
            var filePath = of.StorageFile.FilePath;
            var storageName = of.StorageFile.StorageName;

            //读取文件内容
            byte[] bytes = new byte[of.Stream.Length];
            UTF8Encoding temp = new UTF8Encoding(true);
            of.Stream.Seek(0, SeekOrigin.Begin);
            of.Stream.Read(bytes, 0, bytes.Length);
            string readStr = temp.GetString(bytes);

            //写文件并保存
            string writeString = "This is another teststring";
            byte[] buffer1 = UTF8Encoding.Default.GetBytes(writeString);
            of.Stream.Write(buffer1, 0, buffer1.Length);
            storage.SaveFile(of);

            //再读取文件内容
            var of1 = storage.OpenFile("a.txt", FileMode.Open);
            byte[] b = new byte[of1.Stream.Length];
            UTF8Encoding temp1 = new UTF8Encoding(true);
            of1.Stream.Seek(0, SeekOrigin.Begin);
            of1.Stream.Read(b, 0, b.Length);
            string readStr1 = temp1.GetString(b);

            Assert.AreEqual("a.txt", fileName);
            Assert.AreEqual("a.txt", filePath);
            Assert.AreEqual("Test", storageName);
            Assert.AreEqual("This is a.text", readStr);
            Assert.AreEqual("This is a.text" + writeString, readStr1);
        }
    }
}
