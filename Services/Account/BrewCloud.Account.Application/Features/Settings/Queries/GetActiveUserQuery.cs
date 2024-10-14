using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrewCloud.Shared.Accounts;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;

namespace BrewCloud.Account.Application.Features.Settings.Queries
{
    public class GetActiveUserQuery : IRequest<Response<SignupDto>>
    {
    }

    public class GetActiveUserQueryHandler : IRequestHandler<GetActiveUserQuery, Response<SignupDto>>
    {
        private readonly IIdentityRepository _identity;

        public GetActiveUserQueryHandler(IIdentityRepository identity)
        {
            _identity = identity;
        }

        public async Task<Response<SignupDto>> Handle(GetActiveUserQuery request, CancellationToken cancellationToken)
        {
            var response = Response<SignupDto>.Success(200);
            try
            {
                response.Data = new SignupDto();
                response.Data.Id = _identity.Account.UserId.ToString();
               
            }
            catch (Exception ex)
            {
                return Response<SignupDto>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
