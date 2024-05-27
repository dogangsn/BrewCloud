using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Enums;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Settings.SmsParameters.Commands
{
    public class CreateSmsParametersCommand : IRequest<Response<bool>>
    {
        public SmsParameterType SmsIntegrationType { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CreateSmsParametersCommandHandler : IRequestHandler<CreateSmsParametersCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSmsParametersCommandHandler> _logger;
        private readonly IRepository<VetSmsParameters> _smsParametersRepository;

        public CreateSmsParametersCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateSmsParametersCommandHandler> logger, IRepository<VetSmsParameters> smsParametersRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _smsParametersRepository = smsParametersRepository;
        }

        public async Task<Response<bool>> Handle(CreateSmsParametersCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            try
            {
                var parameters = await _smsParametersRepository.FirstOrDefaultAsync(x => x.SmsIntegrationType == (int)request.SmsIntegrationType && x.Deleted == false);
                if (parameters != null)
                {
                    response.Data = false;
                    response.IsSuccessful = false;
                    return response;
                }

                var smsParameters = new VetSmsParameters()
                {
                    Id = Guid.NewGuid(),
                    Active = request.Active,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    Password = request.Password,
                    UserName = request.UserName,
                    SmsIntegrationType = (int)request.SmsIntegrationType,
                };
                await _smsParametersRepository.AddAsync(smsParameters);
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
