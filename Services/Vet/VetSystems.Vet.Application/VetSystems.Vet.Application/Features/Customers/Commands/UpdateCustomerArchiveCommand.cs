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
    public class UpdateCustomerArchiveCommand: IRequest<Response<bool>>
    {
        public Guid CustomerId { get; set; }
        public bool Archive { get; set; }
    }

    public class UpdateCustomerArchiveCommandHandler : IRequestHandler<UpdateCustomerArchiveCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCustomerArchiveCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customersRepository;

        public UpdateCustomerArchiveCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCustomerArchiveCommandHandler> logger, 
            IRepository<VetCustomers> customersRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _customersRepository = customersRepository;
        }

        public async Task<Response<bool>> Handle(UpdateCustomerArchiveCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {
                VetCustomers customers = await _customersRepository.GetByIdAsync(request.CustomerId);
                if (customers == null)
                {
                    _logger.LogWarning($"Not Found: {request.CustomerId}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                customers.IsArchive = request.Archive;
                customers.UpdateDate = DateTime.Now;
                customers.UpdateUsers = _identity.Account.UserName;
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
