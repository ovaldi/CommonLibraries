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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Data.IsolatedStorage
{
    public class IsolatedStorageFileStream : IDisposable
    {
        public IsolatedStorageFile StorageFile { get; set; }
        public Stream Stream { get; set; }

        #region Dispose
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (Stream != null)
            {
                Stream.Close();
                Stream = null;
            }
            disposed = true;
        }
        #endregion
    }
}
