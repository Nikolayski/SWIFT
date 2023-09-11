using System.IO;
using System.Threading.Tasks;

namespace SWIFT.Engine.Services
{
    public interface IFileInfoService
    {
        SWIFT.Models.FileInfo Create(string name, string hour);

        Task AddAsync(SWIFT.Models.FileInfo fileInfo);
    }
}
