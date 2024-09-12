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
    public class CreateAppointmentTypesCommand : IRequest<Response<bool>>
    {
        public string Remark { get; set; } = string.Empty;
        public bool IsDefaultPrice { get; set; } = false;
        public decimal Price { get; set; }
        public Guid TaxisId { get; set; }
        public string Colors { get; set; } = string.Empty;
    }

    public class CreateAppointmentTypesCommandHandler : IRequestHandler<CreateAppointmentTypesCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAppointmentTypesCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAppointmentTypes> _appointmentTypesRepository;

        public CreateAppointmentTypesCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateAppointmentTypesCommandHandler> logger, IRepository<VetAppointmentTypes> appointmentTypesRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _appointmentTypesRepository = appointmentTypesRepository;
        }

        public async Task<Response<bool>> Handle(CreateAppointmentTypesCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                int _type = 1;
                var lastrecord = (await _appointmentTypesRepository.GetAsync(x => x.Deleted == false)).OrderBy(x => x.Type).ToList();
                if (lastrecord.Count > 0)
                {
                    _type = lastrecord.LastOrDefault().Type + 1;
                }

                Vet.Domain.Entities.VetAppointmentTypes appointmentTypes = new()
                {
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    Remark = request.Remark,
                    IsDefaultPrice = request.IsDefaultPrice,
                    Price = request.Price,
                    TaxisId = request.TaxisId,
                    Type = _type,
                    Colors = request.Colors
                };
                await _appointmentTypesRepository.AddAsync(appointmentTypes);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.IsSuccessful = false;
                response.Data = false;
            }

            return response;
        }
    }
}
