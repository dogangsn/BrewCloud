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
    public class DeletePayChartCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeletePayChartCommandHandler : IRequestHandler<DeletePayChartCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePayChartCommandHandler> _logger;
        private readonly IRepository<VetPaymentCollection> _vetPaymentCollectionRepository;
        private readonly IRepository<VetSaleBuyOwner> _vetSaleBuyOwnerRepository;

        public DeletePayChartCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeletePayChartCommandHandler> logger, IRepository<VetPaymentCollection> vetPaymentCollectionRepository, IRepository<VetSaleBuyOwner> vetSaleBuyOwnerRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _vetPaymentCollectionRepository = vetPaymentCollectionRepository ?? throw new ArgumentNullException(nameof(vetPaymentCollectionRepository));
            _vetSaleBuyOwnerRepository = vetSaleBuyOwnerRepository ?? throw new ArgumentNullException(nameof(vetSaleBuyOwnerRepository));
        }

        public async Task<Response<bool>> Handle(DeletePayChartCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {




            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
