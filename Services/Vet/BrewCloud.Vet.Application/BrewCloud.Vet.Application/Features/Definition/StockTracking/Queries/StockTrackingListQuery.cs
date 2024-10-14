using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Application.Models.Definition.StockTracking;

namespace BrewCloud.Vet.Application.Features.Definition.StockTracking.Queries
{
    public class StockTrackingListQuery : IRequest<Response<List<StockTrackingDto>>>
    { 
    }
}
