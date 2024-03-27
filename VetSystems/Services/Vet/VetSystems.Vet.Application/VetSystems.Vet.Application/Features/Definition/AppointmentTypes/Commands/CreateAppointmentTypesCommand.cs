using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Features.Definition.AppointmentTypes.Commands
{
    public class CreateAppointmentTypesCommand : IRequest<Response<bool>>
    {
    }

    public class CreateAppointmentTypesCommandHandler : IRequestHandler<CreateAppointmentTypesCommand, Response<bool>>
    {
        public Task<Response<bool>> Handle(CreateAppointmentTypesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
