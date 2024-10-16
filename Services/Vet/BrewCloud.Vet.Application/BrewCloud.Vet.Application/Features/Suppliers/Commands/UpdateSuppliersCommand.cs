﻿using AutoMapper;
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
    public class UpdateSuppliersCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
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

    public class UpdateSuppliersCommandHandler : IRequestHandler<UpdateSuppliersCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSuppliersCommandHandler> _logger;
        private readonly IRepository<Domain.Entities.VetSuppliers> _suppliersRepository;

        public UpdateSuppliersCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateSuppliersCommandHandler> logger, IRepository<Domain.Entities.VetSuppliers> suppliersRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _suppliersRepository = suppliersRepository ?? throw new ArgumentNullException(nameof(suppliersRepository));
        }

        public async Task<Response<bool>> Handle(UpdateSuppliersCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var casingDefinitions = await _suppliersRepository.GetByIdAsync(request.Id);
                if (casingDefinitions == null)
                {
                    _logger.LogWarning($"Casing update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                casingDefinitions.SupplierName = request.SupplierName;
                casingDefinitions.Email = request.Email;
                casingDefinitions.Phone = request.Phone;
                casingDefinitions.Active = request.Active;
                casingDefinitions.UpdateDate = DateTime.Now;
                casingDefinitions.Adress = request.Adress;
                casingDefinitions.InvoiceType = request.InvoiceType;
                casingDefinitions.CompanyName = request.CompanyName;
                casingDefinitions.WebSite = request.WebSite;
                casingDefinitions.TaxNumber = request.TaxNumber;
                casingDefinitions.TaxOffice = request.TaxOffice;
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
