#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Data.IsolatedStorage
{
    public class IsolatedStorageFile
    {
        public IsolatedStorageFile(string fileName, string filePath, string storageName)
        {
            this.FileName = fileName;
            this.FilePath = filePath;
            this.StorageName = storageName;
        }
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string StorageName { get; private set; }
    }
}
