using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Response<bool>>
    {
        public CustomersDto CreateCustomers { get; set; }
    }

    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customerRepository;

        public CreateCustomerHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.VetCustomers> customerRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<Response<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {

                Vet.Domain.Entities.VetAdress adress = new()
                {
                    Id = Guid.NewGuid(),
                    Province = request.CreateCustomers.Province,
                    Deleted = false,
                    CreateDate = DateTime.UtcNow,
                    LongAdress = request.CreateCustomers.LongAdress,
                    County = "TR"
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
                    CreateDate = DateTime.UtcNow,
                };
                if (request.CreateCustomers.PatientDetails.Any())
                {
                    foreach (var item in request.CreateCustomers.PatientDetails)
                    {
                        VetPatients patients = new()
                        {
                            Active= item.Active,
                            AnimalBreed = string.IsNullOrEmpty(item.AnimalBreed) ? 0 : Convert.ToInt32(item.AnimalBreed),
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

                await _customerRepository.AddAsync(customers);
                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
            }
            return response;

        }
    }
}
