using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Application.Features.Definition.AppointmentTypes.Commands;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Domain.Contracts;
using AutoMapper;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Definition.Taxis.Commands
{
    public class CreateTaxisCommand : IRequest<Response<bool>>
    {
        public string TaxName { get; set; } = string.Empty;
        public int TaxRatio { get; set; }
    }

    public class CreateTaxisCommandHandler : IRequestHandler<CreateTaxisCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTaxisCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetTaxis> _taxisRepository;

        public CreateTaxisCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateTaxisCommandHandler> logger, IRepository<VetTaxis> taxisRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _taxisRepository = taxisRepository;
        }

        public async Task<Response<bool>> Handle(CreateTaxisCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                Vet.Domain.Entities.VetTaxis taxis = new()
                {
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    TaxName = request.TaxName,
                    TaxRatio = request.TaxRatio,
                };
                await _taxisRepository.AddAsync(taxis);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }
}
