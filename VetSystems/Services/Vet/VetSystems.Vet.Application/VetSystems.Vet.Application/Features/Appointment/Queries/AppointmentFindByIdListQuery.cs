using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Appointments;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Appointment.Queries
{
    public class AppointmentFindByIdListQuery : IRequest<Response<List<AppointmentsDto>>>
    {
        public string CustomerId { get; set; }
    }

    public class AppointmentFindByIdListQueryHandler : IRequestHandler<AppointmentFindByIdListQuery, Response<List<AppointmentsDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<Vet.Domain.Entities.VetAppointments> _AppointmentRepository;

        public AppointmentFindByIdListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<Domain.Entities.VetAppointments> appointmentRepository)
        {
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(_uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _AppointmentRepository = appointmentRepository;
        }

        public async Task<Response<List<AppointmentsDto>>> Handle(AppointmentFindByIdListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<AppointmentsDto>>.Success(200);
            try
            {
                List<VetAppointments> appointmentsList = (await _AppointmentRepository.GetAsync(x => x.Deleted == false && x.CustomerId == Guid.Parse(request.CustomerId))).ToList();
                var result = _mapper.Map<List<AppointmentsDto>>(appointmentsList.OrderByDescending(e => e.CreateDate));
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
