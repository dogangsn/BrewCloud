using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{

    public class UpdatePaymentReceivedAppointmentCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class UpdatePaymentReceivedAppointmentCommandHandler : IRequestHandler<UpdatePaymentReceivedAppointmentCommand, Response<bool>>
    {
        public Task<Response<bool>> Handle(UpdatePaymentReceivedAppointmentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
