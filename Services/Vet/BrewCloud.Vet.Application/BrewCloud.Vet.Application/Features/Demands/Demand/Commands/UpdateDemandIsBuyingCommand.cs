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

namespace BrewCloud.Vet.Application.Features.Demands.Demand.Commands
{

    public class UpdateDemandIsBuyingCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public bool? iscomplated { get; set; }
    }

    public class UpdateDemandIsBuyingCommandHandler : IRequestHandler<UpdateDemandIsBuyingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDemandIsBuyingCommandHandler> _logger;
        private readonly IRepository<Domain.Entities.VetDemands> _demandssRepository;

        public UpdateDemandIsBuyingCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateDemandIsBuyingCommandHandler> logger, IRepository<Domain.Entities.VetDemands> demandsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _demandssRepository = demandsRepository ?? throw new ArgumentNullException(nameof(demandsRepository));
        }

        public async Task<Response<bool>> Handle(UpdateDemandIsBuyingCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {

                var demands = await _demandssRepository.GetByIdAsync(request.Id);
                if (demands == null)
                {
                    _logger.LogWarning($"Demand update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }
                //demands.suppliers = request.suppliers;
                //demands.date = request.date;
                //demands.documentno = request.documentno;
                //demands.deliverydate = request.deliverydate;
                //demands.note = request.note;
                demands.isBuying = true;
                demands.UpdateDate = DateTime.Now;
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
