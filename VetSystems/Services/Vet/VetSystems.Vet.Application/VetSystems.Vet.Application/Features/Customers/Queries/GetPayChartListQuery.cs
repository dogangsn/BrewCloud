using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.Customers;

namespace VetSystems.Vet.Application.Features.Customers.Queries
{
    public class GetPayChartListQuery  : IRequest<Response<List<PayChartListDto>>>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetPayChartListQueryHandler : IRequestHandler<GetPayChartListQuery, Response<List<PayChartListDto>>>
    {
        public Task<Response<List<PayChartListDto>>> Handle(GetPayChartListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
