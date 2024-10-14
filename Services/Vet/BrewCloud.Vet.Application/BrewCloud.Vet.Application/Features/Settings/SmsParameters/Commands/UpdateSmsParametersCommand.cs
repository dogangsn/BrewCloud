using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Enums;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Settings.SmsParameters.Commands
{
    public class UpdateSmsParametersCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public SmsParameterType SmsIntegrationType { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UpdateSmsParametersCommandHandler : IRequestHandler<UpdateSmsParametersCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSmsParametersCommandHandler> _logger;
        private readonly IRepository<VetSmsParameters> _smsParametersRepository;

        public UpdateSmsParametersCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateSmsParametersCommandHandler> logger, IRepository<VetSmsParameters> smsParametersRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _smsParametersRepository = smsParametersRepository;
        }

        public async Task<Response<bool>> Handle(UpdateSmsParametersCommand request, CancellationToken cancellationToken)
        {

            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            try
            {
                var parameters = await _smsParametersRepository.FirstOrDefaultAsync(x => x.SmsIntegrationType == (int)request.SmsIntegrationType && x.Deleted == false);
                if (parameters == null)
                {
                    response.Data = false;
                    response.IsSuccessful = false;
                    return response;
                }

                parameters.Active = request.Active;
                parameters.Password = request.Password;
                parameters.UserName = request.UserName;
                parameters.SmsIntegrationType = (int)request.SmsIntegrationType;

                await _uow.SaveChangesAsync(cancellationToken);
                response.IsSuccessful = true;

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }

            return response;
        }
    }
}
