using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWIFT.Engine.Services;
using SWIFT.Engine.Services.Transaction;
using SWIFT.Entities;
using SWIFT.Main;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SWIFT.Engine
{
    public class StartUp
    {
        private static string mainDirectory = @"C:\SwiftFiles";
        private readonly IConfiguration configuration;

        public StartUp(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   this.configuration.GetConnectionString(@"Server=.;Database=SWIFT;Trusted_Connection=True;MultipleActiveResultSets=true")));
        }
        static async Task Main(string[] args)
        {
            CreateDirectories(mainDirectory);
            var dirInfo = new DirectoryInfo(mainDirectory);
            var files = dirInfo.GetFiles("*.txt");
            var fileNameAndExtension = "";
            if (files.Any())
            {
                var isFileValid = true;
                var fileInfoService = new FileInfoService(new ApplicationDbContext());
                var transactionService = new TransactionService(new ApplicationDbContext());
                foreach (var file in files)
                {
                    var fileName = file.Name.Replace(".txt", "");
                    fileNameAndExtension = file.Name;
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
                        bool areTagsValid = CheckTags(textHeader.OperationType,
                                                      textHeader.SenderReference,
                                                      textHeader.Currency,
                                                      textHeader.Amount,
                                                      textHeader.Name,
                                                      textHeader.DetailsOfCharges,
                                                      basicHeader.ApplicationId,
                                                      basicHeader.ServiceId,
                                                      basicHeader.LtAddress,
                                                      basicHeader.SessionNumber,
                                                      basicHeader.SequenceNumber);
                        if (!areTagsValid)
                        {
                            isFileValid = false;
                            break;
                        }
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

                        transaction.FileInfoId = fileInfo.Id;
                        await fileInfoService.AddAsync(fileInfo);
                        await transactionService.AddAsync(transaction);
                    }
                }

                MoveFile(isFileValid, mainDirectory + @"\SUCCESS", mainDirectory + @"\FAILED", fileNameAndExtension);
            }
        }

        private static void MoveFile(bool isFileValid, string mainDirectorySuccessFolder, string mainDirectoryFailedFolder, string fileNameAndExtension)
        {
            if (isFileValid)
            {
                File.Move(mainDirectory + @"\" + fileNameAndExtension, mainDirectorySuccessFolder + @"\" + fileNameAndExtension);
            }
            else
            {
                File.Move(mainDirectory + @"\" + fileNameAndExtension, mainDirectoryFailedFolder + @"\" + fileNameAndExtension);
            }
        }

        private static void CreateDirectories(string mainDirectory)
        {
            var mainDirectorySuccessFolder = mainDirectory + @"\SUCCESS";
            var mainDirectoryFailedFolder = mainDirectory + @"\FAILED";
            if (!Directory.Exists(mainDirectory))
            {
                Directory.CreateDirectory(mainDirectory);
                if (!Directory.Exists(mainDirectorySuccessFolder))
                {
                    Directory.CreateDirectory(mainDirectorySuccessFolder);
                }

                if (!Directory.Exists(mainDirectoryFailedFolder))
                {
                    Directory.CreateDirectory(mainDirectoryFailedFolder);
                }


            }
        }

        private static bool CheckTags(string operationType, string senderReference, string currency, string amount, string name, string detailsOfCharges, string applicationId, string serviceId, string ltAddress, string sessionNumber, string sequenceNumber)
        {
            return !string.IsNullOrEmpty(operationType)
               && !string.IsNullOrEmpty(senderReference)
               && !string.IsNullOrEmpty(currency)
               && !string.IsNullOrEmpty(amount)
               && !string.IsNullOrEmpty(name)
               && !string.IsNullOrEmpty(detailsOfCharges)
               && !string.IsNullOrEmpty(applicationId)
               && !string.IsNullOrEmpty(serviceId)
               && !string.IsNullOrEmpty(ltAddress)
               && !string.IsNullOrEmpty(sessionNumber)
               && !string.IsNullOrEmpty(sequenceNumber);
        }
    }
}

