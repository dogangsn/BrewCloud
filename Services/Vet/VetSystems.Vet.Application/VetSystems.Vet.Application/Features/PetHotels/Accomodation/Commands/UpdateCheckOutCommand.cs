using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Features.PetHotels.Accomodation.Commands
{
    public class UpdateCheckOutCommand : IRequest<Response<bool>>
    {
    }

    public class UpdateCheckOutCommandHandler : IRequestHandler<UpdateCheckOutCommand, Response<bool>>
    {


        public async Task<Response<bool>> Handle(UpdateCheckOutCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {



            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
