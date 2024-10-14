using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;

namespace BrewCloud.Shared.Service
{
    public interface ILogService
    {
        Task CreateLog(List<LogDto> logs);
        //Task<List<LogDto>> GetLogs(LogFilterDto filter);
    }
}
