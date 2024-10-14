using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Vaccine;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Vaccine.Commands
{
    public class CreateVaccineCommand : IRequest<Response<string>>
    {
        public int AnimalType { get; set; }
        public string VaccineName { get; set; }
        public int TimeDone { get; set; }
        public int RenewalOption { get; set; }
        public int Obligation { get; set; }
        public List<VetVaccineMedicineListDto> VaccineMedicine { get; set; }
    }

    public class CreateVaccineCommandHandler : IRequestHandler<CreateVaccineCommand, Response<string>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateVaccineCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetVaccine> _vetVaccineRepository;
        private readonly IMediator _mediator;

        public CreateVaccineCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateVaccineCommandHandler> logger, IRepository<VetVaccine> vetVaccineRepository, IMediator mediator)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetVaccineRepository = vetVaccineRepository;
            _mediator = mediator;
        }

        public async Task<Response<string>> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
                Data = string.Empty
            };
            _uow.CreateTransaction(IsolationLevel.ReadCommitted);
            try
            {

                VetVaccine vetVaccine = new()
                {
                    Id = Guid.NewGuid(),
                    VaccineName = request.VaccineName,
                    AnimalType = request.AnimalType,
                    Obligation = request.Obligation,
                    TimeDone = request.TimeDone,
                    RenewalOption = (RenewalOption)request.RenewalOption,
                    CreateDate = DateTime.Now,
                    
                };

                if (request.VaccineMedicine.Any())
                {
                    foreach (var item in request.VaccineMedicine)
                    {
                        VetVaccineMedicine vetVaccineMedicine = new()
                        {
                            Id = Guid.NewGuid(),
                            VaccineId = vetVaccine.Id,
                            Remark = item.Remark,
                            Quantity = item.Quantity,
                            ProductId = item.ProductId,
                            SalesAmount = item.SalesAmount,
                            TaxisId = item.TaxisId,
                            
                        };
                        vetVaccine.VetVaccineMedicine.Add(vetVaccineMedicine);
                    }
                }
                vetVaccine.TotalSaleAmount = vetVaccine.VetVaccineMedicine.Sum(x => x.SalesAmount);
                

                await _vetVaccineRepository.AddAsync(vetVaccine);
                await _uow.SaveChangesAsync(cancellationToken);
                _uow.Commit();
                response.Data = vetVaccine.Id.ToString();


            }
            catch (Exception ex)
            {
                _uow.Rollback();
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }
            return response;
        }
    }
}
