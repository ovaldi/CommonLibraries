#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Data.IsolatedStorage
{
    public class DiskIsolateStorage : IIsolatedStorage
    {
        #region .ctor
        private string baseDirectory;

        private string physicalPath;
        public DiskIsolateStorage(string name, string baseDirectory)
        {
            this.Name = name;
            this.baseDirectory = baseDirectory;

            this.physicalPath = Path.Combine(this.baseDirectory, name);
        }
        #endregion

        #region IIsolatedStorage
        public string Name
        {
            get;
            private set;
        }

        public long Quota
        {
            get;
            private set;
        }

        public long UsedSize
        {
            get { throw new NotImplementedException(); }
        }

        public bool IncreaseQuotaTo(long newQuotaSize)
        {
            throw new NotImplementedException();
        }

        public void InitStore()
        {
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }
        }

        public void Remove()
        {
            if (Directory.Exists(physicalPath))
            {
                IOUtility.DeleteDirectory(this.physicalPath, true);
            }
        }

        public void CopyFile(string sourceFileName, string destinationFileName)
        {
            CopyFile(sourceFileName, destinationFileName, false);
        }

        public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
        {
            string fullSourceFileName = Path.Combine(this.physicalPath, sourceFileName);
            string fullDestionationFileName = Path.Combine(this.physicalPath, destinationFileName);

            File.Copy(fullSourceFileName, fullDestionationFileName, overwrite);
        }

        public void CreateDirectory(string dir)
        {
            var physicalPath = Path.Combine(this.physicalPath, dir);
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }
        }

        public void CreateFile(string path, Stream stream)
        {
            string fullPath = Path.Combine(this.physicalPath, path);

            var fileStream = File.Create(fullPath);

            stream.Position = 0;
            stream.CopyTo(fileStream);
            fileStream.Close();
        }

        public void DeleteDirectory(string dir)
        {
            string fullPath = Path.Combine(this.physicalPath, dir);
            IOUtility.DeleteDirectory(fullPath, true);
        }

        public void DeleteFile(string file)
        {
            string fullPath = Path.Combine(this.physicalPath, file);
            File.Delete(fullPath);
        }

        public bool DirectoryExists(string path)
        {
            string fullPath = Path.Combine(this.physicalPath, path);

            return Directory.Exists(fullPath);
        }

        public bool FileExists(string path)
        {
            string fullPath = Path.Combine(this.physicalPath, path);

            return File.Exists(fullPath);
        }

        public DateTimeOffset GetCreationTimeUtc(string path)
        {
            string fullPath = Path.Combine(this.physicalPath, path);

            if (File.Exists(fullPath))
            {
                return File.GetCreationTimeUtc(fullPath);
            }
            else
            {
                return Directory.GetCreationTimeUtc(fullPath);
            }
        }

        public string[] GetDirectoryNames(string path)
        {
            return GetDirectoryNames(path, null);
        }

        public string[] GetDirectoryNames(string path, string searchPattern)
        {
            var fullPath = this.physicalPath;
            if (!string.IsNullOrEmpty(path))
            {
                fullPath = Path.Combine(this.physicalPath, path);
            }
            if (Directory.Exists(this.physicalPath))
            {
                IEnumerable<string> dirs;
                if (string.IsNullOrEmpty(searchPattern))
                {
                    dirs = Directory.EnumerateDirectories(fullPath);
                }
                else
                {
                    dirs = Directory.EnumerateDirectories(fullPath, searchPattern);
                }


                return dirs.Select(it => it.Substring(fullPath.Length)).ToArray();
            }
            return new string[0];
        }

        public string[] GetFileNames(string path)
        {
            return GetFileNames(path, null);
        }

        public string[] GetFileNames(string path, string searchPattern)
        {
            var fullPath = this.physicalPath;
            if (!string.IsNullOrEmpty(path))
            {
                fullPath = Path.Combine(this.physicalPath, path);
            }
            if (Directory.Exists(this.physicalPath))
            {
                IEnumerable<string> dirs;
                if (string.IsNullOrEmpty(searchPattern))
                {
                    dirs = Directory.EnumerateFiles(fullPath);
                }
                else
                {
                    dirs = Directory.EnumerateFiles(fullPath, searchPattern);
                }


                return dirs.Select(it => it.Substring(fullPath.Length)).ToArray();
            }
            return new string[0];
        }

        public DateTimeOffset GetLastAccessTimeUtc(string path)
        {
            string fullPath = Path.Combine(this.physicalPath, path);

            if (File.Exists(fullPath))
            {
                return File.GetLastAccessTimeUtc(fullPath);
            }
            else
            {
                return Directory.GetLastAccessTimeUtc(fullPath);
            }
        }

        public DateTimeOffset GetLastWriteTimeUtc(string path)
        {
            string fullPath = Path.Combine(this.physicalPath, path);

            if (File.Exists(fullPath))
            {
                return File.GetLastWriteTimeUtc(fullPath);
            }
            else
            {
                return Directory.GetLastWriteTimeUtc(fullPath);
            }
        }

        public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
        {
            string fullSourceDirectoryName = Path.Combine(this.physicalPath, sourceDirectoryName);
            string fullDestionationDirectoryName = Path.Combine(this.physicalPath, destinationDirectoryName);

            Directory.Move(fullSourceDirectoryName, fullDestionationDirectoryName);
        }

        public void MoveFile(string sourceFileName, string destinationFileName)
        {
            string fullSourceFileName = Path.Combine(this.physicalPath, sourceFileName);
            string fullDestionationFileName = Path.Combine(this.physicalPath, destinationFileName);

            Directory.Move(fullSourceFileName, fullDestionationFileName);
        }

        public IsolatedStorageFileStream OpenFile(string path, System.IO.FileMode mode)
        {
            return OpenFile(path, mode, FileAccess.ReadWrite);
        }

        public IsolatedStorageFileStream OpenFile(string path, System.IO.FileMode mode, System.IO.FileAccess access)
        {
            return OpenFile(path, mode, access, FileShare.None);
        }

        public IsolatedStorageFileStream OpenFile(string path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share)
        {
            string fullPath = Path.Combine(this.physicalPath, path);

            var storageFileStream = new IsolatedStorageFileStream();
            storageFileStream.StorageFile = new IsolatedStorageFile(Path.GetFileName(path), path, this.Name);
            using (var fileStream = File.Open(fullPath, mode, access, share))
            {
                var memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                storageFileStream.Stream = memoryStream;
            }
            return storageFileStream;
        }

        public void SaveFile(IsolatedStorageFileStream storageFileStream)
        {
            var fullPath = Path.Combine(this.physicalPath, storageFileStream.StorageFile.FilePath);

            using (storageFileStream.Stream)
            {
                var fileStream = File.Open(fullPath, FileMode.OpenOrCreate);
                storageFileStream.Stream.Position = 0;
                storageFileStream.Stream.CopyTo(fileStream);
                fileStream.Close();
            }
        }

        public void Dispose()
        {

        }
        #endregion
    }
}
