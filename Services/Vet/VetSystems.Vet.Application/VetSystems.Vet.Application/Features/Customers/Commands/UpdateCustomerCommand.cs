using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Features.Definition.CustomerGroup.Commands;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhoneNumber2 { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public string VKNTCNo { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; } = 0;
        public bool? emailNotification { get; set; } = false;
        public bool? smsNotification { get; set; } = false;
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string LongAdress { get; set; } = string.Empty;
        public Guid? CustomerGroup { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customersRepository;
        private readonly IRepository<VetAdress> _adressRepository;

        public UpdateCustomerCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCustomerCommandHandler> logger, IRepository<Domain.Entities.VetCustomers> customersRepository, IRepository<VetAdress> adressRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customersRepository = customersRepository ?? throw new ArgumentNullException(nameof(customersRepository));
            _adressRepository = adressRepository ?? throw new ArgumentNullException(nameof(adressRepository)); ;
        }

        public async Task<Response<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {

                Expression<Func<VetCustomers, bool>> predicate = c => c.Id == request.Id;
                string includeString = "Adress";

                var customers = (await _customersRepository.GetAsync(predicate, null, includeString, false)).FirstOrDefault();
                if (customers == null)
                {
                    _logger.LogWarning($"Not Foun number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }
                  
  
                customers.IsEmail = request.emailNotification;
                customers.IsPhone = request.smsNotification;
                customers.DiscountRate = request.DiscountRate;
                customers.FirstName = request.FirstName;
                customers.LastName = request.LastName;
                customers.Note = request.Note;
                customers.PhoneNumber = request.PhoneNumber;
                customers.PhoneNumber2 = request.PhoneNumber2;
                customers.VKNTCNo = request.VKNTCNo;
                customers.TaxOffice = request.TaxOffice;
                customers.UpdateDate = DateTime.Now;
                customers.UpdateUsers = _identity.Account.UserName;
                customers.EMail = request.EMail;
                customers.CustomerGroup = request.CustomerGroup;

                customers.Adress.District = request.District;
                customers.Adress.Province = request.Province;
                customers.Adress.LongAdress = request.LongAdress;

                await _uow.SaveChangesAsync(cancellationToken);
                response.IsSuccessful = true;

            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }


}
