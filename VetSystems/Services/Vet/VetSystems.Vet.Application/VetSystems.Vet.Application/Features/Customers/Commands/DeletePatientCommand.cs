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
    public class DeletePatientCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePatientCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetPatients> _patientsRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeletePatientCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeletePatientCommandHandler> logger, IRepository<Domain.Entities.VetPatients> patientsRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _patientsRepository = patientsRepository ?? throw new ArgumentNullException(nameof(patientsRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var patients = await _patientsRepository.GetByIdAsync(request.Id);
                if (patients == null)
                {
                    _logger.LogWarning($"Casing update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                patients.Deleted = true;
                patients.DeletedDate = DateTime.Now;
                patients.DeletedUsers = _identityRepository.Account.UserName;
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
