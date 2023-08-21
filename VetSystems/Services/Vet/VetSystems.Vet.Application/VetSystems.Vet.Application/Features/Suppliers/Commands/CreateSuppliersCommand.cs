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

namespace VetSystems.Vet.Application.Features.Suppliers.Commands
{
    public class CreateSuppliersCommand : IRequest<Response<bool>>
    {
        public string SupplierName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool Active { get; set; }
    }

    public class CreateSuppliersCommandHandler : IRequestHandler<CreateSuppliersCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSuppliersCommandHandler> _logger;
        private readonly IRepository<Domain.Entities.Suppliers> _suppliersRepository;

        public CreateSuppliersCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateSuppliersCommandHandler> logger, IRepository<Domain.Entities.Suppliers> suppliersRepository)
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
                Domain.Entities.Suppliers suppliers = new()
                {
                    Id = Guid.NewGuid(),
                    SupplierName = request.SupplierName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Active = request.Active,
                    CreateDate = DateTime.Now,
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
