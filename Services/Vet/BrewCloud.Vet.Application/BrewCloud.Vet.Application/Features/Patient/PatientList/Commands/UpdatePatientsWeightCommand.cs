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
using BrewCloud.Vet.Application.Models.Appointments;
using BrewCloud.Vet.Application.Models.Customers;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Patient.Examination.Commands
{
    public class UpdatePatientsWeightCommand : IRequest<Response<bool>>
    {
        public string PatientId { get; set; }
        public double Weight { get; set; }
    }

    public class UpdatePatientsWeightnHandler : IRequestHandler<UpdatePatientsWeightCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePatientsWeightnHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetWeightControl> _weightControlRepository;

        public UpdatePatientsWeightnHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdatePatientsWeightnHandler> logger, IRepository<VetWeightControl> WeightControlRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weightControlRepository = WeightControlRepository ?? throw new ArgumentNullException(nameof(WeightControlRepository));
        }

        public async Task<Response<bool>> Handle(UpdatePatientsWeightCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

                VetWeightControl vetWeightControl = new()
                {
                    PatientId = Guid.Parse(request.PatientId),
                    ControlDate = DateTime.Now,
                    Weight = request.Weight,
                    CreateDate = DateTime.UtcNow,
                    CreateUsers = _identity.Account.UserName
                };

                await _weightControlRepository.AddAsync(vetWeightControl);
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
