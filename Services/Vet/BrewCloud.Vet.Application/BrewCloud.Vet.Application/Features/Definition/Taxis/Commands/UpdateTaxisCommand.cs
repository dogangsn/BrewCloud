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

namespace BrewCloud.Vet.Application.Features.Definition.Taxis.Commands
{
    public class UpdateTaxisCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string TaxName { get; set; } = string.Empty;
        public int TaxRatio { get; set; }
    }

    public class UpdateTaxisCommandHandler : IRequestHandler<UpdateTaxisCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTaxisCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetTaxis> _taxisRepository;
        private readonly IIdentityRepository _identityRepository;

        public UpdateTaxisCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateTaxisCommandHandler> logger, IRepository<VetTaxis> taxisRepository, IIdentityRepository identityRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _taxisRepository = taxisRepository;
            _identityRepository = identityRepository;
        }

        public async Task<Response<bool>> Handle(UpdateTaxisCommand request, CancellationToken cancellationToken)
        {

            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var taxis = await _taxisRepository.GetByIdAsync(request.Id);
                if (taxis == null)
                {
                    _logger.LogWarning($"taxis deleted failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }
                 
                taxis.TaxName = request.TaxName;
                taxis.TaxRatio = request.TaxRatio;
                taxis.UpdateDate = DateTime.Now;
                taxis.UpdateUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
