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

namespace VetSystems.Vet.Application.Features.Vaccine.Commands
{
    public class DeteleVaccineCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeteleVaccineCommandHandler : IRequestHandler<DeteleVaccineCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeteleVaccineCommandHandler> _logger; 
        private readonly IIdentityRepository _identityRepository;

        private readonly IRepository<Vet.Domain.Entities.VetVaccine> _vetVaccineRepository;
        private readonly IRepository<Vet.Domain.Entities.VetVaccineMedicine> _vetVaccineMedicineRepository;

        public DeteleVaccineCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeteleVaccineCommandHandler> logger,
           IIdentityRepository identityRepository, IRepository<Domain.Entities.VetVaccine> vetVaccineRepository, IRepository<Domain.Entities.VetVaccineMedicine> vetVaccineMedicineRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _vetVaccineRepository = vetVaccineRepository;
            _vetVaccineMedicineRepository = vetVaccineMedicineRepository;
        }

        public async Task<Response<bool>> Handle(DeteleVaccineCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var _vaccine = await _vetVaccineRepository.GetByIdAsync(request.Id);
                if (_vaccine == null)
                {
                    _logger.LogWarning($"Vaccine update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Vaccine update failed", 404);
                }
                _vaccine.Deleted = true;
                _vaccine.DeletedDate = DateTime.Now;
                _vaccine.DeletedUsers = _identityRepository.Account.UserName;

                List<Vet.Domain.Entities.VetVaccineMedicine> _medicine = (await _vetVaccineMedicineRepository.GetAsync(x => x.VaccineId == request.Id)).ToList();
                if (_medicine != null)
                {
                    foreach (var item in _medicine)
                    {
                        item.Deleted = true;
                        item.DeletedDate = DateTime.Now;
                        item.DeletedUsers = _identityRepository.Account.UserName;
                    }
                }

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
