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
using VetSystems.Vet.Application.Models.Vaccine;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Vaccine.Commands
{
    public class UpdateVaccineCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public int AnimalType { get; set; }
        public string VaccineName { get; set; }
        public int TimeDone { get; set; }
        public int RenewalOption { get; set; }
        public int Obligation { get; set; }
        public List<VetVaccineMedicineListDto> VaccineMedicine { get; set; }
    }

    public class UpdateVaccineCommandHandler : IRequestHandler<UpdateVaccineCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateVaccineCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<Vet.Domain.Entities.VetVaccine> _vetVaccineRepository;
        private readonly IRepository<Vet.Domain.Entities.VetVaccineMedicine> _vetVaccineMedicineRepository;

        public UpdateVaccineCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateVaccineCommandHandler> logger,
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

        public async Task<Response<bool>> Handle(UpdateVaccineCommand request, CancellationToken cancellationToken)
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
                
                _vaccine.AnimalType = request.AnimalType;
                _vaccine.VaccineName = request.VaccineName;
                _vaccine.TimeDone = request.TimeDone;
                _vaccine.RenewalOption = (RenewalOption)request.RenewalOption;
                _vaccine.Obligation = request.Obligation;
                _vaccine.UpdateDate = DateTime.Now;
                _vaccine.UpdateUsers = _identityRepository.Account.UserName;


                if (request.VaccineMedicine.Any())
                {
                    foreach (var item in request.VaccineMedicine)
                    {
                        var _vaccineMedicine = await _vetVaccineMedicineRepository.GetByIdAsync(item.Id);
                        if (_vaccineMedicine != null)
                        {
                            _vaccineMedicine.ProductId = item.ProductId;
                            _vaccineMedicine.Quantity = item.Quantity;
                            _vaccineMedicine.SalesAmount = item.SalesAmount;
                            _vaccineMedicine.TaxisId = item.TaxisId;
                            _vaccineMedicine.Remark = item.Remark;

                            _vetVaccineMedicineRepository.Update(_vaccineMedicine);
                        }
                        else
                        {
                            VetVaccineMedicine vetVaccineMedicine = new()
                            {
                                Id = Guid.NewGuid(),
                                VaccineId = _vaccine.Id,
                                Remark = item.Remark,
                                Quantity = item.Quantity,
                                ProductId = item.ProductId,
                                SalesAmount = item.SalesAmount,
                                TaxisId = item.TaxisId,
                            };
                            _vaccine.VetVaccineMedicine.Add(vetVaccineMedicine);

                            await _vetVaccineMedicineRepository.AddAsync(vetVaccineMedicine);
                        }
                    }

                    _vaccine.TotalSaleAmount = _vaccine.VetVaccineMedicine.Sum(x=> x.SalesAmount);
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
