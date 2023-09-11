using System.IO;
using System.Threading.Tasks;

namespace SWIFT.Engine.Services
{
    public class FileInfoService : IFileInfoService
    {
        private readonly ApplicationDbContext db;
        public FileInfoService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(SWIFT.Models.FileInfo fileInfo)
        {
           await this.db.AddAsync(fileInfo);
            await this.db.SaveChangesAsync();
        }

        public SWIFT.Models.FileInfo Create(string name, string hour)
        {
            return new SWIFT.Models.FileInfo(name, hour);
        }
    }
}
