using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Features.Definition.AppointmentTypes.Commands
{
    public class DeleteAppointmentTypesCommand : IRequest<Response<bool>>
    {
    }

    public class DeleteAppointmentTypesCommandHandler : IRequestHandler<DeleteAppointmentTypesCommand, Response<bool>>
    {
        public Task<Response<bool>> Handle(DeleteAppointmentTypesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
