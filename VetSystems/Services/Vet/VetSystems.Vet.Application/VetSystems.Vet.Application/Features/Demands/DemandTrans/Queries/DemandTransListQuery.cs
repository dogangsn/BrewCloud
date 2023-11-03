using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Definition.Product;
using VetSystems.Vet.Application.Models.Demands.DemandTrans;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Demands.DemandTrans.Queries
{

    public class DemandTransListQuery : IRequest<Response<List<DemandTransDto>>>
    {
        public Guid Id { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? StockState { get; set; }
        public int? isActive { get; set; }
        public decimal? Reserved { get; set; }
        public string Barcode { get; set; }
    }

    public class DemandTransListQueryHandler : IRequestHandler<DemandTransListQuery, Response<List<DemandTransDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DemandTransListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<DemandTransDto>>> Handle(DemandTransListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<DemandTransDto>>();
            try
            {
                string query = "Select * from vetDemandTrans where Deleted = 0 and ownerid = @ownerid order by CreateDate desc";
                var _data = _uow.Query<DemandTransDto>(query, new {ownerid = request.Id}).ToList();
                response = new Response<List<DemandTransDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                //response.Errors = ex.ToString();
            }

            return response;


        }
    }
}
