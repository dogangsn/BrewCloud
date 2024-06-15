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
using VetSystems.Vet.Application.Features.Vaccine.Commands;
using VetSystems.Vet.Application.Models.Definition.Taxis;
using VetSystems.Vet.Application.Models.Vaccine;
using VetSystems.Vet.Application.Models.Vaccine;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Vaccine.Queries
{
    public class VaccineListQuery : IRequest<Response<List<VaccineListDto>>>
    {
        public int? AnimalType { get; set; }
    }
    public class VaccineListQueryHandler : IRequestHandler<VaccineListQuery, Response<List<VaccineListDto>>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<VaccineListQueryHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetVaccine> _vetVaccineRepository;
        private readonly IRepository<Vet.Domain.Entities.VetVaccineMedicine> _vetVaccineMedicineRepository;
        private readonly IMediator _mediator;

        public VaccineListQueryHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<VaccineListQueryHandler> logger, IRepository<VetVaccine> vetVaccineRepository, IMediator mediator, IRepository<VetVaccineMedicine> vetVaccineMedicineRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetVaccineRepository = vetVaccineRepository;
            _mediator = mediator;
            _vetVaccineMedicineRepository = vetVaccineMedicineRepository;
        }

        public async Task<Response<List<VaccineListDto>>> Handle(VaccineListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<VaccineListDto>>.Success(200);
            try
            {
                List<VetVaccine> _vaccine = (await _vetVaccineRepository.GetAsync(x => x.Deleted == false)).ToList();
                foreach (var item in _vaccine)
                {
                    List<Vet.Domain.Entities.VetVaccineMedicine> _medicine = (await _vetVaccineMedicineRepository.GetAsync(x => x.VaccineId == item.Id)).ToList();
                    if (_medicine.Count > 0)
                    {
                        item.VetVaccineMedicine = _medicine;
                    }
                }
                if (request.AnimalType>0)
                {
                    _vaccine = _vaccine.Where(p => p.AnimalType == request.AnimalType).ToList();
                    var result = _mapper.Map<List<VaccineListDto>>(_vaccine.OrderBy(e => e.TimeDone));
                    response.Data = result;

                }
                else
                {
                    var result = _mapper.Map<List<VaccineListDto>>(_vaccine.OrderByDescending(e => e.CreateDate));
                    response.Data = result;
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
