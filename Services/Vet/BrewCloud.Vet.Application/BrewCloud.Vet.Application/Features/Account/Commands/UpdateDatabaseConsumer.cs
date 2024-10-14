using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Events;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Account.Commands
{
    public class UpdateDatabaseConsumer : IConsumer<CreateSubscriptionRequestEvent>
    {

        private readonly ILogger<UpdateDatabaseConsumer> _logger;
        private readonly Domain.Contracts.IUnitOfWork _uof;
        public UpdateDatabaseConsumer(ILogger<UpdateDatabaseConsumer> logger, IUnitOfWork uof)
        {
            _logger = logger;
            _uof = uof;
        }

        public async Task Consume(ConsumeContext<CreateSubscriptionRequestEvent> context)
        {
            //try
            //{
            //    if (context.Message.MoveHistoryTable)
            //    {
            //        await _uof.MoveMigrationTable(context.Message.ConnectionString, context.Message.HistoryTable);
            //    }
            //    else
            //    {
            //        await _uof.MigrateDatabase(context.Message.ConnectionString, context.Message.TargetMigration, context.Message.HistoryTable);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("Erp Migration errors Cpmpany:{MessageCompany}, Tenant:{MessageTenantId}, Message: {ExMessage}", context.Message.Company, context.Message.TenantId, ex.Message);
            //}

        }
    }
}
