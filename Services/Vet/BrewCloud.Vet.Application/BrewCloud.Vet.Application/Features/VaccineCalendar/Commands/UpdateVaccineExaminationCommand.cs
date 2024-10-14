using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    public class UpdateVaccineExaminationCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public DateTime VaccinationDate { get; set; }
        public DateTime? NextVaccinationDate { get; set; }
        public bool CreateNextAppointment { get; set; }
    }

    public class UpdateVaccineExaminationCommandHandler : IRequestHandler<UpdateVaccineExaminationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateVaccineExaminationCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<Vet.Domain.Entities.VetVaccineCalendar> _vetVaccineCalendarRepository;

        public UpdateVaccineExaminationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateVaccineExaminationCommandHandler> logger,
           IIdentityRepository identityRepository, IRepository<Domain.Entities.VetVaccineCalendar> vetVaccineCalendarRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _vetVaccineCalendarRepository = vetVaccineCalendarRepository;
        }

        public async Task<Response<bool>> Handle(UpdateVaccineExaminationCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {

                VetVaccineCalendar vaccineCalendar = new VetVaccineCalendar();

                var _vaccine = await _vetVaccineCalendarRepository.GetByIdAsync(request.Id);
                if (_vaccine == null)
                {
                    _logger.LogWarning($"Vaccine update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Vaccine update failed", 404);
                } 
                
                _vaccine.IsDone = true;
                _vaccine.VaccinationDate = request.VaccinationDate;
                _vaccine.UpdateDate = DateTime.Now;
                _vaccine.UpdateUsers = _identityRepository.Account.UserName;

                if (request.CreateNextAppointment == true)
                {
                    VetVaccineCalendar vetVaccineCalendar = new()
                                        {
                                            Id = Guid.NewGuid(),
                                            IsDone = false,
                                            PatientId = _vaccine.PatientId,
                                            CustomerId = _vaccine.CustomerId,
                                            VaccineId = _vaccine.Id,
                                            IsAdd = true,
                                            VaccineName = _vaccine.VaccineName,
                                            VaccineDate = request.NextVaccinationDate?? new DateTime(2099, 1, 1),
                                            CreateDate = DateTime.Now,
                                            AnimalType = _vaccine.AnimalType,
                                            CreateUsers = _identityRepository.Account.UserName,
                                        };

                    vaccineCalendar = vetVaccineCalendar;
                    await _vetVaccineCalendarRepository.AddAsync(vaccineCalendar);
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
