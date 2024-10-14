using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Parameters;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Settings.Parameters.Commands
{

    public class UpdateParametersCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public int? appointmentReminderDuration { get; set; }
        public int? agendaNoteReminder { get; set; }
        public string days { get; set; }
        public Guid? smsCompany { get; set; }
        public Guid? cashAccount { get; set; }
        public Guid? creditCardCashAccount { get; set; }
        public Guid? bankTransferCashAccount { get; set; }
        public Guid? whatsappTemplate { get; set; }
        public Guid? customerWelcomeTemplate { get; set; }
        public Guid? automaticAppointmentReminderMessageTemplate { get; set; }
        public bool? isOtoCustomerWelcomeMessage { get; set; }
        public bool? displayVetNo { get; set; }
        public bool? autoSms { get; set; }
        public bool? IsAnimalsBreeds { get; set; }
        public bool? IsFirstInspection { get; set; }
        public string AppointmentBeginDate { get; set; } = string.Empty;
        public string AppointmentEndDate { get; set; } = string.Empty; 
        public bool? IsExaminationAmuntZero { get; set; }
        public int? datetimestatus { get; set; }
        public int? appointmentinterval { get; set; }
        public int? appointmentSeansDuration { get; set; }
        public int? PetHotelsDateTimeFormat { get; set; }
    }

    public class UpdateParametersCommandHandler : IRequestHandler<UpdateParametersCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateParametersCommandHandler> _logger;
        private readonly IRepository<Domain.Entities.VetParameters> _parametersRepository;

        public UpdateParametersCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateParametersCommandHandler> logger, IRepository<Domain.Entities.VetParameters> parametersRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _parametersRepository = parametersRepository ?? throw new ArgumentNullException(nameof(parametersRepository));
        }

        public async Task<Response<bool>> Handle(UpdateParametersCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {

                string query = "Select * from vetParameters where Deleted = 0";
                var _data = _uow.Query<ParametersDto>(query).ToList();
                if (!_data.Any())
                {
                    VetParameters crparameters = new()
                    {
                        Id = Guid.NewGuid(),
                        AgendaNoteReminder = request.agendaNoteReminder,
                        AppointmentReminderDuration = request.appointmentReminderDuration,
                        AutomaticAppointmentReminderMessageTemplate = request.automaticAppointmentReminderMessageTemplate,
                        AutoSms = request.autoSms,
                        BankTransferCashAccount = request.bankTransferCashAccount,
                        CashAccount = request.cashAccount,
                        CreditCardCashAccount = request.creditCardCashAccount,
                        Days = request.days,
                        CustomerWelcomeTemplate = request.customerWelcomeTemplate,
                        IsOtoCustomerWelcomeMessage = request.isOtoCustomerWelcomeMessage,
                        SmsCompany = request.smsCompany,
                        DisplayVetNo = request.displayVetNo,
                        WhatsappTemplate = request.whatsappTemplate,
                        CreateUsers = _identity.Account.UserName,
                        CreateDate = DateTime.Now,
                        IsAnimalsBreeds = request.IsAnimalsBreeds.GetValueOrDefault(),
                        IsFirstInspection = request.IsFirstInspection.GetValueOrDefault(),
                        AppointmentBeginDate=request.AppointmentBeginDate,
                        AppointmentEndDate=request.AppointmentEndDate,
                        IsExaminationAmuntZero = request.IsExaminationAmuntZero,
                        DatetimeStatus = request.datetimestatus,
                        AppointmentInterval = request.appointmentinterval,
                        AppointmentSeansDuration = request.appointmentSeansDuration,
                        PetHotelsDateTimeFormat = request.PetHotelsDateTimeFormat

                    };
                    await _parametersRepository.AddAsync(crparameters);
                    await _uow.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    var parameters = await _parametersRepository.GetByIdAsync(request.Id);
                    if (parameters == null)
                    {
                        _logger.LogWarning($"parameters update failed. Id number: {request.Id}");
                        return Response<bool>.Fail("Property update failed", 404);
                    }

                    parameters.AgendaNoteReminder = request.agendaNoteReminder;
                    parameters.AppointmentReminderDuration = request.appointmentReminderDuration;
                    parameters.AutomaticAppointmentReminderMessageTemplate = request.automaticAppointmentReminderMessageTemplate;
                    parameters.AutoSms = request.autoSms;
                    parameters.BankTransferCashAccount = request.bankTransferCashAccount;
                    parameters.CashAccount = request.cashAccount;
                    parameters.CreditCardCashAccount = request.creditCardCashAccount;
                    parameters.Days = request.days;
                    parameters.CustomerWelcomeTemplate = request.customerWelcomeTemplate;
                    parameters.IsOtoCustomerWelcomeMessage = request.isOtoCustomerWelcomeMessage;
                    parameters.SmsCompany = request.smsCompany;
                    parameters.DisplayVetNo = request.displayVetNo;
                    parameters.WhatsappTemplate = request.whatsappTemplate;
                    parameters.UpdateUsers = _identity.Account.UserName;
                    parameters.UpdateDate = DateTime.Now;
                    parameters.IsAnimalsBreeds = request.IsAnimalsBreeds.GetValueOrDefault();
                    parameters.IsAnimalsBreeds = request.IsFirstInspection.GetValueOrDefault();
                    parameters.AppointmentBeginDate = request.AppointmentBeginDate;
                    parameters.AppointmentEndDate = request.AppointmentEndDate;
                    parameters.IsExaminationAmuntZero = request.IsExaminationAmuntZero;
                    parameters.DatetimeStatus = request.datetimestatus;
                    parameters.AppointmentInterval = request.appointmentinterval;
                    parameters.AppointmentSeansDuration = request.appointmentSeansDuration;
                    parameters.PetHotelsDateTimeFormat = request.PetHotelsDateTimeFormat;

                    await _uow.SaveChangesAsync(cancellationToken);
                }

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }

            return response;

        }
    }
}
