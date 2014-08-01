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
    public class DiskIsloateStorageTests
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        [TestMethod]
        public void Test_Name()
        {
            var storage = new DiskIsloateStorage("Test", baseDirectory);

            Assert.AreEqual("Test", storage.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Test_UsedSize()
        {
            var storage = new DiskIsloateStorage("Test", baseDirectory);
            Assert.AreEqual(0, storage.UsedSize);
        }
        [TestMethod]
        public void Test_InitStore()
        {
            var storeName = Guid.NewGuid().ToString();
            var storage = new DiskIsloateStorage(storeName, baseDirectory);
            storage.InitStore();

            string storePath = Path.Combine(baseDirectory, storeName);

            Assert.IsTrue(Directory.Exists(storePath));
        }
    }
}
