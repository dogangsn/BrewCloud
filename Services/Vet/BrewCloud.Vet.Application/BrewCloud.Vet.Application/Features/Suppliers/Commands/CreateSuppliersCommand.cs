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
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Suppliers.Commands
{
    public class CreateSuppliersCommand : IRequest<Response<bool>>
    {
        public string SupplierName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool Active { get; set; }
        public string Adress { get; set; } = string.Empty;
        public InvoiceTpe InvoiceType { get; set; }
        public string CompanyName { get; set; } = string.Empty; 
        public string WebSite { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
    }

    public class CreateSuppliersCommandHandler : IRequestHandler<CreateSuppliersCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSuppliersCommandHandler> _logger;
        private readonly IRepository<Domain.Entities.VetSuppliers> _suppliersRepository;

        public CreateSuppliersCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateSuppliersCommandHandler> logger, IRepository<Domain.Entities.VetSuppliers> suppliersRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _suppliersRepository = suppliersRepository ?? throw new ArgumentNullException(nameof(suppliersRepository));
        }

        public async Task<Response<bool>> Handle(CreateSuppliersCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                Domain.Entities.VetSuppliers suppliers = new()
                {
                    Id = Guid.NewGuid(),
                    SupplierName = request.SupplierName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Active = request.Active,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    Adress = request.Adress,
                    InvoiceType = request.InvoiceType,
                    CompanyName = request.CompanyName,
                    WebSite = request.WebSite,
                    TaxOffice = request.TaxOffice,
                    TaxNumber = request.TaxNumber,

                };
                await _suppliersRepository.AddAsync(suppliers);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }

            return response;

        }
    }
}
