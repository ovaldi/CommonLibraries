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
    public interface IIsolatedStorage : IDisposable
    {
        /// <summary>
        /// IsloatedStorage name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The limit of isolated storage space, in bytes.
        /// </summary>
        long Quota { get; }
        /// <summary>
        /// The used amount of isolated storage space, in bytes.
        /// </summary>
        long UsedSize { get; }
        /// <summary>
        /// The requested new quota size, in bytes, for the user to approve.
        /// </summary>
        /// <param name="newQuotaSize"></param>
        /// <returns></returns>
        bool IncreaseQuotaTo(long newQuotaSize);
        void InitStore();
        void Remove();

        void CopyFile(string sourceFileName, string destinationFileName);
        void CopyFile(string sourceFileName, string destinationFileName, bool overwrite);
        void CreateDirectory(string dir);
        void CreateFile(string path, Stream stream);
        void DeleteDirectory(string dir);
        void DeleteFile(string file);
        bool DirectoryExists(string path);
        bool FileExists(string path);
        DateTimeOffset GetCreationTimeUtc(string path);

        string[] GetDirectoryNames(string path);
        string[] GetDirectoryNames(string path, string searchPattern);

        string[] GetFileNames(string path);
        string[] GetFileNames(string path, string searchPattern);

        DateTimeOffset GetLastAccessTimeUtc(string path);
        DateTimeOffset GetLastWriteTimeUtc(string path);

        void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName);
        void MoveFile(string sourceFileName, string destinationFileName);

        IsolatedStorageFileStream OpenFile(string path, FileMode mode);
        IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access);
        IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share);

        void UpdateFile(string path, Stream stream);
    }
}
