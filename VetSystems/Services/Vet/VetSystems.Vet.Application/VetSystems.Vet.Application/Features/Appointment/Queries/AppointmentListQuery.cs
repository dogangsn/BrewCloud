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

namespace VetSystems.Vet.Application.Features.Appointment.Queries
{
    public class AppointmentsListQuery : IRequest<Response<List<AppointmentsDto>>>
    {
    }

    public class AppointmentsListQueryHandler : IRequestHandler<AppointmentsListQuery, Response<List<AppointmentsDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AppointmentsListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<AppointmentsDto>>> Handle(AppointmentsListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<AppointmentsDto>>();
            try
            {
                string query = "Select * from VetAppointments where Deleted = 0";
                var _data = _uow.Query<AppointmentsDto>(query).ToList();
                response = new Response<List<AppointmentsDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                //response.Errors = ex.ToString();
            }

            return response;
        }
    }
}
