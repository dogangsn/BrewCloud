using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Enums;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Appointments;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Patient.Examination.Commands
{
    public class CreateExaminationCommand : IRequest<Response<bool>>
    {
        public DateTime MuayeneTarihi { get; set; }
        public string MuayneDurumu { get; set; }
        public string CustomerId { get; set; }
        public string PatientId { get; set; }
        public double BodyTemperature { get; set; }
        public double Pulse { get; set; }
        public double RespiratoryRate { get; set; }
        public double Weight { get; set; }
        public string ComplaintAndHistory { get; set; }
        public string TreatmentDescription { get; set; }
        public string Semptomlar { get; set; }
    }

    public class CreateExaminationHandler : IRequestHandler<CreateExaminationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateExaminationHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetExamination> _ExaminationRepository;

        public CreateExaminationHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateExaminationHandler> logger, IRepository<Domain.Entities.VetExamination> ExaminationRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ExaminationRepository = ExaminationRepository ?? throw new ArgumentNullException(nameof(ExaminationRepository));
        }

        public async Task<Response<bool>> Handle(CreateExaminationCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

                Vet.Domain.Entities.VetExamination examination = new()
                {
                    Date = request.MuayeneTarihi,
                    Status = request.MuayneDurumu == "Tamamlandı" ? 1 : 0,
                    CustomerId = Guid.Parse(request.CustomerId),
                    PatientId = Guid.Parse(request.PatientId),
                    BodyTemperature = (decimal)request.BodyTemperature,
                    Pulse = (decimal)request.Pulse,
                    RespiratoryRate = (decimal)request.RespiratoryRate,
                    Weight = (decimal)request.Weight,
                    Symptoms = request.Semptomlar, 
                    ComplaintStory = request.ComplaintAndHistory, 
                    TreatmentDescription = request.TreatmentDescription,
                    CreateDate = DateTime.UtcNow,
                    CreateUsers = _identity.Account.UserName
                };



                await _ExaminationRepository.AddAsync(examination);
                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }
            return response;

        }
    }
}
