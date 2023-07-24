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
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Response<bool>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhoneNumber2 { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public string VKNTCNo { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty; 
        public decimal DiscountRate { get; set; } = 0;
        public bool? IsEmail { get; set; } = true;
        public bool? IsPhone { get; set; } = true;
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string LongAdress { get; set; } = string.Empty;
    }

    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.Customers> _customerRepository;

        public CreateCustomerHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.Customers> customerRepository)
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

                Vet.Domain.Entities.Adress adress = new()
                {
                    Id = Guid.NewGuid(),
                    Province = request.Province,
                    Deleted = false,
                    CreateDate = DateTime.UtcNow,
                    LongAdress = request.LongAdress,
                    County = "TR"
                };

                Vet.Domain.Entities.Customers customers = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    PhoneNumber2 = request.PhoneNumber2,
                    EMail = request.EMail,
                    TaxOffice = request.TaxOffice,
                    VKNTCNo = request.VKNTCNo,
                    Note = request.Note,
                    DiscountRate = request.DiscountRate,
                    IsEmail = request.IsEmail,
                    IsPhone = request.IsPhone,
                    Adress = adress,
                    Deleted = false,
                    CreateDate = DateTime.UtcNow,
                    
                };
                await _customerRepository.AddAsync(customers);
                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {

            }
            return response;

        }
    }
}
