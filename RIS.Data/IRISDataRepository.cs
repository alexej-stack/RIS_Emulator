using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RIS.Data
{
    public interface IRISDataRepository
    {
        string dataRepositoryName { get; set; }
        bool Initialize(string dataDirectoryName);
        Task<bool> DeinitializeAsync();
        IEnumerable<FileInfo> GetData(IEnumerable<DirectoryInfo> directories, string searchPattern, SearchOption searchOption);
    }
}
