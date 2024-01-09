using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{
    public class UpdateCompletedAppointmentCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; } 
    }

    public class UpdateCompletedAppointmentCommandHandler : IRequestHandler<UpdateCompletedAppointmentCommand, Response<bool>>
    {
        public Task<Response<bool>> Handle(UpdateCompletedAppointmentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
