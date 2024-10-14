using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;

namespace BrewCloud.Account.Application.Features.Settings.Commands
{
    public class UpdateCompanyCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string CompanyCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string CompanyTitle { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public int? TaxNumber { get; set; }
        public string TaxOffice { get; set; } = string.Empty;
        public int? DefaultInvoiceType { get; set; }
        public bool? InvoiceAmountNotes { get; set; }
        public bool? InvoiceNoAutoCreate { get; set; }
        public bool? InvoiceSendEMail { get; set; }
    }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;
        private readonly IRepository<Company> _companyRepository;

        public UpdateCompanyCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCompanyCommandHandler> logger, IRepository<Company> companyRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        public async Task<Response<bool>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {

            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };

            try
            {
                var company = await _companyRepository.GetByIdAsync(request.Id);
                if (company == null)
                {
                    _logger.LogWarning($"Company update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                company.Adress = request.Adress;
                company.InvoiceNoAutoCreate = request.InvoiceNoAutoCreate;
                company.InvoiceSendEMail = request.InvoiceSendEMail;
                company.Phone = request.Phone;
                company.CompanyCode = request.CompanyCode;
                company.CompanyName = request.CompanyName;
                company.TaxNumber = request.TaxNumber;
                company.CompanyTitle = request.CompanyTitle;
                company.EMail = request.EMail;
                company.DefaultInvoiceType = request.DefaultInvoiceType;
                company.TaxOffice = request.TaxOffice;
                company.UpdateDate = DateTime.Now;
                company.UpdateUser = _identity.Account.UserName;
                company.InvoiceAmountNotes = request.InvoiceAmountNotes;

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
