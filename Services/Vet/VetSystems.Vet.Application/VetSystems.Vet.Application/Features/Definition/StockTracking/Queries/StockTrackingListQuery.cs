using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.Definition.StockTracking;

namespace VetSystems.Vet.Application.Features.Definition.StockTracking.Queries
{
    public class StockTrackingListQuery : IRequest<Response<List<StockTrackingDto>>>
    { 
    }
}
