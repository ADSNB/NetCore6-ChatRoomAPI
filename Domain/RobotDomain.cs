using Domain.Enum;
using Domain.Helpers;
using Domain.InputModel.GroupChatMessage;
using Domain.Models;
using Domain.Models.RobotService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using Repository.Entity;
using Repository.UnitOfWork;
using Serilog;
using System.Reflection;

namespace Domain
{
    public class RobotDomain
    {
        private readonly AppSettingsModel _appSettingsModel;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RobotDomain(AppSettingsModel appSettings, IServiceScopeFactory serviceScopeFactory)
        {
            _appSettingsModel = appSettings;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ExecuteRobotJob()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var csvData = ReadCSVData();
                    var uow = scope.ServiceProvider.GetService<IUnitOfWork>();
                    var uow2 = scope.ServiceProvider.GetService<IUnitOfWork>();
                    var groupChatMessageDomain = scope.ServiceProvider.GetService<GroupChatMessageDomain>();

                    var toProcessList = uow.ProcessingQueueRepository.GetAll().Where(e => e.CodProcessingQueueStatus == ProcessingQueueStatusEnum.InQueue.GetHashCode() || e.CodProcessingQueueStatus == ProcessingQueueStatusEnum.Processing.GetHashCode()).OrderBy(e => e.Id);
                    foreach (var process in toProcessList)
                    {
                        process.UpdateStatusInProgress();
                        uow2.ProcessingQueueRepository.Update(process);

                        var search = csvData.Find(f => f.Symbol.Trim().ToLower() == IdentifyCommand(process.CommandName).Trim().ToLower());
                        if (search != null)
                        {
                            var message = $"{search.Symbol} quote is ${search.Close} per share.";
                            groupChatMessageDomain.SendMessageToGroup(new SendMessageToGroupInputModel()
                            {
                                Message = message,
                                CodGroupChat = process.CodGroupChat,
                                ConnectionId = String.Empty,
                                FromUser = "System Robot",
                                IsRobotProcessingCall = true
                            });
                        }
                        process.UpdateStatusDone();
                        uow2.ProcessingQueueRepository.Update(process);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"An error occured in robot processing: {JsonConvert.SerializeObject(ex)}");
            }
        }

        public void CreateProcessingQueueRequest(int codGroupChat, string message, string fromUser)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var uow = scope.ServiceProvider.GetService<IUnitOfWork>();
                var processingQueueEntity = new ProcessingQueueEntity(codGroupChat, message, fromUser);
                uow.ProcessingQueueRepository.Create(processingQueueEntity);
            }
        }

        private string IdentifyCommand(string command)
        {
            // more commands can be added
            if (command.Contains(EnumHelper.GetEnumDescription(CommandTypesEnum.GetStockValue)))
                return command.Replace(EnumHelper.GetEnumDescription(CommandTypesEnum.GetStockValue), "");

            return string.Empty;
        }

        private List<CSVTemplateModel> ReadCSVData()
        {
            var csvFileLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), _appSettingsModel.RobotExcelSpreadsheetLocation);

            var file = new List<CSVTemplateModel>();

            return file = File.ReadAllLines(csvFileLocation)
                    .Skip(1)
                    .Select(v => CSVTemplateModel.FromCsv(v))
                    .ToList();
        }

        private void GetExcelDataByTemplateFromFile()
        {
            var xlsFileLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), _appSettingsModel.RobotExcelSpreadsheetLocation);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage(new FileInfo(xlsFileLocation)))
            {
                var worksheet = excel.Workbook.Worksheets["aapl.us"];

                var excelData = worksheet.ConvertSheetToObjects<ExcelTemplateModel>().ToList();

                if (excelData == null || excelData.Count == 0)
                    throw new Exception("Não foi encontrado dados na planilha enviada");

                // remove o cabeçalho de informações
                excelData.RemoveAt(0);

                // return excelData;
            }
        }

        private List<ExcelTemplateModel> GetExcelDataByTemplateFromFormFile(IFormFile file)
        {
            var stream = new MemoryStream();
            file.CopyTo(stream);

            var excel = new ExcelPackage(stream);
            var worksheet = excel.Workbook.Worksheets[0];

            var excelData = worksheet.ConvertSheetToObjects<ExcelTemplateModel>().ToList();

            if (excelData == null || excelData.Count == 0)
                throw new Exception("Não foi encontrado dados na planilha enviada");

            // remove o cabeçalho de informações
            excelData.RemoveAt(0);

            return excelData;
        }
    }
}