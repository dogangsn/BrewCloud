using AutoMapper;
using MassTransit.Internals.GraphValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Demands.Demands;
using BrewCloud.Vet.Domain.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BrewCloud.Vet.Application.Features.Demands.Demand.Commands
{
    public class UpdateDemandCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public DateTime? date { get; set; }
        public string documentno { get; set; }
        public Guid? suppliers { get; set; }
        public DateTime? deliverydate { get; set; }
        public string note { get; set; }
        public int? state { get; set; }
        public bool? iscomplated { get; set; }
    }

    public class UpdateDemandCommandHandler : IRequestHandler<UpdateDemandCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDemandCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetDemands> _demandsRepository;

        public UpdateDemandCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateDemandCommandHandler> logger, IRepository<Domain.Entities.VetDemands> demandsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _demandsRepository = demandsRepository ?? throw new ArgumentNullException(nameof(demandsRepository));
        }

        public async Task<Response<bool>> Handle(UpdateDemandCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
               
                    var demands = await _demandsRepository.GetByIdAsync(request.Id);
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
                    demands.iscomplated = true;
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
