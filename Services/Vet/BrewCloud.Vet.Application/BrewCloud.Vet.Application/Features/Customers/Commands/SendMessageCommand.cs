using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Enums.Message;
using BrewCloud.Shared.Events;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Services.Message.Sms;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Customers.Commands
{
    public class SendMessageCommand : IRequest<Response<bool>>
    {
        public MessageType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
    }

    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<SendMessageCommandHandler> _logger;
        private readonly IRepository<VetSmsParameters> _smsParametersRepository;
        private readonly ISmsService _smsService;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customersRepository;

        public SendMessageCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<SendMessageCommandHandler> logger, IRepository<VetSmsParameters> smsParametersRepository, ISmsService smsService, IRepository<VetCustomers> customersRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _smsParametersRepository = smsParametersRepository;
            _smsService = smsService;
            _customersRepository = customersRepository;
        }

        public async Task<Response<bool>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {

                var customers = await _customersRepository.GetByIdAsync(request.CustomerId);
                if (customers == null)
                {
                    return Response<bool>.Fail("Not Found", 404);
                }

                if (!customers.IsPhone.GetValueOrDefault())
                {
                    return Response<bool>.Fail("Müşteri İzni Yok.", 404);
                }

                if (string.IsNullOrEmpty(customers.PhoneNumber))
                {
                    return Response<bool>.Fail("Telefon Numarası Bulunamadı.", 404);
                }

                string _phoneNumber = customers.PhoneNumber.Replace(")", "")
                                    .Replace("(", "")
                                    .Replace("-", "")
                                    .Replace(",", "")
                                    .Replace(" ", "");
                if (_phoneNumber.Length != 10)
                {
                    return Response<bool>.Fail("Telefon Numarasını Kontrol Ednizi.", 404);
                }

                string[] charArray = new string[] { _phoneNumber };
                var smsParamters = await _smsParametersRepository.FirstOrDefaultAsync(x => x.Active == true);
                if (smsParamters != null)
                {
                    var req = new SendMessageRequestEvent
                    {
                        UserName = smsParamters.UserName,
                        PassWord = smsParamters.Password,
                        Content = request.Content,
                        Title = request.Title,
                        SendPhone = charArray
                    };
                    var result = _smsService.SendSms(req);
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 404);
            }
            return response;
        }
    }
}
