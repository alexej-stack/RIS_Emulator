using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RIS.Data
{
    public class DirectoryDataRepository : IRISDataRepository
    {
        private DirectoryInfo _dataDirectory;

        public string dataRepositoryName
        {
            get;
            set;
        }

        public DirectoryInfo dataRepository
        {
            get { return _dataDirectory; }
            set { _dataDirectory = value; }
        }
        public bool  Initialize(string dataDirectoryName)
        {
            dataRepositoryName = dataDirectoryName;
            if (!Directory.Exists(dataDirectoryName))
            {
                return false;
            }
            _dataDirectory = new DirectoryInfo(dataDirectoryName);
            return true;
        }
        public Task<bool> DeinitializeAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="directories">The directories.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        public IEnumerable<FileInfo> GetData(IEnumerable<DirectoryInfo> directories, string searchPattern, SearchOption searchOption)
        {

            var files = new List<FileInfo>();

            foreach (var directory in directories.Where(directory => directory.Exists).Select(directory => directory))
            {
                files.AddRange(directory.EnumerateFiles(searchPattern, searchOption));
            }

            return files.AsEnumerable();

        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <param name="directoryInfoName">Name of the directory information.</param>
        /// <returns></returns>
        public IEnumerable<DirectoryInfo> GetDirectories(string  directoryInfoName)
        {
            var directoryInfo = new DirectoryInfo(directoryInfoName);
            yield return directoryInfo;
             var directories = directoryInfo.EnumerateDirectories();
            foreach(var directory in directories)
            {
                yield return directory;
            }
           
        }

    }
}
