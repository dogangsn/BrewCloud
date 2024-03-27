using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Features.Definition.AppointmentTypes.Commands
{
    public class UpdateAppointmentTypesCommand : IRequest<Response<bool>>
    {
    }

    public class UpdateAppointmentTypesCommandHandler : IRequestHandler<UpdateAppointmentTypesCommand, Response<bool>>
    {
        public Task<Response<bool>> Handle(UpdateAppointmentTypesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
