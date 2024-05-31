using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Enums.Message;
using VetSystems.Shared.Events;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Services.Message.Sms;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Customers.Commands
{
    public class SendMessageCommand : IRequest<Response<bool>>
    {
        public MessageType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<SendMessageCommandHandler> _logger;
        private readonly IRepository<VetSmsParameters> _smsParametersRepository;
        private readonly ISmsService _smsService;

        public SendMessageCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<SendMessageCommandHandler> logger, IRepository<VetSmsParameters> smsParametersRepository, ISmsService smsService)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _smsParametersRepository = smsParametersRepository;
            _smsService = smsService;
        }

        public async Task<Response<bool>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                var smsParamters = await _smsParametersRepository.FirstOrDefaultAsync(x => x.Active == true);
                if (smsParamters != null)
                {
                    var req = new SendMessageRequestEvent
                    {
                        UserName = smsParamters.UserName,
                        PassWord = smsParamters.Password,
                        Content = request.Content,
                        Title = request.Title,
                    };
                    var result = _smsService.SendSms(req);
                }
            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }
}
