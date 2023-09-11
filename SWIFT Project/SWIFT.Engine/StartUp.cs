using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWIFT.Engine.Services;
using SWIFT.Engine.Services.Transaction;
using SWIFT.Entities;
using SWIFT.Main;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SWIFT.Engine
{
    public class StartUp
    {
        private readonly IConfiguration configuration;

        public StartUp(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //db
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   this.configuration.GetConnectionString(@"Server=.;Database=SWIFT;Trusted_Connection=True;MultipleActiveResultSets=true")));

        }
        static async Task Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string mainDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
            Console.WriteLine(mainDirectory);
            var dirInfo = new DirectoryInfo(mainDirectory);
            var files = dirInfo.GetFiles("*.txt");
            var fileInfoService = new FileInfoService(new ApplicationDbContext());
            var transactionService = new TransactionService(new ApplicationDbContext());
            foreach (var file in files)
            {
                var fileName = file.Name;
                var hour = DateTime.Now.ToString("HH:mm:ss");
                var strResult = string.Empty;
                using (StreamReader streamReader = File.OpenText(file.ToString()))
                {
                    strResult = streamReader.ReadToEnd();
                    var parser = new Parser();
                    var messagesInfo = parser.SeparateTxtFile(strResult.Trim());
                    var basicHeader = new BasicHeader(messagesInfo["BasicHeader"]);
                    var applicationHeader = new ApplicationHeader(messagesInfo["ApplicationHeader"]);
                    var textHeader = new TextHeader(messagesInfo["TextHeader"]);
                    SWIFT.Models.FileInfo fileInfo = fileInfoService.Create(fileName, hour);
                    SWIFT.Models.Transaction transaction = null;
                    if (applicationHeader.Direction == "I")
                    {
                        transaction = transactionService.Create(textHeader.Name, textHeader.Amount, textHeader.OperationType, textHeader.Currency, textHeader.Account, basicHeader.LtAddress, applicationHeader.ReceiverAddress);
                    }

                    else
                    {
                        transaction = transactionService.Create(textHeader.Name, textHeader.Amount, textHeader.OperationType, textHeader.Currency, textHeader.Account, applicationHeader.SenderBIC, basicHeader.LtAddress);
                    }

                    fileInfo.Transactions.Add(transaction);
                    await transactionService.AddAsync(transaction);
                    await fileInfoService.AddAsync(fileInfo);
                }
            }








        }
    }
}

