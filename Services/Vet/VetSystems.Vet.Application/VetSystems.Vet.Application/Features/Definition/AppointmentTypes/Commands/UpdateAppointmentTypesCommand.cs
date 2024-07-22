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

namespace VetSystems.Vet.Application.Features.Definition.AppointmentTypes.Commands
{
    public class UpdateAppointmentTypesCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Remark { get; set; } = string.Empty;
        public bool IsDefaultPrice { get; set; } = false;
        public decimal Price { get; set; }
        public Guid TaxisId { get; set; }
        public string Colors { get; set; } = string.Empty;
    }

    public class UpdateAppointmentTypesCommandHandler : IRequestHandler<UpdateAppointmentTypesCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAppointmentTypesCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAppointmentTypes> _appointmentTypesRepository;
        private readonly IIdentityRepository _identityRepository;

        public UpdateAppointmentTypesCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateAppointmentTypesCommandHandler> logger, IRepository<VetAppointmentTypes> appointmentTypesRepository, IIdentityRepository identityRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _appointmentTypesRepository = appointmentTypesRepository;
            _identityRepository = identityRepository;
        }

        public async Task<Response<bool>> Handle(UpdateAppointmentTypesCommand request, CancellationToken cancellationToken)
        {

            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var appointmentTypes = await _appointmentTypesRepository.GetByIdAsync(request.Id);
                if (appointmentTypes == null)
                {
                    _logger.LogWarning($"taxis deleted failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                appointmentTypes.Price = request.Price;
                appointmentTypes.IsDefaultPrice = request.IsDefaultPrice;
                appointmentTypes.TaxisId = request.TaxisId;
                appointmentTypes.Remark = request.Remark;
                appointmentTypes.Colors = request.Colors;

                appointmentTypes.UpdateDate = DateTime.Now;
                appointmentTypes.UpdateUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccessful = false;
            }

            return response;

        }
    }
}
