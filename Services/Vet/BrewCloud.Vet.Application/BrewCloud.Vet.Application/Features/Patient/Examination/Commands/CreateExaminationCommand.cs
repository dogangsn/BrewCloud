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
using BrewCloud.Shared.Enums;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Features.Accounting.Commands;
using BrewCloud.Vet.Application.Models.Accounting;
using BrewCloud.Vet.Application.Models.Appointments;
using BrewCloud.Vet.Application.Models.Customers;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Patient.Examination.Commands
{
    public class CreateExaminationCommand : IRequest<Response<bool>>
    {
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CustomerId { get; set; }
        public string PatientId { get; set; } 
        public double BodyTemperature { get; set; }
        public int Pulse { get; set; }
        public int RespiratoryRate { get; set; }
        public double Weight { get; set; }
        public string ComplaintStory { get; set; } = string.Empty;
        public string TreatmentDescription { get; set; } = string.Empty;
        public string Symptoms { get; set; } = string.Empty;
        public bool IsPrice { get; set; } = false;
        public decimal Price { get; set; } = 0;
        public List<SaleTransRequestDto>? Trans { get; set; }
    }

    public class CreateExaminationHandler : IRequestHandler<CreateExaminationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateExaminationHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetExamination> _ExaminationRepository;
        private readonly IRepository<Vet.Domain.Entities.VetWeightControl> _weightControlRepository;
        private readonly IMediator _mediator;
        private readonly IRepository<Domain.Entities.VetParameters> _parametersRepository;

        public CreateExaminationHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateExaminationHandler> logger, IRepository<Domain.Entities.VetExamination> ExaminationRepository, IRepository<VetWeightControl> WeightControlRepository, IMediator mediator, IRepository<VetParameters> parametersRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ExaminationRepository = ExaminationRepository ?? throw new ArgumentNullException(nameof(ExaminationRepository));
            _weightControlRepository = WeightControlRepository ?? throw new ArgumentNullException(nameof(WeightControlRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _parametersRepository = parametersRepository ?? throw new ArgumentNullException(nameof(parametersRepository)); 
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
                    Date = TimeZoneInfo.ConvertTimeFromUtc(request.Date, localTimeZone),
                    Status = request.Status == "Aktif" ? 0 : request.Status == "Tamamlandı" ? 1 : request.Status == "Bekliyor" ? 2 : 3,
                    CustomerId = Guid.Parse(request.CustomerId),
                    PatientId = Guid.Parse(request.PatientId),
                    BodyTemperature = (decimal)request.BodyTemperature,
                    Pulse = (decimal)request.Pulse,
                    RespiratoryRate = (decimal)request.RespiratoryRate,
                    Weight = (decimal)request.Weight,
                    Symptoms = request.Symptoms, 
                    ComplaintStory = request.ComplaintStory, 
                    TreatmentDescription = request.TreatmentDescription,
                    CreateDate = DateTime.UtcNow,
                    CreateUsers = _identity.Account.UserName,
                    Id = Guid.NewGuid(),
                };

                VetWeightControl vetWeightControl = new()
                {
                    PatientId = Guid.Parse(request.PatientId),
                    ControlDate = DateTime.Now,
                    Weight = request.Weight,
                    CreateDate = DateTime.UtcNow,
                    CreateUsers = _identity.Account.UserName
                };

                var req = new CreateSaleCommand()
                {
                    CustomerId = Guid.Parse(request.CustomerId),
                    Date = examination.Date,
                    Remark = "",
                    Trans = request.Trans,
                    IsPrice = request.IsPrice,
                    Price = request.Price,
                    ExaminationId = examination.Id,
                    IsExaminations = true,
                    IsAccomodation = false, 
                };
                await _mediator.Send(req);


                await _weightControlRepository.AddAsync(vetWeightControl);
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
