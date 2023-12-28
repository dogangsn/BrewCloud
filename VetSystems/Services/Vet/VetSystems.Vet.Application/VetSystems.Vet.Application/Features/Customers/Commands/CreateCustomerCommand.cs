using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Dtos.MailKit;
using VetSystems.Shared.Enums;
using VetSystems.Shared.Events;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Event;
using VetSystems.Vet.Application.Features.Appointment.Commands;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Application.Models.Mail;
using VetSystems.Vet.Application.Services.Mails;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Response<string>>
    {
        public CustomersDto CreateCustomers { get; set; }
    }

    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customerRepository;
        private readonly IRepository<VetParameters> _parametersRepository;
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

        public CreateCustomerHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.VetCustomers> customerRepository, IRepository<VetParameters> parametersRepository, IMediator mediator, IMailService mailService)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _parametersRepository = parametersRepository ?? throw new ArgumentNullException(nameof(parametersRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mailService = mailService;
        }

        public async Task<Response<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
                Data = string.Empty
            };
            _uow.CreateTransaction(IsolationLevel.ReadCommitted);
            try
            {
                //Silinen kayitlarin uzerinde islem yapılması
                var recordControl = await _customerRepository.FirstOrDefaultAsync(x=>x.PhoneNumber.Trim() == request.CreateCustomers.PhoneNumber.Trim() && x.Deleted == false);
                if (recordControl != null)
                {
                    return Response<string>.Fail("Sistem Üzerinde Aynı Müşteri Bilgileri ile Kayıt Vardır.", 404);
                }

                VetParameters? _param = (await _parametersRepository.GetAllAsync()).FirstOrDefault();
                if (_param == null)
                {
                    return Response<string>.Fail("Şirket Parametlerini Tamamlayınız.", 404);
                }

                if (request.CreateCustomers.PatientDetails.Any(x => x.AnimalType.GetValueOrDefault() == 0))
                {
                    return Response<string>.Fail("Hayvan Türünün Seçilmesi Zorunludur.", 404);
                }

                if (_param.IsAnimalsBreeds.GetValueOrDefault())
                {
                    if (request.CreateCustomers.PatientDetails.Any(x=>x.AnimalBreed.GetValueOrDefault() == 0))
                    {
                        return Response<string>.Fail("Hayvan Türünün Irkı Seçilmesi Zorunludur.", 404);
                    }
                }

                Vet.Domain.Entities.VetAdress adress = new()
                {
                    Id = Guid.NewGuid(),
                    Province = request.CreateCustomers.Province,
                    Deleted = false,
                    CreateDate = DateTime.UtcNow,
                    LongAdress = request.CreateCustomers.LongAdress,
                    County = "TR",
                    District = request.CreateCustomers.District,
                };

                Vet.Domain.Entities.VetCustomers customers = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.CreateCustomers.FirstName,
                    LastName = request.CreateCustomers.LastName,
                    PhoneNumber = request.CreateCustomers.PhoneNumber,
                    PhoneNumber2 = request.CreateCustomers.PhoneNumber2,
                    EMail = request.CreateCustomers.EMail,
                    TaxOffice = request.CreateCustomers.TaxOffice,
                    VKNTCNo = request.CreateCustomers.VKNTCNo,
                    Note = request.CreateCustomers.Note,
                    DiscountRate = request.CreateCustomers.DiscountRate,
                    IsEmail = request.CreateCustomers.IsEmail,
                    IsPhone = request.CreateCustomers.IsPhone,
                    Adress = adress,
                    Deleted = false,
                    CreateDate = DateTime.Now,
                };

                if (request.CreateCustomers.PatientDetails.Any())
                {
                    foreach (var item in request.CreateCustomers.PatientDetails)
                    {
                        VetPatients patients = new()
                        {
                            Active= item.Active,
                            AnimalBreed =  item.AnimalBreed.GetValueOrDefault(),
                            AnimalColor = string.IsNullOrEmpty(item.AnimalColor) ? 0 : Convert.ToInt32(item.AnimalColor),
                            AnimalType = Convert.ToInt32(item.AnimalType),
                            BirthDate = string.IsNullOrEmpty(item.BirthDate) ? DateTime.Now : Convert.ToDateTime(item.BirthDate),
                            ChipNumber = item.ChipNumber,
                            Name = item.Name ,
                            ReportNumber = item.ReportNumber,
                            Sex  = item.Sex,
                            SpecialNote = item.SpecialNote,
                            Sterilization = item.Sterilization,
                            Id = Guid.NewGuid(),
                            CustomerId = customers.Id,
                            CreateUsers = _identity.Account.UserName,
                            CreateDate = DateTime.Now,
                        };
                        customers.Patients.Add(patients);
                    }
                }

                var appointmentRecord = new CreateAppointmentCommand()
                {
                    AppointmentType = (int)AppointmentType.ilkKayit,
                    BeginDate = customers.CreateDate,
                    CustomerId = Convert.ToString(customers.Id) ?? "",
                    Note = customers.Note,
                    DoctorId = "00000000-0000-0000-0000-000000000000"
                };
                var appointmentResponse = _mediator.Send(appointmentRecord);
          
                await _customerRepository.AddAsync(customers);
                await _uow.SaveChangesAsync(cancellationToken);
                _uow.Commit();
                response.Data = customers.Id.ToString();

                if (_param.IsOtoCustomerWelcomeMessage.GetValueOrDefault() && !string.IsNullOrEmpty(request.CreateCustomers.EMail) && request.CreateCustomers.IsEmail.GetValueOrDefault())
                    SendMail(customers.EMail);


            }
            catch (Exception ex)
            {
                _uow.Rollback();
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }
            return response;

        }

        
        public void SendMail(string emailToId)
        {
            try
            {
                var eventMessage = new SendMailRequestEvent()
                {
                    EmailToId = emailToId,
                    EmailBody = "TEST",
                    EmailSubject = "TEST",
                    EmailToName = "TEST",
                    ConnectionString = _identity.Connection,
                    RecId = _identity.TenantId
                };
                _mailService.SendMail("mail/mailing/SendMail", eventMessage);
            }
            catch (Exception ex)
            {
            }
        }

    }
}
