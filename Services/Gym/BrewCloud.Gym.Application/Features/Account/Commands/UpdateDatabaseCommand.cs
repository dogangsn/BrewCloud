using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Shared.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Gym.Application.Features.Account.Commands
{
    public class UpdateDatabaseCommand : IRequest<Response<bool>>
    {
        public string ConnectionString { get; set; }
    }

    public class UpdateDatabaseCommandHandler : IRequestHandler<UpdateDatabaseCommand, Response<bool>>
    {
        private readonly ILogger<UpdateDatabaseCommandHandler> _logger;
        private readonly IUnitOfWork _uof;

        public UpdateDatabaseCommandHandler(ILogger<UpdateDatabaseCommandHandler> logger, IUnitOfWork uof)
        {
            _logger = logger;
            _uof = uof;
        }

        public async Task<Response<bool>> Handle(UpdateDatabaseCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                await _uof.MigrateDatabase(request.ConnectionString);


                if (true)
                {
                    await _uof.MoveMigrationTable(request.ConnectionString, "");
                }
                else
                {
                    await _uof.MigrateDatabase(request.ConnectionString, "", "");
                }

                response.ResponseType = ResponseType.Ok;
                response.Data = true;

            }
            catch (Exception ex)
            {
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"hub errors: {ex.Message}");
            }
            return response;
        }
    }
}
