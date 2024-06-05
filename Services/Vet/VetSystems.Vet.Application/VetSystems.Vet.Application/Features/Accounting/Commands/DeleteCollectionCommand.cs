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

namespace VetSystems.Vet.Application.Features.Accounting.Commands
{
    public class DeleteCollectionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCollectionCommandHandler : IRequestHandler<DeleteCollectionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCollectionCommandHandler> _logger;
        private readonly IRepository<VetPaymentCollection> _paymentCollectionRepository;

        public DeleteCollectionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteCollectionCommandHandler> logger, IRepository<VetPaymentCollection> paymentCollectionRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _paymentCollectionRepository = paymentCollectionRepository;
        }

        public async Task<Response<bool>> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {
                var _collection = await _paymentCollectionRepository.GetByIdAsync(request.Id);
                if (_collection == null)
                {
                    _logger.LogWarning($"Collection update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Collection update failed", 404);
                }

                _collection.Deleted = false;
                _collection.DeletedDate = DateTime.Now;
                _collection.DeletedUsers = _identity.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;


        }
    }
}
